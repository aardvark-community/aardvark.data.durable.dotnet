open System
open Aardvark.Base
open Aardvark.Data

[<EntryPoint>]
let main argv =

    #if NETCOREAPP3_1
    printfn "NETCOREAPP3_1"
    #endif

    #if NET472
    printfn "NET472"
    #endif

    let buffer = DurableCodec.Serialize(Durable.Aardvark.Cell2d, Cell2d(1, 2, 3));
    printfn "buffer.Length = %d" buffer.Length
    printfn "%s" (String.Join(" ", buffer |> Array.map (fun x -> x.ToString())))

    0
