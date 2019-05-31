﻿using System;
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

        /// <summary></summary>
        public static class Primitives
        {
            /// <summary>
            /// Unit (nothing, none, null, ...).
            /// </summary>
            public static readonly Def Unit = new Def(
                Guid.Empty,
                "Unit",
                "Unit (nothing, none, null, ...).",
                None,
                false
                );

            /// <summary>
            /// A map of key/value pairs, where keys are durable IDs with values of corresponding types.
            /// </summary>
            public static readonly Def DurableMap = new Def(
                new Guid("f03716ef-6c9e-4201-bf19-e0cabc6c6a9a"),
                "DurableMap",
                "A map of key/value pairs, where keys are durable IDs with values of corresponding types.",
                None,
                false
                );

            /// <summary>
            /// Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
            /// </summary>
            public static readonly Def GuidDef = new Def(
                new Guid("a81a39b0-8f61-4efc-b0ce-27e2c5d3199d"),
                "Guid",
                "Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.",
                None,
                false
                );

            /// <summary>
            /// Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
            /// </summary>
            public static readonly Def GuidArray = new Def(
                new Guid("8b5659cd-8fea-46fd-a9f2-52c31bdaf6b3"),
                "Guid[]",
                "Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.",
                None,
                true
                );

            /// <summary>
            /// Signed 8-bit integer. 2-complement.
            /// </summary>
            public static readonly Def Int8 = new Def(
                new Guid("47a73639-7ff3-423a-9562-2561d0f51949"),
                "Int8",
                "Signed 8-bit integer. 2-complement.",
                None,
                false
                );

            /// <summary>
            /// Array of signed 8-bit integers. 2-complement.
            /// </summary>
            public static readonly Def Int8Array = new Def(
                new Guid("1e36f786-1c8d-4c1a-b5dd-6f83bfd65287"),
                "Int8[]",
                "Array of signed 8-bit integers. 2-complement.",
                None,
                true
                );

            /// <summary>
            /// Unsigned 8-bit integer.
            /// </summary>
            public static readonly Def UInt8 = new Def(
                new Guid("83c0db28-feb4-4643-af3a-269377f137b5"),
                "UInt8",
                "Unsigned 8-bit integer.",
                None,
                false
                );

            /// <summary>
            /// Array of unsigned 8-bit integers.
            /// </summary>
            public static readonly Def UInt8Array = new Def(
                new Guid("e1e6a823-d328-461d-bd01-924120b74d5c"),
                "UInt8[]",
                "Array of unsigned 8-bit integers.",
                None,
                true
                );

            /// <summary>
            /// Signed 16-bit integer. 2-complement.
            /// </summary>
            public static readonly Def Int16 = new Def(
                new Guid("4c3f7ded-2037-4f3d-baa9-3a76ef3a1fda"),
                "Int16",
                "Signed 16-bit integer. 2-complement.",
                None,
                false
                );

            /// <summary>
            /// Array of signed 16-bit integers. 2-complement.
            /// </summary>
            public static readonly Def Int16Array = new Def(
                new Guid("80b7028e-e7c8-442c-8ae3-517bb2df645f"),
                "Int16[]",
                "Array of signed 16-bit integers. 2-complement.",
                None,
                true
                );

            /// <summary>
            /// Unsigned 16-bit integer.
            /// </summary>
            public static readonly Def UInt16 = new Def(
                new Guid("8b1bc0ed-64aa-4c4c-992e-dca6b1491dd0"),
                "UInt16",
                "Unsigned 16-bit integer.",
                None,
                false
                );

            /// <summary>
            /// Array of unsigned 16-bit integers.
            /// </summary>
            public static readonly Def UInt16Array = new Def(
                new Guid("0b8a61ac-672f-4247-a8c5-2cf8f23a1eb5"),
                "UInt16[]",
                "Array of unsigned 16-bit integers.",
                None,
                true
                );

            /// <summary>
            /// Signed 32-bit integer. 2-complement.
            /// </summary>
            public static readonly Def Int32 = new Def(
                new Guid("5ce108a4-a578-4edb-841d-068393ed93bf"),
                "Int32",
                "Signed 32-bit integer. 2-complement.",
                None,
                false
                );

            /// <summary>
            /// Array of signed 32-bit integers. 2-complement.
            /// </summary>
            public static readonly Def Int32Array = new Def(
                new Guid("1cfa6f68-5b56-44a7-b4b5-bd675bc910ab"),
                "Int32[]",
                "Array of signed 32-bit integers. 2-complement.",
                None,
                true
                );

            /// <summary>
            /// Unsigned 32-bit integer.
            /// </summary>
            public static readonly Def UInt32 = new Def(
                new Guid("a77758f8-24c4-4d87-95f1-91a6eab9df01"),
                "UInt32",
                "Unsigned 32-bit integer.",
                None,
                false
                );

            /// <summary>
            /// Array of unsigned 32-bit integers.
            /// </summary>
            public static readonly Def UInt32Array = new Def(
                new Guid("4c896235-d378-4860-9b01-581138e565d3"),
                "UInt32[]",
                "Array of unsigned 32-bit integers.",
                None,
                true
                );

            /// <summary>
            /// Signed 64-bit integer. 2-complement.
            /// </summary>
            public static readonly Def Int64 = new Def(
                new Guid("f0909b36-d3c4-4b86-8320-e0ad418226e5"),
                "Int64",
                "Signed 64-bit integer. 2-complement.",
                None,
                false
                );

            /// <summary>
            /// Array of signed 64-bit integers. 2-complement.
            /// </summary>
            public static readonly Def Int64Array = new Def(
                new Guid("39761157-4817-4dbf-9eda-33fad1c0a852"),
                "Int64[]",
                "Array of signed 64-bit integers. 2-complement.",
                None,
                true
                );

            /// <summary>
            /// Unsigned 64-bit integer.
            /// </summary>
            public static readonly Def UInt64 = new Def(
                new Guid("1e29371c-e977-402e-8cd6-9d52a77ce1d6"),
                "UInt64",
                "Unsigned 64-bit integer.",
                None,
                false
                );

            /// <summary>
            /// Array of unsigned 64-bit integers.
            /// </summary>
            public static readonly Def UInt64Array = new Def(
                new Guid("56a89a90-5dde-441c-8d80-d2dca7f6717e"),
                "UInt64[]",
                "Array of unsigned 64-bit integers.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float16 = new Def(
                new Guid("7891b070-5249-479f-81b8-d8bca5127211"),
                "Float16",
                "Floating point value (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float16Array = new Def(
                new Guid("fb1d889e-b7bb-41f4-b047-1f6838cd5fdd"),
                "Float16[]",
                "Array of floating point values (half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float32 = new Def(
                new Guid("23fb286f-663b-4c71-9923-7e51c500f4ed"),
                "Float32",
                "Floating point value (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float32Array = new Def(
                new Guid("a687a789-1b63-49e9-a2e4-8099aa7879e9"),
                "Float32[]",
                "Array of floating point values (single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float64 = new Def(
                new Guid("c58c9b83-c2de-4153-a588-39c808aed50b"),
                "Float64",
                "Floating point value (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float64Array = new Def(
                new Guid("ba60cc30-2d56-45d8-a051-6b895b51bb3f"),
                "Float64[]",
                "Array of floating point values (double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float128 = new Def(
                new Guid("5d343235-21f6-41e4-992e-93541db26502"),
                "Float128",
                "Floating point value (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float128Array = new Def(
                new Guid("6477a574-ffb0-4717-9f00-5fb9aff409ce"),
                "Float128[]",
                "Array of floating point values (quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8).
            /// </summary>
            public static readonly Def StringUTF8 = new Def(
                new Guid("917c15af-0e2d-4265-a732-7b2f147f4b94"),
                "StringUTF8",
                "String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8).",
                None,
                false
                );

            /// <summary>
            /// Array of strings. UTF8 encoding.
            /// </summary>
            public static readonly Def StringUTF8Array = new Def(
                new Guid("852888ff-4168-4f4b-a10a-b582d1735c74"),
                "StringUTF8[]",
                "Array of strings. UTF8 encoding.",
                None,
                true
                );

        }
        /// <summary></summary>
        public static class Aardvark
        {
            /// <summary>
            /// A 2-dim vector of 32-bit integers.
            /// </summary>
            public static readonly Def V2i = new Def(
                new Guid("1193e05b-4c04-409b-b47b-f9f4fbce7fb2"),
                "V2i",
                "A 2-dim vector of 32-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V2i.
            /// </summary>
            public static readonly Def V2iArray = new Def(
                new Guid("a684e893-16fa-42e5-a534-843dbec575e8"),
                "V2i[]",
                "Array of V2i.",
                None,
                true
                );

            /// <summary>
            /// A 2-dim vector of 64-bit integers.
            /// </summary>
            public static readonly Def V2l = new Def(
                new Guid("5573a69d-4df9-4d91-8e3e-aa8204d8ec13"),
                "V2l",
                "A 2-dim vector of 64-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V2l.
            /// </summary>
            public static readonly Def V2lArray = new Def(
                new Guid("f5045bd5-08d1-4084-b717-932b55dcdc5b"),
                "V2l[]",
                "Array of V2l.",
                None,
                true
                );

            /// <summary>
            /// A 2-dim vector of 32-bit floats.
            /// </summary>
            public static readonly Def V2f = new Def(
                new Guid("4f5d8782-3c9a-4913-bf0f-423269a24b1e"),
                "V2f",
                "A 2-dim vector of 32-bit floats.",
                None,
                false
                );

            /// <summary>
            /// Array of V2f.
            /// </summary>
            public static readonly Def V2fArray = new Def(
                new Guid("40d91f9d-ccb3-44fb-83d0-c3ff20189b2d"),
                "V2f[]",
                "Array of V2f.",
                None,
                true
                );

            /// <summary>
            /// A 2-dim vector of 64-bit floats.
            /// </summary>
            public static readonly Def V2d = new Def(
                new Guid("0f70ed18-574f-4431-a2c1-9987e4a7653c"),
                "V2d",
                "A 2-dim vector of 64-bit floats.",
                None,
                false
                );

            /// <summary>
            /// Array of V2d.
            /// </summary>
            public static readonly Def V2dArray = new Def(
                new Guid("17037869-687f-45f1-bd43-09a46a669547"),
                "V2d[]",
                "Array of V2d.",
                None,
                true
                );

            /// <summary>
            /// A 3-dim vector of 32-bit integers.
            /// </summary>
            public static readonly Def V3i = new Def(
                new Guid("876c952e-1749-4d2f-922f-75d1acd2d870"),
                "V3i",
                "A 3-dim vector of 32-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V3i.
            /// </summary>
            public static readonly Def V3iArray = new Def(
                new Guid("e9b3bee6-d6c4-46cb-9b74-be54530d03cd"),
                "V3i[]",
                "Array of V3i.",
                None,
                true
                );

            /// <summary>
            /// A 3-dim vector of 64-bit integers.
            /// </summary>
            public static readonly Def V3l = new Def(
                new Guid("baff1328-3149-4812-901b-23d9b3ba3a29"),
                "V3l",
                "A 3-dim vector of 64-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V3l.
            /// </summary>
            public static readonly Def V3lArray = new Def(
                new Guid("229703bb-c382-4b5a-b333-30a029e77f83"),
                "V3l[]",
                "Array of V3l.",
                None,
                true
                );

            /// <summary>
            /// A 3-dim vector of 32-bit floats.
            /// </summary>
            public static readonly Def V3f = new Def(
                new Guid("ad8adcb6-8cf1-474e-99da-851343858935"),
                "V3f",
                "A 3-dim vector of 32-bit floats.",
                None,
                false
                );

            /// <summary>
            /// Array of V3f.
            /// </summary>
            public static readonly Def V3fArray = new Def(
                new Guid("f14f7607-3ddd-4e52-9ff3-c877c2242021"),
                "V3f[]",
                "Array of V3f.",
                None,
                true
                );

            /// <summary>
            /// A 3-dim vector of 64-bit floats.
            /// </summary>
            public static readonly Def V3d = new Def(
                new Guid("7a0be234-ab45-464d-b706-87157aba4361"),
                "V3d",
                "A 3-dim vector of 64-bit floats.",
                None,
                false
                );

            /// <summary>
            /// Array of V3d.
            /// </summary>
            public static readonly Def V3dArray = new Def(
                new Guid("2cce99b6-e823-4b34-8615-f7ab88746554"),
                "V3d[]",
                "Array of V3d.",
                None,
                true
                );

            /// <summary>
            /// A 4-dim vector of 32-bit integers.
            /// </summary>
            public static readonly Def V4i = new Def(
                new Guid("244a0ae8-c234-4024-821b-d5b3ad28701d"),
                "V4i",
                "A 4-dim vector of 32-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V4i.
            /// </summary>
            public static readonly Def V4iArray = new Def(
                new Guid("4e5839a1-0b5b-4407-a55c-cfa5fecf757c"),
                "V4i[]",
                "Array of V4i.",
                None,
                true
                );

            /// <summary>
            /// A 4-dim vector of 64-bit integers.
            /// </summary>
            public static readonly Def V4l = new Def(
                new Guid("04f77262-b56a-44d5-af79-c1679493acff"),
                "V4l",
                "A 4-dim vector of 64-bit integers.",
                None,
                false
                );

            /// <summary>
            /// Array of V4l.
            /// </summary>
            public static readonly Def V4lArray = new Def(
                new Guid("8aecdd7e-acf1-41aa-9d02-f954b43d6c62"),
                "V4l[]",
                "Array of V4l.",
                None,
                true
                );

            /// <summary>
            /// A 4-dim vector of 32-bit float.
            /// </summary>
            public static readonly Def V4f = new Def(
                new Guid("969daa40-9ea2-4bce-8189-f416d65a9c3e"),
                "V4f",
                "A 4-dim vector of 32-bit float.",
                None,
                false
                );

            /// <summary>
            /// Array of V4f.
            /// </summary>
            public static readonly Def V4fArray = new Def(
                new Guid("be5a8fda-4a6a-46e8-9654-356721d03f17"),
                "V4f[]",
                "Array of V4f.",
                None,
                true
                );

            /// <summary>
            /// A 4-dim vector of 64-bit float.
            /// </summary>
            public static readonly Def V4d = new Def(
                new Guid("b2dd492b-aaf8-4dfa-bcc2-833af6cbd637"),
                "V4d",
                "A 4-dim vector of 64-bit float.",
                None,
                false
                );

            /// <summary>
            /// Array of V4d.
            /// </summary>
            public static readonly Def V4dArray = new Def(
                new Guid("800184a5-c207-4b4a-88a0-60d9281ecdc1"),
                "V4d[]",
                "Array of V4d.",
                None,
                true
                );

            /// <summary>
            /// A 2x2 matrix of Float32.
            /// </summary>
            public static readonly Def M22f = new Def(
                new Guid("4f01ceee-2595-4c2d-859d-4f14df35a048"),
                "M22f",
                "A 2x2 matrix of Float32.",
                None,
                false
                );

            /// <summary>
            /// Array of M22f.
            /// </summary>
            public static readonly Def M22fArray = new Def(
                new Guid("480269a5-304c-401a-848b-64e2392ddd3e"),
                "M22f[]",
                "Array of M22f.",
                None,
                true
                );

            /// <summary>
            /// A 2x2 matrix of Float64.
            /// </summary>
            public static readonly Def M22d = new Def(
                new Guid("f16842ef-531c-4782-92f2-385bf5fd42ab"),
                "M22d",
                "A 2x2 matrix of Float64.",
                None,
                false
                );

            /// <summary>
            /// Array of M22d.
            /// </summary>
            public static readonly Def M22dArray = new Def(
                new Guid("2e6b3a90-3e45-4e5c-8770-e351640f5d47"),
                "M22d[]",
                "Array of M22d.",
                None,
                true
                );

            /// <summary>
            /// A 3x3 matrix of Float32.
            /// </summary>
            public static readonly Def M33f = new Def(
                new Guid("587a4a05-db34-4b2a-aa67-5cfe5c4d82cc"),
                "M33f",
                "A 3x3 matrix of Float32.",
                None,
                false
                );

            /// <summary>
            /// Array of M33f.
            /// </summary>
            public static readonly Def M33fArray = new Def(
                new Guid("95be083c-6f03-4279-872f-624f044599c6"),
                "M33f[]",
                "Array of M33f.",
                None,
                true
                );

            /// <summary>
            /// A 3x3 matrix of Float64.
            /// </summary>
            public static readonly Def M33d = new Def(
                new Guid("ccd797e3-0ca4-4191-840d-53751e021972"),
                "M33d",
                "A 3x3 matrix of Float64.",
                None,
                false
                );

            /// <summary>
            /// Array of M33d.
            /// </summary>
            public static readonly Def M33dArray = new Def(
                new Guid("378429d5-2517-46bb-b90d-b7bc34a86466"),
                "M33d[]",
                "Array of M33d.",
                None,
                true
                );

            /// <summary>
            /// A 4x4 matrix of Float32.
            /// </summary>
            public static readonly Def M44f = new Def(
                new Guid("652a5421-e262-4da8-ae38-78df761a365e"),
                "M44f",
                "A 4x4 matrix of Float32.",
                None,
                false
                );

            /// <summary>
            /// Array of M44f.
            /// </summary>
            public static readonly Def M44fArray = new Def(
                new Guid("cf53c4e6-f3ca-4be5-a449-0434ae455b85"),
                "M44f[]",
                "Array of M44f.",
                None,
                true
                );

            /// <summary>
            /// A 4x4 matrix of Float64.
            /// </summary>
            public static readonly Def M44d = new Def(
                new Guid("b9498622-db77-4d4c-b78c-62522ccf9252"),
                "M44d",
                "A 4x4 matrix of Float64.",
                None,
                false
                );

            /// <summary>
            /// Array of M44d.
            /// </summary>
            public static readonly Def M44dArray = new Def(
                new Guid("f07abd79-60a6-429a-94f1-11eb6c319db2"),
                "M44d[]",
                "Array of M44d.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of UInt8.
            /// </summary>
            public static readonly Def Range1b = new Def(
                new Guid("db31d0e0-2c56-48da-a769-5a2c1abad38c"),
                "Range1b",
                "An 1-dim range with limits [Min, Max] of UInt8.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1b.
            /// </summary>
            public static readonly Def Range1bArray = new Def(
                new Guid("b720aba1-6b8a-4431-8bea-8b2dad497358"),
                "Range1b[]",
                "Array of Range1b.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Int8.
            /// </summary>
            public static readonly Def Range1sb = new Def(
                new Guid("59e5322f-1677-47e4-b991-3e87e43ac005"),
                "Range1sb",
                "An 1-dim range with limits [Min, Max] of Int8.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1sb.
            /// </summary>
            public static readonly Def Range1sbArray = new Def(
                new Guid("6b769f32-b462-4a8a-a9eb-d908f650dae2"),
                "Range1sb[]",
                "Array of Range1sb.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Int16.
            /// </summary>
            public static readonly Def Range1s = new Def(
                new Guid("ed0450e7-2a14-4fe6-b3ac-f4a8ee314fad"),
                "Range1s",
                "An 1-dim range with limits [Min, Max] of Int16.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1s.
            /// </summary>
            public static readonly Def Range1sArray = new Def(
                new Guid("f8ce22da-f877-45a9-9f69-129473d22809"),
                "Range1s[]",
                "Array of Range1s.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of UInt16.
            /// </summary>
            public static readonly Def Range1us = new Def(
                new Guid("7809e939-1d9b-4033-9b7e-7459a2e53b73"),
                "Range1us",
                "An 1-dim range with limits [Min, Max] of UInt16.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1us.
            /// </summary>
            public static readonly Def Range1usArray = new Def(
                new Guid("9aad8600-41f0-4f83-a431-de44c4031e9a"),
                "Range1us[]",
                "Array of Range1us.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Int32.
            /// </summary>
            public static readonly Def Range1i = new Def(
                new Guid("06fad1c2-33a1-4962-92af-19a7c84560a9"),
                "Range1i",
                "An 1-dim range with limits [Min, Max] of Int32.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1i.
            /// </summary>
            public static readonly Def Range1iArray = new Def(
                new Guid("4c1a43f6-23ba-49aa-9cbf-b308d8524850"),
                "Range1i[]",
                "Array of Range1i.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of UInt32.
            /// </summary>
            public static readonly Def Range1ui = new Def(
                new Guid("7ff2c8c9-9c4d-4fb2-a750-f07338ebe0b5"),
                "Range1ui",
                "An 1-dim range with limits [Min, Max] of UInt32.",
                None,
                false
                );

            /// <summary>
            /// Array of Range1ui.
            /// </summary>
            public static readonly Def Range1uiArray = new Def(
                new Guid("a9688eae-5f72-48e3-be92-c214dcf106ca"),
                "Range1ui[]",
                "Array of Range1ui.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Int64
            /// </summary>
            public static readonly Def Range1l = new Def(
                new Guid("03ac4568-a97b-4ca6-b005-587cd9afde75"),
                "Range1l",
                "An 1-dim range with limits [Min, Max] of Int64",
                None,
                false
                );

            /// <summary>
            /// Array of Range1l.
            /// </summary>
            public static readonly Def Range1lArray = new Def(
                new Guid("85f5ed95-0b6a-4ea9-a4f1-38055a93781f"),
                "Range1l[]",
                "Array of Range1l.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of UInt64
            /// </summary>
            public static readonly Def Range1ul = new Def(
                new Guid("b7e36341-3dbb-47a0-b5c7-2d199f8d909b"),
                "Range1ul",
                "An 1-dim range with limits [Min, Max] of UInt64",
                None,
                false
                );

            /// <summary>
            /// Array of Range1ul.
            /// </summary>
            public static readonly Def Range1ulArray = new Def(
                new Guid("7b733797-ee9a-4432-bfd4-4d428e3710d9"),
                "Range1ul[]",
                "Array of Range1ul.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Float32
            /// </summary>
            public static readonly Def Range1f = new Def(
                new Guid("f5b3c83b-a294-4f40-90aa-4abd7c627e95"),
                "Range1f",
                "An 1-dim range with limits [Min, Max] of Float32",
                None,
                false
                );

            /// <summary>
            /// Array of Range1f.
            /// </summary>
            public static readonly Def Range1fArray = new Def(
                new Guid("7406e3c8-c5b0-465f-afa9-9104b2387462"),
                "Range1f[]",
                "Array of Range1f.",
                None,
                true
                );

            /// <summary>
            /// An 1-dim range with limits [Min, Max] of Float64
            /// </summary>
            public static readonly Def Range1d = new Def(
                new Guid("b82bff3c-d075-4d16-85d5-0be5b31a9465"),
                "Range1d",
                "An 1-dim range with limits [Min, Max] of Float64",
                None,
                false
                );

            /// <summary>
            /// Array of Range1d.
            /// </summary>
            public static readonly Def Range1dArray = new Def(
                new Guid("91420eb9-069d-47da-9911-058e12c6ad43"),
                "Range1d[]",
                "Array of Range1d.",
                None,
                true
                );

            /// <summary>
            /// An 2-dim axis-aligned box with limits [Min, Max] of V2i.
            /// </summary>
            public static readonly Def Box2i = new Def(
                new Guid("0edba5a6-1cec-401f-8d98-78bb4b3319e5"),
                "Box2i",
                "An 2-dim axis-aligned box with limits [Min, Max] of V2i.",
                None,
                false
                );

            /// <summary>
            /// Array of Box2i.
            /// </summary>
            public static readonly Def Box2iArray = new Def(
                new Guid("16afc42c-d4fd-4988-bd38-a039608ce612"),
                "Box2i[]",
                "Array of Box2i.",
                None,
                true
                );

            /// <summary>
            /// An 2-dim axis-aligned box with limits [Min, Max] of V2l.
            /// </summary>
            public static readonly Def Box2l = new Def(
                new Guid("380422d0-0428-47a6-aeb3-3ab328e21bef"),
                "Box2l",
                "An 2-dim axis-aligned box with limits [Min, Max] of V2l.",
                None,
                false
                );

            /// <summary>
            /// Array of Box2l.
            /// </summary>
            public static readonly Def Box2lArray = new Def(
                new Guid("062de2a5-1bdc-4b62-8a58-50d2f002676f"),
                "Box2l[]",
                "Array of Box2l.",
                None,
                true
                );

            /// <summary>
            /// An 2-dim axis-aligned box with limits [Min, Max] of V2f.
            /// </summary>
            public static readonly Def Box2f = new Def(
                new Guid("414d504d-f350-439b-a73a-4fcc38aafa89"),
                "Box2f",
                "An 2-dim axis-aligned box with limits [Min, Max] of V2f.",
                None,
                false
                );

            /// <summary>
            /// Array of Box2f.
            /// </summary>
            public static readonly Def Box2fArray = new Def(
                new Guid("8a515f76-89cb-45e5-9c3e-81afecab0dad"),
                "Box2f[]",
                "Array of Box2f.",
                None,
                true
                );

            /// <summary>
            /// An 2-dim axis-aligned box with limits [Min, Max] of V2d.
            /// </summary>
            public static readonly Def Box2d = new Def(
                new Guid("2fb054de-db29-4c1c-bc97-5a0cce4bc291"),
                "Box2d",
                "An 2-dim axis-aligned box with limits [Min, Max] of V2d.",
                None,
                false
                );

            /// <summary>
            /// Array of Box2d.
            /// </summary>
            public static readonly Def Box2dArray = new Def(
                new Guid("e2f9e9a9-c78f-436c-89e3-da69efefb9ec"),
                "Box2d[]",
                "Array of Box2d.",
                None,
                true
                );

            /// <summary>
            /// An 3-dim axis-aligned box with limits [Min, Max] of V3i.
            /// </summary>
            public static readonly Def Box3i = new Def(
                new Guid("c1301768-a349-489d-907e-a8967166cd7c"),
                "Box3i",
                "An 3-dim axis-aligned box with limits [Min, Max] of V3i.",
                None,
                false
                );

            /// <summary>
            /// Array of Box3i.
            /// </summary>
            public static readonly Def Box3iArray = new Def(
                new Guid("c5bfc96a-fcf7-47d9-9c29-b403734403c4"),
                "Box3i[]",
                "Array of Box3i.",
                None,
                true
                );

            /// <summary>
            /// An 3-dim axis-aligned box with limits [Min, Max] of V3l.
            /// </summary>
            public static readonly Def Box3l = new Def(
                new Guid("b22529e1-926a-4312-bb5c-3bc63700e4ac"),
                "Box3l",
                "An 3-dim axis-aligned box with limits [Min, Max] of V3l.",
                None,
                false
                );

            /// <summary>
            /// Array of Box3l.
            /// </summary>
            public static readonly Def Box3lArray = new Def(
                new Guid("0d57063d-cf07-4830-b678-ce7ad7b0e6a1"),
                "Box3l[]",
                "Array of Box3l.",
                None,
                true
                );

            /// <summary>
            /// An 3-dim axis-aligned box with limits [Min, Max] of V3f.
            /// </summary>
            public static readonly Def Box3f = new Def(
                new Guid("416721ca-6df1-4ada-b7ad-1da7256f490d"),
                "Box3f",
                "An 3-dim axis-aligned box with limits [Min, Max] of V3f.",
                None,
                false
                );

            /// <summary>
            /// Array of Box3f.
            /// </summary>
            public static readonly Def Box3fArray = new Def(
                new Guid("82b363ce-f626-4ac3-bd69-038874f4b661"),
                "Box3f[]",
                "Array of Box3f.",
                None,
                true
                );

            /// <summary>
            /// An 3-dim axis-aligned box with limits [Min, Max] of V3d.
            /// </summary>
            public static readonly Def Box3d = new Def(
                new Guid("5926f1ce-37fb-4022-a6e5-536b22ad79ea"),
                "Box3d",
                "An 3-dim axis-aligned box with limits [Min, Max] of V3d.",
                None,
                false
                );

            /// <summary>
            /// Array of Box3d.
            /// </summary>
            public static readonly Def Box3dArray = new Def(
                new Guid("59e43b45-5221-4127-8da9-0b28c07a5b22"),
                "Box3d[]",
                "Array of Box3d.",
                None,
                true
                );

            /// <summary>
            /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent.
            /// </summary>
            public static readonly Def Cell = new Def(
                new Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6"),
                "Cell",
                "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent.",
                None,
                false
                );

            /// <summary>
            /// Array of Cell.
            /// </summary>
            public static readonly Def CellArray = new Def(
                new Guid("2732639f-20b2-46dc-8d54-007a2ef2d2ea"),
                "Cell[]",
                "Array of Cell.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGR of UInt8.
            /// </summary>
            public static readonly Def C3b = new Def(
                new Guid("73656667-ea6a-468f-962c-64cd4e24f409"),
                "C3b",
                "A color with channels BGR of UInt8.",
                None,
                false
                );

            /// <summary>
            /// Array of C3b.
            /// </summary>
            public static readonly Def C3bArray = new Def(
                new Guid("41dde1c8-2b63-4a18-90c8-8f0c67c685b7"),
                "C3b[]",
                "Array of C3b.",
                None,
                true
                );

            /// <summary>
            /// A color with channels RGB of UInt16.
            /// </summary>
            public static readonly Def C3us = new Def(
                new Guid("e606b2fc-5a95-420e-8ddc-66a65e7e31f0"),
                "C3us",
                "A color with channels RGB of UInt16.",
                None,
                false
                );

            /// <summary>
            /// Array of C3us.
            /// </summary>
            public static readonly Def C3usArray = new Def(
                new Guid("4225db2b-dcb6-458b-bc14-ba5d9e7ab557"),
                "C3us[]",
                "Array of C3us.",
                None,
                true
                );

            /// <summary>
            /// A color with channels RGB of UInt32.
            /// </summary>
            public static readonly Def C3ui = new Def(
                new Guid("0314f5c3-28b8-4523-8a2f-1b75d01d055b"),
                "C3ui",
                "A color with channels RGB of UInt32.",
                None,
                false
                );

            /// <summary>
            /// Array of C3ui.
            /// </summary>
            public static readonly Def C3uiArray = new Def(
                new Guid("394e46b5-ac4d-44ca-8c48-88b12a974d7c"),
                "C3ui[]",
                "Array of C3ui.",
                None,
                true
                );

            /// <summary>
            /// A color with channels RGB of Float32.
            /// </summary>
            public static readonly Def C3f = new Def(
                new Guid("76873abb-5cd1-46a9-90f6-92db62731bcf"),
                "C3f",
                "A color with channels RGB of Float32.",
                None,
                false
                );

            /// <summary>
            /// Array of C3f.
            /// </summary>
            public static readonly Def C3fArray = new Def(
                new Guid("e0ffeee7-96a5-4705-9589-2f1bac139068"),
                "C3f[]",
                "Array of C3f.",
                None,
                true
                );

            /// <summary>
            /// A color with channels RGB of Float64.
            /// </summary>
            public static readonly Def C3d = new Def(
                new Guid("b87f7505-d164-45e1-bcde-72304e924abe"),
                "C3d",
                "A color with channels RGB of Float64.",
                None,
                false
                );

            /// <summary>
            /// Array of C3d.
            /// </summary>
            public static readonly Def C3dArray = new Def(
                new Guid("47eab504-ace3-47d4-8ad7-e5ed8599c01a"),
                "C3d[]",
                "Array of C3d.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGRA of UInt8.
            /// </summary>
            public static readonly Def C4b = new Def(
                new Guid("3f34792b-e03d-4d21-a4a6-4890d5f3f67f"),
                "C4b",
                "A color with channels BGRA of UInt8.",
                None,
                false
                );

            /// <summary>
            /// Array of C4b.
            /// </summary>
            public static readonly Def C4bArray = new Def(
                new Guid("06318db7-1518-43eb-97c4-ba13c83fc64b"),
                "C4b[]",
                "Array of C4b.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGRA of UInt16.
            /// </summary>
            public static readonly Def C4us = new Def(
                new Guid("85917bcd-00e9-4402-abc7-38c973c96ecc"),
                "C4us",
                "A color with channels BGRA of UInt16.",
                None,
                false
                );

            /// <summary>
            /// Array of C4us.
            /// </summary>
            public static readonly Def C4usArray = new Def(
                new Guid("f3944349-d463-486b-be25-8bc1764f1323"),
                "C4us[]",
                "Array of C4us.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGRA of UInt32.
            /// </summary>
            public static readonly Def C4ui = new Def(
                new Guid("7018167d-2316-4ff0-a239-0ebf95c32adf"),
                "C4ui",
                "A color with channels BGRA of UInt32.",
                None,
                false
                );

            /// <summary>
            /// Array of C4ui.
            /// </summary>
            public static readonly Def C4uiArray = new Def(
                new Guid("1afb8c51-a29a-4919-8c32-a62ade22a857"),
                "C4ui[]",
                "Array of C4ui.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGRA of Float32.
            /// </summary>
            public static readonly Def C4f = new Def(
                new Guid("e09cfff1-b186-42e8-9b3d-6a4325117ba4"),
                "C4f",
                "A color with channels BGRA of Float32.",
                None,
                false
                );

            /// <summary>
            /// Array of C4f.
            /// </summary>
            public static readonly Def C4fArray = new Def(
                new Guid("4b9b675b-a575-47f0-9b7e-0f49b9904dc5"),
                "C4f[]",
                "Array of C4f.",
                None,
                true
                );

            /// <summary>
            /// A color with channels BGRA of Float64.
            /// </summary>
            public static readonly Def C4d = new Def(
                new Guid("fe74ea05-4b9a-4723-a075-eec853c9cc19"),
                "C4d",
                "A color with channels BGRA of Float64.",
                None,
                false
                );

            /// <summary>
            /// Array of C4d.
            /// </summary>
            public static readonly Def C4dArray = new Def(
                new Guid("75e7b36b-fb9e-4b9e-b92c-728c69b6feae"),
                "C4d[]",
                "Array of C4d.",
                None,
                true
                );

            /// <summary>
            /// Aardvark.Geometry.PointRkdTreeFData.
            /// </summary>
            public static readonly Def PointRkdTreeFData = new Def(
                new Guid("023ebe21-d8c2-4ccd-9c6d-326ead4a0ee9"),
                "PointRkdTreeFData",
                "Aardvark.Geometry.PointRkdTreeFData.",
                None,
                false
                );

            /// <summary>
            /// Aardvark.Geometry.PointRkdTreeDData.
            /// </summary>
            public static readonly Def PointRkdTreeDData = new Def(
                new Guid("e6445682-46e8-4598-9757-22757e6110ca"),
                "PointRkdTreeDData",
                "Aardvark.Geometry.PointRkdTreeDData.",
                None,
                false
                );

        }
        /// <summary></summary>
        public static class Octree
        {
            /// <summary>
            /// Octree. An octree node. DurableMap.
            /// </summary>
            public static readonly Def Node = new Def(
                new Guid("e0883944-1d81-4ff5-845f-0b96075880b7"),
                "Octree.Node",
                "Octree. An octree node. DurableMap.",
                Primitives.DurableMap.Id,
                false
                );

            /// <summary>
            /// Octree. An octree node followed by all other nodes in depth first order. DurableMap.
            /// </summary>
            public static readonly Def Tree = new Def(
                new Guid("708b4b8e-286b-4658-82ac-dd8ea98d1b1c"),
                "Octree.Tree",
                "Octree. An octree node followed by all other nodes in depth first order. DurableMap.",
                Primitives.DurableMap.Id,
                false
                );

            /// <summary>
            /// Octree. Node's unique id.
            /// </summary>
            public static readonly Def NodeId = new Def(
                new Guid("1100ffd5-7789-4872-9ef2-67d45be0c489"),
                "Octree.NodeId",
                "Octree. Node's unique id.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Subnodes as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes.
            /// </summary>
            public static readonly Def SubnodesGuids = new Def(
                new Guid("eb44f9b0-3247-4426-b458-1b6e9880d466"),
                "Octree.Subnodes.Guids",
                "Octree. Subnodes as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes.",
                Primitives.GuidArray.Id,
                false
                );

            /// <summary>
            /// Octree. Exact bounding box of this node's positions. Global space. Box3d.
            /// </summary>
            public static readonly Def BoundingBoxExactGlobal = new Def(
                new Guid("7912c862-74b4-4f44-a8cd-d11ea1da9304"),
                "Octree.BoundingBoxExactGlobal",
                "Octree. Exact bounding box of this node's positions. Global space. Box3d.",
                Aardvark.Box3d.Id,
                false
                );

            /// <summary>
            /// Octree. Exact bounding box of this node's positions. Local space. Box3f.
            /// </summary>
            public static readonly Def BoundingBoxExactLocal = new Def(
                new Guid("aadbb622-1cf6-42e0-86df-be79d28d6757"),
                "Octree.BoundingBoxExactLocal",
                "Octree. Exact bounding box of this node's positions. Local space. Box3f.",
                Aardvark.Box3f.Id,
                false
                );

            /// <summary>
            /// Octree. Cell's index. Global space. Cell.
            /// </summary>
            public static readonly Def Cell = new Def(
                new Guid("9f8121e4-83af-40e3-aed9-5fd908a140ee"),
                "Octree.Cell",
                "Octree. Cell's index. Global space. Cell.",
                Aardvark.Cell.Id,
                false
                );

            /// <summary>
            /// Octree. Number of points in this cell. Int32.
            /// </summary>
            public static readonly Def PointCountCell = new Def(
                new Guid("172e1f20-0ffc-4d9c-9b3d-903fca41abe3"),
                "Octree.PointCountCell",
                "Octree. Number of points in this cell. Int32.",
                Primitives.Int32.Id,
                false
                );

            /// <summary>
            /// Octree. Total number of points in this tree's leaf nodes. Int64.
            /// </summary>
            public static readonly Def PointCountTreeLeafs = new Def(
                new Guid("71e80c00-06b6-4e84-a0f7-dbababd2613c"),
                "Octree.PointCountTreeLeafs",
                "Octree. Total number of points in this tree's leaf nodes. Int64.",
                Primitives.Int64.Id,
                false
                );

            /// <summary>
            /// Octree. Average distance of each point to its nearest neighbour.
            /// </summary>
            public static readonly Def AveragePointDistance = new Def(
                new Guid("39c21132-4570-4624-afae-6304851567d7"),
                "Octree.AveragePointDistance",
                "Octree. Average distance of each point to its nearest neighbour.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Standard deviation of average distances of each point to its nearest neighbour.
            /// </summary>
            public static readonly Def AveragePointDistanceStdDev = new Def(
                new Guid("94cac234-b6ea-443a-b196-c7dd8e5def0d"),
                "Octree.AveragePointDistanceStdDev",
                "Octree. Standard deviation of average distances of each point to its nearest neighbour.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Min depth of this tree. A leaf node has depth 0. Int32.
            /// </summary>
            public static readonly Def MinTreeDepth = new Def(
                new Guid("42edbdd6-a29e-4dfd-9836-050ab7fa4e31"),
                "Octree.MinTreeDepth",
                "Octree. Min depth of this tree. A leaf node has depth 0. Int32.",
                Primitives.Int32.Id,
                false
                );

            /// <summary>
            /// Octree. Max depth of this tree. A leaf node has depth 0. Int32.
            /// </summary>
            public static readonly Def MaxTreeDepth = new Def(
                new Guid("d6f54b9e-e907-46c5-9106-d26cd453dc97"),
                "Octree.MaxTreeDepth",
                "Octree. Max depth of this tree. A leaf node has depth 0. Int32.",
                Primitives.Int32.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point positions in global space. V3d[].
            /// </summary>
            public static readonly Def PositionsGlobal3d = new Def(
                new Guid("61ef7c1e-6aeb-45cd-85ed-ad0ed2584553"),
                "Octree.PositionsGlobal3d",
                "Octree. Per-point positions in global space. V3d[].",
                Aardvark.V3dArray.Id,
                false
                );

            /// <summary>
            /// Octree. Centroid of positions (global space). V3d.
            /// </summary>
            public static readonly Def PositionsGlobal3dCentroid = new Def(
                new Guid("2040882e-75b8-4fef-9965-1ffef92f4fd3"),
                "Octree.PositionsGlobal3d.Centroid",
                "Octree. Centroid of positions (global space). V3d.",
                Aardvark.V3d.Id,
                false
                );

            /// <summary>
            /// Octree. Average point distance to centroid (global space). Float64.
            /// </summary>
            public static readonly Def PositionsGlobal3dDistToCentroidAverage = new Def(
                new Guid("03a45262-6764-459d-9e2d-73bf6338d3a6"),
                "Octree.PositionsGlobal3d.DistToCentroid.Average",
                "Octree. Average point distance to centroid (global space). Float64.",
                Primitives.Float64.Id,
                false
                );

            /// <summary>
            /// Octree. Standard deviation of average point distance to centroid (global space). Float64.
            /// </summary>
            public static readonly Def PositionsGlobal3dDistToCentroidStdDev = new Def(
                new Guid("61d5ad06-37d3-4df2-a999-6599efc2ae83"),
                "Octree.PositionsGlobal3d.DistToCentroid.StdDev",
                "Octree. Standard deviation of average point distance to centroid (global space). Float64.",
                Primitives.Float64.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[].
            /// </summary>
            public static readonly Def PositionsLocal3f = new Def(
                new Guid("05eb38fa-1b6a-4576-820b-780163199db9"),
                "Octree.PositionsLocal3f",
                "Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[].",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Octree. Centroid of positions (local space). V3f.
            /// </summary>
            public static readonly Def PositionsLocal3fCentroid = new Def(
                new Guid("bd6cc4ab-6a41-49b3-aca2-ca4f21510609"),
                "Octree.PositionsLocal3f.Centroid",
                "Octree. Centroid of positions (local space). V3f.",
                Aardvark.V3f.Id,
                false
                );

            /// <summary>
            /// Octree. Average point distance to centroid (local space). Float32.
            /// </summary>
            public static readonly Def PositionsLocal3fDistToCentroidAverage = new Def(
                new Guid("1b7e74c5-b2ba-46fd-a7db-c08734da3b75"),
                "Octree.PositionsLocal3f.DistToCentroid.Average",
                "Octree. Average point distance to centroid (local space). Float32.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Standard deviation of average point distance to centroid (local space). Float32.
            /// </summary>
            public static readonly Def PositionsLocal3fDistToCentroidStdDev = new Def(
                new Guid("c927d42b-02d8-480e-be93-0660eefd62a5"),
                "Octree.PositionsLocal3f.DistToCentroid.StdDev",
                "Octree. Standard deviation of average point distance to centroid (local space). Float32.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.
            /// </summary>
            public static readonly Def PositionsLocal3fReference = new Def(
                new Guid("f3d3264d-abb4-47c5-963b-39d1a1728fa9"),
                "Octree.PositionsLocal3f.Reference",
                "Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals (V3f[]).
            /// </summary>
            public static readonly Def Normals3f = new Def(
                new Guid("712d0a0c-a8d0-42d1-bfc7-77eac2e4a755"),
                "Octree.Normals3f",
                "Octree. Per-point normals (V3f[]).",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point normals (Guid).
            /// </summary>
            public static readonly Def Normals3fReference = new Def(
                new Guid("0fb38f30-08fb-402f-bc10-a7c54d92fb26"),
                "Octree.Normals3f.Reference",
                "Octree. Reference to per-point normals (Guid).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
            /// </summary>
            public static readonly Def Normals3sb = new Def(
                new Guid("aaf4872c-0964-4351-9530-8a3e2be94a6e"),
                "Octree.Normals3sb",
                "Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].",
                Primitives.Int8Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
            /// </summary>
            public static readonly Def Normals3sbReference = new Def(
                new Guid("eb245ac4-a207-4428-87ea-2e715b9f01ef"),
                "Octree.Normals3sb.Reference",
                "Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16 = new Def(
                new Guid("144770e4-70ea-4dd2-91a5-91f48672e87e"),
                "Octree.Normals.Oct16",
                "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.Int16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16Reference = new Def(
                new Guid("7a397f13-b0dd-4925-89a2-066ef5426be3"),
                "Octree.Normals.Oct16.Reference",
                "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16P = new Def(
                new Guid("5fdf162c-bd21-4688-aa5c-91dd0a550c44"),
                "Octree.Normals.Oct16P",
                "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.Int16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16PReference = new Def(
                new Guid("eec0ba91-bdcf-469a-b2f2-9c46009b04e6"),
                "Octree.Normals.Oct16P.Reference",
                "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point colors. C3b[].
            /// </summary>
            public static readonly Def Colors3b = new Def(
                new Guid("61cb1fa8-b2e2-41ae-8022-5787b44ee058"),
                "Octree.Colors3b",
                "Octree. Per-point colors. C3b[].",
                Aardvark.C3bArray.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point colors. Guid.
            /// </summary>
            public static readonly Def Colors3bReference = new Def(
                new Guid("b8a664d9-c77d-4ea6-a196-6d82602356a2"),
                "Octree.Colors3b.Reference",
                "Octree. Reference to per-point colors. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point colors. C4b[].
            /// </summary>
            public static readonly Def Colors4b = new Def(
                new Guid("c91dfea3-243d-4272-9dba-b572931dba23"),
                "Octree.Colors4b",
                "Octree. Per-point colors. C4b[].",
                Aardvark.C3bArray.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point colors. Guid.
            /// </summary>
            public static readonly Def Colors4bReference = new Def(
                new Guid("cb2bdeae-2085-442b-90bc-990b892fdb61"),
                "Octree.Colors4b.Reference",
                "Octree. Reference to per-point colors. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].
            /// </summary>
            public static readonly Def ColorsRGB565 = new Def(
                new Guid("bf36c54b-f199-4138-a32f-c089cf527dad"),
                "Octree.Colors.RGB565",
                "Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].",
                Primitives.UInt16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid.
            /// </summary>
            public static readonly Def ColorsRGB565Reference = new Def(
                new Guid("9557c438-16b0-49c7-979d-8de5dc8829b4"),
                "Octree.Colors.RGB565.Reference",
                "Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point intensities. Int32[].
            /// </summary>
            public static readonly Def Intensities1i = new Def(
                new Guid("361027fd-ac58-4de8-89ee-98695f8c5520"),
                "Octree.Intensities1i",
                "Octree. Per-point intensities. Int32[].",
                Primitives.Int32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point intensities. Guid.
            /// </summary>
            public static readonly Def Intensities1iReference = new Def(
                new Guid("4e6842a2-3c3a-4b4e-a773-06ba138ad86e"),
                "Octree.Intensities1i.Reference",
                "Octree. Reference to per-point intensities. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point classifications. uint8[].
            /// </summary>
            public static readonly Def Classifications1b = new Def(
                new Guid("bf0975e4-43bd-4742-9e61-c7469d81805d"),
                "Octree.Classifications1b",
                "Octree. Per-point classifications. uint8[].",
                Primitives.UInt8Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point uint8 classifications. Guid.
            /// </summary>
            public static readonly Def Classifications1bReference = new Def(
                new Guid("41d796a1-4f34-49a3-8669-1be4a0b17ac4"),
                "Octree.Classifications1b.Reference",
                "Octree. Reference to per-point uint8 classifications. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point classifications. uint16[].
            /// </summary>
            public static readonly Def Classifications1s = new Def(
                new Guid("33f675ea-09d4-4bf5-82d3-ec6be885de6d"),
                "Octree.Classifications1s",
                "Octree. Per-point classifications. uint16[].",
                Primitives.UInt16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point uint16 classifications. Guid.
            /// </summary>
            public static readonly Def Classifications1sReference = new Def(
                new Guid("093ace41-4ec6-4e3e-9881-1f10a082df44"),
                "Octree.Classifications1s.Reference",
                "Octree. Reference to per-point uint16 classifications. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.
            /// </summary>
            public static readonly Def KdTreeIndexArray = new Def(
                new Guid("c533bd54-9aff-40e1-a2bb-c69c9778fecb"),
                "Octree.KdTreeIndexArray",
                "Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.",
                Primitives.Int32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.
            /// </summary>
            public static readonly Def KdTreeIndexArrayReference = new Def(
                new Guid("fc2b48cb-ab79-4579-92a3-0a421c8d9112"),
                "Octree.KdTreeIndexArray.Reference",
                "Octree. Reference to kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Aardvark.Geometry.PointRkdTreeFData.
            /// </summary>
            public static readonly Def PointRkdTreeFData = new Def(
                new Guid("c90f303c-f9be-49d1-9188-51fde1e1e75d"),
                "Octree.PointRkdTreeFData",
                "Octree. Aardvark.Geometry.PointRkdTreeFData.",
                Aardvark.PointRkdTreeFData.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to Aardvark.Geometry.PointRkdTreeFData.
            /// </summary>
            public static readonly Def PointRkdTreeFDataReference = new Def(
                new Guid("d48d006e-9840-433c-afdb-c5fc5b14be54"),
                "Octree.PointRkdTreeFData.Reference",
                "Octree. Reference to Aardvark.Geometry.PointRkdTreeFData.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Aardvark.Geometry.PointRkdTreeDData.
            /// </summary>
            public static readonly Def PointRkdTreeDData = new Def(
                new Guid("c00e3cc0-983c-4c51-801f-8c55ff337b2d"),
                "Octree.PointRkdTreeDData",
                "Octree. Aardvark.Geometry.PointRkdTreeDData.",
                Aardvark.PointRkdTreeDData.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to Aardvark.Geometry.PointRkdTreeDData.
            /// </summary>
            public static readonly Def PointRkdTreeDDataReference = new Def(
                new Guid("05cf0cac-4f8f-41bc-ac50-1b291297f892"),
                "Octree.PointRkdTreeDData.Reference",
                "Octree. Reference to Aardvark.Geometry.PointRkdTreeDData.",
                Primitives.GuidDef.Id,
                false
                );

        }
    }
}