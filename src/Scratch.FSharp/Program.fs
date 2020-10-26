open System
open Aardvark.Base
open Aardvark.Data

[<EntryPoint>]
let main argv =

    #if NET472
    printfn "NET472"
    #endif

    #if NETCOREAPP2_2
    printfn "NETCOREAPP2_2"
    #endif

    #if NETCOREAPP3_1
    printfn "NETCOREAPP3_1"
    #endif

    #if NET5_0
    printfn "NET5_0"
    #endif

    let buffer = DurableCodec.Serialize(Durable.Aardvark.Cell2d, Cell2d(1, 2, 3));
    printfn "buffer.Length = %d" buffer.Length
    printfn "%s" (String.Join(" ", buffer |> Array.map (fun x -> x.ToString())))

    //let buffer = DurableCodec.Serialize(Durable.Primitives.Unit, null)

    0
