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

#if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0

using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Aardvark.Data
{
    public static partial class DurableCodec
    {
#region Encode

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Write<T>(this Stream s, object o) where T : struct
        {
            var x = (T)o;
            var ros = MemoryMarshal.CreateReadOnlySpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(ros);
            s.Write(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Write<T>(this Stream s, object o, int offset, int count) where T : struct
        {
            var x = (T)o;
            var ros = MemoryMarshal.CreateReadOnlySpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(ros).Slice(offset, count);
            s.Write(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Write<T>(this Stream s, ref T x) where T : struct
        {
            var ros = MemoryMarshal.CreateReadOnlySpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(ros);
            s.Write(p);
        }

#region DurableMap

        private static void EncodeDurableMapEntry(Stream stream, Durable.Def def, object x)
        {
            var key = (def.Type != Durable.Primitives.Unit.Id) ? def.Type : def.Id;

            if (s_encoders.TryGetValue(key, out var encoder))
            {
                EncodeGuid(stream, def.Id);
                ((Action<Stream, object>)encoder)(stream, x);
            }
            else
            {
                var unknownDef = Durable.Get(key);
                throw new InvalidOperationException(
                    $"No encoder for {unknownDef}. Invariant 63e28e62-1c7b-4ed6-9945-f6d469bee271."
                    );
            }
        }

        private static readonly Action<Stream, object> EncodeDurableMapWithoutHeader =
            (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<Durable.Def, object>>)o;
                var count = xs.Count();
                s.Write(ref count);
                foreach (var x in xs) EncodeDurableMapEntry(s, x.Key, x.Value);
            };

        private static Action<Stream, object> CreateEncodeDurableMapAlignedWithoutHeader(int alignmentInBytes)
            => (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<Durable.Def, object>>)o;

                // number of entries (int + padding)
                var count = xs.Count();
                s.Write(ref count); // 4 bytes
                PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 8552bd4a-292d-426f-9a58-a04860a8ab58.");
#endif

                // entries (+ padding after each entry)
                foreach (var x in xs)
                {
                    EncodeWithoutTypeForPrimitives(s, x.Key, x.Value);
                    PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 06569c61-8b8f-422a-9648-50a994fb09c7.");
#endif
                }

                void PadToNextMultipleOf(int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0) s.Write(stackalloc byte[numberOfBytes - m]);
                }
            };

        private static readonly Action<Stream, object> EncodeDurableMap8WithoutHeader =
            CreateEncodeDurableMapAlignedWithoutHeader(8);

        private static readonly Action<Stream, object> EncodeDurableMap16WithoutHeader =
            CreateEncodeDurableMapAlignedWithoutHeader(16);

#endregion

        private static readonly Action<Stream, object> EncodeGZipped =
            (s, o) =>
            {
                var gzipped = (DurableGZipped)o;
                using var ms = new MemoryStream();
                EncodeGuid(ms, gzipped.Def.Id);
                EncodeWithoutTypeForPrimitives(ms, gzipped.Def, gzipped.Value);
                ms.Flush();

                var buffer = ms.ToArray();
                var bufferLength = buffer.Length;

                var bufferGZipped = buffer.GZipCompress();
                var bufferGZippedLength = bufferGZipped.Length;

                s.Write(ref bufferLength);
                s.Write(ref bufferGZippedLength);
                s.Write(bufferGZipped, 0, bufferGZipped.Length);
            };


        private static readonly Action<Stream, object> EncodeUnit = (s, o) => { };

        private static readonly Action<Stream, object> EncodeGuid = Write<Guid>;
        private static readonly Action<Stream, object> EncodeGuidArray = (s, o) => EncodeArray(s, (Guid[])o);

        private static readonly Action<Stream, object> EncodeInt8 = Write<sbyte>;
        private static readonly Action<Stream, object> EncodeInt8Array = (s, o) => EncodeArray(s, (sbyte[])o);
        private static readonly Action<Stream, object> EncodeUInt8 = Write<byte>;
        private static readonly Action<Stream, object> EncodeUInt8Array = (s, o) => EncodeArray(s, (byte[])o);

        private static readonly Action<Stream, object> EncodeInt16 = Write<short>;
        private static readonly Action<Stream, object> EncodeInt16Array = (s, o) => EncodeArray(s, (short[])o);
        private static readonly Action<Stream, object> EncodeUInt16 = Write<ushort>;
        private static readonly Action<Stream, object> EncodeUInt16Array = (s, o) => EncodeArray(s, (ushort[])o);

        private static readonly Action<Stream, object> EncodeInt32 = Write<int>;
        private static readonly Action<Stream, object> EncodeInt32Array = (s, o) => EncodeArray(s, (int[])o);
        private static readonly Action<Stream, object> EncodeUInt32 = Write<uint>;
        private static readonly Action<Stream, object> EncodeUInt32Array = (s, o) => EncodeArray(s, (uint[])o);

        private static readonly Action<Stream, object> EncodeInt64 = Write<long>;
        private static readonly Action<Stream, object> EncodeInt64Array = (s, o) => EncodeArray(s, (long[])o);
        private static readonly Action<Stream, object> EncodeUInt64 = Write<ulong>;
        private static readonly Action<Stream, object> EncodeUInt64Array = (s, o) => EncodeArray(s, (ulong[])o);

        private static readonly Action<Stream, object> EncodeFloat32 = Write<float>;
        private static readonly Action<Stream, object> EncodeFloat32Array = (s, o) => EncodeArray(s, (float[])o);

        private static readonly Action<Stream, object> EncodeFloat64 = Write<double>;
        private static readonly Action<Stream, object> EncodeFloat64Array = (s, o) => EncodeArray(s, (double[])o);

        private static readonly Action<Stream, object> EncodeStringUtf8 = (s, o) => EncodeArray(s, Encoding.UTF8.GetBytes((string)o));
        private static readonly Action<Stream, object> EncodeStringUtf8Array = (s, o) =>
        {
            var xs = (string[])o;
            var length = xs.Length;
            s.Write(ref length);
            foreach (var x in xs) EncodeArray(s, Encoding.UTF8.GetBytes(x));
        };

        private static readonly Action<Stream, object> EncodeCell = (s, o) => Write<Cell>(s, o, 0, 28);
        private static readonly Action<Stream, object> EncodeCellArray = (s, o) => EncodeArray(s, (Cell[])o);

        private static readonly Action<Stream, object> EncodeCellPadded32 = (s, o) => Write<Cell>(s, o);
        private static readonly Action<Stream, object> EncodeCellPadded32Array = (s, o) => EncodeArray(s, (Cell[])o);

        private static readonly Action<Stream, object> EncodeCell2d = (s, o) => Write<Cell2d>(s, o, 0, 20);
        private static readonly Action<Stream, object> EncodeCell2dArray = (s, o) => EncodeArray(s, (Cell2d[])o);

        private static readonly Action<Stream, object> EncodeCell2dPadded24 = (s, o) => Write<Cell2d>(s, o);
        private static readonly Action<Stream, object> EncodeCell2dPadded24Array = (s, o) => EncodeArray(s, (Cell2d[])o);



        private static readonly Action<Stream, object> EncodeC3b = Write<C3b>;
        private static readonly Action<Stream, object> EncodeC3bArray = (s, o) => EncodeArray(s, (C3b[])o);

        private static readonly Action<Stream, object> EncodeC4b = Write<C4b>;
        private static readonly Action<Stream, object> EncodeC4bArray = (s, o) => EncodeArray(s, (C4b[])o);



        private static readonly Action<Stream, object> EncodePolygon2d = (s, o) => EncodeArray(s, ((Polygon2d)o).Points.ToArray());
        private static readonly Action<Stream, object> EncodePolygon2dArray = (s, o) =>
        {
            var xs = (Polygon2d[])o;
            var length = xs.Length;
            s.Write(ref length);
            foreach (var x in xs) EncodeArray(s, x.Points.ToArray());
        };

        private static readonly Action<Stream, object> EncodePolygon3d = (s, o) => EncodeArray(s, ((Polygon3d)o).Points.ToArray());
        private static readonly Action<Stream, object> EncodePolygon3dArray = (s, o) =>
        {
            var xs = (Polygon3d[])o;
            var length = xs.Length;
            s.Write(ref length);
            foreach (var x in xs) EncodeArray(s, x.Points.ToArray());
        };



        private static unsafe void EncodeArray<T>(Stream s, params T[] xs) where T : struct
        {
            var length = xs.Length;
            var gc = GCHandle.Alloc(xs, GCHandleType.Pinned);
            var size = length * Marshal.SizeOf<T>();
            var dst = new byte[size];
            try
            {
                Marshal.Copy(gc.AddrOfPinnedObject(), dst, 0, size);
                s.Write(ref length);
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
        private static void EncodeWithoutTypeForPrimitives(Stream stream, Durable.Def def, object x)
        {
            if (def.Type != Durable.Primitives.Unit.Id)
            {
                if (s_encoders.TryGetValue(def.Type, out var encoder))
                {
                    EncodeGuid(stream, def.Id);
                    ((Action<Stream, object>)encoder)(stream, x);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Type);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 009acad8-31bc-48fa-b0ad-0ccb1da4b26d."
                        );
                }
            }
            else
            {
                if (s_encoders.TryGetValue(def.Id, out var encoder))
                {
                    ((Action<Stream, object>)encoder)(stream, x);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Id);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 4ae8b2d1-2a5d-4d87-9ddc-3d780de516fc."
                        );
                }
            }
        }

#endregion

#region Decode

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T Read<T>(this Stream s) where T : struct
        {
            T x = default;
            var span = MemoryMarshal.CreateSpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(span);
            if (s.Read(p) != p.Length) throw new Exception($"Invariant f96c6373-e9c1-4af2-b253-efa048cfbb2d. {typeof(T)}.");
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object ReadBoxed<T>(this Stream s) where T : struct
        {
            T x = default;
            var span = MemoryMarshal.CreateSpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(span);
            if (s.Read(p) != p.Length) throw new Exception($"Invariant 79a51e2f-8cf0-4d37-ab3a-5fc5b30be658. {typeof(T)}.");
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object ReadBoxed<T>(this Stream s, int offset, int count) where T : struct
        {
            T x = default;
            var span = MemoryMarshal.CreateSpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(span).Slice(offset, count);
            if (s.Read(p) != count) throw new Exception($"Invariant 7fdf55c3-15a5-41c3-ad9b-c3000fff6141. {typeof(T)}.");
            return x;
        }

        private static readonly Func<Stream, object> DecodeDurableMapWithoutHeader =
            s =>
            {
                var count = s.Read<int>();
                var map = ImmutableDictionary<Durable.Def, object>.Empty;
                for (var i = 0; i < count; i++)
                {
                    var (def, o) = Decode(s);
                    map = map.Add(def, o);
                }
                return map;
            };

        private static Func<Stream, object> CreateDecodeDurableMapAlignedWithoutHeader(int alignmentInBytes)
            => s =>
            {
                var count = s.Read<int>();
                SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant f54b0828-683f-4e63-bab8-e2331d8db36d.");
#endif

                var map = ImmutableDictionary<Durable.Def, object>.Empty;
                for (var i = 0; i < count; i++)
                {
                    var (def, o) = Decode(s);
                    map = map.Add(def, o);
                    SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 3dd73301-ca55-4330-b976-60df90f383c8.");
#endif
                }
                return map;

                static void SkipToNextMultipleOf(Stream s, int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0) s.Seek(numberOfBytes - m, SeekOrigin.Current);
                }
            };

        private static readonly Func<Stream, object> DecodeDurableMap8WithoutHeader =
            CreateDecodeDurableMapAlignedWithoutHeader(8);

        private static readonly Func<Stream, object> DecodeDurableMap16WithoutHeader =
            CreateDecodeDurableMapAlignedWithoutHeader(16);

        private static readonly Func<Stream, object> DecodeGZipped =
            s =>
            {
                var uncompressedBufferSize = s.Read<int>();
                var compressedBufferSize = s.Read<int>();

                var compressedBuffer = new byte[compressedBufferSize];
                s.Read(compressedBuffer, 0, compressedBufferSize);

                var uncompressedBuffer = compressedBuffer.GZipDecompress(uncompressedBufferSize);

                using var ms = new MemoryStream(uncompressedBuffer);

                var (def, o) = Decode(ms);
                return new DurableGZipped(def, o);
            };

        private static readonly Func<Stream, object> DecodeUnit = s => null;

        private static readonly Func<Stream, object> DecodeGuid = ReadBoxed<Guid>;

        private static readonly Func<Stream, object> DecodeStringUtf8 = s => Encoding.UTF8.GetString(DecodeArray<byte>(s));
        private static readonly Func<Stream, object> DecodeStringUtf8Array = s =>
        {
            var count = s.Read<int>();
            var xs = new string[count];
            for (var i = 0; i < count; i++)
                xs[i] = Encoding.UTF8.GetString(DecodeArray<byte>(s));
            return xs;
        };

        private static readonly Func<Stream, object> DecodeInt8 = ReadBoxed<sbyte>;
        private static readonly Func<Stream, object> DecodeInt8Array = DecodeArray<sbyte>;

        private static readonly Func<Stream, object> DecodeUInt8 = ReadBoxed<byte>;
        private static readonly Func<Stream, object> DecodeUInt8Array = DecodeArray<byte>;

        private static readonly Func<Stream, object> DecodeInt16 = ReadBoxed<short>;
        private static readonly Func<Stream, object> DecodeInt16Array = DecodeArray<short>;

        private static readonly Func<Stream, object> DecodeUInt16 = ReadBoxed<ushort>;
        private static readonly Func<Stream, object> DecodeUInt16Array = DecodeArray<ushort>;

        private static readonly Func<Stream, object> DecodeInt32 = ReadBoxed<int>;
        private static readonly Func<Stream, object> DecodeInt32Array = DecodeArray<int>;

        private static readonly Func<Stream, object> DecodeUInt32 = ReadBoxed<uint>;
        private static readonly Func<Stream, object> DecodeUInt32Array = DecodeArray<uint>;

        private static readonly Func<Stream, object> DecodeInt64 = ReadBoxed<long>;
        private static readonly Func<Stream, object> DecodeInt64Array = DecodeArray<long>;

        private static readonly Func<Stream, object> DecodeUInt64 = ReadBoxed<ulong>;
        private static readonly Func<Stream, object> DecodeUInt64Array = DecodeArray<ulong>;

        private static readonly Func<Stream, object> DecodeFloat32 = ReadBoxed<float>;
        private static readonly Func<Stream, object> DecodeFloat32Array = DecodeArray<float>;

        private static readonly Func<Stream, object> DecodeFloat64 = ReadBoxed<double>;
        private static readonly Func<Stream, object> DecodeFloat64Array = DecodeArray<double>;


        private static readonly Func<Stream, object> DecodeCell = s => ReadBoxed<Cell>(s, 0, 28);
        //private static readonly Func<Stream, object> DecodeCellArray = DecodeArray<Cell>;

        private static readonly Func<Stream, object> DecodeCellPadded32 = s => ReadBoxed<Cell>(s);
        private static readonly Func<Stream, object> DecodeCellPadded32Array = DecodeArray<Cell>;

        private static readonly Func<Stream, object> DecodeCell2d = s => ReadBoxed<Cell2d>(s, 0, 20);
        //private static readonly Func<Stream, object> DecodeCell2dArray = DecodeArray<Cell2d>;

        private static readonly Func<Stream, object> DecodeCell2dPadded24 = s => ReadBoxed<Cell2d>(s);
        private static readonly Func<Stream, object> DecodeCell2dPadded24Array = DecodeArray<Cell2d>;



        private static readonly Func<Stream, object> DecodeC3b = ReadBoxed<C3b>;
        private static readonly Func<Stream, object> DecodeC3bArray = DecodeArray<C3b>;

        private static readonly Func<Stream, object> DecodeC4b = ReadBoxed<C4b>;
        private static readonly Func<Stream, object> DecodeC4bArray = DecodeArray<C4b>;



        private static readonly Func<Stream, object> DecodePolygon2d = s => new Polygon2d(DecodeArray<V2d>(s));
        private static readonly Func<Stream, object> DecodePolygon2dArray = s =>
        {
            var count = s.Read<int>();
            var xs = new Polygon2d[count];
            for (var i = 0; i < count; i++)
                xs[i] = new Polygon2d(DecodeArray<V2d>(s));
            return xs;
        };

        private static readonly Func<Stream, object> DecodePolygon3d = s => new Polygon3d(DecodeArray<V3d>(s));
        private static readonly Func<Stream, object> DecodePolygon3dArray = s =>
        {
            var count = s.Read<int>();
            var xs = new Polygon3d[count];
            for (var i = 0; i < count; i++)
                xs[i] = new Polygon3d(DecodeArray<V3d>(s));
            return xs;
        };



        private static unsafe T[] DecodeArray<T>(Stream s) where T : struct
        {
            var count = s.Read<int>();
            var xs = new T[count];
            var p = MemoryMarshal.Cast<T, byte>(xs.AsSpan());
            if (s.Read(p) != p.Length) throw new Exception("Invariant 24838184-9d56-4c1f-a626-ba006641ef94.");
            return xs;
        }

        private static readonly Func<Stream, object> DecodeGuidArray = s => DecodeArray<Guid>(s);

        private static (Durable.Def, object) Decode(Stream stream)
        {
            var key = (Guid)DecodeGuid(stream);
            if (!Durable.TryGet(key, out var def))
            {
                stream.Position -= 16;
                def = Durable.Get(Durable.Primitives.DurableMap.Id);
            }

            if (def.Type != Durable.Primitives.Unit.Id)
            {
                if (s_decoders.TryGetValue(def.Type, out var decoder))
                {
                    var o = ((Func<Stream, object>)decoder)(stream);
                    return (def, o);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Type);
                    throw new InvalidOperationException($"Unknown definition {unknownDef}. Invariant 07f5aa80-431a-4e21-9186-fcb9206248b7.");
                }
            }
            else
            {
                if (s_decoders.TryGetValue(def.Id, out var decoder))
                {
                    var o = ((Func<Stream, object>)decoder)(stream);
                    return (def, o);
                }
                else
                {
                    var unknownDef = Durable.Get(def.Id);
                    throw new InvalidOperationException($"Unknown definition {unknownDef}. Invariant e5938812-3f34-4dfd-b0f1-ea247cd88ad8.");
                }
            }
        }

#endregion

#region Serialization

        /// <summary>
        /// Serializes value x to byte array. 
        /// Can be deserialized with Deserialize.
        /// </summary>
        public static byte[] Serialize<T>(Durable.Def def, T x)
        {
            using var ms = new MemoryStream();
            if (def.Type == Durable.Primitives.Unit.Id)
            {
                // encode type of primitive value, so we can roundtrip with Deserialize
                // (since it is not encoded by the Encode function called below)
                EncodeGuid(ms, def.Id);
            }

            EncodeWithoutTypeForPrimitives(ms, def, x);
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes value x to stream. 
        /// Can be deserialized with Deserialize.
        /// </summary>
        public static void Serialize<T>(Stream stream, Durable.Def def, T x)
        {
            if (def.Type == Durable.Primitives.Unit.Id)
            {
                // encode type of primitive value, so we can roundtrip with Deserialize
                // (since it is not encoded by the Encode function called below)
                EncodeGuid(stream, def.Id);
            }

            Serialize(stream, def, x);
        }


        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static (Durable.Def def, object obj) Deserialize(Stream stream)
            => Decode(stream);

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>

        public static (Durable.Def def, object obj) Deserialize(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return Decode(stream);
        }

        /// <summary>
        /// Deserializes value from file.
        /// </summary>
        public static (Durable.Def dev, object obj) Deserialize(string filename)
        {
            using var ms = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Decode(ms);
        }


        /// <summary>
        /// Deserializes value from stream.
        /// </summary>
        public static T DeserializeAs<T>(Stream stream) => (T)Deserialize(stream).obj;

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static T DeserializeAs<T>(byte[] buffer) => (T)Deserialize(buffer).obj;

        /// <summary>
        /// Deserializes value from file.
        /// </summary>
        public static T DeserializeAs<T>(string filename) => (T)Deserialize(filename).obj;

#endregion
    }
}


#endif