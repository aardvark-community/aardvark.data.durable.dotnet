(*
    MIT License

    Copyright (c) 2020 Aardworx GmbH (https://aardworx.com). All rights reserved.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*)

namespace Aardvark.Data.Durable

open Newtonsoft.Json.Linq
open System
open System.Collections.Generic

module Codegen = 

    let doNotGenerateCodecFor = Set.ofList <| [
        Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6") // Aardvark.Cell
        Guid("8665c4d4-69c1-4b47-a493-ce452e075643") // Aardvark.CellPadded32
        Guid("9d580e5d-a559-4c5e-9413-7675f1dfe93c") // Aardvark.Cell2d
        Guid("3b022668-faa8-47a9-b622-a7a26060c620") // Aardvark.Cell2dPadded24
        Guid("73656667-ea6a-468f-962c-64cd4e24f409") // Aardvark.C3b
        Guid("3f34792b-e03d-4d21-a4a6-4890d5f3f67f") // Aardvark.C4b
        ]

    let binaryWriterAllowedWriteTypes = Set.ofList <| [
        "Float32"; "Float64"
        "Int8"; "Int16"; "Int32"; "Int64"
        "UInt8"; "UInt16"; "UInt32"; "UInt64"
        ]

    let binaryWriterAllowedAardvarkEncodeTypes = Set.ofList <| [
        "Aardvark.V2i"; "Aardvark.V2l"; "Aardvark.V2f"; "Aardvark.V2d"
        "Aardvark.V3i"; "Aardvark.V3l"; "Aardvark.V3f"; "Aardvark.V3d"
        "Aardvark.Circle2d"; "Aardvark.Circle3d"
        "Aardvark.ObliqueCone3d"; "Aardvark.Cone3d"
        "Aardvark.Sphere3d"
        ]

    let binaryReaderReadFuns = Map.ofList <| [
        ("Float32", "ReadSingle")
        ("Float64", "ReadDouble")
        ("Int8", "ReadSByte")
        ("Int16", "ReadInt16")
        ("Int32", "ReadInt32")
        ("Int64", "ReadInt64")
        ("UInt8", "ReadByte")
        ("UInt16", "ReadUInt16")
        ("UInt32", "ReadUInt32")
        ("UInt64", "ReadUInt64")
        ]

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
        Layout : Option<(string*string)[]>
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

        let layout = 
            if def.ContainsKey("layout") then
                def.["layout"].Values<JProperty>() |> Seq.map(fun x -> (x.Name, x.Value.ToString())) |> Seq.toArray |> Some
            else
                None
            

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
            Layout = layout
        }
        defs <- defs |> Map.add e.Id e
        rawname2def <- rawname2def |> Map.add e.RawName e
        e

    let header = """/*
    MIT License

    Copyright (c) 2020 Aardworx GmbH (https://aardworx.com). All rights reserved.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/"""

    let isAutoGeneratedEntry (e : Entry) =
        if doNotGenerateCodecFor |> Set.contains e.Id then
            false
        else
            match e.IsArray, e.Layout with
            | true, _ -> false
            | _, None -> false
            | false, Some layout ->
                layout |> Array.forall (fun (k,v) ->
                    (binaryWriterAllowedAardvarkEncodeTypes |> Set.contains v)
                    || ((binaryWriterAllowedWriteTypes |> Set.contains v) && (binaryReaderReadFuns |> Map.containsKey v))
                    )

    let generateCodecAuto (json : JObject) : string seq = seq {
        yield sprintf "%s%s" header """
using Aardvark.Base;
using System;
using System.IO;

namespace Aardvark.Data
{
        /// <summary></summary>
        public static partial class DurableCodec
        {
            private static void Init()
            {
"""

        for kv in json :> IEnumerable<KeyValuePair<string, JToken>> do
            let category = kv.Key
            //yield sprintf "        /// <summary></summary>"
            //yield sprintf "        public static class %s" category
            //yield sprintf "        {"
            for kv in kv.Value :?> JObject :> IEnumerable<KeyValuePair<string, JToken>> do

                let entry = parseEntry category kv.Key (kv.Value :?> JObject)

                if isAutoGeneratedEntry entry then
                    match entry.IsArray, entry.Layout with
                    | _, None -> ()
                    | true, _ -> ()
                    | false, Some layout ->

                        let defName = sprintf "Durable.%s.%s" category entry.LetName

                        let nameEncodeFun = sprintf "Encode%s" entry.LetName
                        let nameDecodeFun = sprintf "Decode%s" entry.LetName

                        yield sprintf "                #region %s" defName
                        yield ""

                        yield sprintf "                #if NETSTANDARD2_0 || NET472"
                        let writes = String.Join("", layout |> Array.map (fun (k,v) ->
                            if binaryWriterAllowedWriteTypes |> Set.contains v then
                                sprintf "s.Write(x.%s); " k
                            elif binaryWriterAllowedAardvarkEncodeTypes |> Set.contains v then
                                let v = v.Substring("Aardvark.".Length)
                                sprintf "Encode%s(s, x.%s); " v k
                            else
                                failwith <| sprintf "Don't know how to write '%s'." v
                            ))
                        //                                   Action<BinaryWriter, object> {0} = (s, o) => {  var x = ({1})o; EncodeV3d(s, x.Min); EncodeV3d(s, x.Min); };
                        yield String.Format("                Action<BinaryWriter, object> {0} = (s, o) => {{ var x = ({1})o; {2}}};", nameEncodeFun, entry.LetName, writes)
                        yield String.Format("                Action<BinaryWriter, object> {0}Array = (s, o) => EncodeArray(s, ({1}[])o);", nameEncodeFun, entry.LetName)
                        let reads = String.Join(", ", layout |> Array.map (fun (k,v) ->
                            if binaryReaderReadFuns |> Map.containsKey v then
                                sprintf "s.%s()" (binaryReaderReadFuns |> Map.find v)
                            elif binaryWriterAllowedAardvarkEncodeTypes |> Set.contains v then
                                let v = v.Substring("Aardvark.".Length)
                                sprintf "(%s)Decode%s(s)" v v
                            else
                                failwith <| sprintf "Don't know how to read '%s'." v
                            ))
                        //                                   Func<BinaryReader, object> DecodeBox3d = s => new Box3d((V3d)DecodeV3d(s), (V3d)DecodeV3d(s));
                        yield String.Format("                Func<BinaryReader, object> {0} = s => new {1}({2});", nameDecodeFun, entry.LetName, reads)
                        yield String.Format("                Func<BinaryReader, object> {0}Array = DecodeArray<{1}>;", nameDecodeFun, entry.LetName)
                        yield sprintf "                #endif"

                        yield sprintf "                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0"
                        yield String.Format("                Action<Stream, object> {0} = Write<{1}>;", nameEncodeFun, entry.LetName)
                        yield String.Format("                Action<Stream, object> {0}Array = (s, o) => EncodeArray(s, ({1}[])o);", nameEncodeFun, entry.LetName)
                        yield String.Format("                Func<Stream, object> {0} = ReadBoxed<{1}>;", nameDecodeFun, entry.LetName)
                        yield String.Format("                Func<Stream, object> {0}Array = DecodeArray<{1}>;", nameDecodeFun, entry.LetName)
                        yield sprintf "                #endif"

                        yield sprintf "                s_encoders[%s.Id] = %s;" defName nameEncodeFun
                        yield sprintf "                s_decoders[%s.Id] = %s;" defName nameDecodeFun
                        yield sprintf "                s_encoders[%sArray.Id] = %sArray;" defName nameEncodeFun
                        yield sprintf "                s_decoders[%sArray.Id] = %sArray;" defName nameDecodeFun
                        yield ""
                        
                        yield sprintf "                #endregion"
                        yield ""
                else
                    ()

        yield sprintf "%s" """
            }
        }
}
"""
    }

    let generateDurableDataDefsCSharp (json : JObject) : string seq = seq {

        yield sprintf "%s%s" header """
using System;
using System.Collections.Generic;

namespace Aardvark.Data
{
    /// <summary></summary>
    public static class Durable
    {
        /// <summary></summary>
        public class Def : IEquatable<Def>, IComparable, IComparable<Def>
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
                lock (defs)
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

            /// <summary></summary>
            public override string ToString() => $"[{Name}, {Id}]";

            /// <summary></summary>
            public override int GetHashCode() => Id.GetHashCode();
            
            /// <summary></summary>
            public override bool Equals(object obj) => obj is Def other && Id == other.Id;

            /// <summary></summary>
            public bool Equals(Def other) => !(other is null || Id != other.Id);

            /// <summary></summary>
            public int CompareTo(object obj)
                => obj is Def other
                    ? CompareTo(other)
                    : throw new ArgumentException($"Can't compare Def with {obj?.GetType()}.", nameof(obj))
                    ;

            /// <summary></summary>
            public int CompareTo(Def other) => other is null ? 1 : Id.CompareTo(other.Id);
        }

        private static readonly Dictionary<Guid, Def> defs = new Dictionary<Guid, Def>();

        /// <summary></summary>
        public static Def Get(Guid key) => defs[key];

        /// <summary></summary>
        public static bool TryGet(Guid key, out Def def) => defs.TryGetValue(key, out def);

        private static readonly Guid None = Guid.Empty;
"""
    
        for kv in json :> IEnumerable<KeyValuePair<string, JToken>> do
            let category = kv.Key
            yield sprintf "        /// <summary></summary>"
            yield sprintf "        public static class %s" category
            yield sprintf "        {"
            for kv in kv.Value :?> JObject :> IEnumerable<KeyValuePair<string, JToken>> do

                let entry = parseEntry category kv.Key (kv.Value :?> JObject)

                let formatGuid g = if g = Guid.Empty then "Guid.Empty" else sprintf "new Guid(\"%A\")" g
            
                yield sprintf "            /// <summary>"
                yield sprintf "            /// %s" (entry.Description)
                yield sprintf "            /// </summary>"
                yield sprintf "            public static readonly Def %s = new Def(" entry.LetName
                yield sprintf "                %s," (formatGuid entry.Id)
                yield sprintf "                \"%s\"," entry.Name
                yield sprintf "                \"%s\"," (entry.Description)
                yield sprintf "                %s," entry.Type
                yield sprintf "                %A" entry.IsArray
                yield sprintf "                );"
                yield sprintf ""

            yield sprintf "        }"

        yield sprintf "    }"
        yield sprintf "}"
    }