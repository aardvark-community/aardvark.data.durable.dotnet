﻿open Aardvark.Data
open System.Collections.Generic

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

    #if NET6_0
    printfn "NET6_0"
    #endif

    //let map = [|
    //    KeyValuePair<string, struct (Durable.Def * obj)>("NodeDataId", (Durable.Primitives.StringUTF8,      "babalu"     :> obj))
    //    KeyValuePair<string, struct (Durable.Def * obj)>("Keys",       (Durable.Primitives.StringUTF8Array, [| "key0" |] :> obj))
    //    KeyValuePair<string, struct (Durable.Def * obj)>("Offsets",    (Durable.Primitives.Int32Array,      [| 17 |]     :> obj))
    //    KeyValuePair<string, struct (Durable.Def * obj)>("Sizes",      (Durable.Primitives.Int32Array,      [| 42 |]     :> obj))
    //    |]
        
    //let indexBuffer = DurableCodec.Serialize(Durable.Primitives.DurableNamedMapDeprecated20221021, map);

    //let decoded = DurableCodec.Deserialize(indexBuffer)

    0
