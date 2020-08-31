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

#if NETSTANDARD2_0 || NET472

using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Aardvark.Data
{
    public static partial class DurableCodec
    {
#region Encode

#region DurableMap

        private static void EncodeDurableMapEntry(BinaryWriter stream, Durable.Def def, object x)
        {
            var key = (def.Type != Durable.Primitives.Unit.Id) ? def.Type : def.Id;

            if (s_encoders.TryGetValue(key, out var encoder))
            {
                EncodeGuid(stream, def.Id);
                ((Action<BinaryWriter, object>)encoder)(stream, x);
            }
            else
            {
                var unknownDef = Durable.Get(key);
                throw new InvalidOperationException(
                    $"No encoder for {unknownDef}. Invariant 723c80aa-dfe4-4da9-9922-1f3c3c39aac0."
                    );
            }
        }

        private static readonly Action<BinaryWriter, object> EncodeDurableMapWithoutHeader =
            (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<Durable.Def, object>>)o;
                var count = xs.Count();
                s.Write(count);
                foreach (var x in xs) EncodeDurableMapEntry(s, x.Key, x.Value);
            };

        private static Action<BinaryWriter, object> CreateEncodeDurableMapAlignedWithoutHeader(int alignmentInBytes)
            => (bw, o) =>
            {
                var s = bw.BaseStream;

                var xs = (IEnumerable<KeyValuePair<Durable.Def, object>>)o;

                // number of entries (int + padding)
                var count = xs.Count();
                bw.Write(count); // 4 bytes
                PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 75944da3-3efa-4a0d-935d-c020d5ca7d56.");
#endif

                // entries (+ padding after each entry)
                foreach (var x in xs)
                {
                    EncodeDurableMapEntry(bw, x.Key, x.Value);
                    PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 9212ca74-a0a4-406f-9f7b-262e2e516918.");
#endif
                }

                void PadToNextMultipleOf(int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0)
                    {
                        var size = numberOfBytes - m;
                        var buffer = new byte[size];
                        s.Write(buffer, 0, size);
                    }
                }

            };

        private static readonly Action<BinaryWriter, object> EncodeDurableMap8WithoutHeader =
            CreateEncodeDurableMapAlignedWithoutHeader(8);

        private static readonly Action<BinaryWriter, object> EncodeDurableMap16WithoutHeader =
            CreateEncodeDurableMapAlignedWithoutHeader(16);

#endregion

        private static readonly Action<BinaryWriter, object> EncodeGZipped =
            (s, o) =>
            {
                var gzipped = (DurableGZipped)o;
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                EncodeGuid(bw, gzipped.Def.Id);
                EncodeWithoutTypeForPrimitives(bw, gzipped.Def, gzipped.Value);
                bw.Flush();

                var buffer = ms.ToArray();
                var bufferDebug = string.Join(", ", buffer.Map(x => x.ToString()));

                var bufferGZipped = buffer.GZipCompress();
                s.Write(buffer.Length);
                s.Write(bufferGZipped.Length);
                s.Write(bufferGZipped, 0, bufferGZipped.Length);
            };


        private static readonly Action<BinaryWriter, object> EncodeUnit = (s, o) => { };
        private static readonly Action<BinaryWriter, object> EncodeGuid = (s, o) => s.Write(((Guid)o).ToByteArray(), 0, 16);
        private static readonly Action<BinaryWriter, object> EncodeGuidArray = (s, o) => EncodeArray(s, (Guid[])o);
        private static readonly Action<BinaryWriter, object> EncodeInt8 = (s, o) => s.Write((sbyte)o);
        private static readonly Action<BinaryWriter, object> EncodeInt8Array = (s, o) => EncodeArray(s, (sbyte[])o);
        private static readonly Action<BinaryWriter, object> EncodeUInt8 = (s, o) => s.Write((byte)o);
        private static readonly Action<BinaryWriter, object> EncodeUInt8Array = (s, o) => EncodeArray(s, (byte[])o);
        private static readonly Action<BinaryWriter, object> EncodeInt16 = (s, o) => s.Write((short)o);
        private static readonly Action<BinaryWriter, object> EncodeInt16Array = (s, o) => EncodeArray(s, (short[])o);
        private static readonly Action<BinaryWriter, object> EncodeUInt16 = (s, o) => s.Write((ushort)o);
        private static readonly Action<BinaryWriter, object> EncodeUInt16Array = (s, o) => EncodeArray(s, (ushort[])o);
        private static readonly Action<BinaryWriter, object> EncodeInt32 = (s, o) => s.Write((int)o);
        private static readonly Action<BinaryWriter, object> EncodeInt32Array = (s, o) => EncodeArray(s, (int[])o);
        private static readonly Action<BinaryWriter, object> EncodeUInt32 = (s, o) => s.Write((uint)o);
        private static readonly Action<BinaryWriter, object> EncodeUInt32Array = (s, o) => EncodeArray(s, (uint[])o);
        private static readonly Action<BinaryWriter, object> EncodeInt64 = (s, o) => s.Write((long)o);
        private static readonly Action<BinaryWriter, object> EncodeInt64Array = (s, o) => EncodeArray(s, (long[])o);
        private static readonly Action<BinaryWriter, object> EncodeUInt64 = (s, o) => s.Write((ulong)o);
        private static readonly Action<BinaryWriter, object> EncodeUInt64Array = (s, o) => EncodeArray(s, (ulong[])o);
        private static readonly Action<BinaryWriter, object> EncodeFloat32 = (s, o) => s.Write((float)o);
        private static readonly Action<BinaryWriter, object> EncodeFloat32Array = (s, o) => EncodeArray(s, (float[])o);
        private static readonly Action<BinaryWriter, object> EncodeFloat64 = (s, o) => s.Write((double)o);
        private static readonly Action<BinaryWriter, object> EncodeFloat64Array = (s, o) => EncodeArray(s, (double[])o);
        private static readonly Action<BinaryWriter, object> EncodeStringUtf8 = (s, o) => EncodeArray(s, Encoding.UTF8.GetBytes((string)o));
        private static readonly Action<BinaryWriter, object> EncodeStringUtf8Array = (s, o) =>
        {
            var xs = (string[])o;
            s.Write(xs.Length);
            foreach (var x in xs) EncodeArray(s, Encoding.UTF8.GetBytes(x));
        };

        private static readonly Action<BinaryWriter, object> EncodeCell =
            (s, o) => { var x = (Cell)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.Exponent); };
        private static readonly Action<BinaryWriter, object> EncodeCellArray =
            (s, o) => EncodeArray(s, (Cell[])o);

        private static readonly Action<BinaryWriter, object> EncodeCellPadded32 =
            (s, o) => { var x = (Cell)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.Exponent); s.Write(new byte[4]); };
        private static readonly Action<BinaryWriter, object> EncodeCellPadded32Array =
            (s, o) => EncodeArray(s, (Cell[])o);

        private static readonly Action<BinaryWriter, object> EncodeCell2d =
            (s, o) => { var x = (Cell2d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Exponent); };
        private static readonly Action<BinaryWriter, object> EncodeCell2dArray =
            (s, o) => EncodeArray(s, (Cell2d[])o);

        private static readonly Action<BinaryWriter, object> EncodeCell2dPadded24 =
            (s, o) => { var x = (Cell2d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Exponent); s.Write(new byte[4]); };
        private static readonly Action<BinaryWriter, object> EncodeCell2dPadded24Array =
            (s, o) => EncodeArray(s, (Cell2d[])o);

        private static readonly Action<BinaryWriter, object> EncodeC3b =
            (s, o) => { var x = (C3b)o; s.Write(x.B); s.Write(x.G); s.Write(x.R); };
        private static readonly Action<BinaryWriter, object> EncodeC3bArray =
            (s, o) => EncodeArray(s, (C3b[])o);


        private static readonly Action<BinaryWriter, object> EncodeC4b =
            (s, o) => { var x = (C4b)o; s.Write(x.B); s.Write(x.G); s.Write(x.R); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4bArray =
            (s, o) => EncodeArray(s, (C4b[])o);


        private static unsafe void EncodeArray<T>(BinaryWriter s, params T[] xs) where T : struct
        {
            var gc = GCHandle.Alloc(xs, GCHandleType.Pinned);
            var size = xs.Length * Marshal.SizeOf<T>();
            var dst = new byte[size];
            try
            {
                Marshal.Copy(gc.AddrOfPinnedObject(), dst, 0, size);
                s.Write(xs.Length);
                s.Write(dst);
            }
            finally
            {
                gc.Free();
            }
        }

        /// <summary>
        /// If def/x is a primitive (def.Type == Durable.Primitives.Unit.Id), then type def is not encoded before object x.
        /// </summary>
        private static void EncodeWithoutTypeForPrimitives(BinaryWriter stream, Durable.Def def, object x)
        {
            if (def.Type != Durable.Primitives.Unit.Id)
            {
                if (s_encoders.TryGetValue(def.Type, out var encoder))
                {
                    EncodeGuid(stream, def.Id);
                    ((Action<BinaryWriter, object>)encoder)(stream, x);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Type);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 4619ee6d-81e7-4212-b813-bfe2178d906f."
                        );
                }
            }
            else
            {
                if (s_encoders.TryGetValue(def.Id, out var encoder))
                {
                    ((Action<BinaryWriter, object>)encoder)(stream, x);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Id);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 0de99f99-a339-421b-ac5d-4f55b71342de."
                        );
                }
            }
        }

        /// <summary>
        /// Serializes value x to byte array. 
        /// Can be deserialized with Deserialize.
        /// </summary>
        public static byte[] Serialize<T>(Durable.Def def, T x)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            if (def.Type == Durable.Primitives.Unit.Id)
            {
                // encode type of primitive value, so we can roundtrip with Deserialize
                // (since it is not encoded by the Encode function called below)
                EncodeGuid(bw, def.Id);
            }

            EncodeWithoutTypeForPrimitives(bw, def, x);
            bw.Flush();
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes value x to stream. 
        /// Can be deserialized with Deserialize.
        /// </summary>
        public static void Serialize<T>(BinaryWriter stream, Durable.Def def, T x)
        {
            if (def.Type == Durable.Primitives.Unit.Id)
            {
                // encode type of primitive value, so we can roundtrip with Deserialize
                // (since it is not encoded by the Encode function called below)
                EncodeGuid(stream, def.Id);
            }

            Serialize(stream, def, x);
        }

#endregion

#region Decode

        private static readonly Func<BinaryReader, object> DecodeDurableMapWithoutHeader =
            s =>
            {
                var count = s.ReadInt32();
                var entries = new KeyValuePair<Durable.Def, object>[count];
                for (var i = 0; i < count; i++)
                {
                    var e = Decode(s);
                    entries[i] = new KeyValuePair<Durable.Def, object>(e.Item1, e.Item2);
                }
                return ImmutableDictionary.CreateRange(entries);
            };

        private static Func<BinaryReader, object> CreateDecodeDurableMapAlignedWithoutHeader(int alignmentInBytes)
            => br =>
            {
                var s = br.BaseStream;

                var count = br.ReadInt32();
                SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant a28bd69e-9807-4a66-971c-c3cfa46eebad.");
#endif

                var map = ImmutableDictionary<Durable.Def, object>.Empty;
                for (var i = 0; i < count; i++)
                {
                    var (def, o) = Decode(br);
                    map = map.Add(def, o);
                    SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 078e73a4-b743-46f5-acc2-79c22e9a1d89.");
#endif
                }
                return map;

                static void SkipToNextMultipleOf(Stream s, int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0) s.Seek(numberOfBytes - m, SeekOrigin.Current);
                }
            };

        private static readonly Func<BinaryReader, object> DecodeDurableMap8WithoutHeader =
            CreateDecodeDurableMapAlignedWithoutHeader(8);

        private static readonly Func<BinaryReader, object> DecodeDurableMap16WithoutHeader =
            CreateDecodeDurableMapAlignedWithoutHeader(16);

        private static readonly Func<BinaryReader, object> DecodeGZipped =
            s =>
            {
                var uncompressedBufferSize = s.ReadInt32();
                var compressedBufferSize = s.ReadInt32();
                var compressedBuffer = s.ReadBytes(compressedBufferSize);

                var uncompressedBuffer = compressedBuffer.GZipDecompress(uncompressedBufferSize);

                using var ms = new MemoryStream(uncompressedBuffer);
                using var br = new BinaryReader(ms);

                var (def, o) = Decode(br);
                return new DurableGZipped(def, o);
            };

        private static readonly Func<BinaryReader, object> DecodeUnit = s => null;

        private static readonly Func<BinaryReader, object> DecodeGuid = s =>
        {
            var buffer = s.ReadBytes(16);
            return new Guid(buffer);
        };

        private static readonly Func<BinaryReader, object> DecodeStringUtf8 = s => Encoding.UTF8.GetString(DecodeArray<byte>(s));
        private static readonly Func<BinaryReader, object> DecodeStringUtf8Array = s =>
        {
            var count = s.ReadInt32();
            var xs = new string[count];
            for (var i = 0; i < count; i++)
                xs[i] = Encoding.UTF8.GetString(DecodeArray<byte>(s));
            return xs;
        };

        private static readonly Func<BinaryReader, object> DecodeInt8 = s => s.ReadSByte();
        private static readonly Func<BinaryReader, object> DecodeInt8Array = DecodeArray<sbyte>;

        private static readonly Func<BinaryReader, object> DecodeUInt8 = s => s.ReadByte();
        private static readonly Func<BinaryReader, object> DecodeUInt8Array = DecodeArray<byte>;

        private static readonly Func<BinaryReader, object> DecodeInt16 = s => s.ReadInt16();
        private static readonly Func<BinaryReader, object> DecodeInt16Array = DecodeArray<short>;

        private static readonly Func<BinaryReader, object> DecodeUInt16 = s => s.ReadUInt16();
        private static readonly Func<BinaryReader, object> DecodeUInt16Array = DecodeArray<ushort>;

        private static readonly Func<BinaryReader, object> DecodeInt32 = s => s.ReadInt32();
        private static readonly Func<BinaryReader, object> DecodeInt32Array = DecodeArray<int>;

        private static readonly Func<BinaryReader, object> DecodeUInt32 = s => s.ReadUInt32();
        private static readonly Func<BinaryReader, object> DecodeUInt32Array = DecodeArray<uint>;

        private static readonly Func<BinaryReader, object> DecodeInt64 = s => s.ReadInt64();
        private static readonly Func<BinaryReader, object> DecodeInt64Array = DecodeArray<long>;

        private static readonly Func<BinaryReader, object> DecodeUInt64 = s => s.ReadUInt64();
        private static readonly Func<BinaryReader, object> DecodeUInt64Array = DecodeArray<ulong>;

        private static readonly Func<BinaryReader, object> DecodeFloat32 = s => s.ReadSingle();
        private static readonly Func<BinaryReader, object> DecodeFloat32Array = DecodeArray<float>;

        private static readonly Func<BinaryReader, object> DecodeFloat64 = s => s.ReadDouble();
        private static readonly Func<BinaryReader, object> DecodeFloat64Array = DecodeArray<double>;

        private static readonly Func<BinaryReader, object> DecodeCell = s => new Cell(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt32());
        //private static readonly Func<BinaryReader, object> DecodeCellArray = DecodeArray<Cell>;
        // -> no dense encoding for Cell[]
        // -> instead DecodeCellPadded32Array is used

        private static readonly Func<BinaryReader, object> DecodeCellPadded32 = s =>
        {
            var x = new Cell(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt32());
            s.BaseStream.Seek(4, SeekOrigin.Current);
            return x;
        };
        private static readonly Func<BinaryReader, object> DecodeCellPadded32Array = DecodeArray<Cell>;

        private static readonly Func<BinaryReader, object> DecodeCell2d = s => new Cell2d(s.ReadInt64(), s.ReadInt64(), s.ReadInt32());
        //private static readonly Func<BinaryReader, object> DecodeCell2dArray = DecodeArray<Cell2d>;
        // -> no dense encoding for Cell2d[]
        // -> instead DecodeCell2dPadded24Array is used

        private static readonly Func<BinaryReader, object> DecodeCell2dPadded24 = s =>
        {
            var x = new Cell2d(s.ReadInt64(), s.ReadInt64(), s.ReadInt32());
            s.BaseStream.Seek(4, SeekOrigin.Current);
            return x;
        };
        private static readonly Func<BinaryReader, object> DecodeCell2dPadded24Array = DecodeArray<Cell2d>;

        private static readonly Func<BinaryReader, object> DecodeC3b = s =>
        {
            var b = s.ReadByte();
            var g = s.ReadByte();
            var r = s.ReadByte();
            return new C3b(r, g, b);
        };
        private static readonly Func<BinaryReader, object> DecodeC3bArray = DecodeArray<C3b>;

        private static readonly Func<BinaryReader, object> DecodeC4b = s =>
        {
            var b = s.ReadByte();
            var g = s.ReadByte();
            var r = s.ReadByte();
            var a = s.ReadByte();
            return new C4b(r, g, b, a);
        };
        private static readonly Func<BinaryReader, object> DecodeC4bArray = DecodeArray<C4b>;

        private static unsafe T[] DecodeArray<T>(BinaryReader s) where T : struct
        {
            var count = s.ReadInt32();
            var size = count * Marshal.SizeOf<T>();
            var buffer = s.ReadBytes(size);
            var xs = new T[count];
            var gc = GCHandle.Alloc(xs, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(buffer, 0, gc.AddrOfPinnedObject(), size);
                return xs;
            }
            finally
            {
                gc.Free();
            }
        }

        private static readonly Func<BinaryReader, object> DecodeGuidArray = s => DecodeArray<Guid>(s);

        private static (Durable.Def, object) Decode(BinaryReader stream)
        {
            var key = (Guid)DecodeGuid(stream);
            if (!Durable.TryGet(key, out var def))
            {
                stream.BaseStream.Position -= 16;
                def = Durable.Get(Durable.Primitives.DurableMap.Id);
            }

            if (def.Type != Durable.Primitives.Unit.Id)
            {
                if (s_decoders.TryGetValue(def.Type, out var decoder))
                {
                    var o = ((Func<BinaryReader, object>)decoder)(stream);
                    return (def, o);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Type);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant bcdd79e9-06dd-42d1-9906-9974d49e8dd8."
                        );
                }
            }
            else
            {
                if (s_decoders.TryGetValue(def.Id, out var decoder))
                {
                    var o = ((Func<BinaryReader, object>)decoder)(stream);
                    return (def, o);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Id);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 9d4570d5-b9ef-404f-a247-9b571cd4f1f6."
                        );
                }
            }
        }

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static (Durable.Def, object) Deserialize(byte[] buffer)
        {
            using var ms = new MemoryStream(buffer);
            using var br = new BinaryReader(ms);
            return Decode(br);
        }

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static (Durable.Def, object) Deserialize(BinaryReader stream)
            => Decode(stream);

#endregion
    }
}

#endif