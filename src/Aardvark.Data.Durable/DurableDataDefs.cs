/*
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
*/
using System;

namespace Aardvark.Data;

public static partial class Durable
{
    /// <summary></summary>
    public static class Primitives
    {
        private static readonly Guid None = Guid.Empty;

        /// <summary>
        /// Unit (nothing, none, null, ...).
        /// </summary>
        public static readonly Def Unit = new(
            Guid.Empty,
            "Unit",
            "Unit (nothing, none, null, ...).",
            None,
            false
            );

        /// <summary>
        /// A map of durable IDs to corresponding data, with encoding optimized for random access on read.
        /// </summary>
        public static readonly Def DurableMap2 = new(
            new Guid("1aeb96fc-6f6d-4186-b9d5-987db1afbd18"),
            "DurableMap2",
            "A map of durable IDs to corresponding data, with encoding optimized for random access on read.",
            None,
            false
            );

        /// <summary>
        /// A map of key/value pairs, where keys are durable IDs with values of corresponding types.
        /// </summary>
        public static readonly Def DurableMap = new(
            new Guid("f03716ef-6c9e-4201-bf19-e0cabc6c6a9a"),
            "DurableMap",
            "A map of key/value pairs, where keys are durable IDs with values of corresponding types.",
            None,
            false
            );

        /// <summary>
        /// A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 8-byte aligned.
        /// </summary>
        [Obsolete("Deprecated 2022-10-21.")]
        public static readonly Def DurableMapAligned8 = new(
            new Guid("6780296f-c30a-4eba-806f-d07d84c7a5bc"),
            "DurableMapAligned8",
            "A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 8-byte aligned.",
            None,
            false
            );

        /// <summary>
        /// A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 16-byte aligned.
        /// </summary>
        [Obsolete("Deprecated 2022-10-21.")]
        public static readonly Def DurableMapAligned16 = new(
            new Guid("0ca48518-96b9-424f-b146-046ac3c8ed10"),
            "DurableMapAligned16",
            "A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 16-byte aligned.",
            None,
            false
            );

        /// <summary>
        /// Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
        /// </summary>
        public static readonly Def GuidDef = new(
            new Guid("a81a39b0-8f61-4efc-b0ce-27e2c5d3199d"),
            "Guid",
            "Globally unique identifier (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.",
            None,
            false
            );

        /// <summary>
        /// Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.
        /// </summary>
        public static readonly Def GuidArray = new(
            new Guid("8b5659cd-8fea-46fd-a9f2-52c31bdaf6b3"),
            "Guid[]",
            "Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122.",
            None,
            true
            );

        /// <summary>
        /// Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122. Compressed (GZip).
        /// </summary>
        public static readonly Def GuidArrayGz = new(
            new Guid("65112063-17a4-c3dd-e73e-08c647dda6a1"),
            "Guid[].gz",
            "Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122. Compressed (LZ4).
        /// </summary>
        public static readonly Def GuidArrayLz4 = new(
            new Guid("21f5a16c-12cd-d720-d491-5d5d26465e85"),
            "Guid[].lz4",
            "Array of globally unique identifiers (GUID, 16 bytes). https://tools.ietf.org/html/rfc4122. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Signed 8-bit integer. 2-complement.
        /// </summary>
        public static readonly Def Int8 = new(
            new Guid("47a73639-7ff3-423a-9562-2561d0f51949"),
            "Int8",
            "Signed 8-bit integer. 2-complement.",
            None,
            false
            );

        /// <summary>
        /// Array of signed 8-bit integers. 2-complement.
        /// </summary>
        public static readonly Def Int8Array = new(
            new Guid("1e36f786-1c8d-4c1a-b5dd-6f83bfd65287"),
            "Int8[]",
            "Array of signed 8-bit integers. 2-complement.",
            None,
            true
            );

        /// <summary>
        /// Array of signed 8-bit integers. 2-complement. Compressed (GZip).
        /// </summary>
        public static readonly Def Int8ArrayGz = new(
            new Guid("dbf37354-f577-c991-5f5a-479f17749eb4"),
            "Int8[].gz",
            "Array of signed 8-bit integers. 2-complement. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of signed 8-bit integers. 2-complement. Compressed (LZ4).
        /// </summary>
        public static readonly Def Int8ArrayLz4 = new(
            new Guid("cee39ff6-a46e-5b99-3f1a-21664c1b76b0"),
            "Int8[].lz4",
            "Array of signed 8-bit integers. 2-complement. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Unsigned 8-bit integer.
        /// </summary>
        public static readonly Def UInt8 = new(
            new Guid("83c0db28-feb4-4643-af3a-269377f137b5"),
            "UInt8",
            "Unsigned 8-bit integer.",
            None,
            false
            );

        /// <summary>
        /// Array of unsigned 8-bit integers.
        /// </summary>
        public static readonly Def UInt8Array = new(
            new Guid("e1e6a823-d328-461d-bd01-924120b74d5c"),
            "UInt8[]",
            "Array of unsigned 8-bit integers.",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 8-bit integers. Compressed (GZip).
        /// </summary>
        public static readonly Def UInt8ArrayGz = new(
            new Guid("6260fd4d-c8a5-716d-43bc-42719a1dbb68"),
            "UInt8[].gz",
            "Array of unsigned 8-bit integers. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 8-bit integers. Compressed (LZ4).
        /// </summary>
        public static readonly Def UInt8ArrayLz4 = new(
            new Guid("dbd687aa-e59e-7e80-c153-89b097f465ba"),
            "UInt8[].lz4",
            "Array of unsigned 8-bit integers. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Signed 16-bit integer. 2-complement.
        /// </summary>
        public static readonly Def Int16 = new(
            new Guid("4c3f7ded-2037-4f3d-baa9-3a76ef3a1fda"),
            "Int16",
            "Signed 16-bit integer. 2-complement.",
            None,
            false
            );

        /// <summary>
        /// Array of signed 16-bit integers. 2-complement.
        /// </summary>
        public static readonly Def Int16Array = new(
            new Guid("80b7028e-e7c8-442c-8ae3-517bb2df645f"),
            "Int16[]",
            "Array of signed 16-bit integers. 2-complement.",
            None,
            true
            );

        /// <summary>
        /// Array of signed 16-bit integers. 2-complement. Compressed (GZip).
        /// </summary>
        public static readonly Def Int16ArrayGz = new(
            new Guid("cc23a924-f2d0-d073-b325-c09daae67f81"),
            "Int16[].gz",
            "Array of signed 16-bit integers. 2-complement. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of signed 16-bit integers. 2-complement. Compressed (LZ4).
        /// </summary>
        public static readonly Def Int16ArrayLz4 = new(
            new Guid("b1c8a452-438a-57af-0ee3-ac661d6941a1"),
            "Int16[].lz4",
            "Array of signed 16-bit integers. 2-complement. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Unsigned 16-bit integer.
        /// </summary>
        public static readonly Def UInt16 = new(
            new Guid("8b1bc0ed-64aa-4c4c-992e-dca6b1491dd0"),
            "UInt16",
            "Unsigned 16-bit integer.",
            None,
            false
            );

        /// <summary>
        /// Array of unsigned 16-bit integers.
        /// </summary>
        public static readonly Def UInt16Array = new(
            new Guid("0b8a61ac-672f-4247-a8c5-2cf8f23a1eb5"),
            "UInt16[]",
            "Array of unsigned 16-bit integers.",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 16-bit integers. Compressed (GZip).
        /// </summary>
        public static readonly Def UInt16ArrayGz = new(
            new Guid("c598e137-3c25-d230-d5f5-15c79903a506"),
            "UInt16[].gz",
            "Array of unsigned 16-bit integers. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 16-bit integers. Compressed (LZ4).
        /// </summary>
        public static readonly Def UInt16ArrayLz4 = new(
            new Guid("4562e023-d2fb-4854-5a55-9c7d9c3d0ba8"),
            "UInt16[].lz4",
            "Array of unsigned 16-bit integers. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Signed 32-bit integer. 2-complement.
        /// </summary>
        public static readonly Def Int32 = new(
            new Guid("5ce108a4-a578-4edb-841d-068393ed93bf"),
            "Int32",
            "Signed 32-bit integer. 2-complement.",
            None,
            false
            );

        /// <summary>
        /// Array of signed 32-bit integers. 2-complement.
        /// </summary>
        public static readonly Def Int32Array = new(
            new Guid("1cfa6f68-5b56-44a7-b4b5-bd675bc910ab"),
            "Int32[]",
            "Array of signed 32-bit integers. 2-complement.",
            None,
            true
            );

        /// <summary>
        /// Array of signed 32-bit integers. 2-complement. Compressed (GZip).
        /// </summary>
        public static readonly Def Int32ArrayGz = new(
            new Guid("65a62265-8e1d-ecbf-1fce-4b8b1ed23764"),
            "Int32[].gz",
            "Array of signed 32-bit integers. 2-complement. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of signed 32-bit integers. 2-complement. Compressed (LZ4).
        /// </summary>
        public static readonly Def Int32ArrayLz4 = new(
            new Guid("58d68af0-df76-f3c2-16fe-4eb8cf585946"),
            "Int32[].lz4",
            "Array of signed 32-bit integers. 2-complement. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Unsigned 32-bit integer.
        /// </summary>
        public static readonly Def UInt32 = new(
            new Guid("a77758f8-24c4-4d87-95f1-91a6eab9df01"),
            "UInt32",
            "Unsigned 32-bit integer.",
            None,
            false
            );

        /// <summary>
        /// Array of unsigned 32-bit integers.
        /// </summary>
        public static readonly Def UInt32Array = new(
            new Guid("4c896235-d378-4860-9b01-581138e565d3"),
            "UInt32[]",
            "Array of unsigned 32-bit integers.",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 32-bit integers. Compressed (GZip).
        /// </summary>
        public static readonly Def UInt32ArrayGz = new(
            new Guid("3c3a03e0-94a6-489b-fe9c-b258081db7b8"),
            "UInt32[].gz",
            "Array of unsigned 32-bit integers. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 32-bit integers. Compressed (LZ4).
        /// </summary>
        public static readonly Def UInt32ArrayLz4 = new(
            new Guid("cee153e6-f7fd-4628-17b9-ffaf9004e03a"),
            "UInt32[].lz4",
            "Array of unsigned 32-bit integers. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Signed 64-bit integer. 2-complement.
        /// </summary>
        public static readonly Def Int64 = new(
            new Guid("f0909b36-d3c4-4b86-8320-e0ad418226e5"),
            "Int64",
            "Signed 64-bit integer. 2-complement.",
            None,
            false
            );

        /// <summary>
        /// Array of signed 64-bit integers. 2-complement.
        /// </summary>
        public static readonly Def Int64Array = new(
            new Guid("39761157-4817-4dbf-9eda-33fad1c0a852"),
            "Int64[]",
            "Array of signed 64-bit integers. 2-complement.",
            None,
            true
            );

        /// <summary>
        /// Array of signed 64-bit integers. 2-complement. Compressed (GZip).
        /// </summary>
        public static readonly Def Int64ArrayGz = new(
            new Guid("b6591340-128e-3b59-e28c-2dd6d0fb9684"),
            "Int64[].gz",
            "Array of signed 64-bit integers. 2-complement. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of signed 64-bit integers. 2-complement. Compressed (LZ4).
        /// </summary>
        public static readonly Def Int64ArrayLz4 = new(
            new Guid("cc10eeb4-a6b0-ada3-1b01-3f00d0d925b4"),
            "Int64[].lz4",
            "Array of signed 64-bit integers. 2-complement. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Unsigned 64-bit integer.
        /// </summary>
        public static readonly Def UInt64 = new(
            new Guid("1e29371c-e977-402e-8cd6-9d52a77ce1d6"),
            "UInt64",
            "Unsigned 64-bit integer.",
            None,
            false
            );

        /// <summary>
        /// Array of unsigned 64-bit integers.
        /// </summary>
        public static readonly Def UInt64Array = new(
            new Guid("56a89a90-5dde-441c-8d80-d2dca7f6717e"),
            "UInt64[]",
            "Array of unsigned 64-bit integers.",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 64-bit integers. Compressed (GZip).
        /// </summary>
        public static readonly Def UInt64ArrayGz = new(
            new Guid("06a4f9eb-f175-b720-d993-4394cebeb4ce"),
            "UInt64[].gz",
            "Array of unsigned 64-bit integers. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of unsigned 64-bit integers. Compressed (LZ4).
        /// </summary>
        public static readonly Def UInt64ArrayLz4 = new(
            new Guid("69255894-6a33-d59d-5ae5-17605b1aba16"),
            "UInt64[].lz4",
            "Array of unsigned 64-bit integers. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Floating point value (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float16 = new(
            new Guid("7891b070-5249-479f-81b8-d8bca5127211"),
            "Float16",
            "Floating point value (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            false
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float16Array = new(
            new Guid("fb1d889e-b7bb-41f4-b047-1f6838cd5fdd"),
            "Float16[]",
            "Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).
        /// </summary>
        public static readonly Def Float16ArrayGz = new(
            new Guid("6e491d9b-51fc-15f8-80e1-db7d60d7dbd7"),
            "Float16[].gz",
            "Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).
        /// </summary>
        public static readonly Def Float16ArrayLz4 = new(
            new Guid("410885cc-1e6a-b512-50c8-fc25c4866a4b"),
            "Float16[].lz4",
            "Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Floating point value (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float32 = new(
            new Guid("23fb286f-663b-4c71-9923-7e51c500f4ed"),
            "Float32",
            "Floating point value (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            false
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float32Array = new(
            new Guid("a687a789-1b63-49e9-a2e4-8099aa7879e9"),
            "Float32[]",
            "Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).
        /// </summary>
        public static readonly Def Float32ArrayGz = new(
            new Guid("34ae993a-e77c-ec53-1599-fd24328498f8"),
            "Float32[].gz",
            "Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).
        /// </summary>
        public static readonly Def Float32ArrayLz4 = new(
            new Guid("9a523e9c-4a1a-a309-7231-2a19a7b58212"),
            "Float32[].lz4",
            "Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Floating point value (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float64 = new(
            new Guid("c58c9b83-c2de-4153-a588-39c808aed50b"),
            "Float64",
            "Floating point value (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            false
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float64Array = new(
            new Guid("ba60cc30-2d56-45d8-a051-6b895b51bb3f"),
            "Float64[]",
            "Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).
        /// </summary>
        public static readonly Def Float64ArrayGz = new(
            new Guid("761de517-cb60-f46e-47c7-d782d6e84da5"),
            "Float64[].gz",
            "Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).
        /// </summary>
        public static readonly Def Float64ArrayLz4 = new(
            new Guid("d8048265-e847-b905-0d54-5fdb70e80229"),
            "Float64[].lz4",
            "Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Floating point value (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float128 = new(
            new Guid("5d343235-21f6-41e4-992e-93541db26502"),
            "Float128",
            "Floating point value (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            false
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float128Array = new(
            new Guid("6477a574-ffb0-4717-9f00-5fb9aff409ce"),
            "Float128[]",
            "Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).
        /// </summary>
        public static readonly Def Float128ArrayGz = new(
            new Guid("68edd81d-8f3e-511b-a7b2-e4e4cfbedffd"),
            "Float128[].gz",
            "Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).
        /// </summary>
        public static readonly Def Float128ArrayLz4 = new(
            new Guid("55302f6c-cb8e-bb44-5db1-0b47facf8687"),
            "Float128[].lz4",
            "Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Floating point value (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float256 = new(
            new Guid("4c7c4a8d-a4fb-43f5-82d8-34b8b171a05c"),
            "Float256",
            "Floating point value (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            false
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.
        /// </summary>
        public static readonly Def Float256Array = new(
            new Guid("acb4dd89-57f7-4229-9f1a-da017947843b"),
            "Float256[]",
            "Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).
        /// </summary>
        public static readonly Def Float256ArrayGz = new(
            new Guid("5ff35b1c-c91a-267a-3ae8-31d8ffd812e0"),
            "Float256[].gz",
            "Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).
        /// </summary>
        public static readonly Def Float256ArrayLz4 = new(
            new Guid("ec5b47d0-2910-adb0-72f4-694f5a238cfa"),
            "Float256[].lz4",
            "Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID32 = new(
            new Guid("4b970fc3-ce64-45d0-a602-0546b155760a"),
            "DecimalBID32",
            "Decimal floating point value (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID32Array = new(
            new Guid("349d24de-e84f-44dc-81cc-f110bc907062"),
            "DecimalBID32[]",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalBID32ArrayGz = new(
            new Guid("d29d44ed-4340-551f-b5e8-2a9232f41879"),
            "DecimalBID32[].gz",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalBID32ArrayLz4 = new(
            new Guid("f4c7d01c-72c7-85b4-f75c-0b4561d498d1"),
            "DecimalBID32[].lz4",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD32 = new(
            new Guid("067151e9-eaee-498e-88f5-fba450dd6cca"),
            "DecimalDPD32",
            "Decimal floating point value (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD32Array = new(
            new Guid("25d58ac1-cac8-4d67-a0af-65363c86e126"),
            "DecimalDPD32[]",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalDPD32ArrayGz = new(
            new Guid("4d40dbe4-41d0-1973-cc35-f51b1af19021"),
            "DecimalDPD32[].gz",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalDPD32ArrayLz4 = new(
            new Guid("cf963d2a-a0a7-89ba-86a9-69ad86a6d476"),
            "DecimalDPD32[].lz4",
            "Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 64-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID64 = new(
            new Guid("8a4c6d2b-c4f4-4e5c-864f-fa2226fb0414"),
            "DecimalBID64",
            "Decimal floating point value (IEEE 754, 64-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID64Array = new(
            new Guid("dea7b2cd-231a-4796-bfb9-30cc9d874f0c"),
            "DecimalBID64[]",
            "Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalBID64ArrayGz = new(
            new Guid("17050802-1978-cc9f-1b21-eee8304ed482"),
            "DecimalBID64[].gz",
            "Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalBID64ArrayLz4 = new(
            new Guid("d7eb94b7-c3e3-30e5-d858-295def2d3bf8"),
            "DecimalBID64[].lz4",
            "Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD64 = new(
            new Guid("23f81713-bb19-4640-8e89-d91c5a31201e"),
            "DecimalDPD64",
            "Decimal floating point value (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD64Array = new(
            new Guid("34cf3e10-c798-4011-9a5c-00c4c053d34c"),
            "DecimalDPD64[]",
            "Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalDPD64ArrayGz = new(
            new Guid("a8b7d2ef-4291-eecf-a89a-e8e09f2a134f"),
            "DecimalDPD64[].gz",
            "Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalDPD64ArrayLz4 = new(
            new Guid("80c5f610-aee8-c1b2-2980-093ddc7491cf"),
            "DecimalDPD64[].lz4",
            "Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID128 = new(
            new Guid("4911e3b9-8c72-4f82-a1fa-8d1a3a2d799d"),
            "DecimalBID128",
            "Decimal floating point value (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
        /// </summary>
        public static readonly Def DecimalBID128Array = new(
            new Guid("1a90ad03-b00f-4db5-928a-9a949c85c5e4"),
            "DecimalBID128[]",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalBID128ArrayGz = new(
            new Guid("bd858449-0438-de69-d14d-27b8b404ec8a"),
            "DecimalBID128[].gz",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalBID128ArrayLz4 = new(
            new Guid("ab2d2381-2a5e-7614-c2b9-2b8f801ea36e"),
            "DecimalBID128[].lz4",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Decimal floating point value (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD128 = new(
            new Guid("d45469bd-f10c-4fb7-b26c-296c38286044"),
            "DecimalDPD128",
            "Decimal floating point value (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
            None,
            false
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
        /// </summary>
        public static readonly Def DecimalDPD128Array = new(
            new Guid("c4e6665a-5387-4f71-b224-27775cffeaf5"),
            "DecimalDPD128[]",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalDPD128ArrayGz = new(
            new Guid("104a66b9-5dc5-3de4-8d45-ed2d7156b613"),
            "DecimalDPD128[].gz",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalDPD128ArrayLz4 = new(
            new Guid("ccc79f17-726b-ea68-4b9d-320159cd97a5"),
            "DecimalDPD128[].lz4",
            "Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A .NET decimal value (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.
        /// </summary>
        public static readonly Def DecimalDotnet = new(
            new Guid("eada3477-f3a5-48a4-a05c-da2aa359e034"),
            "DecimalDotnet",
            "A .NET decimal value (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.",
            None,
            false
            );

        /// <summary>
        /// Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.
        /// </summary>
        public static readonly Def DecimalDotnetArray = new(
            new Guid("b7327b1f-f349-4014-b244-aa328922e69f"),
            "DecimalDotnet[]",
            "Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.",
            None,
            true
            );

        /// <summary>
        /// Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type. Compressed (GZip).
        /// </summary>
        public static readonly Def DecimalDotnetArrayGz = new(
            new Guid("7ef52c2d-e2f8-0efc-46a6-45aa7bb3b995"),
            "DecimalDotnet[].gz",
            "Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type. Compressed (LZ4).
        /// </summary>
        public static readonly Def DecimalDotnetArrayLz4 = new(
            new Guid("332fc87f-7aba-723f-2773-202dbfc4d225"),
            "DecimalDotnet[].lz4",
            "Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8).
        /// </summary>
        public static readonly Def StringUTF8 = new(
            new Guid("917c15af-0e2d-4265-a732-7b2f147f4b94"),
            "StringUTF8",
            "String. UTF8 encoding. Serialized as UInt32 length followed by length number of bytes (UInt8).",
            None,
            false
            );

        /// <summary>
        /// Array of strings. UTF8 encoding.
        /// </summary>
        public static readonly Def StringUTF8Array = new(
            new Guid("852888ff-4168-4f4b-a10a-b582d1735c74"),
            "StringUTF8[]",
            "Array of strings. UTF8 encoding.",
            None,
            true
            );

        /// <summary>
        /// Array of strings. UTF8 encoding. Compressed (GZip).
        /// </summary>
        public static readonly Def StringUTF8ArrayGz = new(
            new Guid("eede1d29-0337-2f61-d9c9-5a3b65ae5ffc"),
            "StringUTF8[].gz",
            "Array of strings. UTF8 encoding. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of strings. UTF8 encoding. Compressed (LZ4).
        /// </summary>
        public static readonly Def StringUTF8ArrayLz4 = new(
            new Guid("15a6b0f6-ab89-5f75-e316-79cfcc0f35fd"),
            "StringUTF8[].lz4",
            "Array of strings. UTF8 encoding. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Deprecated 2022-10-21. A map of name/key/value entries, where names are strings, keys are durable IDs (Guid) with values of corresponding types. All names, keys, and values are 4-byte aligned.
        /// </summary>
        [Obsolete("Deprecated 2022-10-21.")]
        public static readonly Def DurableNamedMapDeprecated20221021 = new(
            new Guid("29de4f2f-90da-49ff-902c-3315e29457c9"),
            "Primitives.DurableNamedMap.Deprecated.20221021",
            "Deprecated 2022-10-21. A map of name/key/value entries, where names are strings, keys are durable IDs (Guid) with values of corresponding types. All names, keys, and values are 4-byte aligned.",
            None,
            false
            );

        /// <summary>
        /// Deprecated 2022-10-21. A gzipped element.
        /// </summary>
        [Obsolete("Deprecated 2022-10-21.")]
        public static readonly Def GZippedDeprecated20221021 = new(
            new Guid("7d8fc4c0-d727-4171-bc91-78f92f0c1aa4"),
            "Primitives.GZipped.Deprecated.20221021",
            "Deprecated 2022-10-21. A gzipped element.",
            None,
            false
            );

    }
    /// <summary></summary>
    public static class Aardvark
    {
        private static readonly Guid None = Guid.Empty;

        /// <summary>
        /// A 2-dim vector of 32-bit integers.
        /// </summary>
        public static readonly Def V2i = new(
            new Guid("1193e05b-4c04-409b-b47b-f9f4fbce7fb2"),
            "V2i",
            "A 2-dim vector of 32-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V2i.
        /// </summary>
        public static readonly Def V2iArray = new(
            new Guid("a684e893-16fa-42e5-a534-843dbec575e8"),
            "V2i[]",
            "Array of V2i.",
            None,
            true
            );

        /// <summary>
        /// Array of V2i. Compressed (GZip).
        /// </summary>
        public static readonly Def V2iArrayGz = new(
            new Guid("e17be58e-5241-3f1b-ecf3-a3d8b4db1f5e"),
            "V2i[].gz",
            "Array of V2i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V2i. Compressed (LZ4).
        /// </summary>
        public static readonly Def V2iArrayLz4 = new(
            new Guid("db8a32db-8c92-2251-815a-6615cbe86009"),
            "V2i[].lz4",
            "Array of V2i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim vector of 64-bit integers.
        /// </summary>
        public static readonly Def V2l = new(
            new Guid("5573a69d-4df9-4d91-8e3e-aa8204d8ec13"),
            "V2l",
            "A 2-dim vector of 64-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V2l.
        /// </summary>
        public static readonly Def V2lArray = new(
            new Guid("f5045bd5-08d1-4084-b717-932b55dcdc5b"),
            "V2l[]",
            "Array of V2l.",
            None,
            true
            );

        /// <summary>
        /// Array of V2l. Compressed (GZip).
        /// </summary>
        public static readonly Def V2lArrayGz = new(
            new Guid("5f3816fe-afa0-235b-85de-3e9b5f1c8155"),
            "V2l[].gz",
            "Array of V2l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V2l. Compressed (LZ4).
        /// </summary>
        public static readonly Def V2lArrayLz4 = new(
            new Guid("e8492752-a18b-9da7-02e8-ca0b3a83fbd2"),
            "V2l[].lz4",
            "Array of V2l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim vector of 32-bit floats.
        /// </summary>
        public static readonly Def V2f = new(
            new Guid("4f5d8782-3c9a-4913-bf0f-423269a24b1e"),
            "V2f",
            "A 2-dim vector of 32-bit floats.",
            None,
            false
            );

        /// <summary>
        /// Array of V2f.
        /// </summary>
        public static readonly Def V2fArray = new(
            new Guid("40d91f9d-ccb3-44fb-83d0-c3ff20189b2d"),
            "V2f[]",
            "Array of V2f.",
            None,
            true
            );

        /// <summary>
        /// Array of V2f. Compressed (GZip).
        /// </summary>
        public static readonly Def V2fArrayGz = new(
            new Guid("e1625be8-8e27-c279-fcd3-69c2dc0af9fc"),
            "V2f[].gz",
            "Array of V2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def V2fArrayLz4 = new(
            new Guid("f5efe277-ac94-d8b9-2917-fc32b3378d17"),
            "V2f[].lz4",
            "Array of V2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim vector of 64-bit floats.
        /// </summary>
        public static readonly Def V2d = new(
            new Guid("0f70ed18-574f-4431-a2c1-9987e4a7653c"),
            "V2d",
            "A 2-dim vector of 64-bit floats.",
            None,
            false
            );

        /// <summary>
        /// Array of V2d.
        /// </summary>
        public static readonly Def V2dArray = new(
            new Guid("17037869-687f-45f1-bd43-09a46a669547"),
            "V2d[]",
            "Array of V2d.",
            None,
            true
            );

        /// <summary>
        /// Array of V2d. Compressed (GZip).
        /// </summary>
        public static readonly Def V2dArrayGz = new(
            new Guid("555d827c-9109-c31f-a08b-d85fe4d99853"),
            "V2d[].gz",
            "Array of V2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def V2dArrayLz4 = new(
            new Guid("513d5820-280c-01db-f035-a71a8dd30f3b"),
            "V2d[].lz4",
            "Array of V2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim vector of 32-bit integers.
        /// </summary>
        public static readonly Def V3i = new(
            new Guid("876c952e-1749-4d2f-922f-75d1acd2d870"),
            "V3i",
            "A 3-dim vector of 32-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V3i.
        /// </summary>
        public static readonly Def V3iArray = new(
            new Guid("e9b3bee6-d6c4-46cb-9b74-be54530d03cd"),
            "V3i[]",
            "Array of V3i.",
            None,
            true
            );

        /// <summary>
        /// Array of V3i. Compressed (GZip).
        /// </summary>
        public static readonly Def V3iArrayGz = new(
            new Guid("8447d78e-a9ca-8976-b2c2-f906fd90a1c1"),
            "V3i[].gz",
            "Array of V3i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V3i. Compressed (LZ4).
        /// </summary>
        public static readonly Def V3iArrayLz4 = new(
            new Guid("e22b8474-c7e4-29b2-8b5a-29174e137208"),
            "V3i[].lz4",
            "Array of V3i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim vector of 64-bit integers.
        /// </summary>
        public static readonly Def V3l = new(
            new Guid("baff1328-3149-4812-901b-23d9b3ba3a29"),
            "V3l",
            "A 3-dim vector of 64-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V3l.
        /// </summary>
        public static readonly Def V3lArray = new(
            new Guid("229703bb-c382-4b5a-b333-30a029e77f83"),
            "V3l[]",
            "Array of V3l.",
            None,
            true
            );

        /// <summary>
        /// Array of V3l. Compressed (GZip).
        /// </summary>
        public static readonly Def V3lArrayGz = new(
            new Guid("4a00b9c9-7a9b-2150-1bc4-284d4119c9d1"),
            "V3l[].gz",
            "Array of V3l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V3l. Compressed (LZ4).
        /// </summary>
        public static readonly Def V3lArrayLz4 = new(
            new Guid("ae8571bb-cf07-d21e-9312-7b26e3c61c0c"),
            "V3l[].lz4",
            "Array of V3l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim vector of 32-bit floats.
        /// </summary>
        public static readonly Def V3f = new(
            new Guid("ad8adcb6-8cf1-474e-99da-851343858935"),
            "V3f",
            "A 3-dim vector of 32-bit floats.",
            None,
            false
            );

        /// <summary>
        /// Array of V3f.
        /// </summary>
        public static readonly Def V3fArray = new(
            new Guid("f14f7607-3ddd-4e52-9ff3-c877c2242021"),
            "V3f[]",
            "Array of V3f.",
            None,
            true
            );

        /// <summary>
        /// Array of V3f. Compressed (GZip).
        /// </summary>
        public static readonly Def V3fArrayGz = new(
            new Guid("ac35fbb9-19d9-9abe-36b9-51e9ee81f3d5"),
            "V3f[].gz",
            "Array of V3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def V3fArrayLz4 = new(
            new Guid("5409f7ee-7f6d-4811-8313-1dafb108ac1f"),
            "V3f[].lz4",
            "Array of V3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim vector of 64-bit floats.
        /// </summary>
        public static readonly Def V3d = new(
            new Guid("7a0be234-ab45-464d-b706-87157aba4361"),
            "V3d",
            "A 3-dim vector of 64-bit floats.",
            None,
            false
            );

        /// <summary>
        /// Array of V3d.
        /// </summary>
        public static readonly Def V3dArray = new(
            new Guid("2cce99b6-e823-4b34-8615-f7ab88746554"),
            "V3d[]",
            "Array of V3d.",
            None,
            true
            );

        /// <summary>
        /// Array of V3d. Compressed (GZip).
        /// </summary>
        public static readonly Def V3dArrayGz = new(
            new Guid("ce3e650f-4c2d-b5d2-7c88-47c78cf3200d"),
            "V3d[].gz",
            "Array of V3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def V3dArrayLz4 = new(
            new Guid("70a77d2e-f602-34ea-a478-7324839c2310"),
            "V3d[].lz4",
            "Array of V3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4-dim vector of 32-bit integers.
        /// </summary>
        public static readonly Def V4i = new(
            new Guid("244a0ae8-c234-4024-821b-d5b3ad28701d"),
            "V4i",
            "A 4-dim vector of 32-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V4i.
        /// </summary>
        public static readonly Def V4iArray = new(
            new Guid("4e5839a1-0b5b-4407-a55c-cfa5fecf757c"),
            "V4i[]",
            "Array of V4i.",
            None,
            true
            );

        /// <summary>
        /// Array of V4i. Compressed (GZip).
        /// </summary>
        public static readonly Def V4iArrayGz = new(
            new Guid("2b4e0b63-9baa-7c74-30bc-0080c9e8fe41"),
            "V4i[].gz",
            "Array of V4i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V4i. Compressed (LZ4).
        /// </summary>
        public static readonly Def V4iArrayLz4 = new(
            new Guid("b1f63ff4-a990-ca5e-e46f-fb8575b7e06f"),
            "V4i[].lz4",
            "Array of V4i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4-dim vector of 64-bit integers.
        /// </summary>
        public static readonly Def V4l = new(
            new Guid("04f77262-b56a-44d5-af79-c1679493acff"),
            "V4l",
            "A 4-dim vector of 64-bit integers.",
            None,
            false
            );

        /// <summary>
        /// Array of V4l.
        /// </summary>
        public static readonly Def V4lArray = new(
            new Guid("8aecdd7e-acf1-41aa-9d02-f954b43d6c62"),
            "V4l[]",
            "Array of V4l.",
            None,
            true
            );

        /// <summary>
        /// Array of V4l. Compressed (GZip).
        /// </summary>
        public static readonly Def V4lArrayGz = new(
            new Guid("c3ce7e6b-a247-d72e-745f-bf32e140e241"),
            "V4l[].gz",
            "Array of V4l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V4l. Compressed (LZ4).
        /// </summary>
        public static readonly Def V4lArrayLz4 = new(
            new Guid("a67291d5-b1db-8bf3-d19d-239dc59aafb3"),
            "V4l[].lz4",
            "Array of V4l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4-dim vector of 32-bit float.
        /// </summary>
        public static readonly Def V4f = new(
            new Guid("969daa40-9ea2-4bce-8189-f416d65a9c3e"),
            "V4f",
            "A 4-dim vector of 32-bit float.",
            None,
            false
            );

        /// <summary>
        /// Array of V4f.
        /// </summary>
        public static readonly Def V4fArray = new(
            new Guid("be5a8fda-4a6a-46e8-9654-356721d03f17"),
            "V4f[]",
            "Array of V4f.",
            None,
            true
            );

        /// <summary>
        /// Array of V4f. Compressed (GZip).
        /// </summary>
        public static readonly Def V4fArrayGz = new(
            new Guid("3b238275-43c7-e547-fb93-5835a661ea18"),
            "V4f[].gz",
            "Array of V4f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V4f. Compressed (LZ4).
        /// </summary>
        public static readonly Def V4fArrayLz4 = new(
            new Guid("e0ae63fa-0da0-694d-ce50-1d806fc6c9be"),
            "V4f[].lz4",
            "Array of V4f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4-dim vector of 64-bit float.
        /// </summary>
        public static readonly Def V4d = new(
            new Guid("b2dd492b-aaf8-4dfa-bcc2-833af6cbd637"),
            "V4d",
            "A 4-dim vector of 64-bit float.",
            None,
            false
            );

        /// <summary>
        /// Array of V4d.
        /// </summary>
        public static readonly Def V4dArray = new(
            new Guid("800184a5-c207-4b4a-88a0-60d9281ecdc1"),
            "V4d[]",
            "Array of V4d.",
            None,
            true
            );

        /// <summary>
        /// Array of V4d. Compressed (GZip).
        /// </summary>
        public static readonly Def V4dArrayGz = new(
            new Guid("583b3c26-ee82-d5d4-6a08-648982d2e069"),
            "V4d[].gz",
            "Array of V4d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of V4d. Compressed (LZ4).
        /// </summary>
        public static readonly Def V4dArrayLz4 = new(
            new Guid("8363ee0e-a3c7-0c7f-b587-2bc612c11164"),
            "V4d[].lz4",
            "Array of V4d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2x2 matrix of Float32.
        /// </summary>
        public static readonly Def M22f = new(
            new Guid("4f01ceee-2595-4c2d-859d-4f14df35a048"),
            "M22f",
            "A 2x2 matrix of Float32.",
            None,
            false
            );

        /// <summary>
        /// Array of M22f.
        /// </summary>
        public static readonly Def M22fArray = new(
            new Guid("480269a5-304c-401a-848b-64e2392ddd3e"),
            "M22f[]",
            "Array of M22f.",
            None,
            true
            );

        /// <summary>
        /// Array of M22f. Compressed (GZip).
        /// </summary>
        public static readonly Def M22fArrayGz = new(
            new Guid("4e6ef5da-611d-cbd1-ae7f-27517664fe7d"),
            "M22f[].gz",
            "Array of M22f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M22f. Compressed (LZ4).
        /// </summary>
        public static readonly Def M22fArrayLz4 = new(
            new Guid("7ee9d018-72a1-8b52-d589-30f362387072"),
            "M22f[].lz4",
            "Array of M22f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2x2 matrix of Float64.
        /// </summary>
        public static readonly Def M22d = new(
            new Guid("f16842ef-531c-4782-92f2-385bf5fd42ab"),
            "M22d",
            "A 2x2 matrix of Float64.",
            None,
            false
            );

        /// <summary>
        /// Array of M22d.
        /// </summary>
        public static readonly Def M22dArray = new(
            new Guid("2e6b3a90-3e45-4e5c-8770-e351640f5d47"),
            "M22d[]",
            "Array of M22d.",
            None,
            true
            );

        /// <summary>
        /// Array of M22d. Compressed (GZip).
        /// </summary>
        public static readonly Def M22dArrayGz = new(
            new Guid("ce1b482b-a797-50e1-0b7f-fd0f96bb177a"),
            "M22d[].gz",
            "Array of M22d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M22d. Compressed (LZ4).
        /// </summary>
        public static readonly Def M22dArrayLz4 = new(
            new Guid("66ecba63-54d9-906d-b64d-b67d79d9889a"),
            "M22d[].lz4",
            "Array of M22d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3x3 matrix of Float32.
        /// </summary>
        public static readonly Def M33f = new(
            new Guid("587a4a05-db34-4b2a-aa67-5cfe5c4d82cc"),
            "M33f",
            "A 3x3 matrix of Float32.",
            None,
            false
            );

        /// <summary>
        /// Array of M33f.
        /// </summary>
        public static readonly Def M33fArray = new(
            new Guid("95be083c-6f03-4279-872f-624f044599c6"),
            "M33f[]",
            "Array of M33f.",
            None,
            true
            );

        /// <summary>
        /// Array of M33f. Compressed (GZip).
        /// </summary>
        public static readonly Def M33fArrayGz = new(
            new Guid("eb7b4264-081e-6ce9-2a12-598ce426dfd9"),
            "M33f[].gz",
            "Array of M33f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M33f. Compressed (LZ4).
        /// </summary>
        public static readonly Def M33fArrayLz4 = new(
            new Guid("3cd262b3-b732-fb83-b8fa-edddea1501db"),
            "M33f[].lz4",
            "Array of M33f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3x3 matrix of Float64.
        /// </summary>
        public static readonly Def M33d = new(
            new Guid("ccd797e3-0ca4-4191-840d-53751e021972"),
            "M33d",
            "A 3x3 matrix of Float64.",
            None,
            false
            );

        /// <summary>
        /// Array of M33d.
        /// </summary>
        public static readonly Def M33dArray = new(
            new Guid("378429d5-2517-46bb-b90d-b7bc34a86466"),
            "M33d[]",
            "Array of M33d.",
            None,
            true
            );

        /// <summary>
        /// Array of M33d. Compressed (GZip).
        /// </summary>
        public static readonly Def M33dArrayGz = new(
            new Guid("d74c9868-e6a6-81c7-1f54-ee74f046284f"),
            "M33d[].gz",
            "Array of M33d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M33d. Compressed (LZ4).
        /// </summary>
        public static readonly Def M33dArrayLz4 = new(
            new Guid("cd24f862-ed38-177c-4f76-9b767c4f6617"),
            "M33d[].lz4",
            "Array of M33d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4x4 matrix of Float32.
        /// </summary>
        public static readonly Def M44f = new(
            new Guid("652a5421-e262-4da8-ae38-78df761a365e"),
            "M44f",
            "A 4x4 matrix of Float32.",
            None,
            false
            );

        /// <summary>
        /// Array of M44f.
        /// </summary>
        public static readonly Def M44fArray = new(
            new Guid("cf53c4e6-f3ca-4be5-a449-0434ae455b85"),
            "M44f[]",
            "Array of M44f.",
            None,
            true
            );

        /// <summary>
        /// Array of M44f. Compressed (GZip).
        /// </summary>
        public static readonly Def M44fArrayGz = new(
            new Guid("490c4a54-15fc-c116-29d8-53da1dcc4c08"),
            "M44f[].gz",
            "Array of M44f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M44f. Compressed (LZ4).
        /// </summary>
        public static readonly Def M44fArrayLz4 = new(
            new Guid("b43fbf4e-5040-d8eb-3070-7c830b52126d"),
            "M44f[].lz4",
            "Array of M44f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 4x4 matrix of Float64.
        /// </summary>
        public static readonly Def M44d = new(
            new Guid("b9498622-db77-4d4c-b78c-62522ccf9252"),
            "M44d",
            "A 4x4 matrix of Float64.",
            None,
            false
            );

        /// <summary>
        /// Array of M44d.
        /// </summary>
        public static readonly Def M44dArray = new(
            new Guid("f07abd79-60a6-429a-94f1-11eb6c319db2"),
            "M44d[]",
            "Array of M44d.",
            None,
            true
            );

        /// <summary>
        /// Array of M44d. Compressed (GZip).
        /// </summary>
        public static readonly Def M44dArrayGz = new(
            new Guid("65f4a49c-d4f9-edf1-4d5c-9396d1cb8d33"),
            "M44d[].gz",
            "Array of M44d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of M44d. Compressed (LZ4).
        /// </summary>
        public static readonly Def M44dArrayLz4 = new(
            new Guid("f42d6722-a8f0-bf10-29d4-f67a006c6991"),
            "M44d[].lz4",
            "Array of M44d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Struct to represent an affine transformation in 2-dimensional space. It consists of a linear tranformation (invertible 2x2 matrix) and a translational component (2d vector).
        /// </summary>
        public static readonly Def Affine2f = new(
            new Guid("2e90e758-ff1b-4805-84cf-2d382c8b95fc"),
            "Affine2f",
            "Struct to represent an affine transformation in 2-dimensional space. It consists of a linear tranformation (invertible 2x2 matrix) and a translational component (2d vector).",
            None,
            false
            );

        /// <summary>
        /// Array of Affine2f.
        /// </summary>
        public static readonly Def Affine2fArray = new(
            new Guid("6e5ae9e5-ccf4-4b75-acec-9e6da550ab68"),
            "Affine2f[]",
            "Array of Affine2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Affine2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Affine2fArrayGz = new(
            new Guid("9d840094-ac65-3b77-5252-715862d1c659"),
            "Affine2f[].gz",
            "Array of Affine2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Affine2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Affine2fArrayLz4 = new(
            new Guid("af698c37-2196-9f76-1430-9fbd6df07117"),
            "Affine2f[].lz4",
            "Array of Affine2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Struct to represent an affine transformation in 2-dimensional space. It consists of a linear tranformation (invertible 2x2 matrix) and a translational component (2d vector).
        /// </summary>
        public static readonly Def Affine2d = new(
            new Guid("13862bf0-30fd-4ae5-9f3d-2b18f7820f18"),
            "Affine2d",
            "Struct to represent an affine transformation in 2-dimensional space. It consists of a linear tranformation (invertible 2x2 matrix) and a translational component (2d vector).",
            None,
            false
            );

        /// <summary>
        /// Array of Affine2d.
        /// </summary>
        public static readonly Def Affine2dArray = new(
            new Guid("6781406b-c840-4633-aa65-9692722dbe12"),
            "Affine2d[]",
            "Array of Affine2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Affine2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Affine2dArrayGz = new(
            new Guid("c315f9a3-e44c-d1b3-2b6b-6afdc98652d5"),
            "Affine2d[].gz",
            "Array of Affine2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Affine2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Affine2dArrayLz4 = new(
            new Guid("e8eac0ea-ea44-f0a5-a137-b495b597d21d"),
            "Affine2d[].lz4",
            "Array of Affine2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Struct to represent an affine transformation in 3-dimensional space. It consists of a linear tranformation (invertible 3x3 matrix) and a translational component (3d vector).
        /// </summary>
        public static readonly Def Affine3f = new(
            new Guid("157595e2-7c63-4aa3-87c0-25163b91778a"),
            "Affine3f",
            "Struct to represent an affine transformation in 3-dimensional space. It consists of a linear tranformation (invertible 3x3 matrix) and a translational component (3d vector).",
            None,
            false
            );

        /// <summary>
        /// Array of Affine3f.
        /// </summary>
        public static readonly Def Affine3fArray = new(
            new Guid("6d300cd7-ef29-4dcf-80d5-71e879f7b1e9"),
            "Affine3f[]",
            "Array of Affine3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Affine3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Affine3fArrayGz = new(
            new Guid("d6a0e877-c74c-1604-1ba7-9381d8fa35c5"),
            "Affine3f[].gz",
            "Array of Affine3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Affine3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Affine3fArrayLz4 = new(
            new Guid("09f6309e-c66b-e724-7365-389f8c0e46c7"),
            "Affine3f[].lz4",
            "Array of Affine3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Struct to represent an affine transformation in 3-dimensional space. It consists of a linear tranformation (invertible 3x3 matrix) and a translational component (3d vector).
        /// </summary>
        public static readonly Def Affine3d = new(
            new Guid("ef0c8ace-2d14-4909-88b1-0d46d7ece477"),
            "Affine3d",
            "Struct to represent an affine transformation in 3-dimensional space. It consists of a linear tranformation (invertible 3x3 matrix) and a translational component (3d vector).",
            None,
            false
            );

        /// <summary>
        /// Array of Affine3d.
        /// </summary>
        public static readonly Def Affine3dArray = new(
            new Guid("71cb8459-317e-46e9-b751-ae63e0ed4aef"),
            "Affine3d[]",
            "Array of Affine3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Affine3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Affine3dArrayGz = new(
            new Guid("465d9b99-4f53-c53b-fa7c-52d3a00942f5"),
            "Affine3d[].gz",
            "Array of Affine3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Affine3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Affine3dArrayLz4 = new(
            new Guid("893e0134-dbef-7f42-867e-944adb13a782"),
            "Affine3d[].lz4",
            "Array of Affine3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a 2D rotation counterclockwise around the origin.
        /// </summary>
        public static readonly Def Rot2f = new(
            new Guid("d390ffc0-33e9-4465-ad08-1041aa4984f7"),
            "Rot2f",
            "Represents a 2D rotation counterclockwise around the origin.",
            None,
            false
            );

        /// <summary>
        /// Array of Rot2f.
        /// </summary>
        public static readonly Def Rot2fArray = new(
            new Guid("5a4f8839-e124-4c38-9e65-fed679bc6e9e"),
            "Rot2f[]",
            "Array of Rot2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Rot2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Rot2fArrayGz = new(
            new Guid("2d21c61c-c74b-caf1-4e69-2864ef70b5fd"),
            "Rot2f[].gz",
            "Array of Rot2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Rot2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Rot2fArrayLz4 = new(
            new Guid("0d50e129-fda0-9276-7d44-93fbfba1da9c"),
            "Rot2f[].lz4",
            "Array of Rot2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a 2D rotation counterclockwise around the origin.
        /// </summary>
        public static readonly Def Rot2d = new(
            new Guid("1a92edf5-9d85-4aec-8ee4-5bc82671680a"),
            "Rot2d",
            "Represents a 2D rotation counterclockwise around the origin.",
            None,
            false
            );

        /// <summary>
        /// Array of Rot2d.
        /// </summary>
        public static readonly Def Rot2dArray = new(
            new Guid("ebd26aa1-53ad-4d9a-ab64-2c3a7c11008b"),
            "Rot2d[]",
            "Array of Rot2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Rot2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Rot2dArrayGz = new(
            new Guid("90f0c4ed-976a-fd22-4752-c68a875fbc9c"),
            "Rot2d[].gz",
            "Array of Rot2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Rot2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Rot2dArrayLz4 = new(
            new Guid("6e1a1d89-b791-26f0-de79-86ac4341231a"),
            "Rot2d[].lz4",
            "Array of Rot2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a rotation in three dimensions using a unit quaternion (W,X,Y,Z).
        /// </summary>
        public static readonly Def Rot3f = new(
            new Guid("0ab5cb52-ee7b-42e6-8d36-1b0ff024b9c5"),
            "Rot3f",
            "Represents a rotation in three dimensions using a unit quaternion (W,X,Y,Z).",
            None,
            false
            );

        /// <summary>
        /// Array of Rot3f.
        /// </summary>
        public static readonly Def Rot3fArray = new(
            new Guid("89379679-6395-4046-8100-ce4f675aa910"),
            "Rot3f[]",
            "Array of Rot3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Rot3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Rot3fArrayGz = new(
            new Guid("8f515f12-edc7-1885-98be-41f378b8df34"),
            "Rot3f[].gz",
            "Array of Rot3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Rot3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Rot3fArrayLz4 = new(
            new Guid("8d455deb-e12e-c31a-caaf-74ab30292397"),
            "Rot3f[].lz4",
            "Array of Rot3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a rotation in three dimensions using a unit quaternion (W,X,Y,Z).
        /// </summary>
        public static readonly Def Rot3d = new(
            new Guid("fbf5aacb-ed37-4784-a756-1365df952374"),
            "Rot3d",
            "Represents a rotation in three dimensions using a unit quaternion (W,X,Y,Z).",
            None,
            false
            );

        /// <summary>
        /// Array of Rot3d.
        /// </summary>
        public static readonly Def Rot3dArray = new(
            new Guid("23d04982-4838-4802-9405-b5e05f24d18a"),
            "Rot3d[]",
            "Array of Rot3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Rot3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Rot3dArrayGz = new(
            new Guid("888fdd8f-d9e8-1484-fde7-b9d0ebff2d21"),
            "Rot3d[].gz",
            "Array of Rot3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Rot3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Rot3dArrayLz4 = new(
            new Guid("01abe336-2df4-bf12-874d-b99a24775ff8"),
            "Rot3d[].lz4",
            "Array of Rot3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.
        /// </summary>
        public static readonly Def Euclidean2f = new(
            new Guid("28355b4e-63f8-45b4-8b54-97bd7c1bee7e"),
            "Euclidean2f",
            "Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Euclidean2f.
        /// </summary>
        public static readonly Def Euclidean2fArray = new(
            new Guid("128891b0-ebf6-4a86-8115-a5cb55fedeca"),
            "Euclidean2f[]",
            "Array of Euclidean2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Euclidean2fArrayGz = new(
            new Guid("01ed606c-1679-b125-12c9-6e0ef42e0527"),
            "Euclidean2f[].gz",
            "Array of Euclidean2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Euclidean2fArrayLz4 = new(
            new Guid("ffb8e7f6-36ae-d4ae-eef5-00f4ba790dde"),
            "Euclidean2f[].lz4",
            "Array of Euclidean2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.
        /// </summary>
        public static readonly Def Euclidean2d = new(
            new Guid("7ecf1964-cbcb-4cb5-b3df-74c5cce8b3c6"),
            "Euclidean2d",
            "Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Euclidean2d.
        /// </summary>
        public static readonly Def Euclidean2dArray = new(
            new Guid("2fe8f0eb-b782-4380-b103-b7ca1de71009"),
            "Euclidean2d[]",
            "Array of Euclidean2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Euclidean2dArrayGz = new(
            new Guid("9969fb7e-b260-e7ec-0cb2-9505aa921dc4"),
            "Euclidean2d[].gz",
            "Array of Euclidean2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Euclidean2dArrayLz4 = new(
            new Guid("f7b4ba45-7837-e16b-fdc0-59db5a45655d"),
            "Euclidean2d[].lz4",
            "Array of Euclidean2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.
        /// </summary>
        public static readonly Def Euclidean3f = new(
            new Guid("ff4550c6-e6ba-4b5a-9144-47ec189cf0be"),
            "Euclidean3f",
            "Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Euclidean3f.
        /// </summary>
        public static readonly Def Euclidean3fArray = new(
            new Guid("870f9f7d-981a-4b52-aacb-f1d02b276c94"),
            "Euclidean3f[]",
            "Array of Euclidean3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Euclidean3fArrayGz = new(
            new Guid("aab1b7f2-a90f-10cd-cd09-073a6dfefe2c"),
            "Euclidean3f[].gz",
            "Array of Euclidean3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Euclidean3fArrayLz4 = new(
            new Guid("93aa32b8-5459-fa74-9b03-738cf2a5124e"),
            "Euclidean3f[].lz4",
            "Array of Euclidean3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.
        /// </summary>
        public static readonly Def Euclidean3d = new(
            new Guid("278f920c-c165-4ad0-8cec-7b3b4c0ff9a0"),
            "Euclidean3d",
            "Represents a Rigid Transformation (or Rigid Body Transformation) in 2D that is composed of a 2D rotation Rot and a subsequent translation by a 2D vector Trans. This is also called an Euclidean Transformation and is a length preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Euclidean3d.
        /// </summary>
        public static readonly Def Euclidean3dArray = new(
            new Guid("18e1c88c-580a-4a9f-a1c4-849a9f0f0a95"),
            "Euclidean3d[]",
            "Array of Euclidean3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Euclidean3dArrayGz = new(
            new Guid("5f86bca1-d1e5-3d02-8195-8cb2677cea36"),
            "Euclidean3d[].gz",
            "Array of Euclidean3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Euclidean3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Euclidean3dArrayLz4 = new(
            new Guid("3e3cd167-b48e-6310-34f3-a913b1fd8cb1"),
            "Euclidean3d[].lz4",
            "Array of Euclidean3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional scaling transform with different scaling values in each dimension.
        /// </summary>
        public static readonly Def Scale2f = new(
            new Guid("cc6813f4-ce18-4a97-8ca1-d892ce394a1d"),
            "Scale2f",
            "A 2-dimensional scaling transform with different scaling values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Scale2f.
        /// </summary>
        public static readonly Def Scale2fArray = new(
            new Guid("56c296c0-1a02-4e25-848b-b4c17ef194de"),
            "Scale2f[]",
            "Array of Scale2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Scale2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Scale2fArrayGz = new(
            new Guid("fa11ce93-a364-e07e-1a7a-6b2cd8d4088e"),
            "Scale2f[].gz",
            "Array of Scale2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Scale2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Scale2fArrayLz4 = new(
            new Guid("c7c9f7c2-80a5-62db-61c5-906c1cf223b3"),
            "Scale2f[].lz4",
            "Array of Scale2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional scaling transform with different scaling values in each dimension.
        /// </summary>
        public static readonly Def Scale2d = new(
            new Guid("f4e0a9ad-2e42-41d7-831d-21d008ed431a"),
            "Scale2d",
            "A 2-dimensional scaling transform with different scaling values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Scale2d.
        /// </summary>
        public static readonly Def Scale2dArray = new(
            new Guid("a6abe320-021b-491e-89a1-72286d3c2b28"),
            "Scale2d[]",
            "Array of Scale2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Scale2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Scale2dArrayGz = new(
            new Guid("3bdb0dc4-ec7f-6278-8662-ce6402043c10"),
            "Scale2d[].gz",
            "Array of Scale2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Scale2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Scale2dArrayLz4 = new(
            new Guid("a4024560-7adc-c18e-169e-f00487ba59c4"),
            "Scale2d[].lz4",
            "Array of Scale2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dimensional scaling transform with different scaling values in each dimension.
        /// </summary>
        public static readonly Def Scale3f = new(
            new Guid("42465d9c-3f2e-4d3b-abee-2df2779157e8"),
            "Scale3f",
            "A 3-dimensional scaling transform with different scaling values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Scale3f.
        /// </summary>
        public static readonly Def Scale3fArray = new(
            new Guid("20b5b745-a5a6-4eab-80a5-201953b22f1f"),
            "Scale3f[]",
            "Array of Scale3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Scale3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Scale3fArrayGz = new(
            new Guid("e3cd0b52-475a-7cea-03b4-c6f450e27210"),
            "Scale3f[].gz",
            "Array of Scale3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Scale3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Scale3fArrayLz4 = new(
            new Guid("33a6df10-d2d0-7d88-8f45-a5cde0f98dfb"),
            "Scale3f[].lz4",
            "Array of Scale3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dimensional scaling transform with different scaling values in each dimension.
        /// </summary>
        public static readonly Def Scale3d = new(
            new Guid("49d4b715-69a1-440c-88ca-13d9046d0715"),
            "Scale3d",
            "A 3-dimensional scaling transform with different scaling values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Scale3d.
        /// </summary>
        public static readonly Def Scale3dArray = new(
            new Guid("77d2eba3-d7a4-4084-91af-15d18bbaa081"),
            "Scale3d[]",
            "Array of Scale3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Scale3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Scale3dArrayGz = new(
            new Guid("4cae74b8-8caa-d15b-ba08-1c26529aba73"),
            "Scale3d[].gz",
            "Array of Scale3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Scale3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Scale3dArrayLz4 = new(
            new Guid("54c876c0-bdae-9f3b-477a-ca70070088f7"),
            "Scale3d[].lz4",
            "Array of Scale3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional translational transform with different translation values in each dimension.
        /// </summary>
        public static readonly Def Shift2f = new(
            new Guid("dab91ada-116a-4feb-adb5-8c6fb2bdfa09"),
            "Shift2f",
            "A 2-dimensional translational transform with different translation values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Shift2f.
        /// </summary>
        public static readonly Def Shift2fArray = new(
            new Guid("fb665b60-b2be-45c0-a2ef-f5e2163d1025"),
            "Shift2f[]",
            "Array of Shift2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Shift2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Shift2fArrayGz = new(
            new Guid("4c6e7768-fc86-2026-7951-0ef17d09c194"),
            "Shift2f[].gz",
            "Array of Shift2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Shift2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Shift2fArrayLz4 = new(
            new Guid("2dd506cb-6bcb-a299-b948-9f5dfdad9901"),
            "Shift2f[].lz4",
            "Array of Shift2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional translational transform with different translation values in each dimension.
        /// </summary>
        public static readonly Def Shift2d = new(
            new Guid("1440f172-847e-4d20-9c80-3c2862b40e45"),
            "Shift2d",
            "A 2-dimensional translational transform with different translation values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Shift2d.
        /// </summary>
        public static readonly Def Shift2dArray = new(
            new Guid("8888fe11-34d7-423b-952f-756921eaf234"),
            "Shift2d[]",
            "Array of Shift2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Shift2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Shift2dArrayGz = new(
            new Guid("503e0a6f-c16c-f883-11a6-425e699f930d"),
            "Shift2d[].gz",
            "Array of Shift2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Shift2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Shift2dArrayLz4 = new(
            new Guid("8318e9b2-9afe-256f-3e74-7f812976b4d6"),
            "Shift2d[].lz4",
            "Array of Shift2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional translational transform with different translation values in each dimension.
        /// </summary>
        public static readonly Def Shift3f = new(
            new Guid("b278f244-0717-49a8-9a0a-193b571a6fee"),
            "Shift3f",
            "A 2-dimensional translational transform with different translation values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Shift3f.
        /// </summary>
        public static readonly Def Shift3fArray = new(
            new Guid("6e49c963-c3d1-4b84-8ee8-a5374b04fae5"),
            "Shift3f[]",
            "Array of Shift3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Shift3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Shift3fArrayGz = new(
            new Guid("3695abeb-f541-a1d5-4112-09351227877e"),
            "Shift3f[].gz",
            "Array of Shift3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Shift3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Shift3fArrayLz4 = new(
            new Guid("d97cbb4f-13e2-6ebb-06c7-a58610052faa"),
            "Shift3f[].lz4",
            "Array of Shift3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dimensional translational transform with different translation values in each dimension.
        /// </summary>
        public static readonly Def Shift3d = new(
            new Guid("a35e085b-5b24-46ec-9e8d-268c9b16371f"),
            "Shift3d",
            "A 2-dimensional translational transform with different translation values in each dimension.",
            None,
            false
            );

        /// <summary>
        /// Array of Shift3d.
        /// </summary>
        public static readonly Def Shift3dArray = new(
            new Guid("4fa7fb3c-bfd5-4507-ab11-0672517a870d"),
            "Shift3d[]",
            "Array of Shift3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Shift3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Shift3dArrayGz = new(
            new Guid("6ed014c1-ff45-604d-5ed1-ea580d9df5d2"),
            "Shift3d[].gz",
            "Array of Shift3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Shift3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Shift3dArrayLz4 = new(
            new Guid("7c4cce3a-c74c-7604-9baf-887245e785db"),
            "Shift3d[].lz4",
            "Array of Shift3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Similarity Transformation in 2D that is composed of a Uniform Scale and a subsequent Euclidean transformation (2D rotation Rot and a subsequent translation by a 2D vector Trans). This is an angle preserving Transformation.
        /// </summary>
        public static readonly Def Similarity2f = new(
            new Guid("b2fae9eb-6237-4b4c-8e8f-87daf2a2a9ce"),
            "Similarity2f",
            "Represents a Similarity Transformation in 2D that is composed of a Uniform Scale and a subsequent Euclidean transformation (2D rotation Rot and a subsequent translation by a 2D vector Trans). This is an angle preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Similarity2f.
        /// </summary>
        public static readonly Def Similarity2fArray = new(
            new Guid("1269612a-f81f-47db-993b-5182e5d735e3"),
            "Similarity2f[]",
            "Array of Similarity2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Similarity2fArrayGz = new(
            new Guid("f41f9ede-0901-6d82-87a3-7c6ef132df21"),
            "Similarity2f[].gz",
            "Array of Similarity2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Similarity2fArrayLz4 = new(
            new Guid("c7ed93dc-e503-15fe-c37a-523dd05e843f"),
            "Similarity2f[].lz4",
            "Array of Similarity2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Similarity Transformation in 2D that is composed of a Uniform Scale and a subsequent Euclidean transformation (2D rotation Rot and a subsequent translation by a 2D vector Trans). This is an angle preserving Transformation.
        /// </summary>
        public static readonly Def Similarity2d = new(
            new Guid("a8d1bca8-eb17-4a0a-8f05-1910bb98e3cf"),
            "Similarity2d",
            "Represents a Similarity Transformation in 2D that is composed of a Uniform Scale and a subsequent Euclidean transformation (2D rotation Rot and a subsequent translation by a 2D vector Trans). This is an angle preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Similarity2d.
        /// </summary>
        public static readonly Def Similarity2dArray = new(
            new Guid("697fd675-ab04-477a-a620-00d32a986503"),
            "Similarity2d[]",
            "Array of Similarity2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Similarity2dArrayGz = new(
            new Guid("361c6915-6fec-0945-96a7-a34d5f3c5186"),
            "Similarity2d[].gz",
            "Array of Similarity2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Similarity2dArrayLz4 = new(
            new Guid("f92e7c3d-aa85-64d8-69b6-7a79ac51eaf3"),
            "Similarity2d[].lz4",
            "Array of Similarity2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Similarity Transformation in 3D that is composed of a Uniform Scale and a subsequent Euclidean transformation (3D rotation Rot and a subsequent translation by a 3D vector Trans). This is an angle preserving Transformation.
        /// </summary>
        public static readonly Def Similarity3f = new(
            new Guid("c6c9f2ee-4429-4260-a086-66538d43aaad"),
            "Similarity3f",
            "Represents a Similarity Transformation in 3D that is composed of a Uniform Scale and a subsequent Euclidean transformation (3D rotation Rot and a subsequent translation by a 3D vector Trans). This is an angle preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Similarity3f.
        /// </summary>
        public static readonly Def Similarity3fArray = new(
            new Guid("92c53833-8e7c-41dc-a4b5-582c848bb67b"),
            "Similarity3f[]",
            "Array of Similarity3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Similarity3fArrayGz = new(
            new Guid("22764038-5ed1-14b7-f8c7-6c920140b6c0"),
            "Similarity3f[].gz",
            "Array of Similarity3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Similarity3fArrayLz4 = new(
            new Guid("aea447ba-7e03-fdb3-d48d-dca444269727"),
            "Similarity3f[].lz4",
            "Array of Similarity3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Represents a Similarity Transformation in 3D that is composed of a Uniform Scale and a subsequent Euclidean transformation (3D rotation Rot and a subsequent translation by a 3D vector Trans). This is an angle preserving Transformation.
        /// </summary>
        public static readonly Def Similarity3d = new(
            new Guid("ede4cbad-f68b-4c8e-bd72-119025bd4e5d"),
            "Similarity3d",
            "Represents a Similarity Transformation in 3D that is composed of a Uniform Scale and a subsequent Euclidean transformation (3D rotation Rot and a subsequent translation by a 3D vector Trans). This is an angle preserving Transformation.",
            None,
            false
            );

        /// <summary>
        /// Array of Similarity3d.
        /// </summary>
        public static readonly Def Similarity3dArray = new(
            new Guid("a4033261-f55f-48e9-868a-ef70e4603126"),
            "Similarity3d[]",
            "Array of Similarity3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Similarity3dArrayGz = new(
            new Guid("f45074a3-754f-8248-21e2-83c8154d35a3"),
            "Similarity3d[].gz",
            "Array of Similarity3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Similarity3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Similarity3dArrayLz4 = new(
            new Guid("919c80af-4f80-fc3f-4ec3-dc1baf7e96eb"),
            "Similarity3d[].lz4",
            "Array of Similarity3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A trafo is a container for a forward and a backward matrix.
        /// </summary>
        public static readonly Def Trafo2f = new(
            new Guid("8b7ac8b3-0981-48da-ac07-f372e7b5840f"),
            "Trafo2f",
            "A trafo is a container for a forward and a backward matrix.",
            None,
            false
            );

        /// <summary>
        /// Array of Trafo2f.
        /// </summary>
        public static readonly Def Trafo2fArray = new(
            new Guid("9aa73160-6a95-42d6-beac-258c27023449"),
            "Trafo2f[]",
            "Array of Trafo2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Trafo2fArrayGz = new(
            new Guid("1205ef8a-cb67-6a0f-ce5e-79ea327543f2"),
            "Trafo2f[].gz",
            "Array of Trafo2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Trafo2fArrayLz4 = new(
            new Guid("ec5f7e2e-2c4d-ffe6-99af-b1687bd3c717"),
            "Trafo2f[].lz4",
            "Array of Trafo2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A trafo is a container for a forward and a backward matrix.
        /// </summary>
        public static readonly Def Trafo2d = new(
            new Guid("d8cf8bc2-54e0-4aba-9d76-c7741824e20e"),
            "Trafo2d",
            "A trafo is a container for a forward and a backward matrix.",
            None,
            false
            );

        /// <summary>
        /// Array of Trafo2d.
        /// </summary>
        public static readonly Def Trafo2dArray = new(
            new Guid("406a6e3e-e266-44dc-93fa-2d5896b49f40"),
            "Trafo2d[]",
            "Array of Trafo2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Trafo2dArrayGz = new(
            new Guid("6a7243db-7a52-0cd1-6a0c-5cb1ec2f00f0"),
            "Trafo2d[].gz",
            "Array of Trafo2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Trafo2dArrayLz4 = new(
            new Guid("0a75f387-2507-f675-2c07-63d308afab0c"),
            "Trafo2d[].lz4",
            "Array of Trafo2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A trafo is a container for a forward and a backward matrix.
        /// </summary>
        public static readonly Def Trafo3f = new(
            new Guid("57b5db05-9ee7-4318-b533-a69890d3796e"),
            "Trafo3f",
            "A trafo is a container for a forward and a backward matrix.",
            None,
            false
            );

        /// <summary>
        /// Array of Trafo3f.
        /// </summary>
        public static readonly Def Trafo3fArray = new(
            new Guid("88cf99c1-33f2-429a-826e-155f1ba85703"),
            "Trafo3f[]",
            "Array of Trafo3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Trafo3fArrayGz = new(
            new Guid("d5aaadcb-ab0a-dc8c-633f-62555eebb09f"),
            "Trafo3f[].gz",
            "Array of Trafo3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Trafo3fArrayLz4 = new(
            new Guid("596a0ea1-0eb2-5cca-8891-fade1d29c3c2"),
            "Trafo3f[].lz4",
            "Array of Trafo3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A trafo is a container for a forward and a backward matrix.
        /// </summary>
        public static readonly Def Trafo3d = new(
            new Guid("fcec8029-b8b0-432d-990d-2bd3683b53c5"),
            "Trafo3d",
            "A trafo is a container for a forward and a backward matrix.",
            None,
            false
            );

        /// <summary>
        /// Array of Trafo3d.
        /// </summary>
        public static readonly Def Trafo3dArray = new(
            new Guid("6400725a-1e2a-4653-a5f7-e6116157618f"),
            "Trafo3d[]",
            "Array of Trafo3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Trafo3dArrayGz = new(
            new Guid("4bc092c2-67e1-f0d5-fb41-e18c46f1972c"),
            "Trafo3d[].gz",
            "Array of Trafo3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Trafo3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Trafo3dArrayLz4 = new(
            new Guid("0a088520-1c82-6078-7dab-c51d42355beb"),
            "Trafo3d[].lz4",
            "Array of Trafo3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of UInt8.
        /// </summary>
        public static readonly Def Range1b = new(
            new Guid("db31d0e0-2c56-48da-a769-5a2c1abad38c"),
            "Range1b",
            "A 1-dim range with limits [Min, Max] of UInt8.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1b.
        /// </summary>
        public static readonly Def Range1bArray = new(
            new Guid("b720aba1-6b8a-4431-8bea-8b2dad497358"),
            "Range1b[]",
            "Array of Range1b.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1b. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1bArrayGz = new(
            new Guid("b81b62bb-9c81-cf67-875c-438031de872a"),
            "Range1b[].gz",
            "Array of Range1b. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1b. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1bArrayLz4 = new(
            new Guid("98e57065-fb7a-2c7a-5d81-da824b067061"),
            "Range1b[].lz4",
            "Array of Range1b. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Int8.
        /// </summary>
        public static readonly Def Range1sb = new(
            new Guid("59e5322f-1677-47e4-b991-3e87e43ac005"),
            "Range1sb",
            "A 1-dim range with limits [Min, Max] of Int8.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1sb.
        /// </summary>
        public static readonly Def Range1sbArray = new(
            new Guid("6b769f32-b462-4a8a-a9eb-d908f650dae2"),
            "Range1sb[]",
            "Array of Range1sb.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1sb. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1sbArrayGz = new(
            new Guid("8d8714dc-d817-0f2c-7e00-f717e41fe818"),
            "Range1sb[].gz",
            "Array of Range1sb. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1sb. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1sbArrayLz4 = new(
            new Guid("dc919b90-8e14-6d9d-f6d9-2ed8db9100ab"),
            "Range1sb[].lz4",
            "Array of Range1sb. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Int16.
        /// </summary>
        public static readonly Def Range1s = new(
            new Guid("ed0450e7-2a14-4fe6-b3ac-f4a8ee314fad"),
            "Range1s",
            "A 1-dim range with limits [Min, Max] of Int16.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1s.
        /// </summary>
        public static readonly Def Range1sArray = new(
            new Guid("f8ce22da-f877-45a9-9f69-129473d22809"),
            "Range1s[]",
            "Array of Range1s.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1s. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1sArrayGz = new(
            new Guid("3ed7a8bf-67c1-f7ee-9d4a-5aaf9065d676"),
            "Range1s[].gz",
            "Array of Range1s. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1s. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1sArrayLz4 = new(
            new Guid("683f4f6d-6ee4-31df-a0dc-ca769377214e"),
            "Range1s[].lz4",
            "Array of Range1s. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of UInt16.
        /// </summary>
        public static readonly Def Range1us = new(
            new Guid("7809e939-1d9b-4033-9b7e-7459a2e53b73"),
            "Range1us",
            "A 1-dim range with limits [Min, Max] of UInt16.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1us.
        /// </summary>
        public static readonly Def Range1usArray = new(
            new Guid("9aad8600-41f0-4f83-a431-de44c4031e9a"),
            "Range1us[]",
            "Array of Range1us.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1us. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1usArrayGz = new(
            new Guid("125a4fe7-aece-f3c8-ebd3-9e65df6565c7"),
            "Range1us[].gz",
            "Array of Range1us. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1us. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1usArrayLz4 = new(
            new Guid("8f38110b-66eb-c700-cba4-bd1d5ab07364"),
            "Range1us[].lz4",
            "Array of Range1us. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Int32.
        /// </summary>
        public static readonly Def Range1i = new(
            new Guid("06fad1c2-33a1-4962-92af-19a7c84560a9"),
            "Range1i",
            "A 1-dim range with limits [Min, Max] of Int32.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1i.
        /// </summary>
        public static readonly Def Range1iArray = new(
            new Guid("4c1a43f6-23ba-49aa-9cbf-b308d8524850"),
            "Range1i[]",
            "Array of Range1i.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1i. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1iArrayGz = new(
            new Guid("c12e4177-f392-f72e-df92-1acb7f3c9cd6"),
            "Range1i[].gz",
            "Array of Range1i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1i. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1iArrayLz4 = new(
            new Guid("4840b0d0-c66b-de9e-ea1a-c42cfc2fa2ce"),
            "Range1i[].lz4",
            "Array of Range1i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of UInt32.
        /// </summary>
        public static readonly Def Range1ui = new(
            new Guid("7ff2c8c9-9c4d-4fb2-a750-f07338ebe0b5"),
            "Range1ui",
            "A 1-dim range with limits [Min, Max] of UInt32.",
            None,
            false
            );

        /// <summary>
        /// Array of Range1ui.
        /// </summary>
        public static readonly Def Range1uiArray = new(
            new Guid("a9688eae-5f72-48e3-be92-c214dcf106ca"),
            "Range1ui[]",
            "Array of Range1ui.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1ui. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1uiArrayGz = new(
            new Guid("5e0bd3fb-0219-0dfd-54e4-13606e25dc71"),
            "Range1ui[].gz",
            "Array of Range1ui. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1ui. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1uiArrayLz4 = new(
            new Guid("58e9e48e-48ab-5d6a-5308-673c2fbb3211"),
            "Range1ui[].lz4",
            "Array of Range1ui. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Int64
        /// </summary>
        public static readonly Def Range1l = new(
            new Guid("03ac4568-a97b-4ca6-b005-587cd9afde75"),
            "Range1l",
            "A 1-dim range with limits [Min, Max] of Int64",
            None,
            false
            );

        /// <summary>
        /// Array of Range1l.
        /// </summary>
        public static readonly Def Range1lArray = new(
            new Guid("85f5ed95-0b6a-4ea9-a4f1-38055a93781f"),
            "Range1l[]",
            "Array of Range1l.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1l. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1lArrayGz = new(
            new Guid("2ac104d6-d29e-4265-5a81-807e9d184957"),
            "Range1l[].gz",
            "Array of Range1l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1l. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1lArrayLz4 = new(
            new Guid("401efb41-0a50-5a45-3a97-9cc6810e40dd"),
            "Range1l[].lz4",
            "Array of Range1l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of UInt64
        /// </summary>
        public static readonly Def Range1ul = new(
            new Guid("b7e36341-3dbb-47a0-b5c7-2d199f8d909b"),
            "Range1ul",
            "A 1-dim range with limits [Min, Max] of UInt64",
            None,
            false
            );

        /// <summary>
        /// Array of Range1ul.
        /// </summary>
        public static readonly Def Range1ulArray = new(
            new Guid("7b733797-ee9a-4432-bfd4-4d428e3710d9"),
            "Range1ul[]",
            "Array of Range1ul.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1ul. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1ulArrayGz = new(
            new Guid("2909e12e-1a1f-e037-1295-247721c28c1c"),
            "Range1ul[].gz",
            "Array of Range1ul. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1ul. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1ulArrayLz4 = new(
            new Guid("342d0e9d-ce45-5107-2120-78c52b094116"),
            "Range1ul[].lz4",
            "Array of Range1ul. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Float32
        /// </summary>
        public static readonly Def Range1f = new(
            new Guid("f5b3c83b-a294-4f40-90aa-4abd7c627e95"),
            "Range1f",
            "A 1-dim range with limits [Min, Max] of Float32",
            None,
            false
            );

        /// <summary>
        /// Array of Range1f.
        /// </summary>
        public static readonly Def Range1fArray = new(
            new Guid("7406e3c8-c5b0-465f-afa9-9104b2387462"),
            "Range1f[]",
            "Array of Range1f.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1f. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1fArrayGz = new(
            new Guid("82a5ac50-eaa2-1b4b-8b57-f2d50016002f"),
            "Range1f[].gz",
            "Array of Range1f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1fArrayLz4 = new(
            new Guid("85fb8803-6470-2c13-018b-f1a0681cec84"),
            "Range1f[].lz4",
            "Array of Range1f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 1-dim range with limits [Min, Max] of Float64
        /// </summary>
        public static readonly Def Range1d = new(
            new Guid("b82bff3c-d075-4d16-85d5-0be5b31a9465"),
            "Range1d",
            "A 1-dim range with limits [Min, Max] of Float64",
            None,
            false
            );

        /// <summary>
        /// Array of Range1d.
        /// </summary>
        public static readonly Def Range1dArray = new(
            new Guid("91420eb9-069d-47da-9911-058e12c6ad43"),
            "Range1d[]",
            "Array of Range1d.",
            None,
            true
            );

        /// <summary>
        /// Array of Range1d. Compressed (GZip).
        /// </summary>
        public static readonly Def Range1dArrayGz = new(
            new Guid("df97bbb3-c466-25ce-be00-2597fa758d0a"),
            "Range1d[].gz",
            "Array of Range1d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Range1d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Range1dArrayLz4 = new(
            new Guid("25e093bb-58f4-3157-f197-e66284da50eb"),
            "Range1d[].lz4",
            "Array of Range1d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim axis-aligned box with limits [Min, Max] of V2i.
        /// </summary>
        public static readonly Def Box2i = new(
            new Guid("0edba5a6-1cec-401f-8d98-78bb4b3319e5"),
            "Box2i",
            "A 2-dim axis-aligned box with limits [Min, Max] of V2i.",
            None,
            false
            );

        /// <summary>
        /// Array of Box2i.
        /// </summary>
        public static readonly Def Box2iArray = new(
            new Guid("16afc42c-d4fd-4988-bd38-a039608ce612"),
            "Box2i[]",
            "Array of Box2i.",
            None,
            true
            );

        /// <summary>
        /// Array of Box2i. Compressed (GZip).
        /// </summary>
        public static readonly Def Box2iArrayGz = new(
            new Guid("17e2ab65-9e2e-0361-f195-eb8742319f98"),
            "Box2i[].gz",
            "Array of Box2i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box2i. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box2iArrayLz4 = new(
            new Guid("32be572b-dc42-c0ff-0a6f-1fd468224446"),
            "Box2i[].lz4",
            "Array of Box2i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim axis-aligned box with limits [Min, Max] of V2l.
        /// </summary>
        public static readonly Def Box2l = new(
            new Guid("380422d0-0428-47a6-aeb3-3ab328e21bef"),
            "Box2l",
            "A 2-dim axis-aligned box with limits [Min, Max] of V2l.",
            None,
            false
            );

        /// <summary>
        /// Array of Box2l.
        /// </summary>
        public static readonly Def Box2lArray = new(
            new Guid("062de2a5-1bdc-4b62-8a58-50d2f002676f"),
            "Box2l[]",
            "Array of Box2l.",
            None,
            true
            );

        /// <summary>
        /// Array of Box2l. Compressed (GZip).
        /// </summary>
        public static readonly Def Box2lArrayGz = new(
            new Guid("998c12a1-ce64-89de-a365-befbc970ef86"),
            "Box2l[].gz",
            "Array of Box2l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box2l. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box2lArrayLz4 = new(
            new Guid("9fb8b8d1-588f-f704-6f02-f7646666139e"),
            "Box2l[].lz4",
            "Array of Box2l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim axis-aligned box with limits [Min, Max] of V2f.
        /// </summary>
        public static readonly Def Box2f = new(
            new Guid("414d504d-f350-439b-a73a-4fcc38aafa89"),
            "Box2f",
            "A 2-dim axis-aligned box with limits [Min, Max] of V2f.",
            None,
            false
            );

        /// <summary>
        /// Array of Box2f.
        /// </summary>
        public static readonly Def Box2fArray = new(
            new Guid("8a515f76-89cb-45e5-9c3e-81afecab0dad"),
            "Box2f[]",
            "Array of Box2f.",
            None,
            true
            );

        /// <summary>
        /// Array of Box2f. Compressed (GZip).
        /// </summary>
        public static readonly Def Box2fArrayGz = new(
            new Guid("29b2f391-2c4e-21e4-acaa-d98236b95a30"),
            "Box2f[].gz",
            "Array of Box2f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box2f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box2fArrayLz4 = new(
            new Guid("a4a5efa6-6d17-e382-ee8f-ddca300d164e"),
            "Box2f[].lz4",
            "Array of Box2f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2-dim axis-aligned box with limits [Min, Max] of V2d.
        /// </summary>
        public static readonly Def Box2d = new(
            new Guid("2fb054de-db29-4c1c-bc97-5a0cce4bc291"),
            "Box2d",
            "A 2-dim axis-aligned box with limits [Min, Max] of V2d.",
            None,
            false
            );

        /// <summary>
        /// Array of Box2d.
        /// </summary>
        public static readonly Def Box2dArray = new(
            new Guid("e2f9e9a9-c78f-436c-89e3-da69efefb9ec"),
            "Box2d[]",
            "Array of Box2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Box2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Box2dArrayGz = new(
            new Guid("ad4d70a9-d6f1-177c-5b90-f766ceb0ace2"),
            "Box2d[].gz",
            "Array of Box2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box2dArrayLz4 = new(
            new Guid("cd9951d4-83d9-9a8a-671d-8bc3ae5997ad"),
            "Box2d[].lz4",
            "Array of Box2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim axis-aligned box with limits [Min, Max] of V3i.
        /// </summary>
        public static readonly Def Box3i = new(
            new Guid("c1301768-a349-489d-907e-a8967166cd7c"),
            "Box3i",
            "A 3-dim axis-aligned box with limits [Min, Max] of V3i.",
            None,
            false
            );

        /// <summary>
        /// Array of Box3i.
        /// </summary>
        public static readonly Def Box3iArray = new(
            new Guid("c5bfc96a-fcf7-47d9-9c29-b403734403c4"),
            "Box3i[]",
            "Array of Box3i.",
            None,
            true
            );

        /// <summary>
        /// Array of Box3i. Compressed (GZip).
        /// </summary>
        public static readonly Def Box3iArrayGz = new(
            new Guid("513ac103-4fe2-29da-82ff-1ec76895e0bc"),
            "Box3i[].gz",
            "Array of Box3i. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box3i. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box3iArrayLz4 = new(
            new Guid("518e0d8e-8913-cb03-6bd4-128b4d6ac14a"),
            "Box3i[].lz4",
            "Array of Box3i. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim axis-aligned box with limits [Min, Max] of V3l.
        /// </summary>
        public static readonly Def Box3l = new(
            new Guid("b22529e1-926a-4312-bb5c-3bc63700e4ac"),
            "Box3l",
            "A 3-dim axis-aligned box with limits [Min, Max] of V3l.",
            None,
            false
            );

        /// <summary>
        /// Array of Box3l.
        /// </summary>
        public static readonly Def Box3lArray = new(
            new Guid("0d57063d-cf07-4830-b678-ce7ad7b0e6a1"),
            "Box3l[]",
            "Array of Box3l.",
            None,
            true
            );

        /// <summary>
        /// Array of Box3l. Compressed (GZip).
        /// </summary>
        public static readonly Def Box3lArrayGz = new(
            new Guid("281f1f9f-c9a9-8eee-0a34-a6ab8b64e879"),
            "Box3l[].gz",
            "Array of Box3l. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box3l. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box3lArrayLz4 = new(
            new Guid("9611f89b-3502-a661-a228-fab76d1930e1"),
            "Box3l[].lz4",
            "Array of Box3l. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim axis-aligned box with limits [Min, Max] of V3f.
        /// </summary>
        public static readonly Def Box3f = new(
            new Guid("416721ca-6df1-4ada-b7ad-1da7256f490d"),
            "Box3f",
            "A 3-dim axis-aligned box with limits [Min, Max] of V3f.",
            None,
            false
            );

        /// <summary>
        /// Array of Box3f.
        /// </summary>
        public static readonly Def Box3fArray = new(
            new Guid("82b363ce-f626-4ac3-bd69-038874f4b661"),
            "Box3f[]",
            "Array of Box3f.",
            None,
            true
            );

        /// <summary>
        /// Array of Box3f. Compressed (GZip).
        /// </summary>
        public static readonly Def Box3fArrayGz = new(
            new Guid("89121914-9e33-d96c-6666-dab567742a3a"),
            "Box3f[].gz",
            "Array of Box3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box3fArrayLz4 = new(
            new Guid("98998f65-c7ad-e774-c7a6-695c27304db2"),
            "Box3f[].lz4",
            "Array of Box3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3-dim axis-aligned box with limits [Min, Max] of V3d.
        /// </summary>
        public static readonly Def Box3d = new(
            new Guid("5926f1ce-37fb-4022-a6e5-536b22ad79ea"),
            "Box3d",
            "A 3-dim axis-aligned box with limits [Min, Max] of V3d.",
            None,
            false
            );

        /// <summary>
        /// Array of Box3d.
        /// </summary>
        public static readonly Def Box3dArray = new(
            new Guid("59e43b45-5221-4127-8da9-0b28c07a5b22"),
            "Box3d[]",
            "Array of Box3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Box3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Box3dArrayGz = new(
            new Guid("4485f6e7-b92f-3405-e2c3-fbce225b33c3"),
            "Box3d[].gz",
            "Array of Box3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Box3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Box3dArrayLz4 = new(
            new Guid("91741753-d6bf-55e2-2c6c-a84f44dac429"),
            "Box3d[].lz4",
            "Array of Box3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 28 bytes.
        /// </summary>
        public static readonly Def Cell = new(
            new Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6"),
            "Cell",
            "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 28 bytes.",
            None,
            false
            );

        /// <summary>
        /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 32 bytes (with 4 bytes of padding).
        /// </summary>
        public static readonly Def CellPadded32 = new(
            new Guid("8665c4d4-69c1-4b47-a493-ce452e075643"),
            "CellPadded32",
            "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 32 bytes (with 4 bytes of padding).",
            None,
            false
            );

        /// <summary>
        /// Array of CellPadded32.
        /// </summary>
        public static readonly Def CellPadded32Array = new(
            new Guid("9c2e3d4f-7a40-4266-a2dc-bfbde780260a"),
            "CellPadded32[]",
            "Array of CellPadded32.",
            None,
            true
            );

        /// <summary>
        /// Array of CellPadded32. Compressed (GZip).
        /// </summary>
        public static readonly Def CellPadded32ArrayGz = new(
            new Guid("be18bddb-3489-19ec-d1ab-9c8c97240fa3"),
            "CellPadded32[].gz",
            "Array of CellPadded32. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CellPadded32. Compressed (LZ4).
        /// </summary>
        public static readonly Def CellPadded32ArrayLz4 = new(
            new Guid("22dcd02e-0766-d03c-c3c8-7d92d16e9e6c"),
            "CellPadded32[].lz4",
            "Array of CellPadded32. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way.
        /// </summary>
        public static readonly Def CellArray = new(
            new Guid("2732639f-20b2-46dc-8d54-007a2ef2d2ea"),
            "Cell[]",
            "fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way.",
            CellPadded32Array.Id,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way. Compressed (GZip).
        /// </summary>
        public static readonly Def CellArrayGz = new(
            new Guid("35c2c67c-f7d4-2f48-724a-307315b5b2c9"),
            "Cell[].gz",
            "fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way. Compressed (GZip).",
            CellPadded32ArrayGz.Id,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way. Compressed (LZ4).
        /// </summary>
        public static readonly Def CellArrayLz4 = new(
            new Guid("58a4f10e-7fb9-cf6b-9f98-db71f0dee09e"),
            "Cell[].lz4",
            "fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way. Compressed (LZ4).",
            CellPadded32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 20 bytes.
        /// </summary>
        public static readonly Def Cell2d = new(
            new Guid("9d580e5d-a559-4c5e-9413-7675f1dfe93c"),
            "Cell2d",
            "A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 20 bytes.",
            None,
            false
            );

        /// <summary>
        /// A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 24 bytes (with 4 bytes of padding).
        /// </summary>
        public static readonly Def Cell2dPadded24 = new(
            new Guid("3b022668-faa8-47a9-b622-a7a26060c620"),
            "Cell2dPadded24",
            "A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 24 bytes (with 4 bytes of padding).",
            None,
            false
            );

        /// <summary>
        /// Array of Cell2dPadded24.
        /// </summary>
        public static readonly Def Cell2dPadded24Array = new(
            new Guid("269f0837-a71f-4967-a323-96ccfabbb184"),
            "Cell2dPadded24[]",
            "Array of Cell2dPadded24.",
            None,
            true
            );

        /// <summary>
        /// Array of Cell2dPadded24. Compressed (GZip).
        /// </summary>
        public static readonly Def Cell2dPadded24ArrayGz = new(
            new Guid("1c5f85aa-caaa-c95f-91ba-dc6d8662fa49"),
            "Cell2dPadded24[].gz",
            "Array of Cell2dPadded24. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Cell2dPadded24. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cell2dPadded24ArrayLz4 = new(
            new Guid("35295a61-b3f9-c7dd-6184-53a6f214d121"),
            "Cell2dPadded24[].lz4",
            "Array of Cell2dPadded24. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way.
        /// </summary>
        public static readonly Def Cell2dArray = new(
            new Guid("5c23fd56-3736-4a95-ab74-52b26a711e0e"),
            "Cell2d[]",
            "fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way.",
            Cell2dPadded24Array.Id,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way. Compressed (GZip).
        /// </summary>
        public static readonly Def Cell2dArrayGz = new(
            new Guid("a1d25eff-e511-79a3-0f1e-e619f68bb1fa"),
            "Cell2d[].gz",
            "fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way. Compressed (GZip).",
            Cell2dPadded24ArrayGz.Id,
            true
            );

        /// <summary>
        /// fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cell2dArrayLz4 = new(
            new Guid("7f1dc668-e132-6431-249a-b23f9de244a4"),
            "Cell2d[].lz4",
            "fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way. Compressed (LZ4).",
            Cell2dPadded24ArrayLz4.Id,
            true
            );

        /// <summary>
        /// A color with channels BGR of UInt8.
        /// </summary>
        public static readonly Def C3b = new(
            new Guid("73656667-ea6a-468f-962c-64cd4e24f409"),
            "C3b",
            "A color with channels BGR of UInt8.",
            None,
            false
            );

        /// <summary>
        /// Array of C3b.
        /// </summary>
        public static readonly Def C3bArray = new(
            new Guid("41dde1c8-2b63-4a18-90c8-8f0c67c685b7"),
            "C3b[]",
            "Array of C3b.",
            None,
            true
            );

        /// <summary>
        /// Array of C3b. Compressed (GZip).
        /// </summary>
        public static readonly Def C3bArrayGz = new(
            new Guid("7049ca53-50c9-2ba9-d3dc-49a0d536ba48"),
            "C3b[].gz",
            "Array of C3b. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C3b. Compressed (LZ4).
        /// </summary>
        public static readonly Def C3bArrayLz4 = new(
            new Guid("6028b9d7-5de4-0e57-2a6c-47850d454fb6"),
            "C3b[].lz4",
            "Array of C3b. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGB of UInt16.
        /// </summary>
        public static readonly Def C3us = new(
            new Guid("e606b2fc-5a95-420e-8ddc-66a65e7e31f0"),
            "C3us",
            "A color with channels RGB of UInt16.",
            None,
            false
            );

        /// <summary>
        /// Array of C3us.
        /// </summary>
        public static readonly Def C3usArray = new(
            new Guid("4225db2b-dcb6-458b-bc14-ba5d9e7ab557"),
            "C3us[]",
            "Array of C3us.",
            None,
            true
            );

        /// <summary>
        /// Array of C3us. Compressed (GZip).
        /// </summary>
        public static readonly Def C3usArrayGz = new(
            new Guid("867a854a-bb2f-f284-dfa5-39aedf94ba58"),
            "C3us[].gz",
            "Array of C3us. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C3us. Compressed (LZ4).
        /// </summary>
        public static readonly Def C3usArrayLz4 = new(
            new Guid("dd731949-3398-85ae-0b67-88f65db0622e"),
            "C3us[].lz4",
            "Array of C3us. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGB of UInt32.
        /// </summary>
        public static readonly Def C3ui = new(
            new Guid("0314f5c3-28b8-4523-8a2f-1b75d01d055b"),
            "C3ui",
            "A color with channels RGB of UInt32.",
            None,
            false
            );

        /// <summary>
        /// Array of C3ui.
        /// </summary>
        public static readonly Def C3uiArray = new(
            new Guid("394e46b5-ac4d-44ca-8c48-88b12a974d7c"),
            "C3ui[]",
            "Array of C3ui.",
            None,
            true
            );

        /// <summary>
        /// Array of C3ui. Compressed (GZip).
        /// </summary>
        public static readonly Def C3uiArrayGz = new(
            new Guid("49db6443-7207-b5a3-09be-e4de42af874e"),
            "C3ui[].gz",
            "Array of C3ui. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C3ui. Compressed (LZ4).
        /// </summary>
        public static readonly Def C3uiArrayLz4 = new(
            new Guid("835b2163-73af-24dc-cf4c-996a0f593b38"),
            "C3ui[].lz4",
            "Array of C3ui. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGB of Float32.
        /// </summary>
        public static readonly Def C3f = new(
            new Guid("76873abb-5cd1-46a9-90f6-92db62731bcf"),
            "C3f",
            "A color with channels RGB of Float32.",
            None,
            false
            );

        /// <summary>
        /// Array of C3f.
        /// </summary>
        public static readonly Def C3fArray = new(
            new Guid("e0ffeee7-96a5-4705-9589-2f1bac139068"),
            "C3f[]",
            "Array of C3f.",
            None,
            true
            );

        /// <summary>
        /// Array of C3f. Compressed (GZip).
        /// </summary>
        public static readonly Def C3fArrayGz = new(
            new Guid("1126ebd5-5361-0248-2a36-df8e288a1349"),
            "C3f[].gz",
            "Array of C3f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C3f. Compressed (LZ4).
        /// </summary>
        public static readonly Def C3fArrayLz4 = new(
            new Guid("d7dec669-61fd-c6fc-acca-92a73c9b2da4"),
            "C3f[].lz4",
            "Array of C3f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGB of Float64.
        /// </summary>
        public static readonly Def C3d = new(
            new Guid("b87f7505-d164-45e1-bcde-72304e924abe"),
            "C3d",
            "A color with channels RGB of Float64.",
            None,
            false
            );

        /// <summary>
        /// Array of C3d.
        /// </summary>
        public static readonly Def C3dArray = new(
            new Guid("47eab504-ace3-47d4-8ad7-e5ed8599c01a"),
            "C3d[]",
            "Array of C3d.",
            None,
            true
            );

        /// <summary>
        /// Array of C3d. Compressed (GZip).
        /// </summary>
        public static readonly Def C3dArrayGz = new(
            new Guid("ae38a324-754a-5e83-9173-21500bd4b747"),
            "C3d[].gz",
            "Array of C3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def C3dArrayLz4 = new(
            new Guid("7e1ac931-348e-f670-dc53-ef3d96965150"),
            "C3d[].lz4",
            "Array of C3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels BGRA of UInt8.
        /// </summary>
        public static readonly Def C4b = new(
            new Guid("3f34792b-e03d-4d21-a4a6-4890d5f3f67f"),
            "C4b",
            "A color with channels BGRA of UInt8.",
            None,
            false
            );

        /// <summary>
        /// Array of C4b.
        /// </summary>
        public static readonly Def C4bArray = new(
            new Guid("06318db7-1518-43eb-97c4-ba13c83fc64b"),
            "C4b[]",
            "Array of C4b.",
            None,
            true
            );

        /// <summary>
        /// Array of C4b. Compressed (GZip).
        /// </summary>
        public static readonly Def C4bArrayGz = new(
            new Guid("b18f1210-d19f-b7dc-88ae-61dbfb8f6133"),
            "C4b[].gz",
            "Array of C4b. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C4b. Compressed (LZ4).
        /// </summary>
        public static readonly Def C4bArrayLz4 = new(
            new Guid("621fc0c7-ee10-ea6b-d290-a00885df36a0"),
            "C4b[].lz4",
            "Array of C4b. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGBA of UInt16.
        /// </summary>
        public static readonly Def C4us = new(
            new Guid("85917bcd-00e9-4402-abc7-38c973c96ecc"),
            "C4us",
            "A color with channels RGBA of UInt16.",
            None,
            false
            );

        /// <summary>
        /// Array of C4us.
        /// </summary>
        public static readonly Def C4usArray = new(
            new Guid("f3944349-d463-486b-be25-8bc1764f1323"),
            "C4us[]",
            "Array of C4us.",
            None,
            true
            );

        /// <summary>
        /// Array of C4us. Compressed (GZip).
        /// </summary>
        public static readonly Def C4usArrayGz = new(
            new Guid("a6542064-f970-eb79-8b1e-36ec4c326dc3"),
            "C4us[].gz",
            "Array of C4us. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C4us. Compressed (LZ4).
        /// </summary>
        public static readonly Def C4usArrayLz4 = new(
            new Guid("ca78601c-a0dd-1129-c592-a16c31032379"),
            "C4us[].lz4",
            "Array of C4us. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGBA of UInt32.
        /// </summary>
        public static readonly Def C4ui = new(
            new Guid("7018167d-2316-4ff0-a239-0ebf95c32adf"),
            "C4ui",
            "A color with channels RGBA of UInt32.",
            None,
            false
            );

        /// <summary>
        /// Array of C4ui.
        /// </summary>
        public static readonly Def C4uiArray = new(
            new Guid("1afb8c51-a29a-4919-8c32-a62ade22a857"),
            "C4ui[]",
            "Array of C4ui.",
            None,
            true
            );

        /// <summary>
        /// Array of C4ui. Compressed (GZip).
        /// </summary>
        public static readonly Def C4uiArrayGz = new(
            new Guid("3268e82d-f695-cd18-8165-a51067755586"),
            "C4ui[].gz",
            "Array of C4ui. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C4ui. Compressed (LZ4).
        /// </summary>
        public static readonly Def C4uiArrayLz4 = new(
            new Guid("198e83b0-dee1-8a78-8905-e0d3210473e9"),
            "C4ui[].lz4",
            "Array of C4ui. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGBA of Float32.
        /// </summary>
        public static readonly Def C4f = new(
            new Guid("e09cfff1-b186-42e8-9b3d-6a4325117ba4"),
            "C4f",
            "A color with channels RGBA of Float32.",
            None,
            false
            );

        /// <summary>
        /// Array of C4f.
        /// </summary>
        public static readonly Def C4fArray = new(
            new Guid("4b9b675b-a575-47f0-9b7e-0f49b9904dc5"),
            "C4f[]",
            "Array of C4f.",
            None,
            true
            );

        /// <summary>
        /// Array of C4f. Compressed (GZip).
        /// </summary>
        public static readonly Def C4fArrayGz = new(
            new Guid("26ea96cd-dac6-0ac1-dd89-f3a8a784162f"),
            "C4f[].gz",
            "Array of C4f. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C4f. Compressed (LZ4).
        /// </summary>
        public static readonly Def C4fArrayLz4 = new(
            new Guid("4156d51a-c5e3-2892-1477-c42619bc22ab"),
            "C4f[].lz4",
            "Array of C4f. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color with channels RGBA of Float64.
        /// </summary>
        public static readonly Def C4d = new(
            new Guid("fe74ea05-4b9a-4723-a075-eec853c9cc19"),
            "C4d",
            "A color with channels RGBA of Float64.",
            None,
            false
            );

        /// <summary>
        /// Array of C4d.
        /// </summary>
        public static readonly Def C4dArray = new(
            new Guid("75e7b36b-fb9e-4b9e-b92c-728c69b6feae"),
            "C4d[]",
            "Array of C4d.",
            None,
            true
            );

        /// <summary>
        /// Array of C4d. Compressed (GZip).
        /// </summary>
        public static readonly Def C4dArrayGz = new(
            new Guid("a21504f8-d918-31db-5792-2d97e221fabb"),
            "C4d[].gz",
            "Array of C4d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of C4d. Compressed (LZ4).
        /// </summary>
        public static readonly Def C4dArrayLz4 = new(
            new Guid("4bd34c11-ab77-3b1c-fc59-ba3e95f22465"),
            "C4d[].lz4",
            "Array of C4d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CIELAB color space with channels L,a,b. (Float32).
        /// </summary>
        public static readonly Def CieLabf = new(
            new Guid("8f96c96e-7912-4d21-83d5-2e2b0e54ff99"),
            "CieLabf",
            "A color in CIELAB color space with channels L,a,b. (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CieLabf.
        /// </summary>
        public static readonly Def CieLabfArray = new(
            new Guid("527572b6-70eb-416a-a91a-1750c19b419a"),
            "CieLabf[]",
            "Array of CieLabf.",
            None,
            true
            );

        /// <summary>
        /// Array of CieLabf. Compressed (GZip).
        /// </summary>
        public static readonly Def CieLabfArrayGz = new(
            new Guid("4e469fff-8349-ec45-a922-fac26edfc794"),
            "CieLabf[].gz",
            "Array of CieLabf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CieLabf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CieLabfArrayLz4 = new(
            new Guid("c6411bb9-7c72-de0c-d931-b339faa94325"),
            "CieLabf[].lz4",
            "Array of CieLabf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CIELUV color space with channels L,u,v (Float32).
        /// </summary>
        public static readonly Def CIeLuvf = new(
            new Guid("972c001e-a7a8-45ea-a234-f4788489d6e7"),
            "CIeLuvf",
            "A color in CIELUV color space with channels L,u,v (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CIeLuvf.
        /// </summary>
        public static readonly Def CIeLuvfArray = new(
            new Guid("edb50004-8a3f-4267-bb4a-30e060042b00"),
            "CIeLuvf[]",
            "Array of CIeLuvf.",
            None,
            true
            );

        /// <summary>
        /// Array of CIeLuvf. Compressed (GZip).
        /// </summary>
        public static readonly Def CIeLuvfArrayGz = new(
            new Guid("76a30dfe-c986-0cbc-61cf-a54fd1b98224"),
            "CIeLuvf[].gz",
            "Array of CIeLuvf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CIeLuvf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CIeLuvfArrayLz4 = new(
            new Guid("2e6e21f5-398e-4cfb-2fd8-075a709195a0"),
            "CIeLuvf[].lz4",
            "Array of CIeLuvf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CIELUV color space with channels L,u,v (Float32).
        /// </summary>
        public static readonly Def CieLuvf = new(
            new Guid("805bc7f1-f5d3-41c1-b9a8-958efa75ea7a"),
            "CieLuvf",
            "A color in CIELUV color space with channels L,u,v (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CieLuvf.
        /// </summary>
        public static readonly Def CieLuvfArray = new(
            new Guid("617ba25a-cfad-44db-aa38-55805f9b0f65"),
            "CieLuvf[]",
            "Array of CieLuvf.",
            None,
            true
            );

        /// <summary>
        /// Array of CieLuvf. Compressed (GZip).
        /// </summary>
        public static readonly Def CieLuvfArrayGz = new(
            new Guid("a1129545-5986-d759-e7d8-ea8f52f41408"),
            "CieLuvf[].gz",
            "Array of CieLuvf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CieLuvf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CieLuvfArrayLz4 = new(
            new Guid("2e1dfa1e-61df-4d58-e137-c1a254864e37"),
            "CieLuvf[].lz4",
            "Array of CieLuvf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CIEXYZ color space with channels X,Y,Z (Float32).
        /// </summary>
        public static readonly Def CieXYZf = new(
            new Guid("055d4fae-6935-479d-9b8d-04ca1e5cf51d"),
            "CieXYZf",
            "A color in CIEXYZ color space with channels X,Y,Z (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CieXYZf.
        /// </summary>
        public static readonly Def CieXYZfArray = new(
            new Guid("fb71984f-08ff-4e85-b3d8-015302f03f46"),
            "CieXYZf[]",
            "Array of CieXYZf.",
            None,
            true
            );

        /// <summary>
        /// Array of CieXYZf. Compressed (GZip).
        /// </summary>
        public static readonly Def CieXYZfArrayGz = new(
            new Guid("b39f8e87-43f0-77e7-9b46-b13403064369"),
            "CieXYZf[].gz",
            "Array of CieXYZf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CieXYZf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CieXYZfArrayLz4 = new(
            new Guid("13f963c0-eb1b-ad2c-3b05-67af7859c658"),
            "CieXYZf[].lz4",
            "Array of CieXYZf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CIE Yxy color space with channels Y,x,y (Float32).
        /// </summary>
        public static readonly Def CieYxyf = new(
            new Guid("ce20671f-b5da-4c32-9bd0-efff870cf6fd"),
            "CieYxyf",
            "A color in CIE Yxy color space with channels Y,x,y (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CieYxyf.
        /// </summary>
        public static readonly Def CieYxyfArray = new(
            new Guid("0b86f282-a051-41a5-848b-5ca82eab5e1d"),
            "CieYxyf[]",
            "Array of CieYxyf.",
            None,
            true
            );

        /// <summary>
        /// Array of CieYxyf. Compressed (GZip).
        /// </summary>
        public static readonly Def CieYxyfArrayGz = new(
            new Guid("b33e5c64-d96c-5b5f-04f3-a0af1920e677"),
            "CieYxyf[].gz",
            "Array of CieYxyf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CieYxyf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CieYxyfArrayLz4 = new(
            new Guid("7c90c860-24b3-4c6d-9c94-859562fe8768"),
            "CieYxyf[].lz4",
            "Array of CieYxyf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in CMYK color space with channels C,M,Y,K (Float32).
        /// </summary>
        public static readonly Def CMYKf = new(
            new Guid("5f97105d-9d07-4149-86b5-0efc23b5be5b"),
            "CMYKf",
            "A color in CMYK color space with channels C,M,Y,K (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of CMYKf.
        /// </summary>
        public static readonly Def CMYKfArray = new(
            new Guid("af1055e0-b898-45aa-908c-87fbe6a54609"),
            "CMYKf[]",
            "Array of CMYKf.",
            None,
            true
            );

        /// <summary>
        /// Array of CMYKf. Compressed (GZip).
        /// </summary>
        public static readonly Def CMYKfArrayGz = new(
            new Guid("8ac860df-2acf-de03-4b36-c83166571870"),
            "CMYKf[].gz",
            "Array of CMYKf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of CMYKf. Compressed (LZ4).
        /// </summary>
        public static readonly Def CMYKfArrayLz4 = new(
            new Guid("dc533164-636a-01e8-ef23-d18be823bc8b"),
            "CMYKf[].lz4",
            "Array of CMYKf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in HSL color space (hue, saturation, value) with channels H,S,L (Float32).
        /// </summary>
        public static readonly Def HSLf = new(
            new Guid("2cc70bfc-a983-4558-92ce-ab0abb3ffa0c"),
            "HSLf",
            "A color in HSL color space (hue, saturation, value) with channels H,S,L (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of HSLf.
        /// </summary>
        public static readonly Def HSLfArray = new(
            new Guid("c12d9316-b9ca-432a-b95b-d3638bff2f41"),
            "HSLf[]",
            "Array of HSLf.",
            None,
            true
            );

        /// <summary>
        /// Array of HSLf. Compressed (GZip).
        /// </summary>
        public static readonly Def HSLfArrayGz = new(
            new Guid("6c2ad39a-c344-81aa-b3f6-49113092df9c"),
            "HSLf[].gz",
            "Array of HSLf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of HSLf. Compressed (LZ4).
        /// </summary>
        public static readonly Def HSLfArrayLz4 = new(
            new Guid("db1fa9bc-ee29-f08e-19c6-d3ebb9e8a2ce"),
            "HSLf[].lz4",
            "Array of HSLf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in HSV color space (hue, saturation, lightness) with channels H,S,V (Float32).
        /// </summary>
        public static readonly Def HSVf = new(
            new Guid("7ab91015-ecf2-4dd2-9690-5ea33efcdbd9"),
            "HSVf",
            "A color in HSV color space (hue, saturation, lightness) with channels H,S,V (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of HSVf.
        /// </summary>
        public static readonly Def HSVfArray = new(
            new Guid("6a658149-2348-4fc4-b572-686555022ff0"),
            "HSVf[]",
            "Array of HSVf.",
            None,
            true
            );

        /// <summary>
        /// Array of HSVf. Compressed (GZip).
        /// </summary>
        public static readonly Def HSVfArrayGz = new(
            new Guid("45908bc2-0801-6169-8cd2-8d884b2ffe3a"),
            "HSVf[].gz",
            "Array of HSVf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of HSVf. Compressed (LZ4).
        /// </summary>
        public static readonly Def HSVfArrayLz4 = new(
            new Guid("f5e97785-a913-619d-b2c1-c07efefab75b"),
            "HSVf[].lz4",
            "Array of HSVf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A color in Yuv color space with channels Y,u,v (Float32).
        /// </summary>
        public static readonly Def Yuvf = new(
            new Guid("41e1befe-4d13-4ef5-a654-8b9b3b740a6f"),
            "Yuvf",
            "A color in Yuv color space with channels Y,u,v (Float32).",
            None,
            false
            );

        /// <summary>
        /// Array of Yuvf.
        /// </summary>
        public static readonly Def YuvfArray = new(
            new Guid("62c46940-8b06-484e-b447-f4e29b74a596"),
            "Yuvf[]",
            "Array of Yuvf.",
            None,
            true
            );

        /// <summary>
        /// Array of Yuvf. Compressed (GZip).
        /// </summary>
        public static readonly Def YuvfArrayGz = new(
            new Guid("3eaafaf9-e8f2-9a4e-f0a8-3bef51b3b63f"),
            "Yuvf[].gz",
            "Array of Yuvf. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Yuvf. Compressed (LZ4).
        /// </summary>
        public static readonly Def YuvfArrayLz4 = new(
            new Guid("3e402451-d15c-d3d7-d534-f6d7a7678a0b"),
            "Yuvf[].lz4",
            "Array of Yuvf. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Data of an Aardvark.Geometry.PointRkdTreeF.
        /// </summary>
        public static readonly Def PointRkdTreeFData = new(
            new Guid("c5e8c7d1-3b0f-4221-8ad8-443ff1994979"),
            "PointRkdTreeFData",
            "Data of an Aardvark.Geometry.PointRkdTreeF.",
            None,
            false
            );

        /// <summary>
        /// Data of an Aardvark.Geometry.PointRkdTreeD.
        /// </summary>
        public static readonly Def PointRkdTreeDData = new(
            new Guid("ad480012-bcfc-4d0c-ae74-c6f629e7fa87"),
            "PointRkdTreeDData",
            "Data of an Aardvark.Geometry.PointRkdTreeD.",
            None,
            false
            );

        /// <summary>
        /// A capsule in 3-space represented by two points and a radius.
        /// </summary>
        public static readonly Def Capsule3d = new(
            new Guid("8b563d8b-6502-4b3f-a217-cacad6d779ea"),
            "Capsule3d",
            "A capsule in 3-space represented by two points and a radius.",
            None,
            false
            );

        /// <summary>
        /// Array of Capsule3d.
        /// </summary>
        public static readonly Def Capsule3dArray = new(
            new Guid("aa31738b-2cab-4843-b189-f9d31550438f"),
            "Capsule3d[]",
            "Array of Capsule3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Capsule3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Capsule3dArrayGz = new(
            new Guid("293ec2a8-f0e4-b346-3dfb-59c2c0f58137"),
            "Capsule3d[].gz",
            "Array of Capsule3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Capsule3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Capsule3dArrayLz4 = new(
            new Guid("ad7a3972-229c-3341-db53-f1cb50fc2498"),
            "Capsule3d[].lz4",
            "Array of Capsule3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A two dimensional circle represented by center and radius.
        /// </summary>
        public static readonly Def Circle2d = new(
            new Guid("32dffbbb-fd04-4e62-85ea-fc3ae8889c61"),
            "Circle2d",
            "A two dimensional circle represented by center and radius.",
            None,
            false
            );

        /// <summary>
        /// Array of Circle2d.
        /// </summary>
        public static readonly Def Circle2dArray = new(
            new Guid("6d7ce781-f3bb-4139-95ec-6cd885c36aee"),
            "Circle2d[]",
            "Array of Circle2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Circle2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Circle2dArrayGz = new(
            new Guid("562c6d82-227d-e15a-8596-eedc0c932e8d"),
            "Circle2d[].gz",
            "Array of Circle2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Circle2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Circle2dArrayLz4 = new(
            new Guid("5d907d4e-b931-ec87-26c4-87fa0b3c9e6b"),
            "Circle2d[].lz4",
            "Array of Circle2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A circle in 3-space represented by its center, normal (normalized), and a radius.
        /// </summary>
        public static readonly Def Circle3d = new(
            new Guid("91265f74-e53b-4154-a370-e94796078a24"),
            "Circle3d",
            "A circle in 3-space represented by its center, normal (normalized), and a radius.",
            None,
            false
            );

        /// <summary>
        /// Array of Circle3d.
        /// </summary>
        public static readonly Def Circle3dArray = new(
            new Guid("0959298f-ca32-4b0f-9850-840fedd983ae"),
            "Circle3d[]",
            "Array of Circle3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Circle3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Circle3dArrayGz = new(
            new Guid("b95be024-d8ab-14a4-e1b1-13de3ac7a588"),
            "Circle3d[].gz",
            "Array of Circle3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Circle3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Circle3dArrayLz4 = new(
            new Guid("c802c7fd-918f-8967-2ce6-55391fd903ee"),
            "Circle3d[].lz4",
            "Array of Circle3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// An oblique cone in 3-space represented by its origin (apex) and base circle.
        /// </summary>
        public static readonly Def ObliqueCone3d = new(
            new Guid("cfb67319-ecdd-4642-b043-b0e41ce741db"),
            "ObliqueCone3d",
            "An oblique cone in 3-space represented by its origin (apex) and base circle.",
            None,
            false
            );

        /// <summary>
        /// Array of ObliqueCone3d.
        /// </summary>
        public static readonly Def ObliqueCone3dArray = new(
            new Guid("d0e95202-005d-4795-b71f-8b406cf04a43"),
            "ObliqueCone3d[]",
            "Array of ObliqueCone3d.",
            None,
            true
            );

        /// <summary>
        /// Array of ObliqueCone3d. Compressed (GZip).
        /// </summary>
        public static readonly Def ObliqueCone3dArrayGz = new(
            new Guid("f6ba5830-2e7c-90ff-c071-2e1f7ddb376e"),
            "ObliqueCone3d[].gz",
            "Array of ObliqueCone3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of ObliqueCone3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def ObliqueCone3dArrayLz4 = new(
            new Guid("16fd3e79-3656-e267-59d4-74b2af7ba6df"),
            "ObliqueCone3d[].lz4",
            "Array of ObliqueCone3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A cone in 3-space represented by its origin, axis of revolution (Direction), and the angle between axis and outer edge.
        /// </summary>
        public static readonly Def Cone3d = new(
            new Guid("b624ffb1-ef11-4776-b14a-4d34a5314230"),
            "Cone3d",
            "A cone in 3-space represented by its origin, axis of revolution (Direction), and the angle between axis and outer edge.",
            None,
            false
            );

        /// <summary>
        /// Array of Cone3d.
        /// </summary>
        public static readonly Def Cone3dArray = new(
            new Guid("38e3e52a-ac45-4697-83ea-86b580ff1596"),
            "Cone3d[]",
            "Array of Cone3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Cone3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Cone3dArrayGz = new(
            new Guid("1726ddcd-1c99-7521-b9da-4b7e3d3282f6"),
            "Cone3d[].gz",
            "Array of Cone3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Cone3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cone3dArrayLz4 = new(
            new Guid("c04a3f39-829b-73c7-d784-ece58fb87d61"),
            "Cone3d[].lz4",
            "Array of Cone3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Deprecated 2022-03-02. Obsolete field DistanceScale. Use 0b0e6173-393a-4a85-855a-a5f4d5316b36 instead. A cylinder in 3-space.
        /// </summary>
        [Obsolete("Deprecated 2022-03-02. Obsolete field DistanceScale. Use 0b0e6173-393a-4a85-855a-a5f4d5316b36 instead.")]
        public static readonly Def Cylinder3dDeprecated20220302 = new(
            new Guid("6982d0a3-1cb4-4c9f-b479-6ebd3926bf7b"),
            "Aardvark.Cylinder3d.Deprecated.20220302",
            "Deprecated 2022-03-02. Obsolete field DistanceScale. Use 0b0e6173-393a-4a85-855a-a5f4d5316b36 instead. A cylinder in 3-space.",
            None,
            false
            );

        /// <summary>
        /// Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead.
        /// </summary>
        [Obsolete("Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead.")]
        public static readonly Def Cylinder3dDeprecated20220302Array = new(
            new Guid("8f3cbbad-9d20-4db2-9831-9644733a7bc5"),
            "Aardvark.Cylinder3d.Deprecated.20220302[]",
            "Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead.",
            None,
            true
            );

        /// <summary>
        /// Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead. Compressed (GZip).
        /// </summary>
        [Obsolete("Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead.")]
        public static readonly Def Cylinder3dDeprecated20220302ArrayGz = new(
            new Guid("d22c24dd-cd0d-2dfe-a144-4269d4d268ab"),
            "Aardvark.Cylinder3d.Deprecated.20220302[].gz",
            "Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead. Compressed (LZ4).
        /// </summary>
        [Obsolete("Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead.")]
        public static readonly Def Cylinder3dDeprecated20220302ArrayLz4 = new(
            new Guid("14836555-4dab-fae0-84ef-f40067cc1979"),
            "Aardvark.Cylinder3d.Deprecated.20220302[].lz4",
            "Deprecated 2022-03-02. Obsolete field DistanceScale. Use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A cylinder in 3-space.
        /// </summary>
        public static readonly Def Cylinder3d = new(
            new Guid("0b0e6173-393a-4a85-855a-a5f4d5316b36"),
            "Cylinder3d",
            "A cylinder in 3-space.",
            None,
            false
            );

        /// <summary>
        /// Array of Cylinder3d.
        /// </summary>
        public static readonly Def Cylinder3dArray = new(
            new Guid("95ada0ae-0243-43d2-8e99-d0aaea843ae8"),
            "Cylinder3d[]",
            "Array of Cylinder3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Cylinder3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Cylinder3dArrayGz = new(
            new Guid("bff8422f-87db-e2d8-fd66-944b0f2793ca"),
            "Cylinder3d[].gz",
            "Array of Cylinder3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Cylinder3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cylinder3dArrayLz4 = new(
            new Guid("a8a8d8a8-137b-4e6e-d34b-5e0df12a0de2"),
            "Cylinder3d[].lz4",
            "Array of Cylinder3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A line in 2-space with begin P0 and end P1.
        /// </summary>
        public static readonly Def Line2d = new(
            new Guid("c0fb8306-e4b1-43c4-9236-a7eabbdfb245"),
            "Line2d",
            "A line in 2-space with begin P0 and end P1.",
            None,
            false
            );

        /// <summary>
        /// Array of Line2d.
        /// </summary>
        public static readonly Def Line2dArray = new(
            new Guid("01c73cac-5fe7-4ca2-9756-2e2ad33c6338"),
            "Line2d[]",
            "Array of Line2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Line2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Line2dArrayGz = new(
            new Guid("f31484a8-ea72-724e-e151-411e0e6261ce"),
            "Line2d[].gz",
            "Array of Line2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Line2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Line2dArrayLz4 = new(
            new Guid("762a891e-3af3-8e0b-4b2f-8fa6d0f4ff98"),
            "Line2d[].lz4",
            "Array of Line2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A line in 3-space with begin P0 and end P1.
        /// </summary>
        public static readonly Def Line3d = new(
            new Guid("d3c05a9b-03f9-40f0-b169-401532c5d068"),
            "Line3d",
            "A line in 3-space with begin P0 and end P1.",
            None,
            false
            );

        /// <summary>
        /// Array of Line3d.
        /// </summary>
        public static readonly Def Line3dArray = new(
            new Guid("1ace6b08-0376-4c73-8479-3f4edcac48f1"),
            "Line3d[]",
            "Array of Line3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Line3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Line3dArrayGz = new(
            new Guid("147c450e-9865-13a1-b4d7-e923b337ce85"),
            "Line3d[].gz",
            "Array of Line3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Line3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Line3dArrayLz4 = new(
            new Guid("af1fbd46-232e-5472-c0f3-3cad74758a30"),
            "Line3d[].lz4",
            "Array of Line3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A triangle in 2-space with points defined by three points P0, P1, P2.
        /// </summary>
        public static readonly Def Triangle2d = new(
            new Guid("37d849fc-e0c9-4d9f-8516-461123362692"),
            "Triangle2d",
            "A triangle in 2-space with points defined by three points P0, P1, P2.",
            None,
            false
            );

        /// <summary>
        /// Array of Triangle2d.
        /// </summary>
        public static readonly Def Triangle2dArray = new(
            new Guid("a08840e3-7c4c-4a59-8f30-45926c5e6663"),
            "Triangle2d[]",
            "Array of Triangle2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Triangle2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Triangle2dArrayGz = new(
            new Guid("c72e6888-3417-4a0f-6fd2-22d5b9d82071"),
            "Triangle2d[].gz",
            "Array of Triangle2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Triangle2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Triangle2dArrayLz4 = new(
            new Guid("5575bcda-8273-88a5-d8da-0f93ba31086e"),
            "Triangle2d[].lz4",
            "Array of Triangle2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A triangle in 3-space with points defined by three points P0, P1, P2.
        /// </summary>
        public static readonly Def Triangle3d = new(
            new Guid("3a52c3ad-61b4-48a8-b402-97c654fe7f80"),
            "Triangle3d",
            "A triangle in 3-space with points defined by three points P0, P1, P2.",
            None,
            false
            );

        /// <summary>
        /// Array of Triangle3d.
        /// </summary>
        public static readonly Def Triangle3dArray = new(
            new Guid("2b17cea5-e8db-4e86-b5f5-8b401088ca58"),
            "Triangle3d[]",
            "Array of Triangle3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Triangle3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Triangle3dArrayGz = new(
            new Guid("c6fee9c9-55a5-cf16-f073-f5bdff3ae707"),
            "Triangle3d[].gz",
            "Array of Triangle3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Triangle3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Triangle3dArrayLz4 = new(
            new Guid("3384950e-8ca9-d8fa-b026-7e417ea625d4"),
            "Triangle3d[].lz4",
            "Array of Triangle3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A quad in 2-space with points defined by four points P0, P1, P2, P3.
        /// </summary>
        public static readonly Def Quad2d = new(
            new Guid("40dae3a2-0791-4533-8b69-882c3bbbaa6f"),
            "Quad2d",
            "A quad in 2-space with points defined by four points P0, P1, P2, P3.",
            None,
            false
            );

        /// <summary>
        /// Array of Quad2d.
        /// </summary>
        public static readonly Def Quad2dArray = new(
            new Guid("0d23983f-62a1-40d0-96e5-8864283877b9"),
            "Quad2d[]",
            "Array of Quad2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Quad2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Quad2dArrayGz = new(
            new Guid("5033dda8-b0de-4b1b-b000-0581da7fca65"),
            "Quad2d[].gz",
            "Array of Quad2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Quad2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Quad2dArrayLz4 = new(
            new Guid("a0f7c89b-7b5d-2032-0393-1437fb200874"),
            "Quad2d[].lz4",
            "Array of Quad2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A quad in 3-space with points defined by four points P0, P1, P2, P3.
        /// </summary>
        public static readonly Def Quad3d = new(
            new Guid("e234f7f4-09af-4b00-b5cc-a2fad8447aa7"),
            "Quad3d",
            "A quad in 3-space with points defined by four points P0, P1, P2, P3.",
            None,
            false
            );

        /// <summary>
        /// Array of Quad3d.
        /// </summary>
        public static readonly Def Quad3dArray = new(
            new Guid("a0bd7233-a2b7-41e0-ab3e-5a142a67ce5e"),
            "Quad3d[]",
            "Array of Quad3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Quad3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Quad3dArrayGz = new(
            new Guid("e6b945d1-89e9-3677-1706-1a34b8eb001b"),
            "Quad3d[].gz",
            "Array of Quad3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Quad3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Quad3dArrayLz4 = new(
            new Guid("e4432822-5e6c-c22d-24e8-4de24b5feeac"),
            "Quad3d[].lz4",
            "Array of Quad3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 2D ellipse is defined by its center and two half-axes. Note that in principle any two conjugate half-diameters can be used as axes, however some algorithms require that the major and minor half axes are known. By convention in this case, axis0 is the major half axis.
        /// </summary>
        public static readonly Def Ellipse2d = new(
            new Guid("b82f02a5-d0a8-4804-958e-441dab5189d1"),
            "Ellipse2d",
            "A 2D ellipse is defined by its center and two half-axes. Note that in principle any two conjugate half-diameters can be used as axes, however some algorithms require that the major and minor half axes are known. By convention in this case, axis0 is the major half axis.",
            None,
            false
            );

        /// <summary>
        /// Array of Ellipse2d.
        /// </summary>
        public static readonly Def Ellipse2dArray = new(
            new Guid("863d9c94-8594-4f47-adcf-0f2e5cbbe309"),
            "Ellipse2d[]",
            "Array of Ellipse2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Ellipse2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Ellipse2dArrayGz = new(
            new Guid("01fb5c6b-c1a9-5154-4727-c53418435e85"),
            "Ellipse2d[].gz",
            "Array of Ellipse2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Ellipse2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Ellipse2dArrayLz4 = new(
            new Guid("431400cd-de7a-05ad-d4af-0a34c1f7b4ed"),
            "Ellipse2d[].lz4",
            "Array of Ellipse2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A 3D ellipse is defined by its center, its plane normal, and two half-axes. Note that in principle any two conjugate half-diameters can be used as axes, however some algorithms require that the major and minor half axes are known. By convention in this case, axis0 is the major half axis.
        /// </summary>
        public static readonly Def Ellipse3d = new(
            new Guid("f8784424-7d25-4c8a-85bf-370a4452612c"),
            "Ellipse3d",
            "A 3D ellipse is defined by its center, its plane normal, and two half-axes. Note that in principle any two conjugate half-diameters can be used as axes, however some algorithms require that the major and minor half axes are known. By convention in this case, axis0 is the major half axis.",
            None,
            false
            );

        /// <summary>
        /// Array of Ellipse3d.
        /// </summary>
        public static readonly Def Ellipse3dArray = new(
            new Guid("19b7fd2b-7bc5-47cb-92fd-5c2898885499"),
            "Ellipse3d[]",
            "Array of Ellipse3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Ellipse3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Ellipse3dArrayGz = new(
            new Guid("0cf156c4-1676-50f1-864a-44bd59c20dd1"),
            "Ellipse3d[].gz",
            "Array of Ellipse3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Ellipse3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Ellipse3dArrayLz4 = new(
            new Guid("5ab837e0-1809-0d74-686b-72c23fc3dc9e"),
            "Ellipse3d[].lz4",
            "Array of Ellipse3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A three dimensional sphere represented by center and radius.
        /// </summary>
        public static readonly Def Sphere3d = new(
            new Guid("2da2eae2-8fa5-4fe0-8987-0aa295cbe710"),
            "Sphere3d",
            "A three dimensional sphere represented by center and radius.",
            None,
            false
            );

        /// <summary>
        /// Array of Sphere3d.
        /// </summary>
        public static readonly Def Sphere3dArray = new(
            new Guid("7c755f28-1c40-4824-9502-95caf8bda3b9"),
            "Sphere3d[]",
            "Array of Sphere3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Sphere3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Sphere3dArrayGz = new(
            new Guid("1937f27a-3afe-367b-bd41-11d97f97018b"),
            "Sphere3d[].gz",
            "Array of Sphere3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Sphere3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Sphere3dArrayLz4 = new(
            new Guid("53fff1d2-d6d2-50bd-15a0-81b3eaf6d4e9"),
            "Sphere3d[].lz4",
            "Array of Sphere3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A line represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance
        /// </summary>
        public static readonly Def Plane2d = new(
            new Guid("d0293e58-b411-4cc4-b50f-05c6008c22af"),
            "Plane2d",
            "A line represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance",
            None,
            false
            );

        /// <summary>
        /// Array of Plane2d.
        /// </summary>
        public static readonly Def Plane2dArray = new(
            new Guid("c2e69c65-ea56-4052-8d0d-7fcaf31f5545"),
            "Plane2d[]",
            "Array of Plane2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Plane2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Plane2dArrayGz = new(
            new Guid("276e7797-6c6f-6cbb-aaf7-d7be089d0152"),
            "Plane2d[].gz",
            "Array of Plane2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Plane2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Plane2dArrayLz4 = new(
            new Guid("1cc4aefa-729d-c71b-a501-6739195258d1"),
            "Plane2d[].lz4",
            "Array of Plane2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A plane represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance
        /// </summary>
        public static readonly Def Plane3d = new(
            new Guid("7f80cd19-9605-4143-8bdf-a707e0c29f3a"),
            "Plane3d",
            "A plane represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance",
            None,
            false
            );

        /// <summary>
        /// Array of Plane3d.
        /// </summary>
        public static readonly Def Plane3dArray = new(
            new Guid("4fa764ee-af38-4277-96b4-c9464e661d4f"),
            "Plane3d[]",
            "Array of Plane3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Plane3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Plane3dArrayGz = new(
            new Guid("c0f0f833-e58e-2326-197b-c45c96611f72"),
            "Plane3d[].gz",
            "Array of Plane3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Plane3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Plane3dArrayLz4 = new(
            new Guid("87a07f54-4aa6-a7f8-8265-603c4754ad32"),
            "Plane3d[].lz4",
            "Array of Plane3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A two-dimensional ray with an origin and a direction.
        /// </summary>
        public static readonly Def Ray2d = new(
            new Guid("2537391e-9afb-44d8-84c6-3ecce21e471e"),
            "Ray2d",
            "A two-dimensional ray with an origin and a direction.",
            None,
            false
            );

        /// <summary>
        /// Array of Ray2d.
        /// </summary>
        public static readonly Def Ray2dArray = new(
            new Guid("337352c0-318a-4b23-ad44-0f77e900f488"),
            "Ray2d[]",
            "Array of Ray2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Ray2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Ray2dArrayGz = new(
            new Guid("f0e7d5f6-f43d-2833-a2c4-f19c0ba3a790"),
            "Ray2d[].gz",
            "Array of Ray2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Ray2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Ray2dArrayLz4 = new(
            new Guid("ed1eb853-9df0-c94e-f984-eae6206e6f4e"),
            "Ray2d[].lz4",
            "Array of Ray2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A three-dimensional ray with an origin and a direction.
        /// </summary>
        public static readonly Def Ray3d = new(
            new Guid("48e07c48-46af-43b6-a611-78da1077e675"),
            "Ray3d",
            "A three-dimensional ray with an origin and a direction.",
            None,
            false
            );

        /// <summary>
        /// Array of Ray3d.
        /// </summary>
        public static readonly Def Ray3dArray = new(
            new Guid("3c4cecdf-e153-4a41-9d4a-b257c70562bc"),
            "Ray3d[]",
            "Array of Ray3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Ray3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Ray3dArrayGz = new(
            new Guid("e0cbef0d-766c-b014-3381-f10a8ab3ebc3"),
            "Ray3d[].gz",
            "Array of Ray3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Ray3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Ray3dArrayLz4 = new(
            new Guid("f8789cc3-8019-a0f8-a10f-ff9fe9cd6297"),
            "Ray3d[].lz4",
            "Array of Ray3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A three-dimensional torus.
        /// </summary>
        public static readonly Def Torus3d = new(
            new Guid("86b425b6-62a3-4c1a-974c-b52f716f2994"),
            "Torus3d",
            "A three-dimensional torus.",
            None,
            false
            );

        /// <summary>
        /// Array of Torus3d.
        /// </summary>
        public static readonly Def Torus3dArray = new(
            new Guid("6778dd0c-4c92-4ae9-9925-a2ca2d8f554d"),
            "Torus3d[]",
            "Array of Torus3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Torus3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Torus3dArrayGz = new(
            new Guid("38126fee-4452-6d60-943c-805e37724b99"),
            "Torus3d[].gz",
            "Array of Torus3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Torus3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Torus3dArrayLz4 = new(
            new Guid("961f3049-7eed-d8c8-9d01-84d65e7db038"),
            "Torus3d[].lz4",
            "Array of Torus3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A two-dimensional polygon.
        /// </summary>
        public static readonly Def Polygon2d = new(
            new Guid("ed49d9d5-398c-415f-9dd9-85bb9902cf97"),
            "Polygon2d",
            "A two-dimensional polygon.",
            None,
            false
            );

        /// <summary>
        /// Array of Polygon2d.
        /// </summary>
        public static readonly Def Polygon2dArray = new(
            new Guid("2cd7815f-d8b9-488a-b506-adf02ce3b6da"),
            "Polygon2d[]",
            "Array of Polygon2d.",
            None,
            true
            );

        /// <summary>
        /// Array of Polygon2d. Compressed (GZip).
        /// </summary>
        public static readonly Def Polygon2dArrayGz = new(
            new Guid("e4c28db5-3ed8-b376-7e20-b9d3dda52048"),
            "Polygon2d[].gz",
            "Array of Polygon2d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Polygon2d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Polygon2dArrayLz4 = new(
            new Guid("6fa2ddcf-36a0-a81c-8cd0-a9574961a1b6"),
            "Polygon2d[].lz4",
            "Array of Polygon2d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// A three-dimensional polygon.
        /// </summary>
        public static readonly Def Polygon3d = new(
            new Guid("4da0c246-8d1d-44a0-ad7a-49522eee8d6e"),
            "Polygon3d",
            "A three-dimensional polygon.",
            None,
            false
            );

        /// <summary>
        /// Array of Polygon3d.
        /// </summary>
        public static readonly Def Polygon3dArray = new(
            new Guid("7d1c8157-c0a7-49f7-9ed7-e99e0160daad"),
            "Polygon3d[]",
            "Array of Polygon3d.",
            None,
            true
            );

        /// <summary>
        /// Array of Polygon3d. Compressed (GZip).
        /// </summary>
        public static readonly Def Polygon3dArrayGz = new(
            new Guid("1a468890-b22d-f24f-219a-d03c4484d19d"),
            "Polygon3d[].gz",
            "Array of Polygon3d. Compressed (GZip).",
            None,
            true
            );

        /// <summary>
        /// Array of Polygon3d. Compressed (LZ4).
        /// </summary>
        public static readonly Def Polygon3dArrayLz4 = new(
            new Guid("06f252cd-7dd6-bff0-f969-e99b351dd45e"),
            "Polygon3d[].lz4",
            "Array of Polygon3d. Compressed (LZ4).",
            None,
            true
            );

        /// <summary>
        /// Positions. V2f[].
        /// </summary>
        public static readonly Def ChunkPositions2f = new(
            new Guid("30180c1c-1858-42f0-9440-004b4554db5a"),
            "Aardvark.Chunk.Positions2f",
            "Positions. V2f[].",
            V2fArray.Id,
            true
            );

        /// <summary>
        /// Positions. V2f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkPositions2fGz = new(
            new Guid("6e3a216d-6b3c-3fbc-ac37-c6421179ffb6"),
            "Aardvark.Chunk.Positions2f.gz",
            "Positions. V2f[]. Compressed (GZip).",
            V2fArrayGz.Id,
            true
            );

        /// <summary>
        /// Positions. V2f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkPositions2fLz4 = new(
            new Guid("e4a82b09-c1c9-f63c-e4e6-d6381d917548"),
            "Aardvark.Chunk.Positions2f.lz4",
            "Positions. V2f[]. Compressed (LZ4).",
            V2fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Positions. V2d[].
        /// </summary>
        public static readonly Def ChunkPositions2d = new(
            new Guid("1257e31f-58d1-47a2-8252-74a0b9686e29"),
            "Aardvark.Chunk.Positions2d",
            "Positions. V2d[].",
            V2dArray.Id,
            true
            );

        /// <summary>
        /// Positions. V2d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkPositions2dGz = new(
            new Guid("4cddb171-c8a8-a1b4-8e1c-06997f299e98"),
            "Aardvark.Chunk.Positions2d.gz",
            "Positions. V2d[]. Compressed (GZip).",
            V2dArrayGz.Id,
            true
            );

        /// <summary>
        /// Positions. V2d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkPositions2dLz4 = new(
            new Guid("930ae932-56f0-dd90-309b-79c6edccf4b3"),
            "Aardvark.Chunk.Positions2d.lz4",
            "Positions. V2d[]. Compressed (LZ4).",
            V2dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Positions. V3f[].
        /// </summary>
        public static readonly Def ChunkPositions3f = new(
            new Guid("1cc23a98-f387-4df6-a82f-1e73f87bd519"),
            "Aardvark.Chunk.Positions3f",
            "Positions. V3f[].",
            V3fArray.Id,
            true
            );

        /// <summary>
        /// Positions. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkPositions3fGz = new(
            new Guid("63722e11-5b72-4ece-118c-fd9be27dc530"),
            "Aardvark.Chunk.Positions3f.gz",
            "Positions. V3f[]. Compressed (GZip).",
            V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Positions. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkPositions3fLz4 = new(
            new Guid("e8c696aa-5939-4370-3bf8-bb7e90cc01a1"),
            "Aardvark.Chunk.Positions3f.lz4",
            "Positions. V3f[]. Compressed (LZ4).",
            V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Positions. V3d[].
        /// </summary>
        public static readonly Def ChunkPositions3d = new(
            new Guid("b72e1359-6d05-4b61-8546-575f5280675a"),
            "Aardvark.Chunk.Positions3d",
            "Positions. V3d[].",
            V3dArray.Id,
            true
            );

        /// <summary>
        /// Positions. V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkPositions3dGz = new(
            new Guid("abe850bb-798a-0bfc-342f-adfb41a445b5"),
            "Aardvark.Chunk.Positions3d.gz",
            "Positions. V3d[]. Compressed (GZip).",
            V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Positions. V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkPositions3dLz4 = new(
            new Guid("466d859b-2ac6-4cc4-78d1-e5d5a3aad4ce"),
            "Aardvark.Chunk.Positions3d.lz4",
            "Positions. V3d[]. Compressed (LZ4).",
            V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Colors. C3b[].
        /// </summary>
        public static readonly Def ChunkColors3b = new(
            new Guid("52fa40ae-9a54-4a37-a2e3-4b46c78392e1"),
            "Aardvark.Chunk.Colors3b",
            "Colors. C3b[].",
            C3bArray.Id,
            true
            );

        /// <summary>
        /// Colors. C3b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkColors3bGz = new(
            new Guid("2a1e5d7d-a733-5b5a-bc35-f435966a52ef"),
            "Aardvark.Chunk.Colors3b.gz",
            "Colors. C3b[]. Compressed (GZip).",
            C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Colors. C3b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkColors3bLz4 = new(
            new Guid("93f2d78a-bda6-5970-7b75-da99eab272df"),
            "Aardvark.Chunk.Colors3b.lz4",
            "Colors. C3b[]. Compressed (LZ4).",
            C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Colors. C3f[].
        /// </summary>
        public static readonly Def ChunkColors3f = new(
            new Guid("eda92353-7e58-4898-b67c-812cb73a7184"),
            "Aardvark.Chunk.Colors3f",
            "Colors. C3f[].",
            C3fArray.Id,
            true
            );

        /// <summary>
        /// Colors. C3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkColors3fGz = new(
            new Guid("13a2e75c-d4d2-d071-1c6a-1a2736c596f8"),
            "Aardvark.Chunk.Colors3f.gz",
            "Colors. C3f[]. Compressed (GZip).",
            C3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Colors. C3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkColors3fLz4 = new(
            new Guid("aa410ab8-f1bb-66df-2605-1a6a4880c4f0"),
            "Aardvark.Chunk.Colors3f.lz4",
            "Colors. C3f[]. Compressed (LZ4).",
            C3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Colors. C4b[].
        /// </summary>
        public static readonly Def ChunkColors4b = new(
            new Guid("34d162f5-6462-4dd2-a8ec-36b1c326a6db"),
            "Aardvark.Chunk.Colors4b",
            "Colors. C4b[].",
            C4bArray.Id,
            true
            );

        /// <summary>
        /// Colors. C4b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkColors4bGz = new(
            new Guid("c046b981-c69e-5b7a-fe42-94616fb95bc2"),
            "Aardvark.Chunk.Colors4b.gz",
            "Colors. C4b[]. Compressed (GZip).",
            C4bArrayGz.Id,
            true
            );

        /// <summary>
        /// Colors. C4b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkColors4bLz4 = new(
            new Guid("c2b1f184-2a36-9604-14ee-5695f9b66e3e"),
            "Aardvark.Chunk.Colors4b.lz4",
            "Colors. C4b[]. Compressed (LZ4).",
            C4bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Colors. C4f[].
        /// </summary>
        public static readonly Def ChunkColors4f = new(
            new Guid("47cae42a-26a9-403f-8a5a-34f63fb08eb1"),
            "Aardvark.Chunk.Colors4f",
            "Colors. C4f[].",
            C4fArray.Id,
            true
            );

        /// <summary>
        /// Colors. C4f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkColors4fGz = new(
            new Guid("25979251-58ce-dd97-2a7b-2a430f716438"),
            "Aardvark.Chunk.Colors4f.gz",
            "Colors. C4f[]. Compressed (GZip).",
            C4fArrayGz.Id,
            true
            );

        /// <summary>
        /// Colors. C4f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkColors4fLz4 = new(
            new Guid("6078ff90-0d15-5ee8-9944-721e7fd87904"),
            "Aardvark.Chunk.Colors4f.lz4",
            "Colors. C4f[]. Compressed (LZ4).",
            C4fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Normals. V3f[].
        /// </summary>
        public static readonly Def ChunkNormals3f = new(
            new Guid("a8ce542d-f810-4a34-8236-d39d5ceaa99c"),
            "Aardvark.Chunk.Normals3f",
            "Normals. V3f[].",
            V3fArray.Id,
            true
            );

        /// <summary>
        /// Normals. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkNormals3fGz = new(
            new Guid("75bd1431-0035-8baa-2a80-476754cb98c0"),
            "Aardvark.Chunk.Normals3f.gz",
            "Normals. V3f[]. Compressed (GZip).",
            V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Normals. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkNormals3fLz4 = new(
            new Guid("840a13a1-938a-8cef-fe11-b2e4db81b59c"),
            "Aardvark.Chunk.Normals3f.lz4",
            "Normals. V3f[]. Compressed (LZ4).",
            V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt8[].
        /// </summary>
        public static readonly Def ChunkIntensities1b = new(
            new Guid("3b2dddb6-6d5e-4d92-877c-d046ed026b8a"),
            "Aardvark.Chunk.Intensities1b",
            "Intensities. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1bGz = new(
            new Guid("2b1f9228-e886-7b4b-f30d-2dd168ba9b7b"),
            "Aardvark.Chunk.Intensities1b.gz",
            "Intensities. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1bLz4 = new(
            new Guid("053d2799-b3af-a36f-2702-1f6fb30bf83a"),
            "Aardvark.Chunk.Intensities1b.lz4",
            "Intensities. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. Int16[].
        /// </summary>
        public static readonly Def ChunkIntensities1s = new(
            new Guid("71f63c81-9cd4-437d-801b-d2a012ea120c"),
            "Aardvark.Chunk.Intensities1s",
            "Intensities. Int16[].",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Intensities. Int16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1sGz = new(
            new Guid("868c19dd-3483-e201-468d-18039a323d70"),
            "Aardvark.Chunk.Intensities1s.gz",
            "Intensities. Int16[]. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. Int16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1sLz4 = new(
            new Guid("5616f149-9abb-5389-bf5a-b3a4ea8609dd"),
            "Aardvark.Chunk.Intensities1s.lz4",
            "Intensities. Int16[]. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt16[].
        /// </summary>
        public static readonly Def ChunkIntensities1us = new(
            new Guid("75e9e5c3-b510-4e00-bb80-866976ef7df2"),
            "Aardvark.Chunk.Intensities1us",
            "Intensities. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1usGz = new(
            new Guid("b28c541a-0e10-7ffb-1790-2f81b2a1dcf4"),
            "Aardvark.Chunk.Intensities1us.gz",
            "Intensities. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1usLz4 = new(
            new Guid("f34911ef-08de-abb1-9617-a9ad58c6d471"),
            "Aardvark.Chunk.Intensities1us.lz4",
            "Intensities. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. Int32[].
        /// </summary>
        public static readonly Def ChunkIntensities1i = new(
            new Guid("d97c8f2e-8e47-4bea-a226-e655b8520dd7"),
            "Aardvark.Chunk.Intensities1i",
            "Intensities. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Intensities. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1iGz = new(
            new Guid("5af97db1-a61d-4ac4-02fc-1357d9330565"),
            "Aardvark.Chunk.Intensities1i.gz",
            "Intensities. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1iLz4 = new(
            new Guid("095ce10c-4d29-66c2-7777-1df46ed24b8b"),
            "Aardvark.Chunk.Intensities1i.lz4",
            "Intensities. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt32[].
        /// </summary>
        public static readonly Def ChunkIntensities1ui = new(
            new Guid("cc43d018-e67d-4d50-be74-989cb2a296c2"),
            "Aardvark.Chunk.Intensities1ui",
            "Intensities. UInt32[].",
            Primitives.UInt32Array.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1uiGz = new(
            new Guid("0dbb4c58-c3d7-1d40-bfbe-d9c35606caae"),
            "Aardvark.Chunk.Intensities1ui.gz",
            "Intensities. UInt32[]. Compressed (GZip).",
            Primitives.UInt32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. UInt32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1uiLz4 = new(
            new Guid("85790cd0-9914-4fc9-24c0-2c5d9645b3a6"),
            "Aardvark.Chunk.Intensities1ui.lz4",
            "Intensities. UInt32[]. Compressed (LZ4).",
            Primitives.UInt32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. Float32[].
        /// </summary>
        public static readonly Def ChunkIntensities1f = new(
            new Guid("aee8ab41-0ae9-4987-9fd1-65ccef82f67b"),
            "Aardvark.Chunk.Intensities1f",
            "Intensities. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Intensities. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1fGz = new(
            new Guid("18f4e8f1-4019-6993-5bb2-62433887a5ab"),
            "Aardvark.Chunk.Intensities1f.gz",
            "Intensities. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1fLz4 = new(
            new Guid("68305a44-90db-b521-2800-eb151cc9fe19"),
            "Aardvark.Chunk.Intensities1f.lz4",
            "Intensities. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Intensities. Float64[].
        /// </summary>
        public static readonly Def ChunkIntensities1d = new(
            new Guid("59daac88-bc42-4bc1-a76f-e777441efc21"),
            "Aardvark.Chunk.Intensities1d",
            "Intensities. Float64[].",
            Primitives.Float64Array.Id,
            true
            );

        /// <summary>
        /// Intensities. Float64[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkIntensities1dGz = new(
            new Guid("8cf2aff0-9d73-bc77-bd2c-12d7d86a4fef"),
            "Aardvark.Chunk.Intensities1d.gz",
            "Intensities. Float64[]. Compressed (GZip).",
            Primitives.Float64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Intensities. Float64[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkIntensities1dLz4 = new(
            new Guid("c355c939-d799-ada1-a12f-c63bc78c83cb"),
            "Aardvark.Chunk.Intensities1d.lz4",
            "Intensities. Float64[]. Compressed (LZ4).",
            Primitives.Float64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt8[].
        /// </summary>
        public static readonly Def ChunkClassifications1b = new(
            new Guid("3cf3a1b8-1000-4b2f-a674-f0718c60de72"),
            "Aardvark.Chunk.Classifications1b",
            "Classifications. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassifications1bGz = new(
            new Guid("f4a7e8fd-3fba-719f-d595-8341b0422cc3"),
            "Aardvark.Chunk.Classifications1b.gz",
            "Classifications. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassifications1bLz4 = new(
            new Guid("16ba09fd-eed8-3761-cb79-9370f19d3bfc"),
            "Aardvark.Chunk.Classifications1b.lz4",
            "Classifications. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. Int16[].
        /// </summary>
        public static readonly Def ChunkClassifications1s = new(
            new Guid("8673504a-5100-4dbc-87b6-0ca4f2382bcc"),
            "Aardvark.Chunk.Classifications1s",
            "Classifications. Int16[].",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Classifications. Int16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassifications1sGz = new(
            new Guid("c15f79fc-ba26-f328-786a-daa4bdc8f1a3"),
            "Aardvark.Chunk.Classifications1s.gz",
            "Classifications. Int16[]. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. Int16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassifications1sLz4 = new(
            new Guid("884abf70-89cd-dacc-bb30-c0324a0581f8"),
            "Aardvark.Chunk.Classifications1s.lz4",
            "Classifications. Int16[]. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt16[].
        /// </summary>
        public static readonly Def ChunkClassifications1us = new(
            new Guid("4cae2709-c86e-4d24-bba8-086d4845c817"),
            "Aardvark.Chunk.Classifications1us",
            "Classifications. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassifications1usGz = new(
            new Guid("d57b183c-638b-d823-0acd-06eef554aabd"),
            "Aardvark.Chunk.Classifications1us.gz",
            "Classifications. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassifications1usLz4 = new(
            new Guid("423951c1-6853-5b82-53ad-b3b756babe1b"),
            "Aardvark.Chunk.Classifications1us.lz4",
            "Classifications. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. Int32[].
        /// </summary>
        public static readonly Def ChunkClassifications1i = new(
            new Guid("61fea872-aa6e-4249-ae9f-ad8fe75f8638"),
            "Aardvark.Chunk.Classifications1i",
            "Classifications. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Classifications. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassifications1iGz = new(
            new Guid("d7b3aa55-d943-ba01-829f-a28ab8a64e99"),
            "Aardvark.Chunk.Classifications1i.gz",
            "Classifications. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassifications1iLz4 = new(
            new Guid("1571f3c4-8a96-ccbd-c30e-f15939c53fb0"),
            "Aardvark.Chunk.Classifications1i.lz4",
            "Classifications. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt32[].
        /// </summary>
        public static readonly Def ChunkClassifications1ui = new(
            new Guid("3434e3d8-8812-4f7f-9f35-8150de42922c"),
            "Aardvark.Chunk.Classifications1ui",
            "Classifications. UInt32[].",
            Primitives.UInt32Array.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassifications1uiGz = new(
            new Guid("c4a93459-db9a-68d9-dbde-be311a0cf8d1"),
            "Aardvark.Chunk.Classifications1ui.gz",
            "Classifications. UInt32[]. Compressed (GZip).",
            Primitives.UInt32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. UInt32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassifications1uiLz4 = new(
            new Guid("c0e68ac2-0b2c-c25f-e400-201924da8fde"),
            "Aardvark.Chunk.Classifications1ui.lz4",
            "Classifications. UInt32[]. Compressed (LZ4).",
            Primitives.UInt32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Classifications. StringUTF8[].
        /// </summary>
        public static readonly Def ChunkClassificationsString = new(
            new Guid("05d57d11-86a1-4bd4-bb6d-219a47fd9193"),
            "Aardvark.Chunk.ClassificationsString",
            "Classifications. StringUTF8[].",
            Primitives.StringUTF8Array.Id,
            true
            );

        /// <summary>
        /// Classifications. StringUTF8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ChunkClassificationsStringGz = new(
            new Guid("4c3db3e1-a0cd-d8fe-35d6-eff09dcb8d84"),
            "Aardvark.Chunk.ClassificationsString.gz",
            "Classifications. StringUTF8[]. Compressed (GZip).",
            Primitives.StringUTF8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Classifications. StringUTF8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ChunkClassificationsStringLz4 = new(
            new Guid("3d87571a-f68f-683d-1e10-d4144655b6db"),
            "Aardvark.Chunk.ClassificationsString.lz4",
            "Classifications. StringUTF8[]. Compressed (LZ4).",
            Primitives.StringUTF8ArrayLz4.Id,
            true
            );

    }
    /// <summary></summary>
    public static class Generic
    {

        /// <summary>
        /// Generic positions. V3f[].
        /// </summary>
        public static readonly Def Positions3f = new(
            new Guid("40db0cd8-f4fc-4139-a7f0-ba5144b27e11"),
            "Generic.Positions3f",
            "Generic positions. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Generic positions. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Positions3fGz = new(
            new Guid("ccc87e76-e77a-612f-fb8e-3e3a9a98baa6"),
            "Generic.Positions3f.gz",
            "Generic positions. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic positions. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Positions3fLz4 = new(
            new Guid("5e33f01a-fa71-9fe7-6019-1bfdfb73c693"),
            "Generic.Positions3f.lz4",
            "Generic positions. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic positions. V3d[].
        /// </summary>
        public static readonly Def Positions3d = new(
            new Guid("7218415c-dd2e-42e9-bc2f-566353978559"),
            "Generic.Positions3d",
            "Generic positions. V3d[].",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Generic positions. V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Positions3dGz = new(
            new Guid("47bc0d96-becc-a591-cf26-1c4fae6cb2d2"),
            "Generic.Positions3d.gz",
            "Generic positions. V3d[]. Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic positions. V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Positions3dLz4 = new(
            new Guid("eb5dff08-1afa-4628-e095-9bf30c62b7da"),
            "Generic.Positions3d.lz4",
            "Generic positions. V3d[]. Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic normals. V3f[].
        /// </summary>
        public static readonly Def Normals3f = new(
            new Guid("3e8f21d0-b653-4665-811b-4a6fa9f343cb"),
            "Generic.Normals3f",
            "Generic normals. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Generic normals. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Normals3fGz = new(
            new Guid("8c5bf8c3-6867-3e35-36be-591fb700d12d"),
            "Generic.Normals3f.gz",
            "Generic normals. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic normals. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Normals3fLz4 = new(
            new Guid("a4cdf2da-3599-85fa-da26-2a08dc1fd422"),
            "Generic.Normals3f.lz4",
            "Generic normals. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
        /// </summary>
        public static readonly Def Normals3sb = new(
            new Guid("d1707d33-18af-45ed-9bce-870b0fe30310"),
            "Generic.Normals3sb",
            "Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].",
            Primitives.Int8Array.Id,
            true
            );

        /// <summary>
        /// Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Compressed (GZip).
        /// </summary>
        public static readonly Def Normals3sbGz = new(
            new Guid("6df0cc86-bf89-9249-74e6-a376766d8123"),
            "Generic.Normals3sb.gz",
            "Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Compressed (GZip).",
            Primitives.Int8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Normals3sbLz4 = new(
            new Guid("4962fb74-7d51-689c-6386-abde26019922"),
            "Generic.Normals3sb.lz4",
            "Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Compressed (LZ4).",
            Primitives.Int8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        /// </summary>
        public static readonly Def NormalsOct16 = new(
            new Guid("e801cbc2-c1b7-49cd-9bdf-5f212562575c"),
            "Generic.Normals.Oct16",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (GZip).
        /// </summary>
        public static readonly Def NormalsOct16Gz = new(
            new Guid("62bb883e-eb8a-4495-fba8-1d1d289aeedb"),
            "Generic.Normals.Oct16.gz",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (LZ4).
        /// </summary>
        public static readonly Def NormalsOct16Lz4 = new(
            new Guid("8980e964-dc7b-d539-62d6-bea36a8d5d3d"),
            "Generic.Normals.Oct16.lz4",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        /// </summary>
        public static readonly Def NormalsOct16P = new(
            new Guid("e855b0f0-63c7-49ac-810e-aa48dd65349a"),
            "Generic.Normals.Oct16P",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (GZip).
        /// </summary>
        public static readonly Def NormalsOct16PGz = new(
            new Guid("24f3bda4-0b40-9e93-ddf4-761624579d56"),
            "Generic.Normals.Oct16P.gz",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (LZ4).
        /// </summary>
        public static readonly Def NormalsOct16PLz4 = new(
            new Guid("0779ea4d-21af-6237-2c68-019b8cd4e566"),
            "Generic.Normals.Oct16P.lz4",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic colors. C3b[].
        /// </summary>
        public static readonly Def Colors3b = new(
            new Guid("9f7dacb5-3d0e-4623-8ae2-aad072f12cab"),
            "Generic.Colors3b",
            "Generic colors. C3b[].",
            Aardvark.C3bArray.Id,
            true
            );

        /// <summary>
        /// Generic colors. C3b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors3bGz = new(
            new Guid("94b44daf-ecae-657f-7487-8f202bb2b3ee"),
            "Generic.Colors3b.gz",
            "Generic colors. C3b[]. Compressed (GZip).",
            Aardvark.C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic colors. C3b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors3bLz4 = new(
            new Guid("65d82ab9-6dee-9c1f-facb-152a06e739c5"),
            "Generic.Colors3b.lz4",
            "Generic colors. C3b[]. Compressed (LZ4).",
            Aardvark.C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[].
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead.")]
        public static readonly Def Colors4bDeprecated20201117 = new(
            new Guid("b18a2463-a821-4ae7-a259-9a7739257286"),
            "Generic.Colors4b.Deprecated.20201117",
            "Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[].",
            Aardvark.C3bArray.Id,
            true
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[]. Compressed (GZip).
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead.")]
        public static readonly Def Colors4bDeprecated20201117Gz = new(
            new Guid("5607ef98-caf4-0cf3-c3a3-d237b37af787"),
            "Generic.Colors4b.Deprecated.20201117.gz",
            "Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[]. Compressed (GZip).",
            Aardvark.C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[]. Compressed (LZ4).
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead.")]
        public static readonly Def Colors4bDeprecated20201117Lz4 = new(
            new Guid("d1941c43-9461-a024-3a23-98b077bc67d5"),
            "Generic.Colors4b.Deprecated.20201117.lz4",
            "Deprecated 2020-11-17. Wrong type. Use d60b86bf-724c-4d41-ac64-040374557d72 instead. Generic colors. C4b[]. Compressed (LZ4).",
            Aardvark.C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic colors. C4b[].
        /// </summary>
        public static readonly Def Colors4b = new(
            new Guid("d60b86bf-724c-4d41-ac64-040374557d72"),
            "Generic.Colors4b",
            "Generic colors. C4b[].",
            Aardvark.C4bArray.Id,
            true
            );

        /// <summary>
        /// Generic colors. C4b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors4bGz = new(
            new Guid("868a1470-466d-cf9f-cabb-14c1c6a24d7a"),
            "Generic.Colors4b.gz",
            "Generic colors. C4b[]. Compressed (GZip).",
            Aardvark.C4bArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic colors. C4b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors4bLz4 = new(
            new Guid("75ec3019-808f-cc73-4249-307ca32adf07"),
            "Generic.Colors4b.lz4",
            "Generic colors. C4b[]. Compressed (LZ4).",
            Aardvark.C4bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].
        /// </summary>
        public static readonly Def ColorsRGB565 = new(
            new Guid("125f054f-003f-459b-8415-e6150992bb5f"),
            "Generic.Colors.RGB565",
            "Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ColorsRGB565Gz = new(
            new Guid("4972f67a-ee4f-f488-1ddf-ea8461d5b55e"),
            "Generic.Colors.RGB565.gz",
            "Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ColorsRGB565Lz4 = new(
            new Guid("5660f943-5ff6-31b1-c8df-af71b6689c40"),
            "Generic.Colors.RGB565.lz4",
            "Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Int32[].
        /// </summary>
        public static readonly Def Intensities1i = new(
            new Guid("eec1c41a-8f76-4d09-9ddc-bb7755a2f4b8"),
            "Generic.Intensities1i",
            "Generic intensities. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Intensities1iGz = new(
            new Guid("d7d7584d-5d83-2e0d-d8f4-2678aa4fcd35"),
            "Generic.Intensities1i.gz",
            "Generic intensities. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Intensities1iLz4 = new(
            new Guid("38a24f8a-467d-48af-204f-1d4c10e19284"),
            "Generic.Intensities1i.lz4",
            "Generic intensities. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Float32[].
        /// </summary>
        public static readonly Def Intensities1f = new(
            new Guid("e337e43b-ea72-4c96-8712-3684cb5d4b73"),
            "Generic.Intensities1f",
            "Generic intensities. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Intensities1fGz = new(
            new Guid("5a1adbfd-c65f-a1b7-00b7-f05e1a9462eb"),
            "Generic.Intensities1f.gz",
            "Generic intensities. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic intensities. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Intensities1fLz4 = new(
            new Guid("e5d8e901-4155-00ac-f396-2a72a3953ab5"),
            "Generic.Intensities1f.lz4",
            "Generic intensities. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt8[].
        /// </summary>
        public static readonly Def Classifications1b = new(
            new Guid("61a27f4d-b6c8-4ce1-8390-07fe3caee09b"),
            "Generic.Classifications1b",
            "Generic classifications. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1bGz = new(
            new Guid("6c1d591a-d203-255d-cd1b-6f094d8ec38a"),
            "Generic.Classifications1b.gz",
            "Generic classifications. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1bLz4 = new(
            new Guid("59e18742-c766-9114-0638-25856d3258d8"),
            "Generic.Classifications1b.lz4",
            "Generic classifications. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt16[].
        /// </summary>
        public static readonly Def Classifications1s = new(
            new Guid("e7d38eab-5b7f-4469-98a2-a940d5ee8852"),
            "Generic.Classifications1s",
            "Generic classifications. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1sGz = new(
            new Guid("98db9097-401b-d3a7-151f-3d46ec652e10"),
            "Generic.Classifications1s.gz",
            "Generic classifications. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic classifications. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1sLz4 = new(
            new Guid("9cdd3601-ad84-863a-56c5-f73f15d26b52"),
            "Generic.Classifications1s.lz4",
            "Generic classifications. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic classifications. Int32[].
        /// </summary>
        public static readonly Def Classifications1i = new(
            new Guid("fe0b56b8-bf84-4e6e-ab9e-33cd63ae187d"),
            "Generic.Classifications1i",
            "Generic classifications. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Generic classifications. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1iGz = new(
            new Guid("3f0b747e-9aff-a8ff-5c8d-2fb79223dc13"),
            "Generic.Classifications1i.gz",
            "Generic classifications. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic classifications. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1iLz4 = new(
            new Guid("dc96daf6-bca7-87de-1ddd-312844bf4494"),
            "Generic.Classifications1i.lz4",
            "Generic classifications. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3f[]).
        /// </summary>
        public static readonly Def Velocities3f = new(
            new Guid("8916a1b8-59a3-4bc6-ab17-d91abd6a4ee3"),
            "Generic.Velocities3f",
            "Generic velocities (V3f[]).",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3f[]). Compressed (GZip).
        /// </summary>
        public static readonly Def Velocities3fGz = new(
            new Guid("f5ecf184-cc38-67e8-cb00-302b257f9f76"),
            "Generic.Velocities3f.gz",
            "Generic velocities (V3f[]). Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3f[]). Compressed (LZ4).
        /// </summary>
        public static readonly Def Velocities3fLz4 = new(
            new Guid("525048c1-a538-81b6-b004-c503fc579ae0"),
            "Generic.Velocities3f.lz4",
            "Generic velocities (V3f[]). Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3d[]).
        /// </summary>
        public static readonly Def Velocities3d = new(
            new Guid("14528fc1-a5b6-4cbb-9cab-489f962cff6c"),
            "Generic.Velocities3d",
            "Generic velocities (V3d[]).",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3d[]). Compressed (GZip).
        /// </summary>
        public static readonly Def Velocities3dGz = new(
            new Guid("548a8f44-9a26-046d-441e-b88c860ed604"),
            "Generic.Velocities3d.gz",
            "Generic velocities (V3d[]). Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic velocities (V3d[]). Compressed (LZ4).
        /// </summary>
        public static readonly Def Velocities3dLz4 = new(
            new Guid("a7d8cfa8-5ca7-f891-1c22-56cc53397408"),
            "Generic.Velocities3d.lz4",
            "Generic velocities (V3d[]). Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic densities (float32[]).
        /// </summary>
        public static readonly Def Densities1f = new(
            new Guid("e912f821-fba2-4177-9419-007930582a4e"),
            "Generic.Densities1f",
            "Generic densities (float32[]).",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Generic densities (float32[]). Compressed (GZip).
        /// </summary>
        public static readonly Def Densities1fGz = new(
            new Guid("b94f9abd-16cb-99b4-5e78-0cfd3d6c8940"),
            "Generic.Densities1f.gz",
            "Generic densities (float32[]). Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic densities (float32[]). Compressed (LZ4).
        /// </summary>
        public static readonly Def Densities1fLz4 = new(
            new Guid("e026c9c1-50ae-d50c-831c-d39421750cc2"),
            "Generic.Densities1f.lz4",
            "Generic densities (float32[]). Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Generic densities (float64[]).
        /// </summary>
        public static readonly Def Densities1d = new(
            new Guid("5f8fa111-eb42-4a45-8f7a-c1cd45e200be"),
            "Generic.Densities1d",
            "Generic densities (float64[]).",
            Primitives.Float64Array.Id,
            true
            );

        /// <summary>
        /// Generic densities (float64[]). Compressed (GZip).
        /// </summary>
        public static readonly Def Densities1dGz = new(
            new Guid("27324c92-424b-ee63-098c-044557754ce5"),
            "Generic.Densities1d.gz",
            "Generic densities (float64[]). Compressed (GZip).",
            Primitives.Float64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Generic densities (float64[]). Compressed (LZ4).
        /// </summary>
        public static readonly Def Densities1dLz4 = new(
            new Guid("27228a4d-6e16-507d-e1c5-171e9ce12b53"),
            "Generic.Densities1d.lz4",
            "Generic densities (float64[]). Compressed (LZ4).",
            Primitives.Float64ArrayLz4.Id,
            true
            );

    }
    /// <summary></summary>
    public static class Octree
    {

        /// <summary>
        /// Octree. An octree node. DurableMap.
        /// </summary>
        public static readonly Def Node = new(
            new Guid("e0883944-1d81-4ff5-845f-0b96075880b7"),
            "Octree.Node",
            "Octree. An octree node. DurableMap.",
            Primitives.DurableMap.Id,
            false
            );

        /// <summary>
        /// Deprecated 2022-10-21. Octree. An index for octree nodes stored in level order in a single blob. DurableMap.
        /// </summary>
        [Obsolete("Deprecated 2022-10-21.")]
        public static readonly Def MultiNodeIndexDeprecated20221021 = new(
            new Guid("53efc513-d4dd-43f5-908d-6cd73e90962f"),
            "Octree.MultiNodeIndex.Deprecated.20221021",
            "Deprecated 2022-10-21. Octree. An index for octree nodes stored in level order in a single blob. DurableMap.",
            Primitives.DurableNamedMapDeprecated20221021.Id,
            false
            );

        /// <summary>
        /// Octree. Node's unique id. Guid.
        /// </summary>
        public static readonly Def NodeId = new(
            new Guid("1100ffd5-7789-4872-9ef2-67d45be0c489"),
            "Octree.NodeId",
            "Octree. Node's unique id. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Node's unique id. StringUTF8.
        /// </summary>
        public static readonly Def NodeIdString = new(
            new Guid("c0e3a799-d15f-4e0d-b90e-6924635b3f07"),
            "Octree.NodeId.String",
            "Octree. Node's unique id. StringUTF8.",
            Primitives.StringUTF8.Id,
            false
            );

        /// <summary>
        /// Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8]
        /// </summary>
        public static readonly Def SubnodesGuids = new(
            new Guid("eb44f9b0-3247-4426-b458-1b6e9880d466"),
            "Octree.Subnodes.Guids",
            "Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8]",
            Primitives.GuidArray.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8] Compressed (GZip).
        /// </summary>
        public static readonly Def SubnodesGuidsGz = new(
            new Guid("e70bcf12-4382-4f10-4fce-4ef499ac5475"),
            "Octree.Subnodes.Guids.gz",
            "Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8] Compressed (GZip).",
            Primitives.GuidArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8] Compressed (LZ4).
        /// </summary>
        public static readonly Def SubnodesGuidsLz4 = new(
            new Guid("e0c33f06-a3b8-d92e-e8b3-893f3ffeea5d"),
            "Octree.Subnodes.Guids.lz4",
            "Octree. Subnode IDs as array of guids. Array length is 8 for inner nodes (where Guid.Empty means no subnode) and no array for leaf nodes. Guid[8] Compressed (LZ4).",
            Primitives.GuidArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8]
        /// </summary>
        public static readonly Def SubnodesStrings = new(
            new Guid("72b072ac-3973-49a5-b04f-436241b2107b"),
            "Octree.Subnodes.Strings",
            "Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8]",
            Primitives.StringUTF8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (GZip).
        /// </summary>
        public static readonly Def SubnodesStringsGz = new(
            new Guid("ae7a494c-3387-2fe8-fcd6-4880d6a7dd80"),
            "Octree.Subnodes.Strings.gz",
            "Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (GZip).",
            Primitives.StringUTF8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (LZ4).
        /// </summary>
        public static readonly Def SubnodesStringsLz4 = new(
            new Guid("0d5dc6b2-bc2d-83c8-51f2-b665435ce597"),
            "Octree.Subnodes.Strings.lz4",
            "Octree. Subnode IDs as array of strings. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (LZ4).",
            Primitives.StringUTF8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8]
        /// </summary>
        public static readonly Def SubnodesMultiNodeRefs = new(
            new Guid("6d65a05b-8813-4ac0-ac78-f26bae0235b2"),
            "Octree.Subnodes.MultiNodeRefs",
            "Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8]",
            Primitives.StringUTF8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (GZip).
        /// </summary>
        public static readonly Def SubnodesMultiNodeRefsGz = new(
            new Guid("a4e97fde-c4a4-7d67-c18e-c4a9fa196045"),
            "Octree.Subnodes.MultiNodeRefs.gz",
            "Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (GZip).",
            Primitives.StringUTF8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (LZ4).
        /// </summary>
        public static readonly Def SubnodesMultiNodeRefsLz4 = new(
            new Guid("53d1aa86-d04c-9ff6-8300-872a927efef4"),
            "Octree.Subnodes.MultiNodeRefs.lz4",
            "Octree. Subnode IDs as array of multi-node references in format [nodeId]@[insideMultiNodeId] or [nodeId] if subnode is stand-alone. Array length is 8 for inner nodes (where null means no subnode) and no array for leaf nodes. StringUTF8[8] Compressed (LZ4).",
            Primitives.StringUTF8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16].
        /// </summary>
        public static readonly Def SubnodesByteRanges = new(
            new Guid("8818ce56-4429-4661-bac2-14e55d4d1f41"),
            "Octree.Subnodes.ByteRanges",
            "Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16].",
            Primitives.Int64Array.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16]. Compressed (GZip).
        /// </summary>
        public static readonly Def SubnodesByteRangesGz = new(
            new Guid("9fb1c153-cf26-8a22-bf3d-0291119a03fa"),
            "Octree.Subnodes.ByteRanges.gz",
            "Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16]. Compressed (GZip).",
            Primitives.Int64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16]. Compressed (LZ4).
        /// </summary>
        public static readonly Def SubnodesByteRangesLz4 = new(
            new Guid("7eb0a930-2f76-f533-7a6a-113796d93cde"),
            "Octree.Subnodes.ByteRanges.lz4",
            "Octree. Subnode byte ranges inside multi-node blob as array offset/size pairs. In ADDITION to Octree.Subnodes[Guids|Strings]. Array length is 16 (8x Int64 pair) for inner nodes (where (0L, 0L) means no subnode) and no array for leaf nodes. Int64[16]. Compressed (LZ4).",
            Primitives.Int64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2].
        /// </summary>
        public static readonly Def NodeByteRange = new(
            new Guid("9814aafe-bb6c-4b28-b375-23435e879e87"),
            "Octree.NodeByteRange",
            "Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2].",
            Primitives.Int64Array.Id,
            true
            );

        /// <summary>
        /// Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2]. Compressed (GZip).
        /// </summary>
        public static readonly Def NodeByteRangeGz = new(
            new Guid("5cd7c37d-4e2b-2042-c113-dd285b4eba4b"),
            "Octree.NodeByteRange.gz",
            "Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2]. Compressed (GZip).",
            Primitives.Int64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2]. Compressed (LZ4).
        /// </summary>
        public static readonly Def NodeByteRangeLz4 = new(
            new Guid("75053ce1-6d58-3f4a-2e83-9a4ff0160733"),
            "Octree.NodeByteRange.lz4",
            "Octree. Byte range inside multi-node blob as offset/size pair. In ADDITION to Octree.NodeId[String]. Array length is 2 (Int64 pair) and no array for non-multi-node blob. Int64[2]. Compressed (LZ4).",
            Primitives.Int64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Octree nodes stored in level order (multi-node blob). Uint8[].
        /// </summary>
        public static readonly Def MultiNodeBlob = new(
            new Guid("ef8539ab-58c2-4982-8313-3b5bbc871d54"),
            "Octree.MultiNodeBlob",
            "Octree. Octree nodes stored in level order (multi-node blob). Uint8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Octree nodes stored in level order (multi-node blob). Uint8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def MultiNodeBlobGz = new(
            new Guid("da046065-e7f0-763c-d7f3-d0be48bb247f"),
            "Octree.MultiNodeBlob.gz",
            "Octree. Octree nodes stored in level order (multi-node blob). Uint8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Octree nodes stored in level order (multi-node blob). Uint8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def MultiNodeBlobLz4 = new(
            new Guid("2816e3ed-47f1-870a-d7e5-12eb54aa289b"),
            "Octree.MultiNodeBlob.lz4",
            "Octree. Octree nodes stored in level order (multi-node blob). Uint8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Exact bounding box of this tree's positions. Global space. Box3d.
        /// </summary>
        public static readonly Def BoundingBoxExactGlobal = new(
            new Guid("7912c862-74b4-4f44-a8cd-d11ea1da9304"),
            "Octree.BoundingBoxExactGlobal",
            "Octree. Exact bounding box of this tree's positions. Global space. Box3d.",
            Aardvark.Box3d.Id,
            false
            );

        /// <summary>
        /// Octree. Exact bounding box of this node's positions. Local space. Box3f.
        /// </summary>
        public static readonly Def BoundingBoxExactLocal = new(
            new Guid("aadbb622-1cf6-42e0-86df-be79d28d6757"),
            "Octree.BoundingBoxExactLocal",
            "Octree. Exact bounding box of this node's positions. Local space. Box3f.",
            Aardvark.Box3f.Id,
            false
            );

        /// <summary>
        /// Octree. Cell's index. Global space. Cell.
        /// </summary>
        public static readonly Def Cell = new(
            new Guid("9f8121e4-83af-40e3-aed9-5fd908a140ee"),
            "Octree.Cell",
            "Octree. Cell's index. Global space. Cell.",
            Aardvark.Cell.Id,
            false
            );

        /// <summary>
        /// Octree. Number of nodes in this tree (including inner nodes). Int64.
        /// </summary>
        public static readonly Def NodeCountTotal = new(
            new Guid("5f904be4-09fb-4b16-ad9d-460c3ae63248"),
            "Octree.NodeCountTotal",
            "Octree. Number of nodes in this tree (including inner nodes). Int64.",
            Primitives.Int64.Id,
            false
            );

        /// <summary>
        /// Octree. Number of leaf nodes in this tree. Int64.
        /// </summary>
        public static readonly Def NodeCountLeafs = new(
            new Guid("8d50e820-69b0-4923-969d-e10aedecdfc2"),
            "Octree.NodeCountLeafs",
            "Octree. Number of leaf nodes in this tree. Int64.",
            Primitives.Int64.Id,
            false
            );

        /// <summary>
        /// Octree. Number of points in this cell. Int32.
        /// </summary>
        public static readonly Def PointCountCell = new(
            new Guid("172e1f20-0ffc-4d9c-9b3d-903fca41abe3"),
            "Octree.PointCountCell",
            "Octree. Number of points in this cell. Int32.",
            Primitives.Int32.Id,
            false
            );

        /// <summary>
        /// Octree. Total number of points in this tree's leaf nodes. Int64.
        /// </summary>
        public static readonly Def PointCountTreeLeafs = new(
            new Guid("71e80c00-06b6-4e84-a0f7-dbababd2613c"),
            "Octree.PointCountTreeLeafs",
            "Octree. Total number of points in this tree's leaf nodes. Int64.",
            Primitives.Int64.Id,
            false
            );

        /// <summary>
        /// Octree. Total number of points in this tree's leaf nodes. Float64. Backwards compatibility.
        /// </summary>
        public static readonly Def PointCountTreeLeafsFloat64 = new(
            new Guid("6bef7603-47fa-405f-a330-a1ac1b09c475"),
            "Octree.PointCountTreeLeafs.Float64",
            "Octree. Total number of points in this tree's leaf nodes. Float64. Backwards compatibility.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Number of bytes (approx.) of this node blob. Int32.
        /// </summary>
        public static readonly Def NodeBlobBytesApprox = new(
            new Guid("605c7db1-80bd-4f4f-bb4b-2b4bf974bc9d"),
            "Octree.NodeBlobBytesApprox",
            "Octree. Number of bytes (approx.) of this node blob. Int32.",
            Primitives.Int32.Id,
            false
            );

        /// <summary>
        /// Octree. Total number of bytes (approx.) of all octree blobs. Int64.
        /// </summary>
        public static readonly Def TreeBlobsTotalBytesApprox = new(
            new Guid("acdb8562-8fe9-48a5-b92b-0a54b782616c"),
            "Octree.TreeBlobsTotalBytesApprox",
            "Octree. Total number of bytes (approx.) of all octree blobs. Int64.",
            Primitives.Int64.Id,
            false
            );

        /// <summary>
        /// Octree. Average distance of each point to its nearest neighbour.
        /// </summary>
        public static readonly Def AveragePointDistance = new(
            new Guid("39c21132-4570-4624-afae-6304851567d7"),
            "Octree.AveragePointDistance",
            "Octree. Average distance of each point to its nearest neighbour.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Standard deviation of average distances of each point to its nearest neighbour.
        /// </summary>
        public static readonly Def AveragePointDistanceStdDev = new(
            new Guid("94cac234-b6ea-443a-b196-c7dd8e5def0d"),
            "Octree.AveragePointDistanceStdDev",
            "Octree. Standard deviation of average distances of each point to its nearest neighbour.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Min depth of this tree. A leaf node has depth 0. Int32.
        /// </summary>
        public static readonly Def MinTreeDepth = new(
            new Guid("42edbdd6-a29e-4dfd-9836-050ab7fa4e31"),
            "Octree.MinTreeDepth",
            "Octree. Min depth of this tree. A leaf node has depth 0. Int32.",
            Primitives.Int32.Id,
            false
            );

        /// <summary>
        /// Octree. Max depth of this tree. A leaf node has depth 0. Int32.
        /// </summary>
        public static readonly Def MaxTreeDepth = new(
            new Guid("d6f54b9e-e907-46c5-9106-d26cd453dc97"),
            "Octree.MaxTreeDepth",
            "Octree. Max depth of this tree. A leaf node has depth 0. Int32.",
            Primitives.Int32.Id,
            false
            );

        /// <summary>
        /// Octree. Per-cell part index. UInt32.
        /// </summary>
        public static readonly Def PerCellPartIndex1ui = new(
            new Guid("0a80f2b3-6484-4e4d-a573-3e8ff0378e93"),
            "Octree.PerCellPartIndex1ui",
            "Octree. Per-cell part index. UInt32.",
            Primitives.UInt32.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point part index. UInt8[].
        /// </summary>
        public static readonly Def PerPointPartIndex1b = new(
            new Guid("3b393e63-85fc-4258-a79e-a16edf2cb9de"),
            "Octree.PerPointPartIndex1b",
            "Octree. Per-point part index. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PerPointPartIndex1bGz = new(
            new Guid("88b0cf06-abd4-4461-9eea-1919f389ed25"),
            "Octree.PerPointPartIndex1b.gz",
            "Octree. Per-point part index. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PerPointPartIndex1bLz4 = new(
            new Guid("ee8bf99c-d4e7-901d-0ac2-38eeae033d06"),
            "Octree.PerPointPartIndex1b.lz4",
            "Octree. Per-point part index. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point part index. Guid.
        /// </summary>
        public static readonly Def PerPointPartIndex1bReference = new(
            new Guid("050005ac-f6b4-44f0-9709-d877617a5403"),
            "Octree.PerPointPartIndex1b.Reference",
            "Octree. Reference to per-point part index. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point part index. UInt16[].
        /// </summary>
        public static readonly Def PerPointPartIndex1s = new(
            new Guid("5e13d36a-1958-4936-bad0-d0ddf6274f2e"),
            "Octree.PerPointPartIndex1s",
            "Octree. Per-point part index. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PerPointPartIndex1sGz = new(
            new Guid("56d1cbaf-9896-370d-8a65-bcb937da60a7"),
            "Octree.PerPointPartIndex1s.gz",
            "Octree. Per-point part index. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PerPointPartIndex1sLz4 = new(
            new Guid("6a792d3d-330e-1b64-2bbe-7a717cbc0b4e"),
            "Octree.PerPointPartIndex1s.lz4",
            "Octree. Per-point part index. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point part index. Guid.
        /// </summary>
        public static readonly Def PerPointPartIndex1sReference = new(
            new Guid("eedb807b-6e62-4ad0-989b-44b56ba88fb0"),
            "Octree.PerPointPartIndex1s.Reference",
            "Octree. Reference to per-point part index. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point part index. UInt32[].
        /// </summary>
        public static readonly Def PerPointPartIndex1i = new(
            new Guid("987dde4b-d7a0-4eff-aecb-9447aca0afb5"),
            "Octree.PerPointPartIndex1i",
            "Octree. Per-point part index. UInt32[].",
            Primitives.UInt32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PerPointPartIndex1iGz = new(
            new Guid("97cc4d6b-386a-3a00-1a2b-e3a31332f1fc"),
            "Octree.PerPointPartIndex1i.gz",
            "Octree. Per-point part index. UInt32[]. Compressed (GZip).",
            Primitives.UInt32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point part index. UInt32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PerPointPartIndex1iLz4 = new(
            new Guid("d8ab95fb-65ab-61ef-6a86-fb757a355199"),
            "Octree.PerPointPartIndex1i.lz4",
            "Octree. Per-point part index. UInt32[]. Compressed (LZ4).",
            Primitives.UInt32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point part index. Guid.
        /// </summary>
        public static readonly Def PerPointPartIndex1iReference = new(
            new Guid("e04d4f9a-f361-4de3-8de6-0ded1013bded"),
            "Octree.PerPointPartIndex1i.Reference",
            "Octree. Reference to per-point part index. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[].
        /// </summary>
        public static readonly Def PositionsLocal3sb = new(
            new Guid("2a7c36bc-8d84-45b6-86fa-b66b562b3cc2"),
            "Octree.PositionsLocal3sb",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[].",
            Primitives.Int8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3sbGz = new(
            new Guid("21d81a2e-90d4-8ba2-5771-3dc7c1162c4c"),
            "Octree.PositionsLocal3sb.gz",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[]. Compressed (GZip).",
            Primitives.Int8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3sbLz4 = new(
            new Guid("4ac131e0-171b-d337-c454-41a1dcc4a5fe"),
            "Octree.PositionsLocal3sb.lz4",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where -128 is cell.Min and +128 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int8[]. Compressed (LZ4).",
            Primitives.Int8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3sb. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3sbReference = new(
            new Guid("3e225a08-fe26-4038-bbce-8d43275b029e"),
            "Octree.PositionsLocal3sb.Reference",
            "Octree. Reference to Octree.PositionsLocal3sb. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[].
        /// </summary>
        public static readonly Def PositionsLocal3b = new(
            new Guid("c965597a-e2f8-4f1a-bef4-a0071c32e220"),
            "Octree.PositionsLocal3b",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3bGz = new(
            new Guid("f0d43a66-46b0-1686-a040-59751ef5ab94"),
            "Octree.PositionsLocal3b.gz",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3bLz4 = new(
            new Guid("26339b3d-665e-2628-3cd0-66eb6856970f"),
            "Octree.PositionsLocal3b.lz4",
            "Octree. Per-point positions in local cell space as uniform 8-bit subdivision, where 0 is cell.Min and 256 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3b. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3bReference = new(
            new Guid("52aa381e-ddf2-4d77-a746-5cacaa1d10a9"),
            "Octree.PositionsLocal3b.Reference",
            "Octree. Reference to Octree.PositionsLocal3b. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[].
        /// </summary>
        public static readonly Def PositionsLocal3s = new(
            new Guid("2d83f869-39c0-4b4b-9470-af97a82677a7"),
            "Octree.PositionsLocal3s",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[].",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3sGz = new(
            new Guid("799fc895-058b-b860-4bbe-9ef3dfde4784"),
            "Octree.PositionsLocal3s.gz",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[]. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3sLz4 = new(
            new Guid("2cdcbee6-f2d3-4a18-3eba-a8076e56d894"),
            "Octree.PositionsLocal3s.lz4",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where Int16.Min is cell.Min and Int16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int16[]. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3s. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3sReference = new(
            new Guid("35c5b633-2a7f-4ee3-87de-3321c72ca832"),
            "Octree.PositionsLocal3s.Reference",
            "Octree. Reference to Octree.PositionsLocal3s. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[].
        /// </summary>
        public static readonly Def PositionsLocal3us = new(
            new Guid("f3c29fdb-5067-42c2-9809-0f8c103fcb82"),
            "Octree.PositionsLocal3us",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3usGz = new(
            new Guid("4a242c48-20fb-f851-3739-a38ec77a4a69"),
            "Octree.PositionsLocal3us.gz",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3usLz4 = new(
            new Guid("acb00efc-6d6c-011a-4289-6f6ba57d3096"),
            "Octree.PositionsLocal3us.lz4",
            "Octree. Per-point positions in local cell space as uniform 16-bit subdivision, where 0 is cell.Min and UInt16.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3us. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3usReference = new(
            new Guid("8a2ef1f6-179d-4ddb-a342-4d7ae4a1f69f"),
            "Octree.PositionsLocal3us.Reference",
            "Octree. Reference to Octree.PositionsLocal3us. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[].
        /// </summary>
        public static readonly Def PositionsLocal3i = new(
            new Guid("c479bb05-2563-4ebb-b6c4-1a1c1d6c606d"),
            "Octree.PositionsLocal3i",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[].",
            Aardvark.V3iArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3iGz = new(
            new Guid("90ec9d6c-52b0-be12-e5e8-353eb54e9137"),
            "Octree.PositionsLocal3i.gz",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[]. Compressed (GZip).",
            Aardvark.V3iArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3iLz4 = new(
            new Guid("197f5033-eef4-70cc-ff55-aa0f1080d4c3"),
            "Octree.PositionsLocal3i.lz4",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where Int32.Min is cell.Min and Int32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int32[]. Compressed (LZ4).",
            Aardvark.V3iArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3i. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3iReference = new(
            new Guid("6e570668-b471-4dd1-b186-9b4461ee713c"),
            "Octree.PositionsLocal3i.Reference",
            "Octree. Reference to Octree.PositionsLocal3i. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[].
        /// </summary>
        public static readonly Def PositionsLocal3ui = new(
            new Guid("f251364f-d6d2-49e0-8095-45314fe2e80c"),
            "Octree.PositionsLocal3ui",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[].",
            Primitives.UInt32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3uiGz = new(
            new Guid("29a8b213-ceeb-6157-8cea-3717a253afae"),
            "Octree.PositionsLocal3ui.gz",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[]. Compressed (GZip).",
            Primitives.UInt32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3uiLz4 = new(
            new Guid("8b3068d4-6600-18b7-7a1c-67d2df8fc72b"),
            "Octree.PositionsLocal3ui.lz4",
            "Octree. Per-point positions in local cell space as uniform 32-bit subdivision, where 0 is cell.Min and UInt32.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt32[]. Compressed (LZ4).",
            Primitives.UInt32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3ui. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3uiReference = new(
            new Guid("6cfa3f22-27bc-4b86-917c-98f92f38bc46"),
            "Octree.PositionsLocal3ui.Reference",
            "Octree. Reference to Octree.PositionsLocal3ui. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[].
        /// </summary>
        public static readonly Def PositionsLocal3l = new(
            new Guid("89d17a79-6888-4d68-b68b-7556630c13b1"),
            "Octree.PositionsLocal3l",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[].",
            Primitives.Int64Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3lGz = new(
            new Guid("80a1349d-2de9-955e-2295-62e8a50c9739"),
            "Octree.PositionsLocal3l.gz",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[]. Compressed (GZip).",
            Primitives.Int64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3lLz4 = new(
            new Guid("ae6f4ff8-3250-8a25-0470-8d679398c06f"),
            "Octree.PositionsLocal3l.lz4",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where Int64.Min is cell.Min and Int64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. Int64[]. Compressed (LZ4).",
            Primitives.Int64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3l. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3lReference = new(
            new Guid("58a28940-8436-4796-8041-57e8ff5caccf"),
            "Octree.PositionsLocal3l.Reference",
            "Octree. Reference to Octree.PositionsLocal3l. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[].
        /// </summary>
        public static readonly Def PositionsLocal3ul = new(
            new Guid("f256c25e-f599-4135-b3a6-18e811925625"),
            "Octree.PositionsLocal3ul",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[].",
            Primitives.UInt64Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3ulGz = new(
            new Guid("6bfa4ae1-ee93-fe96-dbef-2f808883bbf6"),
            "Octree.PositionsLocal3ul.gz",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[]. Compressed (GZip).",
            Primitives.UInt64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3ulLz4 = new(
            new Guid("3b1fc68a-0b69-e4d4-fbd3-9355e69e091f"),
            "Octree.PositionsLocal3ul.lz4",
            "Octree. Per-point positions in local cell space as uniform 64-bit subdivision, where 0 is cell.Min and UInt64.Max+1 is cell.Max. Layout for n points is [x0,y0,z0,x1,y1,z1,...x(n-1),y(n-1), z(n-1)]. UInt64[]. Compressed (LZ4).",
            Primitives.UInt64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.PositionsLocal3ul. Guid.
        /// </summary>
        public static readonly Def PositionsLocal3ulReference = new(
            new Guid("286b881a-6f4e-4bcf-bb90-dd6b0bd16232"),
            "Octree.PositionsLocal3ul.Reference",
            "Octree. Reference to Octree.PositionsLocal3ul. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3f[].
        /// </summary>
        public static readonly Def PositionsGlobal3f = new(
            new Guid("fcb577f8-28cc-43b2-9aef-ca0569c22a03"),
            "Octree.PositionsGlobal3f",
            "Octree. Per-point positions in global space. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsGlobal3fGz = new(
            new Guid("47c4f92c-2a88-3da6-b192-0241bb27be0f"),
            "Octree.PositionsGlobal3f.gz",
            "Octree. Per-point positions in global space. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsGlobal3fLz4 = new(
            new Guid("5b9f9731-f075-67ff-3bef-d311b27c6bef"),
            "Octree.PositionsGlobal3f.lz4",
            "Octree. Per-point positions in global space. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Centroid of positions (global space). V3f.
        /// </summary>
        public static readonly Def PositionsGlobal3fCentroid = new(
            new Guid("5ace7e46-f67d-4dba-9c60-e63f18b26166"),
            "Octree.PositionsGlobal3f.Centroid",
            "Octree. Centroid of positions (global space). V3f.",
            Aardvark.V3f.Id,
            false
            );

        /// <summary>
        /// Octree. Average point distance to centroid (global space). Float32.
        /// </summary>
        public static readonly Def PositionsGlobal3fDistToCentroidAverage = new(
            new Guid("1d61fded-6d27-4cf3-a6b7-c187ed21ae10"),
            "Octree.PositionsGlobal3f.DistToCentroid.Average",
            "Octree. Average point distance to centroid (global space). Float32.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Standard deviation of average point distance to centroid (global space). Float32.
        /// </summary>
        public static readonly Def PositionsGlobal3fDistToCentroidStdDev = new(
            new Guid("d725dcde-0f50-4f36-8e23-580cd59d04e4"),
            "Octree.PositionsGlobal3f.DistToCentroid.StdDev",
            "Octree. Standard deviation of average point distance to centroid (global space). Float32.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to per-point positions in global space. Guid.
        /// </summary>
        public static readonly Def PositionsGlobal3fReference = new(
            new Guid("03a62e0e-4a4b-4d24-b558-cf700d275edd"),
            "Octree.PositionsGlobal3f.Reference",
            "Octree. Reference to per-point positions in global space. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3d[].
        /// </summary>
        public static readonly Def PositionsGlobal3d = new(
            new Guid("61ef7c1e-6aeb-45cd-85ed-ad0ed2584553"),
            "Octree.PositionsGlobal3d",
            "Octree. Per-point positions in global space. V3d[].",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsGlobal3dGz = new(
            new Guid("54895d13-147c-1c69-d5ab-edd4990b3e97"),
            "Octree.PositionsGlobal3d.gz",
            "Octree. Per-point positions in global space. V3d[]. Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in global space. V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsGlobal3dLz4 = new(
            new Guid("f33c9393-9fc6-b005-3e6a-652c9166e62c"),
            "Octree.PositionsGlobal3d.lz4",
            "Octree. Per-point positions in global space. V3d[]. Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Centroid of positions (global space). V3d.
        /// </summary>
        public static readonly Def PositionsGlobal3dCentroid = new(
            new Guid("2040882e-75b8-4fef-9965-1ffef92f4fd3"),
            "Octree.PositionsGlobal3d.Centroid",
            "Octree. Centroid of positions (global space). V3d.",
            Aardvark.V3d.Id,
            false
            );

        /// <summary>
        /// Octree. Average point distance to centroid (global space). Float64.
        /// </summary>
        public static readonly Def PositionsGlobal3dDistToCentroidAverage = new(
            new Guid("03a45262-6764-459d-9e2d-73bf6338d3a6"),
            "Octree.PositionsGlobal3d.DistToCentroid.Average",
            "Octree. Average point distance to centroid (global space). Float64.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Standard deviation of average point distance to centroid (global space). Float64.
        /// </summary>
        public static readonly Def PositionsGlobal3dDistToCentroidStdDev = new(
            new Guid("61d5ad06-37d3-4df2-a999-6599efc2ae83"),
            "Octree.PositionsGlobal3d.DistToCentroid.StdDev",
            "Octree. Standard deviation of average point distance to centroid (global space). Float64.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to per-point positions in global space. Guid.
        /// </summary>
        public static readonly Def PositionsGlobal3dReference = new(
            new Guid("839e1897-5fa3-426b-b66f-af166048ec34"),
            "Octree.PositionsGlobal3d.Reference",
            "Octree. Reference to per-point positions in global space. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[].
        /// </summary>
        public static readonly Def PositionsLocal3f = new(
            new Guid("05eb38fa-1b6a-4576-820b-780163199db9"),
            "Octree.PositionsLocal3f",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3fGz = new(
            new Guid("5dd6898c-c379-e673-8155-c4ce1f94a0dc"),
            "Octree.PositionsLocal3f.gz",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3fLz4 = new(
            new Guid("953570c3-606b-ad25-e4a4-0005e06e58dd"),
            "Octree.PositionsLocal3f.lz4",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Centroid of positions (local space). V3f.
        /// </summary>
        public static readonly Def PositionsLocal3fCentroid = new(
            new Guid("bd6cc4ab-6a41-49b3-aca2-ca4f21510609"),
            "Octree.PositionsLocal3f.Centroid",
            "Octree. Centroid of positions (local space). V3f.",
            Aardvark.V3f.Id,
            false
            );

        /// <summary>
        /// Octree. Average point distance to centroid (local space). Float32.
        /// </summary>
        public static readonly Def PositionsLocal3fDistToCentroidAverage = new(
            new Guid("1b7e74c5-b2ba-46fd-a7db-c08734da3b75"),
            "Octree.PositionsLocal3f.DistToCentroid.Average",
            "Octree. Average point distance to centroid (local space). Float32.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Standard deviation of average point distance to centroid (local space). Float32.
        /// </summary>
        public static readonly Def PositionsLocal3fDistToCentroidStdDev = new(
            new Guid("c927d42b-02d8-480e-be93-0660eefd62a5"),
            "Octree.PositionsLocal3f.DistToCentroid.StdDev",
            "Octree. Standard deviation of average point distance to centroid (local space). Float32.",
            Primitives.Float32.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.
        /// </summary>
        public static readonly Def PositionsLocal3fReference = new(
            new Guid("f3d3264d-abb4-47c5-963b-39d1a1728fa9"),
            "Octree.PositionsLocal3f.Reference",
            "Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[].
        /// </summary>
        public static readonly Def PositionsLocal3d = new(
            new Guid("303bb5ba-3488-4d40-9002-c484aa4b93e1"),
            "Octree.PositionsLocal3d",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[].",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsLocal3dGz = new(
            new Guid("7b87c409-2357-7bea-15f5-a0fc34c0a93f"),
            "Octree.PositionsLocal3d.gz",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[]. Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsLocal3dLz4 = new(
            new Guid("4c9618fd-4a8d-7f51-39ba-0f0790e6e6e2"),
            "Octree.PositionsLocal3d.lz4",
            "Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[]. Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Centroid of positions (local space). V3d.
        /// </summary>
        public static readonly Def PositionsLocal3dCentroid = new(
            new Guid("000f0635-6b73-49ac-b44a-bcf6d6dcbef0"),
            "Octree.PositionsLocal3d.Centroid",
            "Octree. Centroid of positions (local space). V3d.",
            Aardvark.V3d.Id,
            false
            );

        /// <summary>
        /// Octree. Average point distance to centroid (local space). Float64.
        /// </summary>
        public static readonly Def PositionsLocal3dDistToCentroidAverage = new(
            new Guid("6c191627-eb5b-44a1-82c3-eb5da439e493"),
            "Octree.PositionsLocal3d.DistToCentroid.Average",
            "Octree. Average point distance to centroid (local space). Float64.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Standard deviation of average point distance to centroid (local space). Float64.
        /// </summary>
        public static readonly Def PositionsLocal3dDistToCentroidStdDev = new(
            new Guid("5ea424e2-6a47-411e-a398-062e81194ada"),
            "Octree.PositionsLocal3d.DistToCentroid.StdDev",
            "Octree. Standard deviation of average point distance to centroid (local space). Float64.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.
        /// </summary>
        public static readonly Def PositionsLocal3dReference = new(
            new Guid("b4c7208d-98e2-4c30-a18a-c853746ee78a"),
            "Octree.PositionsLocal3d.Reference",
            "Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[].
        /// </summary>
        public static readonly Def PositionsDistanceToCentroidRange = new(
            new Guid("0b326504-2496-4e21-8ec3-bfb8e7b47ac1"),
            "Octree.Positions.DistanceToCentroid.Range",
            "Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[].",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def PositionsDistanceToCentroidRangeGz = new(
            new Guid("d38358f3-a95a-a926-003b-ff300a6d27ec"),
            "Octree.Positions.DistanceToCentroid.Range.gz",
            "Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[]. Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def PositionsDistanceToCentroidRangeLz4 = new(
            new Guid("527bbf01-7651-7507-2a32-d661d833b4d3"),
            "Octree.Positions.DistanceToCentroid.Range.lz4",
            "Octree. Positions' per-component distance centroid [min, max, avg, stddev]. V3d[]. Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals. V3f[].
        /// </summary>
        public static readonly Def Normals3f = new(
            new Guid("712d0a0c-a8d0-42d1-bfc7-77eac2e4a755"),
            "Octree.Normals3f",
            "Octree. Per-point normals. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Normals3fGz = new(
            new Guid("4ef65720-c2a0-e7ff-a093-5c4484e31f94"),
            "Octree.Normals3f.gz",
            "Octree. Per-point normals. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Normals3fLz4 = new(
            new Guid("9c425ace-b766-875e-3bcf-036d39b7f351"),
            "Octree.Normals3f.lz4",
            "Octree. Per-point normals. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point normals (Guid).
        /// </summary>
        public static readonly Def Normals3fReference = new(
            new Guid("0fb38f30-08fb-402f-bc10-a7c54d92fb26"),
            "Octree.Normals3f.Reference",
            "Octree. Reference to per-point normals (Guid).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[].
        /// </summary>
        public static readonly Def Normals3sb = new(
            new Guid("aaf4872c-0964-4351-9530-8a3e2be94a6e"),
            "Octree.Normals3sb",
            "Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[].",
            Primitives.Int8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Normals3sbGz = new(
            new Guid("f125a474-4d7d-38a2-f5a9-49b855f1d90d"),
            "Octree.Normals3sb.gz",
            "Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[]. Compressed (GZip).",
            Primitives.Int8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Normals3sbLz4 = new(
            new Guid("84d4dc1f-bd40-4ba4-f012-b9065bf78c91"),
            "Octree.Normals3sb.lz4",
            "Octree. Per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0]. Int8[]. Compressed (LZ4).",
            Primitives.Int8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
        /// </summary>
        public static readonly Def Normals3sbReference = new(
            new Guid("eb245ac4-a207-4428-87ea-2e715b9f01ef"),
            "Octree.Normals3sb.Reference",
            "Octree. Reference to per-point normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[].
        /// </summary>
        public static readonly Def NormalsOct16 = new(
            new Guid("144770e4-70ea-4dd2-91a5-91f48672e87e"),
            "Octree.Normals.Oct16",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[].",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def NormalsOct16Gz = new(
            new Guid("01a4f6a5-3c6f-cdf8-35ae-8dbc0cf4884d"),
            "Octree.Normals.Oct16.gz",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def NormalsOct16Lz4 = new(
            new Guid("08cb4e93-fa23-18fd-d550-a5e53fffbcbb"),
            "Octree.Normals.Oct16.lz4",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        /// </summary>
        public static readonly Def NormalsOct16Reference = new(
            new Guid("7a397f13-b0dd-4925-89a2-066ef5426be3"),
            "Octree.Normals.Oct16.Reference",
            "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[].
        /// </summary>
        public static readonly Def NormalsOct16P = new(
            new Guid("5fdf162c-bd21-4688-aa5c-91dd0a550c44"),
            "Octree.Normals.Oct16P",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[].",
            Primitives.Int16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def NormalsOct16PGz = new(
            new Guid("03181ec7-06a3-98f4-37e1-da91573ccb97"),
            "Octree.Normals.Oct16P.gz",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (GZip).",
            Primitives.Int16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def NormalsOct16PLz4 = new(
            new Guid("17b38b5c-bb39-f9ea-c2f8-05651bf5c11b"),
            "Octree.Normals.Oct16P.lz4",
            "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf. Int16[]. Compressed (LZ4).",
            Primitives.Int16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
        /// </summary>
        public static readonly Def NormalsOct16PReference = new(
            new Guid("eec0ba91-bdcf-469a-b2f2-9c46009b04e6"),
            "Octree.Normals.Oct16P.Reference",
            "Octree. Reference to per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point colors. C3b[].
        /// </summary>
        public static readonly Def Colors3b = new(
            new Guid("61cb1fa8-b2e2-41ae-8022-5787b44ee058"),
            "Octree.Colors3b",
            "Octree. Per-point colors. C3b[].",
            Aardvark.C3bArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors. C3b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors3bGz = new(
            new Guid("229230d3-301f-2232-123c-b8d7029a2dc4"),
            "Octree.Colors3b.gz",
            "Octree. Per-point colors. C3b[]. Compressed (GZip).",
            Aardvark.C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors. C3b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors3bLz4 = new(
            new Guid("ef9ac31e-a870-9029-d47b-9ca7332238ea"),
            "Octree.Colors3b.lz4",
            "Octree. Per-point colors. C3b[]. Compressed (LZ4).",
            Aardvark.C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point colors. Guid.
        /// </summary>
        public static readonly Def Colors3bReference = new(
            new Guid("b8a664d9-c77d-4ea6-a196-6d82602356a2"),
            "Octree.Colors3b.Reference",
            "Octree. Reference to per-point colors. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[].
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead.")]
        public static readonly Def Colors4bDeprecated20201117 = new(
            new Guid("c91dfea3-243d-4272-9dba-b572931dba23"),
            "Octree.Colors4b.Deprecated.20201117",
            "Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[].",
            Aardvark.C3bArray.Id,
            true
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[]. Compressed (GZip).
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead.")]
        public static readonly Def Colors4bDeprecated20201117Gz = new(
            new Guid("335790c0-5213-7580-ed7a-c8bec932ac2e"),
            "Octree.Colors4b.Deprecated.20201117.gz",
            "Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[]. Compressed (GZip).",
            Aardvark.C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[]. Compressed (LZ4).
        /// </summary>
        [Obsolete("Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead.")]
        public static readonly Def Colors4bDeprecated20201117Lz4 = new(
            new Guid("7333a94b-f53a-c975-7bfc-f250c374eff4"),
            "Octree.Colors4b.Deprecated.20201117.lz4",
            "Deprecated 2020-11-17. Wrong type. Use 4f6144d1-c424-424c-9e02-915bf58087b2 instead. Octree. Per-point colors. C4b[]. Compressed (LZ4).",
            Aardvark.C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V3f[].
        /// </summary>
        public static readonly Def Colors3bRange = new(
            new Guid("74d787e9-b882-4df5-b6c6-6da02a50d35f"),
            "Octree.Colors3b.Range",
            "Octree. Per-component color [min, max, avg, stddev]. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors3bRangeGz = new(
            new Guid("440fa43b-d33e-5295-11ac-b4884e6bbe83"),
            "Octree.Colors3b.Range.gz",
            "Octree. Per-component color [min, max, avg, stddev]. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors3bRangeLz4 = new(
            new Guid("52d6228e-dd61-8b12-f19f-6d0c5351327c"),
            "Octree.Colors3b.Range.lz4",
            "Octree. Per-component color [min, max, avg, stddev]. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors. C4b[].
        /// </summary>
        public static readonly Def Colors4b = new(
            new Guid("4f6144d1-c424-424c-9e02-915bf58087b2"),
            "Octree.Colors4b",
            "Octree. Per-point colors. C4b[].",
            Aardvark.C4bArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors. C4b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors4bGz = new(
            new Guid("b8e6ba93-dd1f-d6ed-ab90-76d3bf5dce60"),
            "Octree.Colors4b.gz",
            "Octree. Per-point colors. C4b[]. Compressed (GZip).",
            Aardvark.C4bArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors. C4b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors4bLz4 = new(
            new Guid("b8f326e0-8572-386d-7976-734c94557ccb"),
            "Octree.Colors4b.lz4",
            "Octree. Per-point colors. C4b[]. Compressed (LZ4).",
            Aardvark.C4bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point colors. Guid.
        /// </summary>
        public static readonly Def Colors4bReference = new(
            new Guid("cb2bdeae-2085-442b-90bc-990b892fdb61"),
            "Octree.Colors4b.Reference",
            "Octree. Reference to per-point colors. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V4f[].
        /// </summary>
        public static readonly Def Colors4bRange = new(
            new Guid("a808ead8-c8f7-4269-be1c-935f25da4d09"),
            "Octree.Colors4b.Range",
            "Octree. Per-component color [min, max, avg, stddev]. V4f[].",
            Aardvark.V4fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V4f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Colors4bRangeGz = new(
            new Guid("a72583cf-018a-eed5-f6db-9a56cf812449"),
            "Octree.Colors4b.Range.gz",
            "Octree. Per-component color [min, max, avg, stddev]. V4f[]. Compressed (GZip).",
            Aardvark.V4fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-component color [min, max, avg, stddev]. V4f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Colors4bRangeLz4 = new(
            new Guid("f14008d9-13fe-1557-4b10-db4e897e8b47"),
            "Octree.Colors4b.Range.lz4",
            "Octree. Per-component color [min, max, avg, stddev]. V4f[]. Compressed (LZ4).",
            Aardvark.V4fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].
        /// </summary>
        public static readonly Def ColorsRGB565 = new(
            new Guid("bf36c54b-f199-4138-a32f-c089cf527dad"),
            "Octree.Colors.RGB565",
            "Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ColorsRGB565Gz = new(
            new Guid("d5095e07-322a-117e-c885-c0da23510b00"),
            "Octree.Colors.RGB565.gz",
            "Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ColorsRGB565Lz4 = new(
            new Guid("dc135f07-024a-d773-1a99-f17cb59a9281"),
            "Octree.Colors.RGB565.lz4",
            "Octree. Per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid.
        /// </summary>
        public static readonly Def ColorsRGB565Reference = new(
            new Guid("9557c438-16b0-49c7-979d-8de5dc8829b4"),
            "Octree.Colors.RGB565.Reference",
            "Octree. Reference to per-point colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C3b[].
        /// </summary>
        public static readonly Def Cir3b = new(
            new Guid("025da754-95fa-4a51-96c7-e775d68a44c3"),
            "Octree.Cir3b",
            "Octree. Per-point colored infrared (CIR). C3b[].",
            Aardvark.C3bArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C3b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Cir3bGz = new(
            new Guid("4083343b-f71e-470a-be08-c51d5f6388bc"),
            "Octree.Cir3b.gz",
            "Octree. Per-point colored infrared (CIR). C3b[]. Compressed (GZip).",
            Aardvark.C3bArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C3b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cir3bLz4 = new(
            new Guid("cd49870c-092e-f10b-dd49-0d1ab3800138"),
            "Octree.Cir3b.lz4",
            "Octree. Per-point colored infrared (CIR). C3b[]. Compressed (LZ4).",
            Aardvark.C3bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Per-point colored infrared (CIR). Guid.
        /// </summary>
        public static readonly Def Cir3bReference = new(
            new Guid("3192a4c1-f398-4ce7-b3a4-851aacc27f68"),
            "Octree.Cir3b.Reference",
            "Octree. Reference to Per-point colored infrared (CIR). Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[].
        /// </summary>
        public static readonly Def Cir3bRange = new(
            new Guid("af1737d6-8732-4d27-baec-b7446403f006"),
            "Octree.Cir3b.Range",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[].",
            Aardvark.V4fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Cir3bRangeGz = new(
            new Guid("93464f13-257b-0cf2-c778-94ba779614ec"),
            "Octree.Cir3b.Range.gz",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (GZip).",
            Aardvark.V4fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cir3bRangeLz4 = new(
            new Guid("0edef8a9-7786-45eb-dfb1-cfb91be9082d"),
            "Octree.Cir3b.Range.lz4",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (LZ4).",
            Aardvark.V4fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C4b[].
        /// </summary>
        public static readonly Def Cir4b = new(
            new Guid("ea719119-06a0-45cd-b40c-474b013c4827"),
            "Octree.Cir4b",
            "Octree. Per-point colored infrared (CIR). C4b[].",
            Aardvark.C4bArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C4b[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Cir4bGz = new(
            new Guid("ea0379b7-8e63-0d06-14f9-f7e6673e3917"),
            "Octree.Cir4b.gz",
            "Octree. Per-point colored infrared (CIR). C4b[]. Compressed (GZip).",
            Aardvark.C4bArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR). C4b[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cir4bLz4 = new(
            new Guid("6978642e-2e05-a54f-ef93-d7b31f600c87"),
            "Octree.Cir4b.lz4",
            "Octree. Per-point colored infrared (CIR). C4b[]. Compressed (LZ4).",
            Aardvark.C4bArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Per-point colored infrared (CIR). Guid.
        /// </summary>
        public static readonly Def Cir4bReference = new(
            new Guid("54791fac-5798-43a7-87da-a1996fc18bd2"),
            "Octree.Cir4b.Reference",
            "Octree. Reference to Per-point colored infrared (CIR). Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[].
        /// </summary>
        public static readonly Def Cir4bRange = new(
            new Guid("06d227e7-467f-4205-af1a-1705bb5fea39"),
            "Octree.Cir4b.Range",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[].",
            Aardvark.V4fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Cir4bRangeGz = new(
            new Guid("a4fac97c-a4ea-b331-3b8c-5f111570e473"),
            "Octree.Cir4b.Range.gz",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (GZip).",
            Aardvark.V4fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Cir4bRangeLz4 = new(
            new Guid("d222b1ae-23b8-b49f-7f9f-87e506d71fb1"),
            "Octree.Cir4b.Range.lz4",
            "Octree. Per-point colored infrared (CIR) statistics. [min, max, avg, stddev]. V4f[]. Compressed (LZ4).",
            Aardvark.V4fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. UInt8[].
        /// </summary>
        public static readonly Def Intensities1b = new(
            new Guid("3fa600f0-6e99-4c11-918b-810430bae0cb"),
            "Octree.Intensities1b",
            "Octree. Per-point intensities. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Intensities1bGz = new(
            new Guid("35266dd8-3431-7911-867e-b5b8a91850ae"),
            "Octree.Intensities1b.gz",
            "Octree. Per-point intensities. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Intensities1bLz4 = new(
            new Guid("11a66670-0493-3f5a-f036-08e8825a5f0d"),
            "Octree.Intensities1b.lz4",
            "Octree. Per-point intensities. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point intensities. Guid.
        /// </summary>
        public static readonly Def Intensities1bReference = new(
            new Guid("56c02251-b860-40b4-a1d2-3570d1b9e62f"),
            "Octree.Intensities1b.Reference",
            "Octree. Reference to per-point intensities. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point intensities. Int32[].
        /// </summary>
        public static readonly Def Intensities1i = new(
            new Guid("361027fd-ac58-4de8-89ee-98695f8c5520"),
            "Octree.Intensities1i",
            "Octree. Per-point intensities. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Intensities1iGz = new(
            new Guid("115f3492-20eb-e192-5643-8c775224ae15"),
            "Octree.Intensities1i.gz",
            "Octree. Per-point intensities. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Intensities1iLz4 = new(
            new Guid("741f27a6-b977-5abe-ed3f-f5ef911bf181"),
            "Octree.Intensities1i.lz4",
            "Octree. Per-point intensities. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point intensities. Guid.
        /// </summary>
        public static readonly Def Intensities1iReference = new(
            new Guid("4e6842a2-3c3a-4b4e-a773-06ba138ad86e"),
            "Octree.Intensities1i.Reference",
            "Octree. Reference to per-point intensities. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point intensities. Float32[].
        /// </summary>
        public static readonly Def Intensities1f = new(
            new Guid("ebe476d9-32e8-4d47-982e-35703c3a6b4c"),
            "Octree.Intensities1f",
            "Octree. Per-point intensities. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Intensities1fGz = new(
            new Guid("77d21399-6780-4322-bf94-6e4eeabb43fd"),
            "Octree.Intensities1f.gz",
            "Octree. Per-point intensities. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Intensities1fLz4 = new(
            new Guid("ed6ca58f-14d4-3b64-50d4-ff6f1dcaba66"),
            "Octree.Intensities1f.lz4",
            "Octree. Per-point intensities. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.Intensities1f. Guid.
        /// </summary>
        public static readonly Def Intensities1fReference = new(
            new Guid("85509d76-d44c-4839-8c36-52abb2c35679"),
            "Octree.Intensities1f.Reference",
            "Octree. Reference to Octree.Intensities1f. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[].
        /// </summary>
        public static readonly Def IntensitiesWithOffset1f = new(
            new Guid("6753a6e1-9633-4997-b403-661578191f8c"),
            "Octree.IntensitiesWithOffset1f",
            "Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def IntensitiesWithOffset1fGz = new(
            new Guid("497cab83-7a15-bce3-741f-936b30bbdb00"),
            "Octree.IntensitiesWithOffset1f.gz",
            "Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def IntensitiesWithOffset1fLz4 = new(
            new Guid("0f86f555-5768-16b1-8576-be9a8dd76219"),
            "Octree.IntensitiesWithOffset1f.lz4",
            "Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to Octree.IntensitiesWithOffset1f. Guid.
        /// </summary>
        public static readonly Def IntensitiesWithOffset1fReference = new(
            new Guid("d2874881-99a6-4eed-aa15-0d34b03e150e"),
            "Octree.IntensitiesWithOffset1f.Reference",
            "Octree. Reference to Octree.IntensitiesWithOffset1f. Guid.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. The offset for Octree.IntensitiesWithOffset1f[.Reference]. Float64.
        /// </summary>
        public static readonly Def IntensitiesWithOffset1fOffset = new(
            new Guid("5b237e1d-743e-435e-9daa-b9884d7a4419"),
            "Octree.IntensitiesWithOffset1f.Offset",
            "Octree. The offset for Octree.IntensitiesWithOffset1f[.Reference]. Float64.",
            Primitives.Float64.Id,
            false
            );

        /// <summary>
        /// Octree. Intensities range for Octree.IntensitiesWithOffset1f[.Reference]. Range1f.
        /// </summary>
        public static readonly Def IntensitiesWithOffset1fRange = new(
            new Guid("435d8a84-c195-456c-b87b-ded2e5930134"),
            "Octree.IntensitiesWithOffset1f.Range",
            "Octree. Intensities range for Octree.IntensitiesWithOffset1f[.Reference]. Range1f.",
            Aardvark.Range1f.Id,
            false
            );

        /// <summary>
        /// Octree. Range of intensitity values [min, max, avg, stddev]. Float32[].
        /// </summary>
        public static readonly Def IntensitiesRange = new(
            new Guid("eb990bdb-fc45-4129-987c-0912887375d6"),
            "Octree.Intensities.Range",
            "Octree. Range of intensitity values [min, max, avg, stddev]. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Range of intensitity values [min, max, avg, stddev]. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def IntensitiesRangeGz = new(
            new Guid("19d6b58c-902c-dbfe-e732-10bd8725ce19"),
            "Octree.Intensities.Range.gz",
            "Octree. Range of intensitity values [min, max, avg, stddev]. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Range of intensitity values [min, max, avg, stddev]. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def IntensitiesRangeLz4 = new(
            new Guid("34487143-a024-931b-cd7a-6a6e427dd7a1"),
            "Octree.Intensities.Range.lz4",
            "Octree. Range of intensitity values [min, max, avg, stddev]. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt8[].
        /// </summary>
        public static readonly Def Classifications1b = new(
            new Guid("d25cff0e-ea80-445b-ab72-d0a5a1013818"),
            "Octree.Classifications1b",
            "Octree. Per-point classifications. UInt8[].",
            Primitives.UInt8Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt8[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1bGz = new(
            new Guid("a9a23e98-f397-66fc-faf8-fdbe08f44041"),
            "Octree.Classifications1b.gz",
            "Octree. Per-point classifications. UInt8[]. Compressed (GZip).",
            Primitives.UInt8ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt8[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1bLz4 = new(
            new Guid("8060ebde-5ea3-a9c5-1533-3acc25ebfa19"),
            "Octree.Classifications1b.lz4",
            "Octree. Per-point classifications. UInt8[]. Compressed (LZ4).",
            Primitives.UInt8ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point classifications (UInt8[]).
        /// </summary>
        public static readonly Def Classifications1bReference = new(
            new Guid("9056806d-eb49-4c09-83cd-0fec099b016e"),
            "Octree.Classifications1b.Reference",
            "Octree. Reference to per-point classifications (UInt8[]).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt16[].
        /// </summary>
        public static readonly Def Classifications1s = new(
            new Guid("b1619ade-79be-4554-894e-3f7e46240119"),
            "Octree.Classifications1s",
            "Octree. Per-point classifications. UInt16[].",
            Primitives.UInt16Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt16[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1sGz = new(
            new Guid("998532f3-0659-df8f-8a2b-2117a73cf619"),
            "Octree.Classifications1s.gz",
            "Octree. Per-point classifications. UInt16[]. Compressed (GZip).",
            Primitives.UInt16ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. UInt16[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1sLz4 = new(
            new Guid("48c43927-ef18-96ef-a5a9-8621171bd905"),
            "Octree.Classifications1s.lz4",
            "Octree. Per-point classifications. UInt16[]. Compressed (LZ4).",
            Primitives.UInt16ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point classifications (UInt16[]).
        /// </summary>
        public static readonly Def Classifications1sReference = new(
            new Guid("3142284a-d7e0-45f9-8044-44800df1daac"),
            "Octree.Classifications1s.Reference",
            "Octree. Reference to per-point classifications (UInt16[]).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point classifications. Int32[].
        /// </summary>
        public static readonly Def Classifications1i = new(
            new Guid("826cc58d-ed89-4d56-b389-e4b581c71706"),
            "Octree.Classifications1i",
            "Octree. Per-point classifications. Int32[].",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. Int32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Classifications1iGz = new(
            new Guid("f457d3ed-b10a-193c-1f62-f1f0c712d93c"),
            "Octree.Classifications1i.gz",
            "Octree. Per-point classifications. Int32[]. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications. Int32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Classifications1iLz4 = new(
            new Guid("e426b104-b766-da95-8d71-35b3c21a802c"),
            "Octree.Classifications1i.lz4",
            "Octree. Per-point classifications. Int32[]. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point classifications (Guid -> Int32[]).
        /// </summary>
        public static readonly Def Classifications1iReference = new(
            new Guid("045cc89b-73de-4170-bb55-e108853e9779"),
            "Octree.Classifications1i.Reference",
            "Octree. Reference to per-point classifications (Guid -> Int32[]).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Range of classification values. Range1i.
        /// </summary>
        public static readonly Def ClassificationsRange = new(
            new Guid("1844a484-33d5-42b6-9471-6b892d105284"),
            "Octree.Classifications.Range",
            "Octree. Range of classification values. Range1i.",
            Aardvark.Range1i.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[].
        /// </summary>
        public static readonly Def ClassificationsHistogram = new(
            new Guid("ecada344-5cc2-497f-9078-3d80559ba08c"),
            "Octree.Classifications.Histogram",
            "Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[].",
            Primitives.Int64Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[]. Compressed (GZip).
        /// </summary>
        public static readonly Def ClassificationsHistogramGz = new(
            new Guid("9089d380-53c3-55b7-2c73-1611321831d2"),
            "Octree.Classifications.Histogram.gz",
            "Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[]. Compressed (GZip).",
            Primitives.Int64ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def ClassificationsHistogramLz4 = new(
            new Guid("cfe35b99-e0ec-c631-1dbc-ecda6fca5e85"),
            "Octree.Classifications.Histogram.lz4",
            "Octree. Per-point classifications histogram. [value_0, count_0, ... value_n, count_n]. Int64[]. Compressed (LZ4).",
            Primitives.Int64ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point velocities. V3f[].
        /// </summary>
        public static readonly Def Velocities3f = new(
            new Guid("c8db5f0a-1ddf-47ab-8266-f8e929cf98c5"),
            "Octree.Velocities3f",
            "Octree. Per-point velocities. V3f[].",
            Aardvark.V3fArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point velocities. V3f[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Velocities3fGz = new(
            new Guid("9544a0b8-6686-6448-db40-de0fd7ba9989"),
            "Octree.Velocities3f.gz",
            "Octree. Per-point velocities. V3f[]. Compressed (GZip).",
            Aardvark.V3fArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point velocities. V3f[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Velocities3fLz4 = new(
            new Guid("09dcd3f2-dcb8-539f-0e53-5ba968541e61"),
            "Octree.Velocities3f.lz4",
            "Octree. Per-point velocities. V3f[]. Compressed (LZ4).",
            Aardvark.V3fArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point velocities (Guid).
        /// </summary>
        public static readonly Def Velocities3fReference = new(
            new Guid("390115cc-5928-4526-8c28-37e709bf31d2"),
            "Octree.Velocities3f.Reference",
            "Octree. Reference to per-point velocities (Guid).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point velocities. V3d[].
        /// </summary>
        public static readonly Def Velocities3d = new(
            new Guid("3f8a922d-3458-427f-8237-a189e338bf77"),
            "Octree.Velocities3d",
            "Octree. Per-point velocities. V3d[].",
            Aardvark.V3dArray.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point velocities. V3d[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Velocities3dGz = new(
            new Guid("13353638-7c53-9f2c-d96d-bef9c333e9af"),
            "Octree.Velocities3d.gz",
            "Octree. Per-point velocities. V3d[]. Compressed (GZip).",
            Aardvark.V3dArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point velocities. V3d[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Velocities3dLz4 = new(
            new Guid("7928d366-8f9b-20c5-e572-19e7c8530571"),
            "Octree.Velocities3d.lz4",
            "Octree. Per-point velocities. V3d[]. Compressed (LZ4).",
            Aardvark.V3dArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point velocities (Guid).
        /// </summary>
        public static readonly Def Velocities3dReference = new(
            new Guid("f7d97f68-e1e1-4b1b-9133-42689b6fb65b"),
            "Octree.Velocities3d.Reference",
            "Octree. Reference to per-point velocities (Guid).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Per-point densities. Float32[].
        /// </summary>
        public static readonly Def Densities1f = new(
            new Guid("040d084d-1f1b-4058-afc7-ea300bbb551d"),
            "Octree.Densities1f",
            "Octree. Per-point densities. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point densities. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def Densities1fGz = new(
            new Guid("be9c7392-391b-d2e8-d1eb-04493025c3f6"),
            "Octree.Densities1f.gz",
            "Octree. Per-point densities. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Per-point densities. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def Densities1fLz4 = new(
            new Guid("068f649b-ded4-60ba-8f12-18c27ae09d58"),
            "Octree.Densities1f.lz4",
            "Octree. Per-point densities. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to per-point densities (Guid).
        /// </summary>
        public static readonly Def Densities1fReference = new(
            new Guid("bf51bb51-7947-49ec-9f60-2d4f78a60674"),
            "Octree.Densities1f.Reference",
            "Octree. Reference to per-point densities (Guid).",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Range of density values [min, max, avg, stddev]. Float32[].
        /// </summary>
        public static readonly Def DensititesRange = new(
            new Guid("05c6b527-0952-424b-86b9-5be5df884c5e"),
            "Octree.Densitites.Range",
            "Octree. Range of density values [min, max, avg, stddev]. Float32[].",
            Primitives.Float32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Range of density values [min, max, avg, stddev]. Float32[]. Compressed (GZip).
        /// </summary>
        public static readonly Def DensititesRangeGz = new(
            new Guid("9f5a4b96-b66e-86c4-c0a8-38d71651de26"),
            "Octree.Densitites.Range.gz",
            "Octree. Range of density values [min, max, avg, stddev]. Float32[]. Compressed (GZip).",
            Primitives.Float32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Range of density values [min, max, avg, stddev]. Float32[]. Compressed (LZ4).
        /// </summary>
        public static readonly Def DensititesRangeLz4 = new(
            new Guid("b6b8cecb-9bb8-14da-79af-cf9d79244922"),
            "Octree.Densitites.Range.lz4",
            "Octree. Range of density values [min, max, avg, stddev]. Float32[]. Compressed (LZ4).",
            Primitives.Float32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.
        /// </summary>
        public static readonly Def KdTreeIndexArray = new(
            new Guid("c533bd54-9aff-40e1-a2bb-c69c9778fecb"),
            "Octree.KdTreeIndexArray",
            "Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively.",
            Primitives.Int32Array.Id,
            true
            );

        /// <summary>
        /// Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively. Compressed (GZip).
        /// </summary>
        public static readonly Def KdTreeIndexArrayGz = new(
            new Guid("d82f7171-ff4f-b2a4-2a11-8167dee0cf7b"),
            "Octree.KdTreeIndexArray.gz",
            "Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively. Compressed (GZip).",
            Primitives.Int32ArrayGz.Id,
            true
            );

        /// <summary>
        /// Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively. Compressed (LZ4).
        /// </summary>
        public static readonly Def KdTreeIndexArrayLz4 = new(
            new Guid("1a1ddb16-338d-9a98-debc-c3710433bfa5"),
            "Octree.KdTreeIndexArray.lz4",
            "Octree. Kd-tree index array. Int32[], where pivot is stored at index n/2 recursively. Compressed (LZ4).",
            Primitives.Int32ArrayLz4.Id,
            true
            );

        /// <summary>
        /// Octree. Reference to kd-tree index array. Guid -> Int32[].
        /// </summary>
        public static readonly Def KdTreeIndexArrayReference = new(
            new Guid("fc2b48cb-ab79-4579-92a3-0a421c8d9112"),
            "Octree.KdTreeIndexArray.Reference",
            "Octree. Reference to kd-tree index array. Guid -> Int32[].",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Aardvark.Geometry.PointRkdTreeFData.
        /// </summary>
        public static readonly Def PointRkdTreeFData = new(
            new Guid("c90f303c-f9be-49d1-9188-51fde1e1e75d"),
            "Octree.PointRkdTreeFData",
            "Octree. Aardvark.Geometry.PointRkdTreeFData.",
            Aardvark.PointRkdTreeFData.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to Aardvark.Geometry.PointRkdTreeFData.
        /// </summary>
        public static readonly Def PointRkdTreeFDataReference = new(
            new Guid("d48d006e-9840-433c-afdb-c5fc5b14be54"),
            "Octree.PointRkdTreeFData.Reference",
            "Octree. Reference to Aardvark.Geometry.PointRkdTreeFData.",
            Primitives.GuidDef.Id,
            false
            );

        /// <summary>
        /// Octree. Aardvark.Geometry.PointRkdTreeDData.
        /// </summary>
        public static readonly Def PointRkdTreeDData = new(
            new Guid("c00e3cc0-983c-4c51-801f-8c55ff337b2d"),
            "Octree.PointRkdTreeDData",
            "Octree. Aardvark.Geometry.PointRkdTreeDData.",
            Aardvark.PointRkdTreeDData.Id,
            false
            );

        /// <summary>
        /// Octree. Reference to Aardvark.Geometry.PointRkdTreeDData.
        /// </summary>
        public static readonly Def PointRkdTreeDDataReference = new(
            new Guid("05cf0cac-4f8f-41bc-ac50-1b291297f892"),
            "Octree.PointRkdTreeDData.Reference",
            "Octree. Reference to Aardvark.Geometry.PointRkdTreeDData.",
            Primitives.GuidDef.Id,
            false
            );

    }
}
