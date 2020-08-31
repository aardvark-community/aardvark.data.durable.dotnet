/*
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
*/
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
            /// A durable definition stored inline. [Id:Guid][Name:StringUTF8][Description:StringUTF8][Type:StringUTF8].
            /// </summary>
            public static readonly Def DurableDefinition = new Def(
                new Guid("f924fd35-ae59-4b0e-a05b-e9c85536c52c"),
                "DurableDefinition",
                "A durable definition stored inline. [Id:Guid][Name:StringUTF8][Description:StringUTF8][Type:StringUTF8].",
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
            /// A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 8-byte aligned.
            /// </summary>
            public static readonly Def DurableMapAligned8 = new Def(
                new Guid("6780296f-c30a-4eba-806f-d07d84c7a5bc"),
                "DurableMapAligned8",
                "A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 8-byte aligned.",
                None,
                false
                );

            /// <summary>
            /// A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 16-byte aligned.
            /// </summary>
            public static readonly Def DurableMapAligned16 = new Def(
                new Guid("0ca48518-96b9-424f-b146-046ac3c8ed10"),
                "DurableMapAligned16",
                "A map of key/value pairs, where keys are durable IDs with values of corresponding types. Entries are 16-byte aligned.",
                None,
                false
                );

            /// <summary>
            /// A gzipped element.
            /// </summary>
            public static readonly Def GZipped = new Def(
                new Guid("7d8fc4c0-d727-4171-bc91-78f92f0c1aa4"),
                "GZipped",
                "A gzipped element.",
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
            /// Floating point value (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float16 = new Def(
                new Guid("7891b070-5249-479f-81b8-d8bca5127211"),
                "Float16",
                "Floating point value (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float16Array = new Def(
                new Guid("fb1d889e-b7bb-41f4-b047-1f6838cd5fdd"),
                "Float16[]",
                "Array of floating point values (IEEE 754, half precision, 16-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float32 = new Def(
                new Guid("23fb286f-663b-4c71-9923-7e51c500f4ed"),
                "Float32",
                "Floating point value (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float32Array = new Def(
                new Guid("a687a789-1b63-49e9-a2e4-8099aa7879e9"),
                "Float32[]",
                "Array of floating point values (IEEE 754, single precision, 32-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float64 = new Def(
                new Guid("c58c9b83-c2de-4153-a588-39c808aed50b"),
                "Float64",
                "Floating point value (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float64Array = new Def(
                new Guid("ba60cc30-2d56-45d8-a051-6b895b51bb3f"),
                "Float64[]",
                "Array of floating point values (IEEE 754, double precision, 64-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float128 = new Def(
                new Guid("5d343235-21f6-41e4-992e-93541db26502"),
                "Float128",
                "Floating point value (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float128Array = new Def(
                new Guid("6477a574-ffb0-4717-9f00-5fb9aff409ce"),
                "Float128[]",
                "Array of floating point values (IEEE 754, quadruple precision, 128-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Floating point value (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float256 = new Def(
                new Guid("4c7c4a8d-a4fb-43f5-82d8-34b8b171a05c"),
                "Float256",
                "Floating point value (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                false
                );

            /// <summary>
            /// Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.
            /// </summary>
            public static readonly Def Float256Array = new Def(
                new Guid("acb4dd89-57f7-4229-9f1a-da017947843b"),
                "Float256[]",
                "Array of floating point values (IEEE 754, octuple precision, 256-bit). https://en.wikipedia.org/wiki/IEEE_754.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID32 = new Def(
                new Guid("4b970fc3-ce64-45d0-a602-0546b155760a"),
                "DecimalBID32",
                "Decimal floating point value (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID32Array = new Def(
                new Guid("349d24de-e84f-44dc-81cc-f110bc907062"),
                "DecimalBID32[]",
                "Array of decimal floating point values (IEEE 754, 32-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD32 = new Def(
                new Guid("067151e9-eaee-498e-88f5-fba450dd6cca"),
                "DecimalDPD32",
                "Decimal floating point value (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD32Array = new Def(
                new Guid("25d58ac1-cac8-4d67-a0af-65363c86e126"),
                "DecimalDPD32[]",
                "Array of decimal floating point values (IEEE 754, 32-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal32_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 64-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID64 = new Def(
                new Guid("8a4c6d2b-c4f4-4e5c-864f-fa2226fb0414"),
                "DecimalBID64",
                "Decimal floating point value (IEEE 754, 64-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID64Array = new Def(
                new Guid("dea7b2cd-231a-4796-bfb9-30cc9d874f0c"),
                "DecimalBID64[]",
                "Array of decimal floating point values (6IEEE 754, 4-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD64 = new Def(
                new Guid("23f81713-bb19-4640-8e89-d91c5a31201e"),
                "DecimalDPD64",
                "Decimal floating point value (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD64Array = new Def(
                new Guid("34cf3e10-c798-4011-9a5c-00c4c053d34c"),
                "DecimalDPD64[]",
                "Array of decimal floating point values (IEEE 754, 64-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal64_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID128 = new Def(
                new Guid("4911e3b9-8c72-4f82-a1fa-8d1a3a2d799d"),
                "DecimalBID128",
                "Decimal floating point value (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
            /// </summary>
            public static readonly Def DecimalBID128Array = new Def(
                new Guid("1a90ad03-b00f-4db5-928a-9a949c85c5e4"),
                "DecimalBID128[]",
                "Array of decimal floating point values (IEEE 754, 128-bit) encoded as binary integer decimal (BID). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// Decimal floating point value (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD128 = new Def(
                new Guid("d45469bd-f10c-4fb7-b26c-296c38286044"),
                "DecimalDPD128",
                "Decimal floating point value (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
                None,
                false
                );

            /// <summary>
            /// Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.
            /// </summary>
            public static readonly Def DecimalDPD128Array = new Def(
                new Guid("c4e6665a-5387-4f71-b224-27775cffeaf5"),
                "DecimalDPD128[]",
                "Array of decimal floating point values (IEEE 754, 128-bit) encoded as densely packed decimal (DPD). https://en.wikipedia.org/wiki/Decimal128_floating-point_format.",
                None,
                true
                );

            /// <summary>
            /// A .NET decimal value (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.
            /// </summary>
            public static readonly Def DecimalDotnet = new Def(
                new Guid("eada3477-f3a5-48a4-a05c-da2aa359e034"),
                "DecimalDotnet",
                "A .NET decimal value (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.",
                None,
                false
                );

            /// <summary>
            /// Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.
            /// </summary>
            public static readonly Def DecimalDotnetArray = new Def(
                new Guid("b7327b1f-f349-4014-b244-aa328922e69f"),
                "DecimalDotnet[]",
                "Array of .NET decimal values (System.Decimal, 128-bit). https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types#the-decimal-type.",
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

            /// <summary>
            /// Float64[] data stored as Int32[] plus Float64 offset.
            /// </summary>
            public static readonly Def Float32ArrayWithFloat64Offset = new Def(
                new Guid("aab1d3e2-80f9-4f12-895c-81cd5fc5d096"),
                "Float32ArrayWithFloat64Offset",
                "Float64[] data stored as Int32[] plus Float64 offset.",
                None,
                false
                );

            /// <summary>
            /// Int64[] data stored as Int32[] plus Int64 offset.
            /// </summary>
            public static readonly Def Int32ArrayWithInt64Offset = new Def(
                new Guid("8061bb56-3076-4afd-865d-c9a7701d225a"),
                "Int32ArrayWithInt64Offset",
                "Int64[] data stored as Int32[] plus Int64 offset.",
                None,
                false
                );

            /// <summary>
            /// Int64[] data stored as Int16[] plus Int64 offset.
            /// </summary>
            public static readonly Def Int16ArrayWithInt64Offset = new Def(
                new Guid("f200bcb7-e462-4d42-88e8-d8bfcb10c265"),
                "Int16ArrayWithInt64Offset",
                "Int64[] data stored as Int16[] plus Int64 offset.",
                None,
                false
                );

            /// <summary>
            /// Int64[] data stored as Int8[] plus Int64 offset.
            /// </summary>
            public static readonly Def Int8ArrayWithInt64Offset = new Def(
                new Guid("46cc0b8e-c4e4-4626-940f-d2adc28c0c00"),
                "Int8ArrayWithInt64Offset",
                "Int64[] data stored as Int8[] plus Int64 offset.",
                None,
                false
                );

            /// <summary>
            /// Int32[] data stored as Int16[] plus Int32 offset.
            /// </summary>
            public static readonly Def Int16ArrayWithInt32Offset = new Def(
                new Guid("8daf811d-3d1c-4219-8f2e-22c6c49de6cd"),
                "Int16ArrayWithInt32Offset",
                "Int32[] data stored as Int16[] plus Int32 offset.",
                None,
                false
                );

            /// <summary>
            /// Int32[] data stored as Int8[] plus Int32 offset.
            /// </summary>
            public static readonly Def Int8ArrayWithInt32Offset = new Def(
                new Guid("fd4db85a-5b2c-4390-aeb3-9f2162034ebb"),
                "Int8ArrayWithInt32Offset",
                "Int32[] data stored as Int8[] plus Int32 offset.",
                None,
                false
                );

            /// <summary>
            /// Int16[] data stored as Int8[] plus Int16 offset.
            /// </summary>
            public static readonly Def Int8ArrayWithInt16Offset = new Def(
                new Guid("2a9f5350-02d3-45e7-84db-5ec55d105787"),
                "Int8ArrayWithInt16Offset",
                "Int16[] data stored as Int8[] plus Int16 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt64[] data stored as UInt32[] plus UInt64 offset.
            /// </summary>
            public static readonly Def UInt32ArrayWithUInt64Offset = new Def(
                new Guid("3f3d719a-5a3b-4a97-b9f6-a821c063374f"),
                "UInt32ArrayWithUInt64Offset",
                "UInt64[] data stored as UInt32[] plus UInt64 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt64[] data stored as UInt16[] plus UInt64 offset.
            /// </summary>
            public static readonly Def UInt16ArrayWithUInt64Offset = new Def(
                new Guid("f4387c8f-92de-4af7-96fe-5b1e0e3ff935"),
                "UInt16ArrayWithUInt64Offset",
                "UInt64[] data stored as UInt16[] plus UInt64 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt64[] data stored as UInt8[] plus UInt64 offset.
            /// </summary>
            public static readonly Def UInt8ArrayWithUInt64Offset = new Def(
                new Guid("166f9886-61a9-4d81-b072-b86bab4e3ba3"),
                "UInt8ArrayWithUInt64Offset",
                "UInt64[] data stored as UInt8[] plus UInt64 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt32[] data stored as UInt16[] plus UInt32 offset.
            /// </summary>
            public static readonly Def UInt16ArrayWithUInt32Offset = new Def(
                new Guid("2477f185-8c5b-4f5c-b3d0-21e63f361304"),
                "UInt16ArrayWithUInt32Offset",
                "UInt32[] data stored as UInt16[] plus UInt32 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt32[] data stored as UInt8[] plus UInt32 offset.
            /// </summary>
            public static readonly Def UInt8ArrayWithUInt32Offset = new Def(
                new Guid("92355e69-b783-45cd-bf6b-cd5fb978ea33"),
                "UInt8ArrayWithUInt32Offset",
                "UInt32[] data stored as UInt8[] plus UInt32 offset.",
                None,
                false
                );

            /// <summary>
            /// UInt16[] data stored as UInt8[] plus UInt16 offset.
            /// </summary>
            public static readonly Def UInt8ArrayWithUInt16Offset = new Def(
                new Guid("625a813d-f2bd-4034-a69c-d967fef3da50"),
                "UInt8ArrayWithUInt16Offset",
                "UInt16[] data stored as UInt8[] plus UInt16 offset.",
                None,
                false
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
            /// A 1-dim range with limits [Min, Max] of UInt8.
            /// </summary>
            public static readonly Def Range1b = new Def(
                new Guid("db31d0e0-2c56-48da-a769-5a2c1abad38c"),
                "Range1b",
                "A 1-dim range with limits [Min, Max] of UInt8.",
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
            /// A 1-dim range with limits [Min, Max] of Int8.
            /// </summary>
            public static readonly Def Range1sb = new Def(
                new Guid("59e5322f-1677-47e4-b991-3e87e43ac005"),
                "Range1sb",
                "A 1-dim range with limits [Min, Max] of Int8.",
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
            /// A 1-dim range with limits [Min, Max] of Int16.
            /// </summary>
            public static readonly Def Range1s = new Def(
                new Guid("ed0450e7-2a14-4fe6-b3ac-f4a8ee314fad"),
                "Range1s",
                "A 1-dim range with limits [Min, Max] of Int16.",
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
            /// A 1-dim range with limits [Min, Max] of UInt16.
            /// </summary>
            public static readonly Def Range1us = new Def(
                new Guid("7809e939-1d9b-4033-9b7e-7459a2e53b73"),
                "Range1us",
                "A 1-dim range with limits [Min, Max] of UInt16.",
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
            /// A 1-dim range with limits [Min, Max] of Int32.
            /// </summary>
            public static readonly Def Range1i = new Def(
                new Guid("06fad1c2-33a1-4962-92af-19a7c84560a9"),
                "Range1i",
                "A 1-dim range with limits [Min, Max] of Int32.",
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
            /// A 1-dim range with limits [Min, Max] of UInt32.
            /// </summary>
            public static readonly Def Range1ui = new Def(
                new Guid("7ff2c8c9-9c4d-4fb2-a750-f07338ebe0b5"),
                "Range1ui",
                "A 1-dim range with limits [Min, Max] of UInt32.",
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
            /// A 1-dim range with limits [Min, Max] of Int64
            /// </summary>
            public static readonly Def Range1l = new Def(
                new Guid("03ac4568-a97b-4ca6-b005-587cd9afde75"),
                "Range1l",
                "A 1-dim range with limits [Min, Max] of Int64",
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
            /// A 1-dim range with limits [Min, Max] of UInt64
            /// </summary>
            public static readonly Def Range1ul = new Def(
                new Guid("b7e36341-3dbb-47a0-b5c7-2d199f8d909b"),
                "Range1ul",
                "A 1-dim range with limits [Min, Max] of UInt64",
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
            /// A 1-dim range with limits [Min, Max] of Float32
            /// </summary>
            public static readonly Def Range1f = new Def(
                new Guid("f5b3c83b-a294-4f40-90aa-4abd7c627e95"),
                "Range1f",
                "A 1-dim range with limits [Min, Max] of Float32",
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
            /// A 1-dim range with limits [Min, Max] of Float64
            /// </summary>
            public static readonly Def Range1d = new Def(
                new Guid("b82bff3c-d075-4d16-85d5-0be5b31a9465"),
                "Range1d",
                "A 1-dim range with limits [Min, Max] of Float64",
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
            /// A 2-dim axis-aligned box with limits [Min, Max] of V2i.
            /// </summary>
            public static readonly Def Box2i = new Def(
                new Guid("0edba5a6-1cec-401f-8d98-78bb4b3319e5"),
                "Box2i",
                "A 2-dim axis-aligned box with limits [Min, Max] of V2i.",
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
            /// A 2-dim axis-aligned box with limits [Min, Max] of V2l.
            /// </summary>
            public static readonly Def Box2l = new Def(
                new Guid("380422d0-0428-47a6-aeb3-3ab328e21bef"),
                "Box2l",
                "A 2-dim axis-aligned box with limits [Min, Max] of V2l.",
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
            /// A 2-dim axis-aligned box with limits [Min, Max] of V2f.
            /// </summary>
            public static readonly Def Box2f = new Def(
                new Guid("414d504d-f350-439b-a73a-4fcc38aafa89"),
                "Box2f",
                "A 2-dim axis-aligned box with limits [Min, Max] of V2f.",
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
            /// A 2-dim axis-aligned box with limits [Min, Max] of V2d.
            /// </summary>
            public static readonly Def Box2d = new Def(
                new Guid("2fb054de-db29-4c1c-bc97-5a0cce4bc291"),
                "Box2d",
                "A 2-dim axis-aligned box with limits [Min, Max] of V2d.",
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
            /// A 3-dim axis-aligned box with limits [Min, Max] of V3i.
            /// </summary>
            public static readonly Def Box3i = new Def(
                new Guid("c1301768-a349-489d-907e-a8967166cd7c"),
                "Box3i",
                "A 3-dim axis-aligned box with limits [Min, Max] of V3i.",
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
            /// A 3-dim axis-aligned box with limits [Min, Max] of V3l.
            /// </summary>
            public static readonly Def Box3l = new Def(
                new Guid("b22529e1-926a-4312-bb5c-3bc63700e4ac"),
                "Box3l",
                "A 3-dim axis-aligned box with limits [Min, Max] of V3l.",
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
            /// A 3-dim axis-aligned box with limits [Min, Max] of V3f.
            /// </summary>
            public static readonly Def Box3f = new Def(
                new Guid("416721ca-6df1-4ada-b7ad-1da7256f490d"),
                "Box3f",
                "A 3-dim axis-aligned box with limits [Min, Max] of V3f.",
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
            /// A 3-dim axis-aligned box with limits [Min, Max] of V3d.
            /// </summary>
            public static readonly Def Box3d = new Def(
                new Guid("5926f1ce-37fb-4022-a6e5-536b22ad79ea"),
                "Box3d",
                "A 3-dim axis-aligned box with limits [Min, Max] of V3d.",
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
            /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 28 bytes.
            /// </summary>
            public static readonly Def Cell = new Def(
                new Guid("bb9da8cb-c9d6-43dd-95d6-f569c82d9af6"),
                "Cell",
                "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 28 bytes.",
                None,
                false
                );

            /// <summary>
            /// A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 32 bytes (with 4 bytes of padding).
            /// </summary>
            public static readonly Def CellPadded32 = new Def(
                new Guid("8665c4d4-69c1-4b47-a493-ce452e075643"),
                "CellPadded32",
                "A 2^Exponent sized cube positioned at (X,Y,Z) * 2^Exponent. Serialized to 32 bytes (with 4 bytes of padding).",
                None,
                false
                );

            /// <summary>
            /// Array of CellPadded32.
            /// </summary>
            public static readonly Def CellPadded32Array = new Def(
                new Guid("9c2e3d4f-7a40-4266-a2dc-bfbde780260a"),
                "CellPadded32[]",
                "Array of CellPadded32.",
                None,
                true
                );

            /// <summary>
            /// fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way.
            /// </summary>
            public static readonly Def CellArray = new Def(
                new Guid("2732639f-20b2-46dc-8d54-007a2ef2d2ea"),
                "Cell[]",
                "fix 2020-08-28: changed type from Cell[] to CellPadded32[], because it was always serialized this way.",
                CellPadded32Array.Id,
                true
                );

            /// <summary>
            /// A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 20 bytes.
            /// </summary>
            public static readonly Def Cell2d = new Def(
                new Guid("9d580e5d-a559-4c5e-9413-7675f1dfe93c"),
                "Cell2d",
                "A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 20 bytes.",
                None,
                false
                );

            /// <summary>
            /// A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 24 bytes (with 4 bytes of padding).
            /// </summary>
            public static readonly Def Cell2dPadded24 = new Def(
                new Guid("3b022668-faa8-47a9-b622-a7a26060c620"),
                "Cell2dPadded24",
                "A 2^Exponent sized square positioned at (X,Y) * 2^Exponent. Serialized to 24 bytes (with 4 bytes of padding).",
                None,
                false
                );

            /// <summary>
            /// Array of Cell2dPadded24.
            /// </summary>
            public static readonly Def Cell2dPadded24Array = new Def(
                new Guid("269f0837-a71f-4967-a323-96ccfabbb184"),
                "Cell2dPadded24[]",
                "Array of Cell2dPadded24.",
                None,
                true
                );

            /// <summary>
            /// fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way.
            /// </summary>
            public static readonly Def Cell2dArray = new Def(
                new Guid("5c23fd56-3736-4a95-ab74-52b26a711e0e"),
                "Cell2d[]",
                "fix 2020-08-28: changed type from Cell2d[] to Cell2dPadded24[], because it was always serialized this way.",
                Cell2dPadded24Array.Id,
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
            /// A color in CIELAB color space with channels L,a,b. (Float32).
            /// </summary>
            public static readonly Def CieLabf = new Def(
                new Guid("8f96c96e-7912-4d21-83d5-2e2b0e54ff99"),
                "CieLabf",
                "A color in CIELAB color space with channels L,a,b. (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of CieLabf.
            /// </summary>
            public static readonly Def CieLabfArray = new Def(
                new Guid("527572b6-70eb-416a-a91a-1750c19b419a"),
                "CieLabf[]",
                "Array of CieLabf.",
                None,
                true
                );

            /// <summary>
            /// A color in CIELUV color space with channels L,u,v (Float32).
            /// </summary>
            public static readonly Def CIeLuvf = new Def(
                new Guid("972c001e-a7a8-45ea-a234-f4788489d6e7"),
                "CIeLuvf",
                "A color in CIELUV color space with channels L,u,v (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of CIeLuvf.
            /// </summary>
            public static readonly Def CIeLuvfArray = new Def(
                new Guid("edb50004-8a3f-4267-bb4a-30e060042b00"),
                "CIeLuvf[]",
                "Array of CIeLuvf.",
                None,
                true
                );

            /// <summary>
            /// A color in CIEXYZ color space with channels X,Y,Z (Float32).
            /// </summary>
            public static readonly Def CieXYZf = new Def(
                new Guid("055d4fae-6935-479d-9b8d-04ca1e5cf51d"),
                "CieXYZf",
                "A color in CIEXYZ color space with channels X,Y,Z (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of CieXYZf.
            /// </summary>
            public static readonly Def CieXYZfArray = new Def(
                new Guid("fb71984f-08ff-4e85-b3d8-015302f03f46"),
                "CieXYZf[]",
                "Array of CieXYZf.",
                None,
                true
                );

            /// <summary>
            /// A color in CIE Yxy color space with channels Y,x,y (Float32).
            /// </summary>
            public static readonly Def CieYxyf = new Def(
                new Guid("ce20671f-b5da-4c32-9bd0-efff870cf6fd"),
                "CieYxyf",
                "A color in CIE Yxy color space with channels Y,x,y (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of CieYxyf.
            /// </summary>
            public static readonly Def CieYxyfArray = new Def(
                new Guid("0b86f282-a051-41a5-848b-5ca82eab5e1d"),
                "CieYxyf[]",
                "Array of CieYxyf.",
                None,
                true
                );

            /// <summary>
            /// A color in CMYK color space with channels C,M,Y,K (Float32).
            /// </summary>
            public static readonly Def CMYKf = new Def(
                new Guid("5f97105d-9d07-4149-86b5-0efc23b5be5b"),
                "CMYKf",
                "A color in CMYK color space with channels C,M,Y,K (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of CMYKf.
            /// </summary>
            public static readonly Def CMYKfArray = new Def(
                new Guid("af1055e0-b898-45aa-908c-87fbe6a54609"),
                "CMYKf[]",
                "Array of CMYKf.",
                None,
                true
                );

            /// <summary>
            /// A color in HSL color space (hue, saturation, value) with channels H,S,L (Float32).
            /// </summary>
            public static readonly Def HSLf = new Def(
                new Guid("2cc70bfc-a983-4558-92ce-ab0abb3ffa0c"),
                "HSLf",
                "A color in HSL color space (hue, saturation, value) with channels H,S,L (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of HSLf.
            /// </summary>
            public static readonly Def HSLfArray = new Def(
                new Guid("c12d9316-b9ca-432a-b95b-d3638bff2f41"),
                "HSLf[]",
                "Array of HSLf.",
                None,
                true
                );

            /// <summary>
            /// A color in HSV color space (hue, saturation, lightness) with channels H,S,V (Float32).
            /// </summary>
            public static readonly Def HSVf = new Def(
                new Guid("7ab91015-ecf2-4dd2-9690-5ea33efcdbd9"),
                "HSVf",
                "A color in HSV color space (hue, saturation, lightness) with channels H,S,V (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of HSVf.
            /// </summary>
            public static readonly Def HSVfArray = new Def(
                new Guid("6a658149-2348-4fc4-b572-686555022ff0"),
                "HSVf[]",
                "Array of HSVf.",
                None,
                true
                );

            /// <summary>
            /// A color in Yuv color space with channels Y,u,v (Float32).
            /// </summary>
            public static readonly Def Yuvf = new Def(
                new Guid("41e1befe-4d13-4ef5-a654-8b9b3b740a6f"),
                "Yuvf",
                "A color in Yuv color space with channels Y,u,v (Float32).",
                None,
                false
                );

            /// <summary>
            /// Array of Yuvf.
            /// </summary>
            public static readonly Def YuvfArray = new Def(
                new Guid("62c46940-8b06-484e-b447-f4e29b74a596"),
                "Yuvf[]",
                "Array of Yuvf.",
                None,
                true
                );

            /// <summary>
            /// Data of an Aardvark.Geometry.PointRkdTreeF.
            /// </summary>
            public static readonly Def PointRkdTreeFData = new Def(
                new Guid("c5e8c7d1-3b0f-4221-8ad8-443ff1994979"),
                "PointRkdTreeFData",
                "Data of an Aardvark.Geometry.PointRkdTreeF.",
                None,
                false
                );

            /// <summary>
            /// Data of an Aardvark.Geometry.PointRkdTreeD.
            /// </summary>
            public static readonly Def PointRkdTreeDData = new Def(
                new Guid("ad480012-bcfc-4d0c-ae74-c6f629e7fa87"),
                "PointRkdTreeDData",
                "Data of an Aardvark.Geometry.PointRkdTreeD.",
                None,
                false
                );

            /// <summary>
            /// A capsule in 3-space represented by two points and a radius.
            /// </summary>
            public static readonly Def Capsule3d = new Def(
                new Guid("8b563d8b-6502-4b3f-a217-cacad6d779ea"),
                "Capsule3d",
                "A capsule in 3-space represented by two points and a radius.",
                None,
                false
                );

            /// <summary>
            /// Array of Capsule3d.
            /// </summary>
            public static readonly Def Capsule3dArray = new Def(
                new Guid("aa31738b-2cab-4843-b189-f9d31550438f"),
                "Capsule3d[]",
                "Array of Capsule3d.",
                None,
                true
                );

            /// <summary>
            /// A two dimensional circle represented by center and radius.
            /// </summary>
            public static readonly Def Circle2d = new Def(
                new Guid("32dffbbb-fd04-4e62-85ea-fc3ae8889c61"),
                "Circle2d",
                "A two dimensional circle represented by center and radius.",
                None,
                false
                );

            /// <summary>
            /// Array of Circle2d.
            /// </summary>
            public static readonly Def Circle2dArray = new Def(
                new Guid("6d7ce781-f3bb-4139-95ec-6cd885c36aee"),
                "Circle2d[]",
                "Array of Circle2d.",
                None,
                true
                );

            /// <summary>
            /// A circle in 3-space represented by its center, normal (normalized), and a radius.
            /// </summary>
            public static readonly Def Circle3d = new Def(
                new Guid("91265f74-e53b-4154-a370-e94796078a24"),
                "Circle3d",
                "A circle in 3-space represented by its center, normal (normalized), and a radius.",
                None,
                false
                );

            /// <summary>
            /// Array of Circle3d.
            /// </summary>
            public static readonly Def Circle3dArray = new Def(
                new Guid("0959298f-ca32-4b0f-9850-840fedd983ae"),
                "Circle3d[]",
                "Array of Circle3d.",
                None,
                true
                );

            /// <summary>
            /// An oblique cone in 3-space represented by its origin (apex) and base circle.
            /// </summary>
            public static readonly Def ObliqueCone3d = new Def(
                new Guid("cfb67319-ecdd-4642-b043-b0e41ce741db"),
                "ObliqueCone3d",
                "An oblique cone in 3-space represented by its origin (apex) and base circle.",
                None,
                false
                );

            /// <summary>
            /// Array of ObliqueCone3d.
            /// </summary>
            public static readonly Def ObliqueCone3dArray = new Def(
                new Guid("d0e95202-005d-4795-b71f-8b406cf04a43"),
                "ObliqueCone3d[]",
                "Array of ObliqueCone3d.",
                None,
                true
                );

            /// <summary>
            /// A cone in 3-space represented by its origin, axis of revolution (Direction), and the angle between axis and outer edge.
            /// </summary>
            public static readonly Def Cone3d = new Def(
                new Guid("b624ffb1-ef11-4776-b14a-4d34a5314230"),
                "Cone3d",
                "A cone in 3-space represented by its origin, axis of revolution (Direction), and the angle between axis and outer edge.",
                None,
                false
                );

            /// <summary>
            /// Array of Cone3d.
            /// </summary>
            public static readonly Def Cone3dArray = new Def(
                new Guid("38e3e52a-ac45-4697-83ea-86b580ff1596"),
                "Cone3d[]",
                "Array of Cone3d.",
                None,
                true
                );

            /// <summary>
            /// A cylinder in 3-space.
            /// </summary>
            public static readonly Def Cylinder3d = new Def(
                new Guid("6982d0a3-1cb4-4c9f-b479-6ebd3926bf7b"),
                "Cylinder3d",
                "A cylinder in 3-space.",
                None,
                false
                );

            /// <summary>
            /// Array of Cylinder3d.
            /// </summary>
            public static readonly Def Cylinder3dArray = new Def(
                new Guid("8f3cbbad-9d20-4db2-9831-9644733a7bc5"),
                "Cylinder3d[]",
                "Array of Cylinder3d.",
                None,
                true
                );

            /// <summary>
            /// A line in 2-space with begin P0 and end P1.
            /// </summary>
            public static readonly Def Line2d = new Def(
                new Guid("c0fb8306-e4b1-43c4-9236-a7eabbdfb245"),
                "Line2d",
                "A line in 2-space with begin P0 and end P1.",
                None,
                false
                );

            /// <summary>
            /// Array of Line2d.
            /// </summary>
            public static readonly Def Line2dArray = new Def(
                new Guid("01c73cac-5fe7-4ca2-9756-2e2ad33c6338"),
                "Line2d[]",
                "Array of Line2d.",
                None,
                true
                );

            /// <summary>
            /// A line in 3-space with begin P0 and end P1.
            /// </summary>
            public static readonly Def Line3d = new Def(
                new Guid("d3c05a9b-03f9-40f0-b169-401532c5d068"),
                "Line3d",
                "A line in 3-space with begin P0 and end P1.",
                None,
                false
                );

            /// <summary>
            /// Array of Line3d.
            /// </summary>
            public static readonly Def Line3dArray = new Def(
                new Guid("1ace6b08-0376-4c73-8479-3f4edcac48f1"),
                "Line3d[]",
                "Array of Line3d.",
                None,
                true
                );

            /// <summary>
            /// A triangle in 2-space with points defined by three points P0, P1, P2.
            /// </summary>
            public static readonly Def Triangle2d = new Def(
                new Guid("37d849fc-e0c9-4d9f-8516-461123362692"),
                "Triangle2d",
                "A triangle in 2-space with points defined by three points P0, P1, P2.",
                None,
                false
                );

            /// <summary>
            /// Array of Triangle2d.
            /// </summary>
            public static readonly Def Triangle2dArray = new Def(
                new Guid("a08840e3-7c4c-4a59-8f30-45926c5e6663"),
                "Triangle2d[]",
                "Array of Triangle2d.",
                None,
                true
                );

            /// <summary>
            /// A triangle in 3-space with points defined by three points P0, P1, P2.
            /// </summary>
            public static readonly Def Triangle3d = new Def(
                new Guid("3a52c3ad-61b4-48a8-b402-97c654fe7f80"),
                "Triangle3d",
                "A triangle in 3-space with points defined by three points P0, P1, P2.",
                None,
                false
                );

            /// <summary>
            /// Array of Triangle3d.
            /// </summary>
            public static readonly Def Triangle3dArray = new Def(
                new Guid("2b17cea5-e8db-4e86-b5f5-8b401088ca58"),
                "Triangle3d[]",
                "Array of Triangle3d.",
                None,
                true
                );

            /// <summary>
            /// A quad in 2-space with points defined by four points P0, P1, P2, P3.
            /// </summary>
            public static readonly Def Quad2d = new Def(
                new Guid("40dae3a2-0791-4533-8b69-882c3bbbaa6f"),
                "Quad2d",
                "A quad in 2-space with points defined by four points P0, P1, P2, P3.",
                None,
                false
                );

            /// <summary>
            /// Array of Quad2d.
            /// </summary>
            public static readonly Def Quad2dArray = new Def(
                new Guid("0d23983f-62a1-40d0-96e5-8864283877b9"),
                "Quad2d[]",
                "Array of Quad2d.",
                None,
                true
                );

            /// <summary>
            /// A quad in 3-space with points defined by four points P0, P1, P2, P3.
            /// </summary>
            public static readonly Def Quad3d = new Def(
                new Guid("e234f7f4-09af-4b00-b5cc-a2fad8447aa7"),
                "Quad3d",
                "A quad in 3-space with points defined by four points P0, P1, P2, P3.",
                None,
                false
                );

            /// <summary>
            /// Array of Quad3d.
            /// </summary>
            public static readonly Def Quad3dArray = new Def(
                new Guid("a0bd7233-a2b7-41e0-ab3e-5a142a67ce5e"),
                "Quad3d[]",
                "Array of Quad3d.",
                None,
                true
                );

            /// <summary>
            /// A three dimensional sphere represented by center and radius.
            /// </summary>
            public static readonly Def Sphere3d = new Def(
                new Guid("2da2eae2-8fa5-4fe0-8987-0aa295cbe710"),
                "Sphere3d",
                "A three dimensional sphere represented by center and radius.",
                None,
                false
                );

            /// <summary>
            /// Array of Sphere3d.
            /// </summary>
            public static readonly Def Sphere3dArray = new Def(
                new Guid("7c755f28-1c40-4824-9502-95caf8bda3b9"),
                "Sphere3d[]",
                "Array of Sphere3d.",
                None,
                true
                );

            /// <summary>
            /// A line represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance
            /// </summary>
            public static readonly Def Plane2d = new Def(
                new Guid("d0293e58-b411-4cc4-b50f-05c6008c22af"),
                "Plane2d",
                "A line represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance",
                None,
                false
                );

            /// <summary>
            /// Array of Plane2d.
            /// </summary>
            public static readonly Def Plane2dArray = new Def(
                new Guid("c2e69c65-ea56-4052-8d0d-7fcaf31f5545"),
                "Plane2d[]",
                "Array of Plane2d.",
                None,
                true
                );

            /// <summary>
            /// A plane represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance
            /// </summary>
            public static readonly Def Plane3d = new Def(
                new Guid("7f80cd19-9605-4143-8bdf-a707e0c29f3a"),
                "Plane3d",
                "A plane represented by a (possibly) normalized normal vector and the distance to the origin. Note that the plane does not enforce the normalized normal vector. Equation for points p on the plane: Normal dot p == Distance",
                None,
                false
                );

            /// <summary>
            /// Array of Plane3d.
            /// </summary>
            public static readonly Def Plane3dArray = new Def(
                new Guid("4fa764ee-af38-4277-96b4-c9464e661d4f"),
                "Plane3d[]",
                "Array of Plane3d.",
                None,
                true
                );

            /// <summary>
            /// A two-dimensional ray with an origin and a direction.
            /// </summary>
            public static readonly Def Ray2d = new Def(
                new Guid("2537391e-9afb-44d8-84c6-3ecce21e471e"),
                "Ray2d",
                "A two-dimensional ray with an origin and a direction.",
                None,
                false
                );

            /// <summary>
            /// Array of Ray2d.
            /// </summary>
            public static readonly Def Ray2dArray = new Def(
                new Guid("337352c0-318a-4b23-ad44-0f77e900f488"),
                "Ray2d[]",
                "Array of Ray2d.",
                None,
                true
                );

            /// <summary>
            /// A three-dimensional ray with an origin and a direction.
            /// </summary>
            public static readonly Def Ray3d = new Def(
                new Guid("48e07c48-46af-43b6-a611-78da1077e675"),
                "Ray3d",
                "A three-dimensional ray with an origin and a direction.",
                None,
                false
                );

            /// <summary>
            /// Array of Ray3d.
            /// </summary>
            public static readonly Def Ray3dArray = new Def(
                new Guid("3c4cecdf-e153-4a41-9d4a-b257c70562bc"),
                "Ray3d[]",
                "Array of Ray3d.",
                None,
                true
                );

            /// <summary>
            /// A three-dimensional torus.
            /// </summary>
            public static readonly Def Torus3d = new Def(
                new Guid("86b425b6-62a3-4c1a-974c-b52f716f2994"),
                "Torus3d",
                "A three-dimensional torus.",
                None,
                false
                );

            /// <summary>
            /// Array of Torus3d.
            /// </summary>
            public static readonly Def Torus3dArray = new Def(
                new Guid("6778dd0c-4c92-4ae9-9925-a2ca2d8f554d"),
                "Torus3d[]",
                "Array of Torus3d.",
                None,
                true
                );

            /// <summary>
            /// A two-dimensional polygon.
            /// </summary>
            public static readonly Def Polygon2d = new Def(
                new Guid("ed49d9d5-398c-415f-9dd9-85bb9902cf97"),
                "Polygon2d",
                "A two-dimensional polygon.",
                None,
                false
                );

            /// <summary>
            /// Array of Polygon2d.
            /// </summary>
            public static readonly Def Polygon2dArray = new Def(
                new Guid("2cd7815f-d8b9-488a-b506-adf02ce3b6da"),
                "Polygon2d[]",
                "Array of Polygon2d.",
                None,
                true
                );

            /// <summary>
            /// A three-dimensional polygon.
            /// </summary>
            public static readonly Def Polygon3d = new Def(
                new Guid("4da0c246-8d1d-44a0-ad7a-49522eee8d6e"),
                "Polygon3d",
                "A three-dimensional polygon.",
                None,
                false
                );

            /// <summary>
            /// Array of Polygon3d.
            /// </summary>
            public static readonly Def Polygon3dArray = new Def(
                new Guid("7d1c8157-c0a7-49f7-9ed7-e99e0160daad"),
                "Polygon3d[]",
                "Array of Polygon3d.",
                None,
                true
                );

        }
        /// <summary></summary>
        public static class Generic
        {
            /// <summary>
            /// Generic positions. V3f[].
            /// </summary>
            public static readonly Def Positions3f = new Def(
                new Guid("40db0cd8-f4fc-4139-a7f0-ba5144b27e11"),
                "Generic.Positions3f",
                "Generic positions. V3f[].",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Generic positions. V3d[].
            /// </summary>
            public static readonly Def Positions3d = new Def(
                new Guid("7218415c-dd2e-42e9-bc2f-566353978559"),
                "Generic.Positions3d",
                "Generic positions. V3d[].",
                Aardvark.V3dArray.Id,
                false
                );

            /// <summary>
            /// Generic normals. V3f[].
            /// </summary>
            public static readonly Def Normals3f = new Def(
                new Guid("3e8f21d0-b653-4665-811b-4a6fa9f343cb"),
                "Generic.Normals3f",
                "Generic normals. V3f[].",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].
            /// </summary>
            public static readonly Def Normals3sb = new Def(
                new Guid("d1707d33-18af-45ed-9bce-870b0fe30310"),
                "Generic.Normals3sb",
                "Generic normals (X:int8, Y:int8, Z:int8), where [-128,+127] is mapped to [-1.0,+1.0].",
                Primitives.Int8Array.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16 = new Def(
                new Guid("e801cbc2-c1b7-49cd-9bdf-5f212562575c"),
                "Generic.Normals.Oct16",
                "Octree. Per-point normals encoded as 16bits per normal according to format oct16 decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.Int16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.
            /// </summary>
            public static readonly Def NormalsOct16P = new Def(
                new Guid("e855b0f0-63c7-49ac-810e-aa48dd65349a"),
                "Generic.Normals.Oct16P",
                "Octree. Per-point normals encoded as 16bits per normal according to format oct16P decribed in http://jcgt.org/published/0003/02/01/paper.pdf.",
                Primitives.Int16Array.Id,
                false
                );

            /// <summary>
            /// Generic colors. C3b[].
            /// </summary>
            public static readonly Def Colors3b = new Def(
                new Guid("9f7dacb5-3d0e-4623-8ae2-aad072f12cab"),
                "Generic.Colors3b",
                "Generic colors. C3b[].",
                Aardvark.C3bArray.Id,
                false
                );

            /// <summary>
            /// Generic colors. C4b[].
            /// </summary>
            public static readonly Def Colors4b = new Def(
                new Guid("b18a2463-a821-4ae7-a259-9a7739257286"),
                "Generic.Colors4b",
                "Generic colors. C4b[].",
                Aardvark.C3bArray.Id,
                false
                );

            /// <summary>
            /// Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].
            /// </summary>
            public static readonly Def ColorsRGB565 = new Def(
                new Guid("125f054f-003f-459b-8415-e6150992bb5f"),
                "Generic.Colors.RGB565",
                "Generic colors in RGB565 format, where bits 0 to 4 are the blue value, 5 to 10 are green, and 11-15 are red. UInt16[].",
                Primitives.UInt16Array.Id,
                false
                );

            /// <summary>
            /// Generic intensities. Int32[].
            /// </summary>
            public static readonly Def Intensities1i = new Def(
                new Guid("eec1c41a-8f76-4d09-9ddc-bb7755a2f4b8"),
                "Generic.Intensities1i",
                "Generic intensities. Int32[].",
                Primitives.Int32Array.Id,
                false
                );

            /// <summary>
            /// Generic intensities. Float32[].
            /// </summary>
            public static readonly Def Intensities1f = new Def(
                new Guid("e337e43b-ea72-4c96-8712-3684cb5d4b73"),
                "Generic.Intensities1f",
                "Generic intensities. Float32[].",
                Primitives.Float32Array.Id,
                false
                );

            /// <summary>
            /// Generic classifications. UInt8[].
            /// </summary>
            public static readonly Def Classifications1b = new Def(
                new Guid("61a27f4d-b6c8-4ce1-8390-07fe3caee09b"),
                "Generic.Classifications1b",
                "Generic classifications. UInt8[].",
                Primitives.UInt8Array.Id,
                false
                );

            /// <summary>
            /// Generic classifications. UInt16[].
            /// </summary>
            public static readonly Def Classifications1s = new Def(
                new Guid("e7d38eab-5b7f-4469-98a2-a940d5ee8852"),
                "Generic.Classifications1s",
                "Generic classifications. UInt16[].",
                Primitives.UInt16Array.Id,
                false
                );

            /// <summary>
            /// Generic classifications. Int32[].
            /// </summary>
            public static readonly Def Classifications1i = new Def(
                new Guid("fe0b56b8-bf84-4e6e-ab9e-33cd63ae187d"),
                "Generic.Classifications1i",
                "Generic classifications. Int32[].",
                Primitives.Int32Array.Id,
                false
                );

            /// <summary>
            /// Generic velocities (V3f[]).
            /// </summary>
            public static readonly Def Velocities3f = new Def(
                new Guid("8916a1b8-59a3-4bc6-ab17-d91abd6a4ee3"),
                "Generic.Velocities3f",
                "Generic velocities (V3f[]).",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Generic velocities (V3d[]).
            /// </summary>
            public static readonly Def Velocities3d = new Def(
                new Guid("14528fc1-a5b6-4cbb-9cab-489f962cff6c"),
                "Generic.Velocities3d",
                "Generic velocities (V3d[]).",
                Aardvark.V3dArray.Id,
                false
                );

            /// <summary>
            /// Generic densities (float32[]).
            /// </summary>
            public static readonly Def Densities1f = new Def(
                new Guid("e912f821-fba2-4177-9419-007930582a4e"),
                "Generic.Densities1f",
                "Generic densities (float32[]).",
                Primitives.Float32Array.Id,
                false
                );

            /// <summary>
            /// Generic densities (float64[]).
            /// </summary>
            public static readonly Def Densities1d = new Def(
                new Guid("5f8fa111-eb42-4a45-8f7a-c1cd45e200be"),
                "Generic.Densities1d",
                "Generic densities (float64[]).",
                Primitives.Float64Array.Id,
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
            /// Octree. Node format specifier.
            /// </summary>
            public static readonly Def NodeFormat = new Def(
                new Guid("7c1151c8-9d5f-406d-9c03-9778f657806b"),
                "Octree.NodeFormat",
                "Octree. Node format specifier.",
                Primitives.GuidDef.Id,
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
            /// Octree. Exact bounding box of this tree's positions. Global space. Box3d.
            /// </summary>
            public static readonly Def BoundingBoxExactGlobal = new Def(
                new Guid("7912c862-74b4-4f44-a8cd-d11ea1da9304"),
                "Octree.BoundingBoxExactGlobal",
                "Octree. Exact bounding box of this tree's positions. Global space. Box3d.",
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
            /// Octree. Number of nodes in this tree (including inner nodes). Int64.
            /// </summary>
            public static readonly Def NodeCountTotal = new Def(
                new Guid("5f904be4-09fb-4b16-ad9d-460c3ae63248"),
                "Octree.NodeCountTotal",
                "Octree. Number of nodes in this tree (including inner nodes). Int64.",
                Primitives.Int64.Id,
                false
                );

            /// <summary>
            /// Octree. Number of leaf nodes in this tree. Int64.
            /// </summary>
            public static readonly Def NodeCountLeafs = new Def(
                new Guid("8d50e820-69b0-4923-969d-e10aedecdfc2"),
                "Octree.NodeCountLeafs",
                "Octree. Number of leaf nodes in this tree. Int64.",
                Primitives.Int64.Id,
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
            /// Octree. Total number of points in this tree's leaf nodes. Float64. Backwards compatibility.
            /// </summary>
            public static readonly Def PointCountTreeLeafsFloat64 = new Def(
                new Guid("6bef7603-47fa-405f-a330-a1ac1b09c475"),
                "Octree.PointCountTreeLeafs.Float64",
                "Octree. Total number of points in this tree's leaf nodes. Float64. Backwards compatibility.",
                Primitives.Float64.Id,
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
            /// Octree. Per-point positions in global space. V3f[].
            /// </summary>
            public static readonly Def PositionsGlobal3f = new Def(
                new Guid("fcb577f8-28cc-43b2-9aef-ca0569c22a03"),
                "Octree.PositionsGlobal3f",
                "Octree. Per-point positions in global space. V3f[].",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Octree. Centroid of positions (global space). V3f.
            /// </summary>
            public static readonly Def PositionsGlobal3fCentroid = new Def(
                new Guid("5ace7e46-f67d-4dba-9c60-e63f18b26166"),
                "Octree.PositionsGlobal3f.Centroid",
                "Octree. Centroid of positions (global space). V3f.",
                Aardvark.V3f.Id,
                false
                );

            /// <summary>
            /// Octree. Average point distance to centroid (global space). Float32.
            /// </summary>
            public static readonly Def PositionsGlobal3fDistToCentroidAverage = new Def(
                new Guid("1d61fded-6d27-4cf3-a6b7-c187ed21ae10"),
                "Octree.PositionsGlobal3f.DistToCentroid.Average",
                "Octree. Average point distance to centroid (global space). Float32.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Standard deviation of average point distance to centroid (global space). Float32.
            /// </summary>
            public static readonly Def PositionsGlobal3fDistToCentroidStdDev = new Def(
                new Guid("d725dcde-0f50-4f36-8e23-580cd59d04e4"),
                "Octree.PositionsGlobal3f.DistToCentroid.StdDev",
                "Octree. Standard deviation of average point distance to centroid (global space). Float32.",
                Primitives.Float32.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point positions in global space. Guid.
            /// </summary>
            public static readonly Def PositionsGlobal3fReference = new Def(
                new Guid("03a62e0e-4a4b-4d24-b558-cf700d275edd"),
                "Octree.PositionsGlobal3f.Reference",
                "Octree. Reference to per-point positions in global space. Guid.",
                Primitives.GuidDef.Id,
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
            /// Octree. Reference to per-point positions in global space. Guid.
            /// </summary>
            public static readonly Def PositionsGlobal3dReference = new Def(
                new Guid("839e1897-5fa3-426b-b66f-af166048ec34"),
                "Octree.PositionsGlobal3d.Reference",
                "Octree. Reference to per-point positions in global space. Guid.",
                Primitives.GuidDef.Id,
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
            /// Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[].
            /// </summary>
            public static readonly Def PositionsLocal3d = new Def(
                new Guid("303bb5ba-3488-4d40-9002-c484aa4b93e1"),
                "Octree.PositionsLocal3d",
                "Octree. Per-point positions in local cell space (as offsets from cell's center). V3d[].",
                Aardvark.V3dArray.Id,
                false
                );

            /// <summary>
            /// Octree. Centroid of positions (local space). V3d.
            /// </summary>
            public static readonly Def PositionsLocal3dCentroid = new Def(
                new Guid("000f0635-6b73-49ac-b44a-bcf6d6dcbef0"),
                "Octree.PositionsLocal3d.Centroid",
                "Octree. Centroid of positions (local space). V3d.",
                Aardvark.V3d.Id,
                false
                );

            /// <summary>
            /// Octree. Average point distance to centroid (local space). Float64.
            /// </summary>
            public static readonly Def PositionsLocal3dDistToCentroidAverage = new Def(
                new Guid("6c191627-eb5b-44a1-82c3-eb5da439e493"),
                "Octree.PositionsLocal3d.DistToCentroid.Average",
                "Octree. Average point distance to centroid (local space). Float64.",
                Primitives.Float64.Id,
                false
                );

            /// <summary>
            /// Octree. Standard deviation of average point distance to centroid (local space). Float64.
            /// </summary>
            public static readonly Def PositionsLocal3dDistToCentroidStdDev = new Def(
                new Guid("5ea424e2-6a47-411e-a398-062e81194ada"),
                "Octree.PositionsLocal3d.DistToCentroid.StdDev",
                "Octree. Standard deviation of average point distance to centroid (local space). Float64.",
                Primitives.Float64.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point positions in local cell space (as offsets from cell's center). Guid.
            /// </summary>
            public static readonly Def PositionsLocal3dReference = new Def(
                new Guid("b4c7208d-98e2-4c30-a18a-c853746ee78a"),
                "Octree.PositionsLocal3d.Reference",
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
            /// Octree. Per-point intensities. Float32[].
            /// </summary>
            public static readonly Def Intensities1f = new Def(
                new Guid("ebe476d9-32e8-4d47-982e-35703c3a6b4c"),
                "Octree.Intensities1f",
                "Octree. Per-point intensities. Float32[].",
                Primitives.Float32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to Octree.Intensities1f. Guid.
            /// </summary>
            public static readonly Def Intensities1fReference = new Def(
                new Guid("85509d76-d44c-4839-8c36-52abb2c35679"),
                "Octree.Intensities1f.Reference",
                "Octree. Reference to Octree.Intensities1f. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Intensities range for Octree.Intensities[.Reference]. Range1f[].
            /// </summary>
            public static readonly Def Intensities1fRange = new Def(
                new Guid("2cb2501c-fc7e-450b-9926-fcb75677f08d"),
                "Octree.Intensities1f.Range",
                "Octree. Intensities range for Octree.Intensities[.Reference]. Range1f[].",
                Aardvark.Range1f.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[].
            /// </summary>
            public static readonly Def IntensitiesWithOffset1f = new Def(
                new Guid("6753a6e1-9633-4997-b403-661578191f8c"),
                "Octree.IntensitiesWithOffset1f",
                "Octree. Per-point intensities with offset stored as Octree.IntensitiesWithOffset1f.Offset. Float32[].",
                Primitives.Float32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to Octree.IntensitiesWithOffset1f. Guid.
            /// </summary>
            public static readonly Def IntensitiesWithOffset1fReference = new Def(
                new Guid("d2874881-99a6-4eed-aa15-0d34b03e150e"),
                "Octree.IntensitiesWithOffset1f.Reference",
                "Octree. Reference to Octree.IntensitiesWithOffset1f. Guid.",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. The offset for Octree.IntensitiesWithOffset1f[.Reference]. Float64.
            /// </summary>
            public static readonly Def IntensitiesWithOffset1fOffset = new Def(
                new Guid("5b237e1d-743e-435e-9daa-b9884d7a4419"),
                "Octree.IntensitiesWithOffset1f.Offset",
                "Octree. The offset for Octree.IntensitiesWithOffset1f[.Reference]. Float64.",
                Primitives.Float64.Id,
                false
                );

            /// <summary>
            /// Octree. Intensities range for Octree.IntensitiesWithOffset1f[.Reference]. Range1f.
            /// </summary>
            public static readonly Def IntensitiesWithOffset1fRange = new Def(
                new Guid("435d8a84-c195-456c-b87b-ded2e5930134"),
                "Octree.IntensitiesWithOffset1f.Range",
                "Octree. Intensities range for Octree.IntensitiesWithOffset1f[.Reference]. Range1f.",
                Aardvark.Range1f.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point classifications. UInt8[].
            /// </summary>
            public static readonly Def Classifications1b = new Def(
                new Guid("d25cff0e-ea80-445b-ab72-d0a5a1013818"),
                "Octree.Classifications1b",
                "Octree. Per-point classifications. UInt8[].",
                Primitives.UInt8Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point classifications (UInt8[]).
            /// </summary>
            public static readonly Def Classifications1bReference = new Def(
                new Guid("9056806d-eb49-4c09-83cd-0fec099b016e"),
                "Octree.Classifications1b.Reference",
                "Octree. Reference to per-point classifications (UInt8[]).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point classifications. UInt16[].
            /// </summary>
            public static readonly Def Classifications1s = new Def(
                new Guid("b1619ade-79be-4554-894e-3f7e46240119"),
                "Octree.Classifications1s",
                "Octree. Per-point classifications. UInt16[].",
                Primitives.UInt16Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point classifications (UInt16[]).
            /// </summary>
            public static readonly Def Classifications1sReference = new Def(
                new Guid("3142284a-d7e0-45f9-8044-44800df1daac"),
                "Octree.Classifications1s.Reference",
                "Octree. Reference to per-point classifications (UInt16[]).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point classifications. Int32[].
            /// </summary>
            public static readonly Def Classifications1i = new Def(
                new Guid("826cc58d-ed89-4d56-b389-e4b581c71706"),
                "Octree.Classifications1i",
                "Octree. Per-point classifications. Int32[].",
                Primitives.Int32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point classifications (Guid -> Int32[]).
            /// </summary>
            public static readonly Def Classifications1iReference = new Def(
                new Guid("045cc89b-73de-4170-bb55-e108853e9779"),
                "Octree.Classifications1i.Reference",
                "Octree. Reference to per-point classifications (Guid -> Int32[]).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point velocities (V3f[]).
            /// </summary>
            public static readonly Def Velocities3f = new Def(
                new Guid("c8db5f0a-1ddf-47ab-8266-f8e929cf98c5"),
                "Octree.Velocities3f",
                "Octree. Per-point velocities (V3f[]).",
                Aardvark.V3fArray.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point velocities (Guid).
            /// </summary>
            public static readonly Def Velocities3fReference = new Def(
                new Guid("390115cc-5928-4526-8c28-37e709bf31d2"),
                "Octree.Velocities3f.Reference",
                "Octree. Reference to per-point velocities (Guid).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point velocities (V3d[]).
            /// </summary>
            public static readonly Def Velocities3d = new Def(
                new Guid("3f8a922d-3458-427f-8237-a189e338bf77"),
                "Octree.Velocities3d",
                "Octree. Per-point velocities (V3d[]).",
                Aardvark.V3dArray.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point velocities (Guid).
            /// </summary>
            public static readonly Def Velocities3dReference = new Def(
                new Guid("f7d97f68-e1e1-4b1b-9133-42689b6fb65b"),
                "Octree.Velocities3d.Reference",
                "Octree. Reference to per-point velocities (Guid).",
                Primitives.GuidDef.Id,
                false
                );

            /// <summary>
            /// Octree. Per-point densities (float32[]).
            /// </summary>
            public static readonly Def Densities1f = new Def(
                new Guid("040d084d-1f1b-4058-afc7-ea300bbb551d"),
                "Octree.Densities1f",
                "Octree. Per-point densities (float32[]).",
                Primitives.Float32Array.Id,
                false
                );

            /// <summary>
            /// Octree. Reference to per-point densities (Guid).
            /// </summary>
            public static readonly Def Densities1fReference = new Def(
                new Guid("bf51bb51-7947-49ec-9f60-2d4f78a60674"),
                "Octree.Densities1f.Reference",
                "Octree. Reference to per-point densities (Guid).",
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
            /// Octree. Reference to kd-tree index array. Guid -> Int32[].
            /// </summary>
            public static readonly Def KdTreeIndexArrayReference = new Def(
                new Guid("fc2b48cb-ab79-4579-92a3-0a421c8d9112"),
                "Octree.KdTreeIndexArray.Reference",
                "Octree. Reference to kd-tree index array. Guid -> Int32[].",
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
