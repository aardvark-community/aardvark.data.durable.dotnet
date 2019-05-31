open Newtonsoft.Json.Linq
open System
open System.IO
open System.Net
open Aardvark.Data

type Entry = {
    LetName : string
    Category : string
    Id : Guid
    RawName : string
    Name : string
    Description : string
    RawType : string
    Type : string
    IsArray : bool
}

let mutable defs = Map.empty<Guid, Entry>
let mutable rawname2def = Map.empty<string, Entry>

let parseEntry category (id : string) (def : JObject) =
    
    if not (def.ContainsKey("name")) then failwith "Entry must contain field 'name'."
    let rawName = string def.["name"]

    let isArray = rawName.EndsWith("[]")

    let name = rawName

    let description =
        if def.ContainsKey("description") then
            string def.["description"]
        else
            if isArray then
                sprintf "Array of %s." (name.Replace("[]", ""))
            else
                sprintf "%s." name

    let letName =
        if name = "Guid" then
            "GuidDef"
        else
            let name = if name.EndsWith("[]") then name.Replace("[]", "Array") else name
            let ts = name.Split('.')
            if ts.Length > 1 then
                if ts.[0] = category then
                    String.Join("", ts |> Array.skip(1))
                else
                    failwith "First part of dotted name must be category."
            else
                name

    let rawType = if def.ContainsKey("type") then string def.["type"] else "None"

    let typ =
        if name = "Unit" || rawType = "None" then
            "None"
        else
            match rawname2def |> Map.tryFind rawType with
            | Some r ->
                if category = r.Category then
                    sprintf "%s.Id" r.LetName
                else
                    sprintf "%s.%s.Id" r.Category r.LetName
            | None -> failwith (sprintf "Cannot find type '%s' (in \"%s\" : %s)." rawType id (def.ToString()))

    let e = {
        LetName = letName
        Category = category
        Id = Guid.Parse(id)
        RawName = rawName
        Name = name
        Description = description
        RawType = rawType
        Type = typ
        IsArray = isArray
    }
    defs <- defs |> Map.add e.Id e
    rawname2def <- rawname2def |> Map.add e.RawName e
    e


[<EntryPoint>]
let main argv =

    let address = @"https://raw.githubusercontent.com/aardvark-community/aardvark.data.durable.definitions/master/definitions.json"
    let wc = new WebClient()
    let json = JObject.Parse(wc.DownloadString(address))

    //let json = JObject.Parse(File.ReadAllText(@"..\..\..\..\aardvark.data.durable.definitions\definitions.json"))

    printfn "%s" """using System;
using System.Collections.Generic;
    
namespace Aardvark.Data
{
    /// <summary></summary>
    public static class Durable
    {
        /// <summary></summary>
        public class Def
        {
            /// <summary></summary>
            public readonly Guid Id;
            /// <summary></summary>
            public readonly string Name;
            /// <summary></summary>
            public readonly string Description;
            /// <summary></summary>
            public readonly Guid Type;
            /// <summary></summary>
            public readonly bool IsArray;
    
            /// <summary></summary>
            public Def(Guid id, string name, string description, Guid type, bool isArray)
            {
                if (defs.ContainsKey(id))
                {
                    throw new InvalidOperationException(
                        $"Duplicate Def(id: {id}, name: {name}, description: {description}, type: {type}, isArray: {isArray})."
                        );
                }
    
                Id = id;
                Name = name;
                Description = description;
                Type = type;
                IsArray = isArray;
    
                defs[id] = this;
            }
        }
    
        private static Dictionary<Guid, Def> defs = new Dictionary<Guid, Def>();
    
        /// <summary></summary>
        public static Def Get(Guid key) => defs[key];

        /// <summary></summary>
        public static bool TryGet(Guid key, out Def def) => defs.TryGetValue(key, out def);
    
        private static readonly Guid None = Guid.Empty;
    """
    
    for kv in json do
        let category = kv.Key
        printfn "        /// <summary></summary>"
        printfn "        public static class %s" category
        printfn "        {"
        for kv in kv.Value :?> JObject do

            let entry = parseEntry category kv.Key (kv.Value :?> JObject)

            let formatGuid g = if g = Guid.Empty then "Guid.Empty" else sprintf "new Guid(\"%A\")" g
            
            printfn "        /// <summary>"
            printfn "        /// %s" (entry.Description)
            printfn "        /// </summary>"
            printfn "        public static readonly Def %s = new Def(" entry.LetName
            printfn "            %s," (formatGuid entry.Id)
            printfn "            \"%s\"," entry.Name
            printfn "            \"%s\"," (entry.Description)
            printfn "            %s," entry.Type
            printfn "            %A" entry.IsArray
            printfn "            );"
            printfn ""

        printfn "        }"

    printfn "    }"
    printfn "}"

    0
