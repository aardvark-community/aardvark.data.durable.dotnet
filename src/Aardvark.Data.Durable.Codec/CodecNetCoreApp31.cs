﻿/*
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

#if NETCOREAPP3_1 || NET5_0_OR_GREATER

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
                var unknownDef = Durable.Def.Get(key);
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

#region DurableNamedMap

        private static void EncodeDurableNamedMapEntry(Stream stream, string name, Durable.Def def, object x)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new InvalidOperationException(
                $"Empty names are not allowed. Invariant f3549a31-7d59-4701-b1c5-65f0db93fedd."
                );

            var key = (def.Type != Durable.Primitives.Unit.Id) ? def.Type : def.Id;

            if (s_encoders.TryGetValue(key, out var encoder))
            {
                EncodeStringUtf8(stream, name);
                EncodeGuid(stream, def.Id);
                ((Action<Stream, object>)encoder)(stream, x);
            }
            else
            {
                var unknownDef = Durable.Def.Get(key);
                throw new InvalidOperationException(
                    $"No encoder for {unknownDef}. Invariant d30925d5-2c7f-4f30-871a-9fca4413200c."
                    );
            }
        }

        private static readonly Action<Stream, object> EncodeDurableNamedMapWithoutHeader =
            (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<string, (Durable.Def, object)>>)o;
                var count = xs.Count();
                s.Write(ref count);
                foreach (var kv in xs) EncodeDurableNamedMapEntry(s, kv.Key, kv.Value.Item1, kv.Value.Item2);
            };

        private static Action<Stream, object> CreateEncodeDurableNamedMapAlignedWithoutHeader(int alignmentInBytes)
            => (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<string, (Durable.Def, object)>>)o;

                // number of entries (int + padding)
                var count = xs.Count();
                s.Write(ref count); // 4 bytes
                PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 9e5adda5-10f5-4edf-ad4a-30e59ac19ea3.");
#endif

                // entries (+ padding after each entry)
                foreach (var kv in xs)
                {
                    EncodeStringUtf8(s, kv.Key); 
                    PadToNextMultipleOf(alignmentInBytes);
                    EncodeGuid(s, kv.Value.Item1.Id);
                    EncodeWithoutType(s, kv.Value.Item1, kv.Value.Item2);
                    PadToNextMultipleOf(alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant 8489c178-bb2c-4861-8c96-3a3f1a997f8b.");
#endif
                }

                void PadToNextMultipleOf(int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0) s.Write(stackalloc byte[numberOfBytes - m]);
                }
            };

        private static readonly Action<Stream, object> EncodeDurableNamedMap4WithoutHeader =
            CreateEncodeDurableNamedMapAlignedWithoutHeader(4);

        private static readonly Action<Stream, object> EncodeDurableNamedMap8WithoutHeader =
            CreateEncodeDurableNamedMapAlignedWithoutHeader(8);

        private static readonly Action<Stream, object> EncodeDurableNamedMap16WithoutHeader =
            CreateEncodeDurableNamedMapAlignedWithoutHeader(16);

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

                var bufferGZipped = buffer.Gzip();
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

        private static readonly Action<Stream, object> EncodeCylinder3dDeprecated20220302 =
            (s, o) => { throw new NotSupportedException("Type deprecated, use 0b0e6173-393a-4a85-855a-a5f4d5316b36 instead."); };
        private static readonly Action<Stream, object> EncodeCylinder3dDeprecated20220302Array =
            (s, o) => { throw new NotSupportedException("Type deprecated, use 95ada0ae-0243-43d2-8e99-d0aaea843ae8 instead."); };


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
                    var unknownDef = Durable.Def.Get(def.Type);
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
                    var unknownDef = Durable.Def.Get(def.Id);
                    throw new InvalidOperationException(
                        $"Unknown definition {unknownDef}. Invariant 4ae8b2d1-2a5d-4d87-9ddc-3d780de516fc."
                        );
                }
            }
        }

        private static void EncodeWithoutType(Stream stream, Durable.Def def, object x)
        {
            var key = (def.Type != Durable.Primitives.Unit.Id) ? def.Type : def.Id;
            if (s_encoders.TryGetValue(key, out var encoder))
            {
                ((Action<Stream, object>)encoder)(stream, x);
            }
            else
            {
                var unknownDef = Durable.Def.Get(def.Id);
                throw new InvalidOperationException(
                    $"Unknown definition {unknownDef}. Invariant c38b073e-4526-4984-bce6-d60a69a49286."
                    );
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

#region DurableMap

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

#endregion

#region DurableNamedMap

        private static readonly Func<Stream, object> DecodeDurableNamedMapWithoutHeader =
            s =>
            {
                var count = s.Read<int>();
                var map = ImmutableDictionary<string, (Durable.Def key, object value)>.Empty;
                for (var i = 0; i < count; i++)
                {
                    var name = (string)DecodeStringUtf8(s);
                    var (def, o) = Decode(s);
                    map = map.Add(name, (def, o));
                }
                return map;
            };

        private static Func<Stream, object> CreateDecodeDurableNamedMapAlignedWithoutHeader(int alignmentInBytes)
            => s =>
            {
                var count = s.Read<int>();
                SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant b5dc4067-7131-46fe-97da-da6551c4bf8a.");
#endif

                var map = ImmutableDictionary<string, (Durable.Def key, object value)>.Empty;
                for (var i = 0; i < count; i++)
                {
                    var name = (string)DecodeStringUtf8(s);
                    SkipToNextMultipleOf(s, alignmentInBytes);
                    var (def, o) = Decode(s);
                    map = map.Add(name, (def, o));
                    SkipToNextMultipleOf(s, alignmentInBytes);
#if DEBUG
                    if (s.Position % alignmentInBytes != 0) throw new Exception("Invariant b1c2dec4-6487-4888-abe1-08c6b9ac2bfb.");
#endif
                }
                return map;

                static void SkipToNextMultipleOf(Stream s, int numberOfBytes)
                {
                    var m = (int)(s.Position % numberOfBytes);
                    if (m > 0) s.Seek(numberOfBytes - m, SeekOrigin.Current);
                }
            };

        private static readonly Func<Stream, object> DecodeDurableNamedMap4WithoutHeader =
            CreateDecodeDurableNamedMapAlignedWithoutHeader(4);

        private static readonly Func<Stream, object> DecodeDurableNamedMap8WithoutHeader =
            CreateDecodeDurableNamedMapAlignedWithoutHeader(8);

        private static readonly Func<Stream, object> DecodeDurableNamedMap16WithoutHeader =
            CreateDecodeDurableNamedMapAlignedWithoutHeader(16);

#endregion

        private static readonly Func<Stream, object> DecodeGZipped =
            s =>
            {
                var uncompressedBufferSize = s.Read<int>();
                var compressedBufferSize = s.Read<int>();

                var compressedBuffer = new byte[compressedBufferSize];
                s.Read(compressedBuffer, 0, compressedBufferSize);

                var uncompressedBuffer = compressedBuffer.Ungzip(uncompressedBufferSize);

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

        private static readonly Func<Stream, object> DecodeCylinder3dDeprecated20220302 = s =>
        {
            var p0 = new V3d(s.Read<double>(), s.Read<double>(), s.Read<double>());
            var p1 = new V3d(s.Read<double>(), s.Read<double>(), s.Read<double>());
            var radius = s.Read<double>();
            var distanceScale = s.Read<double>();

            if (distanceScale == 0)
            {
                return new Cylinder3d(p0, p1, radius);
            }
            else
            {
                throw new NotSupportedException($"Cannot decode deprecated Cylinder3d with distanceScale != 0 (is {distanceScale})");
            }
        };

        private static readonly Func<Stream, object> DecodeCylinder3dDeprecated20220302Array = s =>
        {
            var count = s.Read<int>();
            var xs = new Cylinder3d[count];
            for (var i = 0; i < count; i++)
                xs[i] = (Cylinder3d)DecodeCylinder3dDeprecated20220302(s);
            return xs;
        };


        private static  T[] DecodeArray<T>(Stream s) where T : struct
        {
            var count = s.Read<int>();
            var xs = new T[count];
            var p = MemoryMarshal.Cast<T, byte>(xs.AsSpan());
            var totalbytes = p.Length;
            var n = 0;
            while (n < totalbytes)
            {
                var byteCount = s.Read(p);
                n += byteCount;
                p = p[byteCount..];
            }
            if (n != totalbytes) throw new Exception("Invariant 24838184-9d56-4c1f-a626-ba006641ef94.");
            return xs;
        }

        private static readonly Func<Stream, object> DecodeGuidArray = s => DecodeArray<Guid>(s);

        private static (Durable.Def, object) Decode(Stream stream)
        {
            var key = (Guid)DecodeGuid(stream);
            if (!Durable.Def.TryGet(key, out var def))
            {
                stream.Position -= 16;
                def = Durable.Def.Get(Durable.Primitives.DurableMap.Id);
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
                    var unknownDef = Durable.Def.Get(def.Type);
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
                    var unknownDef = Durable.Def.Get(def.Id);
                    throw new InvalidOperationException($"Unknown definition {unknownDef}. Invariant e5938812-3f34-4dfd-b0f1-ea247cd88ad8.");
                }
            }
        }

#endregion

#region Serialization

        /// <summary>
        /// Serialize value x to byte array. 
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
        /// Serialize value x to stream. 
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

            EncodeWithoutTypeForPrimitives(stream, def, x);
        }




        /// <summary>
        /// Deserialize value from byte array.
        /// </summary>
        public static (Durable.Def def, object obj) Deserialize(Stream stream)
            => Decode(stream);

        /// <summary>
        /// Deserialize value from byte array.
        /// </summary>

        public static (Durable.Def def, object obj) Deserialize(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return Decode(stream);
        }

        /// <summary>
        /// Deserialize value from file.
        /// </summary>
        public static (Durable.Def dev, object obj) Deserialize(string filename)
        {
            using var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Decode(fs);
        }






        /// <summary>
        /// Deserialize value from stream.
        /// </summary>
        public static T DeserializeAs<T>(Stream stream) => (T)Deserialize(stream).obj;

        /// <summary>
        /// Deserialize value from byte array.
        /// </summary>
        public static T DeserializeAs<T>(byte[] buffer) => (T)Deserialize(buffer).obj;

        /// <summary>
        /// Deserialize value from file.
        /// </summary>
        public static T DeserializeAs<T>(string filename) => (T)Deserialize(filename).obj;



#region DurableMap

        private static IEnumerable<KeyValuePair<string, (Durable.Def, object)>> ToValueTupleForm(
            IEnumerable<KeyValuePair<string, Tuple<Durable.Def, object>>> xs
            )
            => xs.Select(kv => new KeyValuePair<string, (Durable.Def, object)>(kv.Key, (kv.Value.Item1, kv.Value.Item2)));

        /// <summary>
        /// Serialize DurableMap (dense, no padding) to byte array. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        public static byte[] SerializeDurableMap(IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            using var ms = new MemoryStream();
            EncodeGuid(ms, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(ms, Durable.Primitives.DurableMap, durableMap);
            return ms.ToArray();
        }

        /// <summary>
        /// Serialize DurableMap (dense, no padding) to stream. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        public static void SerializeDurableMap(Stream stream, IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            EncodeGuid(stream, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(stream, Durable.Primitives.DurableMap, durableMap);
        }

        /// <summary>
        /// Serialize DurableMap to byte array. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        [Obsolete]
        public static byte[] SerializeDurableMapAligned8(IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            using var ms = new MemoryStream();
            EncodeGuid(ms, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(ms, Durable.Primitives.DurableMapAligned8, durableMap);
            return ms.ToArray();
        }

        /// <summary>
        /// Serialize DurableMap to stream. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        [Obsolete]
        public static void SerializeDurableMapAligned8(Stream stream, IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            EncodeGuid(stream, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(stream, Durable.Primitives.DurableMapAligned8, durableMap);
        }

        /// <summary>
        /// Serialize DurableMap to byte array. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        [Obsolete]
        public static byte[] SerializeDurableMapAligned16(IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            using var ms = new MemoryStream();
            EncodeGuid(ms, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(ms, Durable.Primitives.DurableMapAligned16, durableMap);
            return ms.ToArray();
        }

        /// <summary>
        /// Serialize DurableMap to stream. 
        /// Can be deserialized with DeserializeDurableMap.
        /// </summary>
        [Obsolete]
        public static void SerializeDurableMapAligned16(Stream stream, IEnumerable<KeyValuePair<Durable.Def, object>> durableMap)
        {
            EncodeGuid(stream, Durable.Primitives.DurableMap.Id);
            EncodeWithoutTypeForPrimitives(stream, Durable.Primitives.DurableMapAligned16, durableMap);
        }


        private static IDictionary<Durable.Def, object> DeserializeDurableMapHelper((Durable.Def def, object obj) x)
        {
            if (x.def == Durable.Primitives.DurableMap ||
#pragma warning disable CS0618 // Type or member is obsolete
                x.def == Durable.Primitives.DurableMapAligned8 ||
                x.def == Durable.Primitives.DurableMapAligned16
#pragma warning restore CS0618 // Type or member is obsolete
                )
            {
                return (IDictionary<Durable.Def, object>)x.obj;
            }

            throw new Exception($"Expected durable map, but found {x.def}. Invariant 057a908d-6b9d-40b1-a157-1399c75f1f29.");
        }

        /// <summary>
        /// Deserialize DurableMap from stream.
        /// </summary>
        public static IDictionary<Durable.Def, object> DeserializeDurableMap(Stream stream)
            => DeserializeDurableMapHelper(Deserialize(stream));

        /// <summary>
        /// Deserialize DurableMap from buffer.
        /// </summary>
        public static IDictionary<Durable.Def, object> DeserializeDurableMap(byte[] buffer)
            => DeserializeDurableMapHelper(Deserialize(buffer));

        /// <summary>
        /// Deserialize DurableMap from file.
        /// </summary>
        public static IDictionary<Durable.Def, object> DeserializeDurableMap(string filename)
            => DeserializeDurableMapHelper(Deserialize(filename));

#endregion

#region DurableNamedMap

        #pragma warning disable CS0618 // Type or member is obsolete

        /// <summary>
        /// Serialize DurableNamedMap to byte array. 
        /// Can be deserialized with DeserializeDurableNamedMap.
        /// </summary>
        public static byte[] SerializeDurableNamedMap(IEnumerable<KeyValuePair<string, (Durable.Def, object)>> durableNamedMap)
        {
            using var ms = new MemoryStream();
            EncodeGuid(ms, Durable.Primitives.DurableNamedMapDeprecated20221021.Id);
            EncodeWithoutTypeForPrimitives(ms, Durable.Primitives.DurableNamedMapDeprecated20221021, durableNamedMap);
            return ms.ToArray();
        }

        /// <summary>
        /// Serialize DurableNamedMap to byte array. 
        /// Can be deserialized with DeserializeDurableNamedMap.
        /// </summary>
        public static byte[] SerializeDurableNamedMap(IEnumerable<KeyValuePair<string, Tuple<Durable.Def, object>>> durableNamedMap)
            => SerializeDurableNamedMap(ToValueTupleForm(durableNamedMap));

        /// <summary>
        /// Serialize DurableNamedMap to stream. 
        /// Can be deserialized with DeserializeDurableNamedMap.
        /// </summary>
        public static void SerializeDurableNamedMap(Stream stream, IEnumerable<KeyValuePair<string, (Durable.Def, object)>> durableNamedMap)
        {
            EncodeGuid(stream, Durable.Primitives.DurableNamedMapDeprecated20221021.Id);
            EncodeWithoutTypeForPrimitives(stream, Durable.Primitives.DurableNamedMapDeprecated20221021, durableNamedMap);
        }

        /// <summary>
        /// Serialize DurableNamedMap to stream. 
        /// Can be deserialized with DeserializeDurableNamedMap.
        /// </summary>
        public static void SerializeDurableNamedMap(Stream stream, IEnumerable<KeyValuePair<string, Tuple<Durable.Def, object>>> durableNamedMap)
            => SerializeDurableNamedMap(stream, ToValueTupleForm(durableNamedMap));


        private static IDictionary<string, (Durable.Def def, object obj)> DeserializeDurableNamedMapHelper((Durable.Def def, object obj) x)
        {
            if (x.def == Durable.Primitives.DurableNamedMapDeprecated20221021)
            {
                return (IDictionary<string, (Durable.Def def, object obj)>)x.obj;
            }

            throw new Exception($"Expected durable named map, but found {x.def}. Invariant b4c6f202-f2a4-45fa-8a8f-440621cb5052.");
        }

        /// <summary>
        /// Deserialize DurableNamedMap from stream.
        /// </summary>
        public static IDictionary<string, (Durable.Def def, object obj)> DeserializeDurableNamedMap(Stream stream)
            => DeserializeDurableNamedMapHelper(Deserialize(stream));

        /// <summary>
        /// Deserialize DurableNamedMap from buffer.
        /// </summary>
        public static IDictionary<string, (Durable.Def def, object obj)> DeserializeDurableNamedMap(byte[] buffer)
            => DeserializeDurableNamedMapHelper(Deserialize(buffer));

        /// <summary>
        /// Deserialize DurableNamedMap from file.
        /// </summary>
        public static IDictionary<string, (Durable.Def def, object obj)> DeserializeDurableNamedMap(string filename)
            => DeserializeDurableNamedMapHelper(Deserialize(filename));
        
        #pragma warning restore CS0618 // Type or member is obsolete

#endregion

#endregion

    }
}


#endif