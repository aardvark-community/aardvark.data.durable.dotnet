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
