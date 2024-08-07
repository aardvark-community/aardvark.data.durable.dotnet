﻿(*
    MIT License

    Copyright (c) 2019-2023 Aardworx GmbH (https://aardworx.at). All rights reserved.

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

open System
open System.Text.Json
open System.Text
open System.Security.Cryptography

module Codegen = 

    let hashGuid (s : string) : Guid = s |> Encoding.UTF8.GetBytes |> MD5.Create().ComputeHash |> Guid

    let doNotGenerateCodecFor = Set.ofList <| [
        Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6") // Aardvark.Cell
        Guid("8665c4d4-69c1-4b47-a493-ce452e075643") // Aardvark.CellPadded32
        Guid("9c2e3d4f-7a40-4266-a2dc-bfbde780260a") // Aardvark.CellPadded32[]
        Guid("2732639f-20b2-46dc-8d54-007a2ef2d2ea") // Aardvark.Cell[]
        Guid("9d580e5d-a559-4c5e-9413-7675f1dfe93c") // Aardvark.Cell2d
        Guid("3b022668-faa8-47a9-b622-a7a26060c620") // Aardvark.Cell2dPadded24
        Guid("269f0837-a71f-4967-a323-96ccfabbb184") // Aardvark.Cell2dPadded24[]
        Guid("5c23fd56-3736-4a95-ab74-52b26a711e0e") // Aardvark.Cell2d[]
        Guid("73656667-ea6a-468f-962c-64cd4e24f409") // Aardvark.C3b
        Guid("41dde1c8-2b63-4a18-90c8-8f0c67c685b7") // Aardvark.C3b[]
        Guid("3f34792b-e03d-4d21-a4a6-4890d5f3f67f") // Aardvark.C4b
        Guid("06318db7-1518-43eb-97c4-ba13c83fc64b") // Aardvark.C4b[]
        Guid("ed49d9d5-398c-415f-9dd9-85bb9902cf97") // Aardvark.Polygon2d
        Guid("2cd7815f-d8b9-488a-b506-adf02ce3b6da") // Aardvark.Polygon2d[]
        Guid("4da0c246-8d1d-44a0-ad7a-49522eee8d6e") // Aardvark.Polygon3d
        Guid("7d1c8157-c0a7-49f7-9ed7-e99e0160daad") // Aardvark.Polygon3d[]
        Guid("6982d0a3-1cb4-4c9f-b479-6ebd3926bf7b") // Aardvark.Cylinder3d.Deprecated.20220302
        Guid("972c001e-a7a8-45ea-a234-f4788489d6e7") // Aardvark.CIeLuvf (Renamed to CieLuvF)
        ]

    let binaryWriterAllowedWriteTypes = Set.ofList <| [
        "Float32"; "Float64"
        "Int8"; "Int16"; "Int32"; "Int64"
        "UInt8"; "UInt16"; "UInt32"; "UInt64"
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
        Name : string
        Description : string
        Type : string
        IsArray : bool
        Layout : Option<(string*string)[]>
        Obsolete : Option<string>
    }

    let mutable defs = Map.empty<Guid, Entry>
    let mutable rawname2def = Map.empty<string, Entry>

    let parseEntry category (id : string) (def : JsonElement) : Entry list =
    
        let rawName = 
            match def.TryGetProperty("name") with
            | false, _ -> failwith "Entry must contain field 'name'."
            | true, x -> x.GetString()

        let mutable isArray = rawName.EndsWith("[]")

        let name = rawName

        let description =
            match def.TryGetProperty("description") with
            | true, x -> x.GetString()
            | false, _ ->
                if isArray then
                    sprintf "Array of %s." (name.Replace("[]", ""))
                else
                    sprintf "%s." name

        let letName =
            if name = "Guid" then
                "GuidDef"
            else
                let name = name.Replace("[]", "Array")
                let ts = name.Split('.') |> Array.map (fun s -> s.Substring(0, 1).ToUpper() + s.Substring(1))

                if ts.Length > 1 then
                    if ts.[0] = category then
                        String.Join("", ts |> Array.skip(1))
                    else
                        failwith (sprintf "First part of dotted name must be category. Value = \"%s\"." name)
                else
                    name

        let rawType = 
            match def.TryGetProperty("type") with
            | true, x -> x.GetString()
            | false, _ -> "None"

        let typ =
            if name = "Unit" || rawType = "None" then
                "None"
            else
                match rawname2def |> Map.tryFind rawType with
                | Some r ->
                    isArray <- r.IsArray
                    if category = r.Category then
                        sprintf "%s.Id" r.LetName
                    else
                        sprintf "%s.%s.Id" r.Category r.LetName
                | None -> failwith (sprintf "Cannot find type '%s' (in \"%s\" : %s)." rawType id (def.ToString()))

        let layout = 
            match def.TryGetProperty("layout") with
            | true, x -> x.EnumerateObject() |> Seq.map(fun x -> (x.Name, x.Value.GetString())) |> Seq.toArray |> Some
            | false, _ -> None
            
        let obsolete = 
            match def.TryGetProperty("obsolete") with
            | true, x -> Some(x.GetString())
            | false, _ -> None

        let e = {
            LetName = letName
            Category = category
            Id = Guid.Parse(id)
            Name = name
            Description = description
            Type = typ
            IsArray = isArray
            Layout = layout
            Obsolete = obsolete
        }
        defs <- defs |> Map.add e.Id e
        rawname2def <- rawname2def |> Map.add e.Name e

        if e.IsArray then
            if rawName <> name then failwith "???"
            
            // gz
            let gzLetName = e.LetName + "Gz"
            let gzId = hashGuid (if e.Category = "Octree" then gzLetName else e.Category + "." + gzLetName)
            let gzEntry = { e with Id = gzId; LetName = gzLetName; Name = e.Name + ".gz"; Description = e.Description + " Compressed (GZip)." }

            // lz4
            let lz4LetName = e.LetName + "Lz4"
            let lz4Id = hashGuid (if e.Category = "Octree" then lz4LetName else e.Category + "." + lz4LetName)
            let lz4Entry = { e with Id = lz4Id; LetName = lz4LetName; Name = e.Name + ".lz4"; Description = e.Description + " Compressed (LZ4)." }

            if rawType = "None" then
                [ e; gzEntry; lz4Entry ]
            else
                let gzTyp = e.Type.Substring(0, e.Type.Length - 3) + "Gz.Id"
                let gzEntry = { gzEntry with Type = gzTyp }
                let lz4Typ = e.Type.Substring(0, e.Type.Length - 3) + "Lz4.Id"
                let lz4Entry = { lz4Entry with Type = lz4Typ }
                [ e; gzEntry; lz4Entry ]
        else
            [ e ]

    let header = """/*
    MIT License

    Copyright (c) 2019-2023 Aardworx GmbH (https://aardworx.at). All rights reserved.

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

    let generateCodecAuto (json : JsonElement) : string seq = seq {

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

        let entries = [
            for kv in json.EnumerateObject() do
                let category = kv.Name
                for kv in kv.Value.EnumerateObject() do
                    parseEntry category kv.Name kv.Value
            ] 
        let entries = entries |> List.collect id

        let names = Set.ofList [ for e in entries do yield e.LetName; yield sprintf "%s.%s" e.Category e.LetName ]

        let isAutoGeneratedEntry (e : Entry) =
            if doNotGenerateCodecFor |> Set.contains e.Id then
                false
            else
                match e.IsArray, e.Layout with
                | true, _ -> false
                | _, None -> false
                | false, Some layout -> layout |> Array.forall (fun (_,v) -> (names |> Set.contains v))

        for entry in entries do
            if isAutoGeneratedEntry entry then
                match entry.IsArray, entry.Layout with
                | _, None -> ()
                | true, _ -> ()
                | false, Some layout ->

                    let defName = sprintf "Durable.%s.%s" entry.Category entry.LetName

                    let nameEncodeFun = sprintf "Encode%s" entry.LetName
                    let nameDecodeFun = sprintf "Decode%s" entry.LetName

                    yield sprintf "                #region %s" defName
                    yield ""

                    yield sprintf "                #if NETSTANDARD2_0 || NET472"
                    let writes = String.Join("", layout |> Array.map (fun (k,v) ->
                        if binaryWriterAllowedWriteTypes |> Set.contains v then
                            sprintf "s.Write(x.%s); " k
                        elif names |> Set.contains v then
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
                        elif names |> Set.contains v then
                            let v = v.Substring("Aardvark.".Length)
                            sprintf "(%s)Decode%s(s)" v v
                        else
                            failwith <| sprintf "Don't know how to read '%s'." v
                        ))
                    //                                   Func<BinaryReader, object> DecodeBox3d = s => new Box3d((V3d)DecodeV3d(s), (V3d)DecodeV3d(s));
                    yield String.Format("                Func<BinaryReader, object> {0} = s => new {1}({2});", nameDecodeFun, entry.LetName, reads)
                    yield String.Format("                Func<BinaryReader, object> {0}Array = DecodeArray<{1}>;", nameDecodeFun, entry.LetName)
                    yield sprintf "                #endif"

                    yield sprintf "                #if NETCOREAPP3_1 || NET5_0_OR_GREATER"
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

    let generateDurableDataDefsCSharp (json : JsonElement) : string seq = seq {

        yield sprintf "%s%s" header """
using System;

namespace Aardvark.Data;

public static partial class Durable
{"""
    
        for kv in json.EnumerateObject() do
            let category = kv.Name
            let entries = kv.Value.EnumerateObject() |> Seq.map (fun kv -> parseEntry category kv.Name kv.Value) |> Seq.collect id |> Seq.toArray
            let usesNone = entries |> Seq.exists (fun e -> e.Type = "None")

            yield sprintf "    /// <summary></summary>"
            yield sprintf "    public static class %s" category
            yield sprintf "    {"
            if usesNone then
                yield sprintf "        private static readonly Guid None = Guid.Empty;"
            yield sprintf ""
            for entry in entries do

                let formatGuid g = if g = Guid.Empty then "Guid.Empty" else sprintf "new Guid(\"%A\")" g
            
                yield sprintf "        /// <summary>"
                yield sprintf "        /// %s" (entry.Description)
                yield sprintf "        /// </summary>"
                match entry.Obsolete with
                | Some s -> yield sprintf "        [Obsolete(\"%s\")]" s
                | None -> ()
                yield sprintf "        public static readonly Def %s = new(" entry.LetName
                yield sprintf "            %s," (formatGuid entry.Id)
                yield sprintf "            \"%s\"," entry.Name
                yield sprintf "            \"%s\"," (entry.Description)
                yield sprintf "            %s," entry.Type
                yield sprintf "            %A" entry.IsArray
                yield sprintf "            );"
                yield sprintf ""

            yield sprintf "    }"

        yield sprintf "}"
    }