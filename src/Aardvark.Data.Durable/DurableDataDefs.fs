namespace Aardvark.Data

open System

module Durable =

    type Def = {
        Id : Guid
        Name : string
        Description : string
        Type : Guid
        IsArray : bool
        }

    let mutable private defs = Map.empty<Guid, Def>
    let private addDef x =
        defs <- defs |> Map.add x.Id x
        x

    let get x = defs.[x]
    let tryGet x = defs |> Map.tryFind x

    let private None = Guid.Empty

    module Primitives =

        /// Unit (nothing, none, null, ...).
        let Unit = addDef {
            Id = Guid.Empty
            Name = "Unit"
            Description = "Unit (nothing, none, null, ...)."
            Type = None
            IsArray = false
            }

        /// A map of key/value pairs, where keys are durable IDs with values of corresponding types.
        let DurableMap = addDef {
            Id = Guid("f03716ef-6c9e-4201-bf19-e0cabc6c6a9a")
            Name = "DurableMap"
            Description = "A map of key/value pairs, where keys are durable IDs with values of corresponding types."
            Type = None
            IsArray = false
            }

        /// Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
        let GuidDef = addDef {
            Id = Guid("a81a39b0-8f61-4efc-b0ce-27e2c5d3199d")
            Name = "Guid"
            Description = "Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122."
            Type = None
            IsArray = false
            }

        /// Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
        let GuidArray = addDef {
            Id = Guid("8b5659cd-8fea-46fd-a9f2-52c31bdaf6b3")
            Name = "Guid[]"
            Description = "Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122."
            Type = None
            IsArray = true
            }

        /// Signed 8-bit integer. 2-complement.
        let Int8 = addDef {
            Id = Guid("47a73639-7ff3-423a-9562-2561d0f51949")
            Name = "Int8"
            Description = "Signed 8-bit integer. 2-complement."
            Type = None
            IsArray = false
            }

        /// Array of signed 8-bit integers. 2-complement.
        let Int8Array = addDef {
            Id = Guid("1e36f786-1c8d-4c1a-b5dd-6f83bfd65287")
            Name = "Int8[]"
            Description = "Array of signed 8-bit integers. 2-complement."
            Type = None
            IsArray = true
            }

        /// Unsigned 8-bit integer.
        let UInt8 = addDef {
            Id = Guid("83c0db28-feb4-4643-af3a-269377f137b5")
            Name = "UInt8"
            Description = "Unsigned 8-bit integer."
            Type = None
            IsArray = false
            }

        /// Array of unsigned 8-bit integers.
        let UInt8Array = addDef {
            Id = Guid("e1e6a823-d328-461d-bd01-924120b74d5c")
            Name = "UInt8[]"
            Description = "Array of unsigned 8-bit integers."
            Type = None
            IsArray = true
            }

        /// Signed 16-bit integer. 2-complement.
        let Int16 = addDef {
            Id = Guid("4c3f7ded-2037-4f3d-baa9-3a76ef3a1fda")
            Name = "Int16"
            Description = "Signed 16-bit integer. 2-complement."
            Type = None
            IsArray = false
            }

        /// Array of signed 16-bit integers. 2-complement.
        let Int16Array = addDef {
            Id = Guid("80b7028e-e7c8-442c-8ae3-517bb2df645f")
            Name = "Int16[]"
            Description = "Array of signed 16-bit integers. 2-complement."
            Type = None
            IsArray = true
            }

        /// Unsigned 16-bit integer.
        let UInt16 = addDef {
            Id = Guid("8b1bc0ed-64aa-4c4c-992e-dca6b1491dd0")
            Name = "UInt16"
            Description = "Unsigned 16-bit integer."
            Type = None
            IsArray = false
            }

        /// Array of unsigned 16-bit integers.
        let UInt16Array = addDef {
            Id = Guid("0b8a61ac-672f-4247-a8c5-2cf8f23a1eb5")
            Name = "UInt16[]"
            Description = "Array of unsigned 16-bit integers."
            Type = None
            IsArray = true
            }

        /// Signed 32-bit integer. 2-complement.
        let Int32 = addDef {
            Id = Guid("5ce108a4-a578-4edb-841d-068393ed93bf")
            Name = "Int32"
            Description = "Signed 32-bit integer. 2-complement."
            Type = None
            IsArray = false
            }

        /// Array of signed 32-bit integers. 2-complement.
        let Int32Array = addDef {
            Id = Guid("1cfa6f68-5b56-44a7-b4b5-bd675bc910ab")
            Name = "Int32[]"
            Description = "Array of signed 32-bit integers. 2-complement."
            Type = None
            IsArray = true
            }

        /// Unsigned 32-bit integer.
        let UInt32 = addDef {
            Id = Guid("a77758f8-24c4-4d87-95f1-91a6eab9df01")
            Name = "UInt32"
            Description = "Unsigned 32-bit integer."
            Type = None
            IsArray = false
            }

        /// Array of unsigned 32-bit integers.
        let UInt32Array = addDef {
            Id = Guid("4c896235-d378-4860-9b01-581138e565d3")
            Name = "UInt32[]"
            Description = "Array of unsigned 32-bit integers."
            Type = None
            IsArray = true
            }

        /// Signed 64-bit integer. 2-complement.
        let Int64 = addDef {
            Id = Guid("f0909b36-d3c4-4b86-8320-e0ad418226e5")
            Name = "Int64"
            Description = "Signed 64-bit integer. 2-complement."
            Type = None
            IsArray = false
            }

        /// Array of signed 64-bit integers. 2-complement.
        let Int64Array = addDef {
            Id = Guid("39761157-4817-4dbf-9eda-33fad1c0a852")
            Name = "Int64[]"
            Description = "Array of signed 64-bit integers. 2-complement."
            Type = None
            IsArray = true
            }

        /// Unsigned 64-bit integer.
        let UInt64 = addDef {
            Id = Guid("1e29371c-e977-402e-8cd6-9d52a77ce1d6")
            Name = "UInt64"
            Description = "Unsigned 64-bit integer."
            Type = None
            IsArray = false
            }

        /// Array of unsigned 64-bit integers.
        let UInt64Array = addDef {
            Id = Guid("56a89a90-5dde-441c-8d80-d2dca7f6717e")
            Name = "UInt64[]"
            Description = "Array of unsigned 64-bit integers."
            Type = None
            IsArray = true
            }

        /// Floating point value (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float16 = addDef {
            Id = Guid("7891b070-5249-479f-81b8-d8bca5127211")
            Name = "Float16"
            Description = "Floating point value (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = false
            }

        /// Array of floating point values (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float16Array = addDef {
            Id = Guid("fb1d889e-b7bb-41f4-b047-1f6838cd5fdd")
            Name = "Float16[]"
            Description = "Array of floating point values (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = true
            }

        /// Floating point value (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float32 = addDef {
            Id = Guid("23fb286f-663b-4c71-9923-7e51c500f4ed")
            Name = "Float32"
            Description = "Floating point value (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = false
            }

        /// Array of floating point values (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float32Array = addDef {
            Id = Guid("a687a789-1b63-49e9-a2e4-8099aa7879e9")
            Name = "Float32[]"
            Description = "Array of floating point values (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = true
            }

        /// Floating point value (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float64 = addDef {
            Id = Guid("c58c9b83-c2de-4153-a588-39c808aed50b")
            Name = "Float64"
            Description = "Floating point value (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = false
            }

        /// Array of floating point values (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float64Array = addDef {
            Id = Guid("ba60cc30-2d56-45d8-a051-6b895b51bb3f")
            Name = "Float64[]"
            Description = "Array of floating point values (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = true
            }

        /// Floating point value (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float128 = addDef {
            Id = Guid("5d343235-21f6-41e4-992e-93541db26502")
            Name = "Float128"
            Description = "Floating point value (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = false
            }

        /// Array of floating point values (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
        let Float128Array = addDef {
            Id = Guid("6477a574-ffb0-4717-9f00-5fb9aff409ce")
            Name = "Float128[]"
            Description = "Array of floating point values (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754."
            Type = None
            IsArray = true
            }

        /// String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8).
        let StringUTF8 = addDef {
            Id = Guid("917c15af-0e2d-4265-a732-7b2f147f4b94")
            Name = "StringUTF8"
            Description = "String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8)."
            Type = None
            IsArray = false
            }

        /// Array of strings. UTF8 encoding.
        let StringUTF8Array = addDef {
            Id = Guid("852888ff-4168-4f4b-a10a-b582d1735c74")
            Name = "StringUTF8[]"
            Description = "Array of strings. UTF8 encoding."
            Type = None
            IsArray = true
            }

    module Aardvark =

        /// A 2-dim vector of 32-bit integers.
        let V2i = addDef {
            Id = Guid("1193e05b-4c04-409b-b47b-f9f4fbce7fb2")
            Name = "V2i"
            Description = "A 2-dim vector of 32-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V2i.
        let V2iArray = addDef {
            Id = Guid("a684e893-16fa-42e5-a534-843dbec575e8")
            Name = "V2i[]"
            Description = "Array of V2i."
            Type = None
            IsArray = true
            }

        /// A 2-dim vector of 64-bit integers.
        let V2l = addDef {
            Id = Guid("5573a69d-4df9-4d91-8e3e-aa8204d8ec13")
            Name = "V2l"
            Description = "A 2-dim vector of 64-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V2l.
        let V2lArray = addDef {
            Id = Guid("f5045bd5-08d1-4084-b717-932b55dcdc5b")
            Name = "V2l[]"
            Description = "Array of V2l."
            Type = None
            IsArray = true
            }

        /// A 2-dim vector of 32-bit floats.
        let V2f = addDef {
            Id = Guid("4f5d8782-3c9a-4913-bf0f-423269a24b1e")
            Name = "V2f"
            Description = "A 2-dim vector of 32-bit floats."
            Type = None
            IsArray = false
            }

        /// Array of V2f.
        let V2fArray = addDef {
            Id = Guid("40d91f9d-ccb3-44fb-83d0-c3ff20189b2d")
            Name = "V2f[]"
            Description = "Array of V2f."
            Type = None
            IsArray = true
            }

        /// A 2-dim vector of 64-bit floats.
        let V2d = addDef {
            Id = Guid("0f70ed18-574f-4431-a2c1-9987e4a7653c")
            Name = "V2d"
            Description = "A 2-dim vector of 64-bit floats."
            Type = None
            IsArray = false
            }

        /// Array of V2d.
        let V2dArray = addDef {
            Id = Guid("17037869-687f-45f1-bd43-09a46a669547")
            Name = "V2d[]"
            Description = "Array of V2d."
            Type = None
            IsArray = true
            }

        /// A 3-dim vector of 32-bit integers.
        let V3i = addDef {
            Id = Guid("876c952e-1749-4d2f-922f-75d1acd2d870")
            Name = "V3i"
            Description = "A 3-dim vector of 32-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V3i.
        let V3iArray = addDef {
            Id = Guid("e9b3bee6-d6c4-46cb-9b74-be54530d03cd")
            Name = "V3i[]"
            Description = "Array of V3i."
            Type = None
            IsArray = true
            }

        /// A 3-dim vector of 64-bit integers.
        let V3l = addDef {
            Id = Guid("baff1328-3149-4812-901b-23d9b3ba3a29")
            Name = "V3l"
            Description = "A 3-dim vector of 64-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V3l.
        let V3lArray = addDef {
            Id = Guid("229703bb-c382-4b5a-b333-30a029e77f83")
            Name = "V3l[]"
            Description = "Array of V3l."
            Type = None
            IsArray = true
            }

        /// A 3-dim vector of 32-bit floats.
        let V3f = addDef {
            Id = Guid("ad8adcb6-8cf1-474e-99da-851343858935")
            Name = "V3f"
            Description = "A 3-dim vector of 32-bit floats."
            Type = None
            IsArray = false
            }

        /// Array of V3f.
        let V3fArray = addDef {
            Id = Guid("f14f7607-3ddd-4e52-9ff3-c877c2242021")
            Name = "V3f[]"
            Description = "Array of V3f."
            Type = None
            IsArray = true
            }

        /// A 3-dim vector of 64-bit floats.
        let V3d = addDef {
            Id = Guid("7a0be234-ab45-464d-b706-87157aba4361")
            Name = "V3d"
            Description = "A 3-dim vector of 64-bit floats."
            Type = None
            IsArray = false
            }

        /// Array of V3d.
        let V3dArray = addDef {
            Id = Guid("2cce99b6-e823-4b34-8615-f7ab88746554")
            Name = "V3d[]"
            Description = "Array of V3d."
            Type = None
            IsArray = true
            }

        /// A 4-dim vector of 32-bit integers.
        let V4i = addDef {
            Id = Guid("244a0ae8-c234-4024-821b-d5b3ad28701d")
            Name = "V4i"
            Description = "A 4-dim vector of 32-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V4i.
        let V4iArray = addDef {
            Id = Guid("4e5839a1-0b5b-4407-a55c-cfa5fecf757c")
            Name = "V4i[]"
            Description = "Array of V4i."
            Type = None
            IsArray = true
            }

        /// A 4-dim vector of 64-bit integers.
        let V4l = addDef {
            Id = Guid("04f77262-b56a-44d5-af79-c1679493acff")
            Name = "V4l"
            Description = "A 4-dim vector of 64-bit integers."
            Type = None
            IsArray = false
            }

        /// Array of V4l.
        let V4lArray = addDef {
            Id = Guid("8aecdd7e-acf1-41aa-9d02-f954b43d6c62")
            Name = "V4l[]"
            Description = "Array of V4l."
            Type = None
            IsArray = true
            }

        /// A 4-dim vector of 32-bit float.
        let V4f = addDef {
            Id = Guid("969daa40-9ea2-4bce-8189-f416d65a9c3e")
            Name = "V4f"
            Description = "A 4-dim vector of 32-bit float."
            Type = None
            IsArray = false
            }

        /// Array of V4f.
        let V4fArray = addDef {
            Id = Guid("be5a8fda-4a6a-46e8-9654-356721d03f17")
            Name = "V4f[]"
            Description = "Array of V4f."
            Type = None
            IsArray = true
            }

        /// A 4-dim vector of 64-bit float.
        let V4d = addDef {
            Id = Guid("b2dd492b-aaf8-4dfa-bcc2-833af6cbd637")
            Name = "V4d"
            Description = "A 4-dim vector of 64-bit float."
            Type = None
            IsArray = false
            }

        /// Array of V4d.
        let V4dArray = addDef {
            Id = Guid("800184a5-c207-4b4a-88a0-60d9281ecdc1")
            Name = "V4d[]"
            Description = "Array of V4d."
            Type = None
            IsArray = true
            }

        /// A 2x2 matrix of Float32.
        let M22f = addDef {
            Id = Guid("4f01ceee-2595-4c2d-859d-4f14df35a048")
            Name = "M22f"
            Description = "A 2x2 matrix of Float32."
            Type = None
            IsArray = false
            }

        /// Array of M22f.
        let M22fArray = addDef {
            Id = Guid("480269a5-304c-401a-848b-64e2392ddd3e")
            Name = "M22f[]"
            Description = "Array of M22f."
            Type = None
            IsArray = true
            }

        /// A 2x2 matrix of Float64.
        let M22d = addDef {
            Id = Guid("f16842ef-531c-4782-92f2-385bf5fd42ab")
            Name = "M22d"
            Description = "A 2x2 matrix of Float64."
            Type = None
            IsArray = false
            }

        /// Array of M22d.
        let M22dArray = addDef {
            Id = Guid("2e6b3a90-3e45-4e5c-8770-e351640f5d47")
            Name = "M22d[]"
            Description = "Array of M22d."
            Type = None
            IsArray = true
            }

        /// A 3x3 matrix of Float32.
        let M33f = addDef {
            Id = Guid("587a4a05-db34-4b2a-aa67-5cfe5c4d82cc")
            Name = "M33f"
            Description = "A 3x3 matrix of Float32."
            Type = None
            IsArray = false
            }

        /// Array of M33f.
        let M33fArray = addDef {
            Id = Guid("95be083c-6f03-4279-872f-624f044599c6")
            Name = "M33f[]"
            Description = "Array of M33f."
            Type = None
            IsArray = true
            }

        /// A 3x3 matrix of Float64.
        let M33d = addDef {
            Id = Guid("ccd797e3-0ca4-4191-840d-53751e021972")
            Name = "M33d"
            Description = "A 3x3 matrix of Float64."
            Type = None
            IsArray = false
            }

        /// Array of M33d.
        let M33dArray = addDef {
            Id = Guid("378429d5-2517-46bb-b90d-b7bc34a86466")
            Name = "M33d[]"
            Description = "Array of M33d."
            Type = None
            IsArray = true
            }

        /// A 4x4 matrix of Float32.
        let M44f = addDef {
            Id = Guid("652a5421-e262-4da8-ae38-78df761a365e")
            Name = "M44f"
            Description = "A 4x4 matrix of Float32."
            Type = None
            IsArray = false
            }

        /// Array of M44f.
        let M44fArray = addDef {
            Id = Guid("cf53c4e6-f3ca-4be5-a449-0434ae455b85")
            Name = "M44f[]"
            Description = "Array of M44f."
            Type = None
            IsArray = true
            }

        /// A 4x4 matrix of Float64.
        let M44d = addDef {
            Id = Guid("b9498622-db77-4d4c-b78c-62522ccf9252")
            Name = "M44d"
            Description = "A 4x4 matrix of Float64."
            Type = None
            IsArray = false
            }

        /// Array of M44d.
        let M44dArray = addDef {
            Id = Guid("f07abd79-60a6-429a-94f1-11eb6c319db2")
            Name = "M44d[]"
            Description = "Array of M44d."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of UInt8.
        let Range1b = addDef {
            Id = Guid("db31d0e0-2c56-48da-a769-5a2c1abad38c")
            Name = "Range1b"
            Description = "An 1-dim range with limits [Min, Max] of UInt8."
            Type = None
            IsArray = false
            }

        /// Array of Range1b.
        let Range1bArray = addDef {
            Id = Guid("b720aba1-6b8a-4431-8bea-8b2dad497358")
            Name = "Range1b[]"
            Description = "Array of Range1b."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Int8.
        let Range1sb = addDef {
            Id = Guid("59e5322f-1677-47e4-b991-3e87e43ac005")
            Name = "Range1sb"
            Description = "An 1-dim range with limits [Min, Max] of Int8."
            Type = None
            IsArray = false
            }

        /// Array of Range1sb.
        let Range1sbArray = addDef {
            Id = Guid("6b769f32-b462-4a8a-a9eb-d908f650dae2")
            Name = "Range1sb[]"
            Description = "Array of Range1sb."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Int16.
        let Range1s = addDef {
            Id = Guid("ed0450e7-2a14-4fe6-b3ac-f4a8ee314fad")
            Name = "Range1s"
            Description = "An 1-dim range with limits [Min, Max] of Int16."
            Type = None
            IsArray = false
            }

        /// Array of Range1s.
        let Range1sArray = addDef {
            Id = Guid("f8ce22da-f877-45a9-9f69-129473d22809")
            Name = "Range1s[]"
            Description = "Array of Range1s."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of UInt16.
        let Range1us = addDef {
            Id = Guid("7809e939-1d9b-4033-9b7e-7459a2e53b73")
            Name = "Range1us"
            Description = "An 1-dim range with limits [Min, Max] of UInt16."
            Type = None
            IsArray = false
            }

        /// Array of Range1us.
        let Range1usArray = addDef {
            Id = Guid("9aad8600-41f0-4f83-a431-de44c4031e9a")
            Name = "Range1us[]"
            Description = "Array of Range1us."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Int32.
        let Range1i = addDef {
            Id = Guid("06fad1c2-33a1-4962-92af-19a7c84560a9")
            Name = "Range1i"
            Description = "An 1-dim range with limits [Min, Max] of Int32."
            Type = None
            IsArray = false
            }

        /// Array of Range1i.
        let Range1iArray = addDef {
            Id = Guid("4c1a43f6-23ba-49aa-9cbf-b308d8524850")
            Name = "Range1i[]"
            Description = "Array of Range1i."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of UInt32.
        let Range1ui = addDef {
            Id = Guid("7ff2c8c9-9c4d-4fb2-a750-f07338ebe0b5")
            Name = "Range1ui"
            Description = "An 1-dim range with limits [Min, Max] of UInt32."
            Type = None
            IsArray = false
            }

        /// Array of Range1ui.
        let Range1uiArray = addDef {
            Id = Guid("a9688eae-5f72-48e3-be92-c214dcf106ca")
            Name = "Range1ui[]"
            Description = "Array of Range1ui."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Int64
        let Range1l = addDef {
            Id = Guid("03ac4568-a97b-4ca6-b005-587cd9afde75")
            Name = "Range1l"
            Description = "An 1-dim range with limits [Min, Max] of Int64"
            Type = None
            IsArray = false
            }

        /// Array of Range1l.
        let Range1lArray = addDef {
            Id = Guid("85f5ed95-0b6a-4ea9-a4f1-38055a93781f")
            Name = "Range1l[]"
            Description = "Array of Range1l."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of UInt64
        let Range1ul = addDef {
            Id = Guid("b7e36341-3dbb-47a0-b5c7-2d199f8d909b")
            Name = "Range1ul"
            Description = "An 1-dim range with limits [Min, Max] of UInt64"
            Type = None
            IsArray = false
            }

        /// Array of Range1ul.
        let Range1ulArray = addDef {
            Id = Guid("7b733797-ee9a-4432-bfd4-4d428e3710d9")
            Name = "Range1ul[]"
            Description = "Array of Range1ul."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Float32
        let Range1f = addDef {
            Id = Guid("f5b3c83b-a294-4f40-90aa-4abd7c627e95")
            Name = "Range1f"
            Description = "An 1-dim range with limits [Min, Max] of Float32"
            Type = None
            IsArray = false
            }

        /// Array of Range1f.
        let Range1fArray = addDef {
            Id = Guid("7406e3c8-c5b0-465f-afa9-9104b2387462")
            Name = "Range1f[]"
            Description = "Array of Range1f."
            Type = None
            IsArray = true
            }

        /// An 1-dim range with limits [Min, Max] of Float64
        let Range1d = addDef {
            Id = Guid("b82bff3c-d075-4d16-85d5-0be5b31a9465")
            Name = "Range1d"
            Description = "An 1-dim range with limits [Min, Max] of Float64"
            Type = None
            IsArray = false
            }

        /// Array of Range1d.
        let Range1dArray = addDef {
            Id = Guid("91420eb9-069d-47da-9911-058e12c6ad43")
            Name = "Range1d[]"
            Description = "Array of Range1d."
            Type = None
            IsArray = true
            }

        /// An 2-dim axis-aligned box with limits [Min, Max] of V2i.
        let Box2i = addDef {
            Id = Guid("0edba5a6-1cec-401f-8d98-78bb4b3319e5")
            Name = "Box2i"
            Description = "An 2-dim axis-aligned box with limits [Min, Max] of V2i."
            Type = None
            IsArray = false
            }

        /// Array of Box2i.
        let Box2iArray = addDef {
            Id = Guid("16afc42c-d4fd-4988-bd38-a039608ce612")
            Name = "Box2i[]"
            Description = "Array of Box2i."
            Type = None
            IsArray = true
            }

        /// An 2-dim axis-aligned box with limits [Min, Max] of V2l.
        let Box2l = addDef {
            Id = Guid("380422d0-0428-47a6-aeb3-3ab328e21bef")
            Name = "Box2l"
            Description = "An 2-dim axis-aligned box with limits [Min, Max] of V2l."
            Type = None
            IsArray = false
            }

        /// Array of Box2l.
        let Box2lArray = addDef {
            Id = Guid("062de2a5-1bdc-4b62-8a58-50d2f002676f")
            Name = "Box2l[]"
            Description = "Array of Box2l."
            Type = None
            IsArray = true
            }

        /// An 2-dim axis-aligned box with limits [Min, Max] of V2f.
        let Box2f = addDef {
            Id = Guid("414d504d-f350-439b-a73a-4fcc38aafa89")
            Name = "Box2f"
            Description = "An 2-dim axis-aligned box with limits [Min, Max] of V2f."
            Type = None
            IsArray = false
            }

        /// Array of Box2f.
        let Box2fArray = addDef {
            Id = Guid("8a515f76-89cb-45e5-9c3e-81afecab0dad")
            Name = "Box2f[]"
            Description = "Array of Box2f."
            Type = None
            IsArray = true
            }

        /// An 2-dim axis-aligned box with limits [Min, Max] of V2d.
        let Box2d = addDef {
            Id = Guid("2fb054de-db29-4c1c-bc97-5a0cce4bc291")
            Name = "Box2d"
            Description = "An 2-dim axis-aligned box with limits [Min, Max] of V2d."
            Type = None
            IsArray = false
            }

        /// Array of Box2d.
        let Box2dArray = addDef {
            Id = Guid("e2f9e9a9-c78f-436c-89e3-da69efefb9ec")
            Name = "Box2d[]"
            Description = "Array of Box2d."
            Type = None
            IsArray = true
            }

        /// An 3-dim axis-aligned box with limits [Min, Max] of V3i.
        let Box3i = addDef {
            Id = Guid("c1301768-a349-489d-907e-a8967166cd7c")
            Name = "Box3i"
            Description = "An 3-dim axis-aligned box with limits [Min, Max] of V3i."
            Type = None
            IsArray = false
            }

        /// Array of Box3i.
        let Box3iArray = addDef {
            Id = Guid("c5bfc96a-fcf7-47d9-9c29-b403734403c4")
            Name = "Box3i[]"
            Description = "Array of Box3i."
            Type = None
            IsArray = true
            }

        /// An 3-dim axis-aligned box with limits [Min, Max] of V3l.
        let Box3l = addDef {
            Id = Guid("b22529e1-926a-4312-bb5c-3bc63700e4ac")
            Name = "Box3l"
            Description = "An 3-dim axis-aligned box with limits [Min, Max] of V3l."
            Type = None
            IsArray = false
            }

        /// Array of Box3l.
        let Box3lArray = addDef {
            Id = Guid("0d57063d-cf07-4830-b678-ce7ad7b0e6a1")
            Name = "Box3l[]"
            Description = "Array of Box3l."
            Type = None
            IsArray = true
            }

        /// An 3-dim axis-aligned box with limits [Min, Max] of V3f.
        let Box3f = addDef {
            Id = Guid("416721ca-6df1-4ada-b7ad-1da7256f490d")
            Name = "Box3f"
            Description = "An 3-dim axis-aligned box with limits [Min, Max] of V3f."
            Type = None
            IsArray = false
            }

        /// Array of Box3f.
        let Box3fArray = addDef {
            Id = Guid("82b363ce-f626-4ac3-bd69-038874f4b661")
            Name = "Box3f[]"
            Description = "Array of Box3f."
            Type = None
            IsArray = true
            }

        /// An 3-dim axis-aligned box with limits [Min, Max] of V3d.
        let Box3d = addDef {
            Id = Guid("5926f1ce-37fb-4022-a6e5-536b22ad79ea")
            Name = "Box3d"
            Description = "An 3-dim axis-aligned box with limits [Min, Max] of V3d."
            Type = None
            IsArray = false
            }

        /// Array of Box3d.
        let Box3dArray = addDef {
            Id = Guid("59e43b45-5221-4127-8da9-0b28c07a5b22")
            Name = "Box3d[]"
            Description = "Array of Box3d."
            Type = None
            IsArray = true
            }

        /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent.
        let Cell = addDef {
            Id = Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6")
            Name = "Cell"
            Description = "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent."
            Type = None
            IsArray = false
            }

        /// Array of Cell.
        let CellArray = addDef {
            Id = Guid("2732639f-20b2-46dc-8d54-007a2ef2d2ea")
            Name = "Cell[]"
            Description = "Array of Cell."
            Type = None
            IsArray = true
            }

        /// A color with channels BGR of UInt8.
        let C3b = addDef {
            Id = Guid("73656667-ea6a-468f-962c-64cd4e24f409")
            Name = "C3b"
            Description = "A color with channels BGR of UInt8."
            Type = None
            IsArray = false
            }

        /// Array of C3b.
        let C3bArray = addDef {
            Id = Guid("41dde1c8-2b63-4a18-90c8-8f0c67c685b7")
            Name = "C3b[]"
            Description = "Array of C3b."
            Type = None
            IsArray = true
            }

        /// A color with channels RGB of UInt16.
        let C3us = addDef {
            Id = Guid("e606b2fc-5a95-420e-8ddc-66a65e7e31f0")
            Name = "C3us"
            Description = "A color with channels RGB of UInt16."
            Type = None
            IsArray = false
            }

        /// Array of C3us.
        let C3usArray = addDef {
            Id = Guid("4225db2b-dcb6-458b-bc14-ba5d9e7ab557")
            Name = "C3us[]"
            Description = "Array of C3us."
            Type = None
            IsArray = true
            }

        /// A color with channels RGB of UInt32.
        let C3ui = addDef {
            Id = Guid("0314f5c3-28b8-4523-8a2f-1b75d01d055b")
            Name = "C3ui"
            Description = "A color with channels RGB of UInt32."
            Type = None
            IsArray = false
            }

        /// Array of C3ui.
        let C3uiArray = addDef {
            Id = Guid("394e46b5-ac4d-44ca-8c48-88b12a974d7c")
            Name = "C3ui[]"
            Description = "Array of C3ui."
            Type = None
            IsArray = true
            }

        /// A color with channels RGB of Float32.
        let C3f = addDef {
            Id = Guid("76873abb-5cd1-46a9-90f6-92db62731bcf")
            Name = "C3f"
            Description = "A color with channels RGB of Float32."
            Type = None
            IsArray = false
            }

        /// Array of C3f.
        let C3fArray = addDef {
            Id = Guid("e0ffeee7-96a5-4705-9589-2f1bac139068")
            Name = "C3f[]"
            Description = "Array of C3f."
            Type = None
            IsArray = true
            }

        /// A color with channels RGB of Float64.
        let C3d = addDef {
            Id = Guid("b87f7505-d164-45e1-bcde-72304e924abe")
            Name = "C3d"
            Description = "A color with channels RGB of Float64."
            Type = None
            IsArray = false
            }

        /// Array of C3d.
        let C3dArray = addDef {
            Id = Guid("47eab504-ace3-47d4-8ad7-e5ed8599c01a")
            Name = "C3d[]"
            Description = "Array of C3d."
            Type = None
            IsArray = true
            }

        /// A color with channels BGRA of UInt8.
        let C4b = addDef {
            Id = Guid("3f34792b-e03d-4d21-a4a6-4890d5f3f67f")
            Name = "C4b"
            Description = "A color with channels BGRA of UInt8."
            Type = None
            IsArray = false
            }

        /// Array of C4b.
        let C4bArray = addDef {
            Id = Guid("06318db7-1518-43eb-97c4-ba13c83fc64b")
            Name = "C4b[]"
            Description = "Array of C4b."
            Type = None
            IsArray = true
            }

        /// A color with channels BGRA of UInt16.
        let C4us = addDef {
            Id = Guid("85917bcd-00e9-4402-abc7-38c973c96ecc")
            Name = "C4us"
            Description = "A color with channels BGRA of UInt16."
            Type = None
            IsArray = false
            }

        /// Array of C4us.
        let C4usArray = addDef {
            Id = Guid("f3944349-d463-486b-be25-8bc1764f1323")
            Name = "C4us[]"
            Description = "Array of C4us."
            Type = None
            IsArray = true
            }

        /// A color with channels BGRA of UInt32.
        let C4ui = addDef {
            Id = Guid("7018167d-2316-4ff0-a239-0ebf95c32adf")
            Name = "C4ui"
            Description = "A color with channels BGRA of UInt32."
            Type = None
            IsArray = false
            }

        /// Array of C4ui.
        let C4uiArray = addDef {
            Id = Guid("1afb8c51-a29a-4919-8c32-a62ade22a857")
            Name = "C4ui[]"
            Description = "Array of C4ui."
            Type = None
            IsArray = true
            }

        /// A color with channels BGRA of Float32.
        let C4f = addDef {
            Id = Guid("e09cfff1-b186-42e8-9b3d-6a4325117ba4")
            Name = "C4f"
            Description = "A color with channels BGRA of Float32."
            Type = None
            IsArray = false
            }

        /// Array of C4f.
        let C4fArray = addDef {
            Id = Guid("4b9b675b-a575-47f0-9b7e-0f49b9904dc5")
            Name = "C4f[]"
            Description = "Array of C4f."
            Type = None
            IsArray = true
            }

        /// A color with channels BGRA of Float64.
        let C4d = addDef {
            Id = Guid("fe74ea05-4b9a-4723-a075-eec853c9cc19")
            Name = "C4d"
            Description = "A color with channels BGRA of Float64."
            Type = None
            IsArray = false
            }

        /// Array of C4d.
        let C4dArray = addDef {
            Id = Guid("75e7b36b-fb9e-4b9e-b92c-728c69b6feae")
            Name = "C4d[]"
            Description = "Array of C4d."
            Type = None
            IsArray = true
            }

        /// Aardvark.Geometry.PointRkdTreeFData.
        let PointRkdTreeFData = addDef {
            Id = Guid("023ebe21-d8c2-4ccd-9c6d-326ead4a0ee9")
            Name = "PointRkdTreeFData"
            Description = "Aardvark.Geometry.PointRkdTreeFData."
            Type = None
            IsArray = false
            }

        /// Aardvark.Geometry.PointRkdTreeDData.
        let PointRkdTreeDData = addDef {
            Id = Guid("e6445682-46e8-4598-9757-22757e6110ca")
            Name = "PointRkdTreeDData"
            Description = "Aardvark.Geometry.PointRkdTreeDData."
            Type = None
            IsArray = false
            }

    module Octree =

        /// Octree. An octree node. DurableMap.
        let Node = addDef {
            Id = Guid("e0883944-1d81-4ff5-845f-0b96075880b7")
            Name = "Octree.Node"
            Description = "Octree. An octree node. DurableMap."
            Type = Primitives.DurableMap.Id
            IsArray = false
            }

        /// Octree. An octree node followed by all other nodes in depth first order. DurableMap.
        let Tree = addDef {
            Id = Guid("708b4b8e-286b-4658-82ac-dd8ea98d1b1c")
            Name = "Octree.Tree"
            Description = "Octree. An octree node followed by all other nodes in depth first order. DurableMap."
            Type = Primitives.DurableMap.Id
            IsArray = false
            }

        /// Octree. Node's unique id.
        let NodeId = addDef {
            Id = Guid("1100ffd5-7789-4872-9ef2-67d45be0c489")
            Name = "Octree.NodeId"
            Description = "Octree. Node's unique id."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Subnodes as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes.
        let SubnodesGuids = addDef {
            Id = Guid("eb44f9b0-3247-4426-b458-1b6e9880d466")
            Name = "Octree.Subnodes.Guids"
            Description = "Octree. Subnodes as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes."
            Type = Primitives.GuidArray.Id
            IsArray = false
            }

        /// Octree. Exact bounding box of this node's positions. Global space. Box3d.
        let BoundingBoxExactGlobal = addDef {
            Id = Guid("7912c862-74b4-4f44-a8cd-d11ea1da9304")
            Name = "Octree.BoundingBoxExactGlobal"
            Description = "Octree. Exact bounding box of this node's positions. Global space. Box3d."
            Type = Aardvark.Box3d.Id
            IsArray = false
            }

        /// Octree. Exact bounding box of this node's positions. Local space. Box3f.
        let BoundingBoxExactLocal = addDef {
            Id = Guid("aadbb622-1cf6-42e0-86df-be79d28d6757")
            Name = "Octree.BoundingBoxExactLocal"
            Description = "Octree. Exact bounding box of this node's positions. Local space. Box3f."
            Type = Aardvark.Box3f.Id
            IsArray = false
            }

        /// Octree. Cell's index. Global space. Cell.
        let Cell = addDef {
            Id = Guid("9f8121e4-83af-40e3-aed9-5fd908a140ee")
            Name = "Octree.Cell"
            Description = "Octree. Cell's index. Global space. Cell."
            Type = Aardvark.Cell.Id
            IsArray = false
            }

        /// Octree. Number of points in this cell. Int32.
        let PointCountCell = addDef {
            Id = Guid("172e1f20-0ffc-4d9c-9b3d-903fca41abe3")
            Name = "Octree.PointCountCell"
            Description = "Octree. Number of points in this cell. Int32."
            Type = Primitives.Int32.Id
            IsArray = false
            }

        /// Octree. Total number of points in this tree's leaf nodes. Int64.
        let PointCountTreeLeafs = addDef {
            Id = Guid("71e80c00-06b6-4e84-a0f7-dbababd2613c")
            Name = "Octree.PointCountTreeLeafs"
            Description = "Octree. Total number of points in this tree's leaf nodes. Int64."
            Type = Primitives.Int64.Id
            IsArray = false
            }

        /// Octree. Average distance of each point to its nearest neighbour.
        let AveragePointDistance = addDef {
            Id = Guid("39c21132-4570-4624-afae-6304851567d7")
            Name = "Octree.AveragePointDistance"
            Description = "Octree. Average distance of each point to its nearest neighbour."
            Type = Primitives.Float32.Id
            IsArray = false
            }

        /// Octree. Standard deviation of average distances of each point to its nearest neighbour.
        let AveragePointDistanceStdDev = addDef {
            Id = Guid("94cac234-b6ea-443a-b196-c7dd8e5def0d")
            Name = "Octree.AveragePointDistanceStdDev"
            Description = "Octree. Standard deviation of average distances of each point to its nearest neighbour."
            Type = Primitives.Float32.Id
            IsArray = false
            }

        /// Octree. Min depth of this tree. A leaf node has depth 0. Int32.
        let MinTreeDepth = addDef {
            Id = Guid("42edbdd6-a29e-4dfd-9836-050ab7fa4e31")
            Name = "Octree.MinTreeDepth"
            Description = "Octree. Min depth of this tree. A leaf node has depth 0. Int32."
            Type = Primitives.Int32.Id
            IsArray = false
            }

        /// Octree. Max depth of this tree. A leaf node has depth 0. Int32.
        let MaxTreeDepth = addDef {
            Id = Guid("d6f54b9e-e907-46c5-9106-d26cd453dc97")
            Name = "Octree.MaxTreeDepth"
            Description = "Octree. Max depth of this tree. A leaf node has depth 0. Int32."
            Type = Primitives.Int32.Id
            IsArray = false
            }

        /// Octree. Per-point positions in global space. V3d[].
        let PositionsGlobal3d = addDef {
            Id = Guid("61ef7c1e-6aeb-45cd-85ed-ad0ed2584553")
            Name = "Octree.PositionsGlobal3d"
            Description = "Octree. Per-point positions in global space. V3d[]."
            Type = Aardvark.V3dArray.Id
            IsArray = false
            }

        /// Octree. Centroid of positions (global space). V3d.
        let PositionsGlobal3dCentroid = addDef {
            Id = Guid("2040882e-75b8-4fef-9965-1ffef92f4fd3")
            Name = "Octree.PositionsGlobal3d.Centroid"
            Description = "Octree. Centroid of positions (global space). V3d."
            Type = Aardvark.V3d.Id
            IsArray = false
            }

        /// Octree. Average point distance to centroid (global space). Float64.
        let PositionsGlobal3dDistToCentroidAverage = addDef {
            Id = Guid("03a45262-6764-459d-9e2d-73bf6338d3a6")
            Name = "Octree.PositionsGlobal3d.DistToCentroid.Average"
            Description = "Octree. Average point distance to centroid (global space). Float64."
            Type = Primitives.Float64.Id
            IsArray = false
            }

        /// Octree. Standard deviation of average point distance to centroid (global space). Float64.
        let PositionsGlobal3dDistToCentroidStdDev = addDef {
            Id = Guid("61d5ad06-37d3-4df2-a999-6599efc2ae83")
            Name = "Octree.PositionsGlobal3d.DistToCentroid.StdDev"
            Description = "Octree. Standard deviation of average point distance to centroid (global space). Float64."
            Type = Primitives.Float64.Id
            IsArray = false
            }

        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[].
        let PositionsLocal3f = addDef {
            Id = Guid("05eb38fa-1b6a-4576-820b-780163199db9")
            Name = "Octree.PositionsLocal3f"
            Description = "Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[]."
            Type = Aardvark.V3fArray.Id
            IsArray = false
            }

        /// Octree. Centroid of positions (local space). V3f.
        let PositionsLocal3fCentroid = addDef {
            Id = Guid("bd6cc4ab-6a41-49b3-aca2-ca4f21510609")
            Name = "Octree.PositionsLocal3f.Centroid"
            Description = "Octree. Centroid of positions (local space). V3f."
            Type = Aardvark.V3f.Id
            IsArray = false
            }

        /// Octree. Average point distance to centroid (local space). Float32.
        let PositionsLocal3fDistToCentroidAverage = addDef {
            Id = Guid("1b7e74c5-b2ba-46fd-a7db-c08734da3b75")
            Name = "Octree.PositionsLocal3f.DistToCentroid.Average"
            Description = "Octree. Average point distance to centroid (local space). Float32."
            Type = Primitives.Float32.Id
            IsArray = false
            }

        /// Octree. Standard deviation of average point distance to centroid (local space). Float32.
        let PositionsLocal3fDistToCentroidStdDev = addDef {
            Id = Guid("c927d42b-02d8-480e-be93-0660eefd62a5")
            Name = "Octree.PositionsLocal3f.DistToCentroid.StdDev"
            Description = "Octree. Standard deviation of average point distance to centroid (local space). Float32."
            Type = Primitives.Float32.Id
            IsArray = false
            }

        /// Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.
        let PositionsLocal3fReference = addDef {
            Id = Guid("f3d3264d-abb4-47c5-963b-39d1a1728fa9")
            Name = "Octree.PositionsLocal3f.Reference"
            Description = "Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point normals (V3f[]).
        let Normals3f = addDef {
            Id = Guid("712d0a0c-a8d0-42d1-bfc7-77eac2e4a755")
            Name = "Octree.Normals3f"
            Description = "Octree. Per-point normals (V3f[])."
            Type = Aardvark.V3fArray.Id
            IsArray = false
            }

        /// Octree. Reference to per-point normals (Guid).
        let Normals3fReference = addDef {
            Id = Guid("0fb38f30-08fb-402f-bc10-a7c54d92fb26")
            Name = "Octree.Normals3f.Reference"
            Description = "Octree. Reference to per-point normals (Guid)."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
        let Normals3sb = addDef {
            Id = Guid("aaf4872c-0964-4351-9530-8a3e2be94a6e")
            Name = "Octree.Normals3sb"
            Description = "Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]."
            Type = Primitives.Int8Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
        let Normals3sbReference = addDef {
            Id = Guid("eb245ac4-a207-4428-87ea-2e715b9f01ef")
            Name = "Octree.Normals3sb.Reference"
            Description = "Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        let NormalsOct16 = addDef {
            Id = Guid("144770e4-70ea-4dd2-91a5-91f48672e87e")
            Name = "Octree.Normals.Oct16"
            Description = "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf."
            Type = Primitives.Int16Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        let NormalsOct16Reference = addDef {
            Id = Guid("7a397f13-b0dd-4925-89a2-066ef5426be3")
            Name = "Octree.Normals.Oct16.Reference"
            Description = "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        let NormalsOct16P = addDef {
            Id = Guid("5fdf162c-bd21-4688-aa5c-91dd0a550c44")
            Name = "Octree.Normals.Oct16P"
            Description = "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf."
            Type = Primitives.Int16Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        let NormalsOct16PReference = addDef {
            Id = Guid("eec0ba91-bdcf-469a-b2f2-9c46009b04e6")
            Name = "Octree.Normals.Oct16P.Reference"
            Description = "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point colors. C3b[].
        let Colors3b = addDef {
            Id = Guid("61cb1fa8-b2e2-41ae-8022-5787b44ee058")
            Name = "Octree.Colors3b"
            Description = "Octree. Per-point colors. C3b[]."
            Type = Aardvark.C3bArray.Id
            IsArray = false
            }

        /// Octree. Reference to per-point colors. Guid.
        let Colors3bReference = addDef {
            Id = Guid("b8a664d9-c77d-4ea6-a196-6d82602356a2")
            Name = "Octree.Colors3b.Reference"
            Description = "Octree. Reference to per-point colors. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point colors. C4b[].
        let Colors4b = addDef {
            Id = Guid("c91dfea3-243d-4272-9dba-b572931dba23")
            Name = "Octree.Colors4b"
            Description = "Octree. Per-point colors. C4b[]."
            Type = Aardvark.C3bArray.Id
            IsArray = false
            }

        /// Octree. Reference to per-point colors. Guid.
        let Colors4bReference = addDef {
            Id = Guid("cb2bdeae-2085-442b-90bc-990b892fdb61")
            Name = "Octree.Colors4b.Reference"
            Description = "Octree. Reference to per-point colors. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].
        let ColorsRGB565 = addDef {
            Id = Guid("bf36c54b-f199-4138-a32f-c089cf527dad")
            Name = "Octree.Colors.RGB565"
            Description = "Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]."
            Type = Primitives.UInt16Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid.
        let ColorsRGB565Reference = addDef {
            Id = Guid("9557c438-16b0-49c7-979d-8de5dc8829b4")
            Name = "Octree.Colors.RGB565.Reference"
            Description = "Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point intensities. Int32[].
        let Intensities1i = addDef {
            Id = Guid("361027fd-ac58-4de8-89ee-98695f8c5520")
            Name = "Octree.Intensities1i"
            Description = "Octree. Per-point intensities. Int32[]."
            Type = Primitives.Int32Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point intensities. Guid.
        let Intensities1iReference = addDef {
            Id = Guid("4e6842a2-3c3a-4b4e-a773-06ba138ad86e")
            Name = "Octree.Intensities1i.Reference"
            Description = "Octree. Reference to per-point intensities. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point classifications. uint8[].
        let Classifications1b = addDef {
            Id = Guid("bf0975e4-43bd-4742-9e61-c7469d81805d")
            Name = "Octree.Classifications1b"
            Description = "Octree. Per-point classifications. uint8[]."
            Type = Primitives.UInt8Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point uint8 classifications. Guid.
        let Classifications1bReference = addDef {
            Id = Guid("41d796a1-4f34-49a3-8669-1be4a0b17ac4")
            Name = "Octree.Classifications1b.Reference"
            Description = "Octree. Reference to per-point uint8 classifications. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Per-point classifications. uint16[].
        let Classifications1s = addDef {
            Id = Guid("33f675ea-09d4-4bf5-82d3-ec6be885de6d")
            Name = "Octree.Classifications1s"
            Description = "Octree. Per-point classifications. uint16[]."
            Type = Primitives.UInt16Array.Id
            IsArray = false
            }

        /// Octree. Reference to per-point uint16 classifications. Guid.
        let Classifications1sReference = addDef {
            Id = Guid("093ace41-4ec6-4e3e-9881-1f10a082df44")
            Name = "Octree.Classifications1s.Reference"
            Description = "Octree. Reference to per-point uint16 classifications. Guid."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.
        let KdTreeIndexArray = addDef {
            Id = Guid("c533bd54-9aff-40e1-a2bb-c69c9778fecb")
            Name = "Octree.KdTreeIndexArray"
            Description = "Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively."
            Type = Primitives.Int32Array.Id
            IsArray = false
            }

        /// Octree. Reference to kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.
        let KdTreeIndexArrayReference = addDef {
            Id = Guid("fc2b48cb-ab79-4579-92a3-0a421c8d9112")
            Name = "Octree.KdTreeIndexArray.Reference"
            Description = "Octree. Reference to kd-tree index array. Int32[], where pivot is stored at index n/2 recursively."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Aardvark.Geometry.PointRkdTreeFData.
        let PointRkdTreeFData = addDef {
            Id = Guid("c90f303c-f9be-49d1-9188-51fde1e1e75d")
            Name = "Octree.PointRkdTreeFData"
            Description = "Octree. Aardvark.Geometry.PointRkdTreeFData."
            Type = Aardvark.PointRkdTreeFData.Id
            IsArray = false
            }

        /// Octree. Reference to Aardvark.Geometry.PointRkdTreeFData.
        let PointRkdTreeFDataReference = addDef {
            Id = Guid("d48d006e-9840-433c-afdb-c5fc5b14be54")
            Name = "Octree.PointRkdTreeFData.Reference"
            Description = "Octree. Reference to Aardvark.Geometry.PointRkdTreeFData."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }

        /// Octree. Aardvark.Geometry.PointRkdTreeDData.
        let PointRkdTreeDData = addDef {
            Id = Guid("c00e3cc0-983c-4c51-801f-8c55ff337b2d")
            Name = "Octree.PointRkdTreeDData"
            Description = "Octree. Aardvark.Geometry.PointRkdTreeDData."
            Type = Aardvark.PointRkdTreeDData.Id
            IsArray = false
            }

        /// Octree. Reference to Aardvark.Geometry.PointRkdTreeDData.
        let PointRkdTreeDDataReference = addDef {
            Id = Guid("05cf0cac-4f8f-41bc-ac50-1b291297f892")
            Name = "Octree.PointRkdTreeDData.Reference"
            Description = "Octree. Reference to Aardvark.Geometry.PointRkdTreeDData."
            Type = Primitives.GuidDef.Id
            IsArray = false
            }