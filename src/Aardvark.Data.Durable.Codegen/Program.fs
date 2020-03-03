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

open Aardvark.Data.Durable
open Newtonsoft.Json.Linq
open System.Net
open System.IO

[<EntryPoint>]
let main argv =

    let address = @"https://raw.githubusercontent.com/aardvark-community/aardvark.data.durable.definitions/master/definitions.json"
    let json = JObject.Parse((new WebClient()).DownloadString(address))
    //let json = JObject.Parse(System.IO.File.ReadAllText(@"..\..\..\..\aardvark.data.durable.definitions\definitions.json"))

    let targetFileName = @"../../../src/Aardvark.Data.Durable/DurableDataDefs.cs"
    if File.Exists(targetFileName) then

        let durableDataDefsCSharp = json |> Codegen.generateDurableDataDefsCSharp |> Seq.toArray
        printfn "generated %d lines of code" durableDataDefsCSharp.Length

        File.WriteAllLines(targetFileName, durableDataDefsCSharp)
        printfn "updated file %s" targetFileName
        printfn ""
        printfn "You can now build the solution!"
        printfn ""

    else

        printfn "Something went wrong!"
        printfn "Could not find file %s" (Path.GetFullPath targetFileName)

    0
