#if NETCOREAPP3_1

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
    /// <summary>
    /// </summary>
    public static class Codec
    {
        private static readonly Dictionary<Guid, object> s_encoders;
        private static readonly Dictionary<Guid, object> s_decoders;

        static Codec()
        {
            // force Durable.Octree initializer
            if (Durable.Octree.NodeId == null) throw new InvalidOperationException("Invariant 98c78cd6-cef2-4f0b-bb8e-907064c305c4.");

            s_encoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.GuidDef.Id] = EncodeGuid,
                [Durable.Primitives.GuidArray.Id] = EncodeGuidArray,

                [Durable.Primitives.Int8.Id] = EncodeInt8,
                [Durable.Primitives.Int8Array.Id] = EncodeInt8Array,
                [Durable.Primitives.UInt8.Id] = EncodeUInt8,
                [Durable.Primitives.UInt8Array.Id] = EncodeUInt8Array,
                [Durable.Primitives.Int16.Id] = EncodeInt16,
                [Durable.Primitives.Int16Array.Id] = EncodeInt16Array,
                [Durable.Primitives.UInt16.Id] = EncodeUInt16,
                [Durable.Primitives.UInt16Array.Id] = EncodeUInt16Array,
                [Durable.Primitives.Int32.Id] = EncodeInt32,
                [Durable.Primitives.Int32Array.Id] = EncodeInt32Array,
                [Durable.Primitives.UInt32.Id] = EncodeUInt32,
                [Durable.Primitives.UInt32Array.Id] = EncodeUInt32Array,
                [Durable.Primitives.Int64.Id] = EncodeInt64,
                [Durable.Primitives.Int64Array.Id] = EncodeInt64Array,
                [Durable.Primitives.UInt64.Id] = EncodeUInt64,
                [Durable.Primitives.UInt64Array.Id] = EncodeUInt64Array,
                [Durable.Primitives.Float32.Id] = EncodeFloat32,
                [Durable.Primitives.Float32Array.Id] = EncodeFloat32Array,
                [Durable.Primitives.Float64.Id] = EncodeFloat64,
                [Durable.Primitives.Float64Array.Id] = EncodeFloat64Array,
                [Durable.Primitives.StringUTF8.Id] = EncodeStringUtf8,
                [Durable.Primitives.DurableMap.Id] = EncodeDurableMapWithoutHeader,

                [Durable.Aardvark.Cell.Id] = EncodeCell,
                [Durable.Aardvark.CellArray.Id] = EncodeCellArray,
                [Durable.Aardvark.V2f.Id] = EncodeV2f,
                [Durable.Aardvark.V2fArray.Id] = EncodeV2fArray,
                [Durable.Aardvark.V3f.Id] = EncodeV3f,
                [Durable.Aardvark.V3fArray.Id] = EncodeV3fArray,
                [Durable.Aardvark.V4f.Id] = EncodeV4f,
                [Durable.Aardvark.V4fArray.Id] = EncodeV4fArray,
                [Durable.Aardvark.V2d.Id] = EncodeV2d,
                [Durable.Aardvark.V2dArray.Id] = EncodeV2dArray,
                [Durable.Aardvark.V3d.Id] = EncodeV3d,
                [Durable.Aardvark.V3dArray.Id] = EncodeV3dArray,
                [Durable.Aardvark.V4d.Id] = EncodeV4d,
                [Durable.Aardvark.V4dArray.Id] = EncodeV4dArray,
                [Durable.Aardvark.Box2f.Id] = EncodeBox2f,
                [Durable.Aardvark.Box2fArray.Id] = EncodeBox2fArray,
                [Durable.Aardvark.Box2d.Id] = EncodeBox2d,
                [Durable.Aardvark.Box2dArray.Id] = EncodeBox2dArray,
                [Durable.Aardvark.Box3f.Id] = EncodeBox3f,
                [Durable.Aardvark.Box3fArray.Id] = EncodeBox3fArray,
                [Durable.Aardvark.Box3d.Id] = EncodeBox3d,
                [Durable.Aardvark.Box3dArray.Id] = EncodeBox3dArray,

                [Durable.Aardvark.C3b.Id] = EncodeC3b,
                [Durable.Aardvark.C3bArray.Id] = EncodeC3bArray,
            };

            s_decoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.GuidDef.Id] = DecodeGuid,
                [Durable.Primitives.GuidArray.Id] = DecodeGuidArray,
                [Durable.Primitives.Int8.Id] = DecodeInt8,
                [Durable.Primitives.Int8Array.Id] = DecodeInt8Array,
                [Durable.Primitives.UInt8.Id] = DecodeUInt8,
                [Durable.Primitives.UInt8Array.Id] = DecodeUInt8Array,
                [Durable.Primitives.Int16.Id] = DecodeInt16,
                [Durable.Primitives.Int16Array.Id] = DecodeInt16Array,
                [Durable.Primitives.UInt16.Id] = DecodeUInt16,
                [Durable.Primitives.UInt16Array.Id] = DecodeUInt16Array,
                [Durable.Primitives.Int32.Id] = DecodeInt32,
                [Durable.Primitives.Int32Array.Id] = DecodeInt32Array,
                [Durable.Primitives.UInt32.Id] = DecodeUInt32,
                [Durable.Primitives.UInt32Array.Id] = DecodeUInt32Array,
                [Durable.Primitives.Int64.Id] = DecodeInt64,
                [Durable.Primitives.Int64Array.Id] = DecodeInt64Array,
                [Durable.Primitives.UInt64.Id] = DecodeUInt64,
                [Durable.Primitives.UInt64Array.Id] = DecodeUInt64Array,
                [Durable.Primitives.Float32.Id] = DecodeFloat32,
                [Durable.Primitives.Float32Array.Id] = DecodeFloat32Array,
                [Durable.Primitives.Float64.Id] = DecodeFloat64,
                [Durable.Primitives.Float64Array.Id] = DecodeFloat64Array,
                [Durable.Primitives.StringUTF8.Id] = DecodeStringUtf8,
                [Durable.Primitives.DurableMap.Id] = DecodeDurableMapWithoutHeader,

                [Durable.Aardvark.Cell.Id] = DecodeCell,
                [Durable.Aardvark.CellArray.Id] = DecodeCellArray,
                [Durable.Aardvark.V2f.Id] = DecodeV2f,
                [Durable.Aardvark.V2fArray.Id] = DecodeV2fArray,
                [Durable.Aardvark.V3f.Id] = DecodeV3f,
                [Durable.Aardvark.V3fArray.Id] = DecodeV3fArray,
                [Durable.Aardvark.V4f.Id] = DecodeV4f,
                [Durable.Aardvark.V4fArray.Id] = DecodeV4fArray,
                [Durable.Aardvark.V2d.Id] = DecodeV2d,
                [Durable.Aardvark.V2dArray.Id] = DecodeV2dArray,
                [Durable.Aardvark.V3d.Id] = DecodeV3d,
                [Durable.Aardvark.V3dArray.Id] = DecodeV3dArray,
                [Durable.Aardvark.V4d.Id] = DecodeV4d,
                [Durable.Aardvark.V4dArray.Id] = DecodeV4dArray,
                [Durable.Aardvark.Box2f.Id] = DecodeBox2f,
                [Durable.Aardvark.Box2fArray.Id] = DecodeBox2fArray,
                [Durable.Aardvark.Box2d.Id] = DecodeBox2d,
                [Durable.Aardvark.Box2dArray.Id] = DecodeBox2dArray,
                [Durable.Aardvark.Box3f.Id] = DecodeBox3f,
                [Durable.Aardvark.Box3fArray.Id] = DecodeBox3fArray,
                [Durable.Aardvark.Box3d.Id] = DecodeBox3d,
                [Durable.Aardvark.Box3dArray.Id] = DecodeBox3dArray,

                [Durable.Aardvark.C3b.Id] = DecodeC3b,
                [Durable.Aardvark.C3bArray.Id] = DecodeC3bArray,
            };
        }

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
        private static void Write<T>(this Stream s, ref T x) where T : struct
        {
            var ros = MemoryMarshal.CreateReadOnlySpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(ros);
            s.Write(p);
        }

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

        private static readonly Action<Stream, object> EncodeDurableMapWithoutHeader =
            (s, o) =>
            {
                var xs = (IEnumerable<KeyValuePair<Durable.Def, object>>)o;
                var count = xs.Count();
                s.Write(ref count);
                foreach (var x in xs) Encode(s, x.Key, x.Value);
            };

        private static readonly Action<Stream, object> EncodeCell = Write<Cell>;
        private static readonly Action<Stream, object> EncodeCellArray = (s, o) => EncodeArray(s, (Cell[])o);


        private static readonly Action<Stream, object> EncodeV2f = Write<V2f>;
        private static readonly Action<Stream, object> EncodeV2fArray = (s, o) => EncodeArray(s, (V2f[])o);

        private static readonly Action<Stream, object> EncodeV3f = Write<V3f>;
        private static readonly Action<Stream, object> EncodeV3fArray = (s, o) => EncodeArray(s, (V3f[])o);

        private static readonly Action<Stream, object> EncodeV4f = Write<V4f>;
        private static readonly Action<Stream, object> EncodeV4fArray = (s, o) => EncodeArray(s, (V4f[])o);


        private static readonly Action<Stream, object> EncodeV2d = Write<V2d>;
        private static readonly Action<Stream, object> EncodeV2dArray = (s, o) => EncodeArray(s, (V2d[])o);

        private static readonly Action<Stream, object> EncodeV3d = Write<V3d>;
        private static readonly Action<Stream, object> EncodeV3dArray = (s, o) => EncodeArray(s, (V3d[])o);

        private static readonly Action<Stream, object> EncodeV4d = Write<V4f>;
        private static readonly Action<Stream, object> EncodeV4dArray = (s, o) => EncodeArray(s, (V4d[])o);


        private static readonly Action<Stream, object> EncodeBox2f = Write<Box3f>;
        private static readonly Action<Stream, object> EncodeBox2fArray = (s, o) => EncodeArray(s, (Box2f[])o);

        private static readonly Action<Stream, object> EncodeBox2d = Write<Box2d>;
        private static readonly Action<Stream, object> EncodeBox2dArray = (s, o) => EncodeArray(s, (Box2d[])o);


        private static readonly Action<Stream, object> EncodeBox3f = Write<Box3f>;
        private static readonly Action<Stream, object> EncodeBox3fArray = (s, o) => EncodeArray(s, (Box3f[])o);

        private static readonly Action<Stream, object> EncodeBox3d = Write<Box3d>;
        private static readonly Action<Stream, object> EncodeBox3dArray = (s, o) => EncodeArray(s, (Box3d[])o);

        private static readonly Action<Stream, object> EncodeC3b = Write<C3b>;
        private static readonly Action<Stream, object> EncodeC3bArray = (s, o) => EncodeArray(s, (C3b[])o);

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

        private static void Encode(Stream stream, Durable.Def def, object x)
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
                    throw new InvalidOperationException($"Unknown definition {unknownDef}.");
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
                    throw new InvalidOperationException($"Unknown definition {unknownDef}.");
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
            if (def.Type == Durable.Primitives.Unit.Id)
            {
                // encode type of primitive value, so we can roundtrip with Deserialize
                // (since it is not encoded by the Encode function called below)
                EncodeGuid(ms, def.Id);
            }

            Encode(ms, def, x);
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

        #endregion

        #region Decode

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T Read<T>(this Stream s) where T : struct
        {
            T x = default;
            var span = MemoryMarshal.CreateSpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(span);
            if (s.Read(p) != p.Length) throw new Exception();
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object ReadBoxed<T>(this Stream s) where T : struct
        {
            T x = default;
            var span = MemoryMarshal.CreateSpan(ref x, 1);
            var p = MemoryMarshal.Cast<T, byte>(span);
            if (s.Read(p) != p.Length) throw new Exception();
            return x;
        }

        private static readonly Func<Stream, object> DecodeGuid = ReadBoxed<Guid>;
        private static readonly Func<Stream, object> DecodeStringUtf8 = s => Encoding.UTF8.GetString(DecodeArray<byte>(s));

        private static readonly Func<Stream, object> DecodeInt8 = ReadBoxed<sbyte>;
        private static readonly Func<Stream, object> DecodeInt8Array = s => DecodeArray<sbyte>(s);

        private static readonly Func<Stream, object> DecodeUInt8 = ReadBoxed<byte>;
        private static readonly Func<Stream, object> DecodeUInt8Array = s => DecodeArray<byte>(s);

        private static readonly Func<Stream, object> DecodeInt16 = ReadBoxed<short>;
        private static readonly Func<Stream, object> DecodeInt16Array = s => DecodeArray<short>(s);

        private static readonly Func<Stream, object> DecodeUInt16 = ReadBoxed<ushort>;
        private static readonly Func<Stream, object> DecodeUInt16Array = s => DecodeArray<ushort>(s);

        private static readonly Func<Stream, object> DecodeInt32 = ReadBoxed<int>;
        private static readonly Func<Stream, object> DecodeInt32Array = s => DecodeArray<int>(s);

        private static readonly Func<Stream, object> DecodeUInt32 = ReadBoxed<uint>;
        private static readonly Func<Stream, object> DecodeUInt32Array = s => DecodeArray<uint>(s);

        private static readonly Func<Stream, object> DecodeInt64 = ReadBoxed<long>;
        private static readonly Func<Stream, object> DecodeInt64Array = s => DecodeArray<long>(s);

        private static readonly Func<Stream, object> DecodeUInt64 = ReadBoxed<ulong>;
        private static readonly Func<Stream, object> DecodeUInt64Array = s => DecodeArray<ulong>(s);

        private static readonly Func<Stream, object> DecodeFloat32 = ReadBoxed<float>;
        private static readonly Func<Stream, object> DecodeFloat32Array = s => DecodeArray<float>(s);

        private static readonly Func<Stream, object> DecodeFloat64 = ReadBoxed<double>;
        private static readonly Func<Stream, object> DecodeFloat64Array = s => DecodeArray<double>(s);

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

        private static readonly Func<Stream, object> DecodeCell = ReadBoxed<Cell>;
        private static readonly Func<Stream, object> DecodeCellArray = s => DecodeArray<Cell>(s);

        private static readonly Func<Stream, object> DecodeV2f = ReadBoxed<V2f>;
        private static readonly Func<Stream, object> DecodeV2fArray = s => DecodeArray<V2f>(s);
        private static readonly Func<Stream, object> DecodeV3f = ReadBoxed<V3f>;
        private static readonly Func<Stream, object> DecodeV3fArray = s => DecodeArray<V3f>(s);
        private static readonly Func<Stream, object> DecodeV4f = ReadBoxed<V4f>;
        private static readonly Func<Stream, object> DecodeV4fArray = s => DecodeArray<V4f>(s);

        private static readonly Func<Stream, object> DecodeV2d = ReadBoxed<V2d>;
        private static readonly Func<Stream, object> DecodeV2dArray = s => DecodeArray<V2d>(s);
        private static readonly Func<Stream, object> DecodeV3d = ReadBoxed<V3d>;
        private static readonly Func<Stream, object> DecodeV3dArray = s => DecodeArray<V3d>(s);
        private static readonly Func<Stream, object> DecodeV4d = ReadBoxed<V4d>;
        private static readonly Func<Stream, object> DecodeV4dArray = s => DecodeArray<V4d>(s);

        private static readonly Func<Stream, object> DecodeBox2f = ReadBoxed<Box2f>;
        private static readonly Func<Stream, object> DecodeBox2fArray = s => DecodeArray<Box2f>(s);
        private static readonly Func<Stream, object> DecodeBox2d = ReadBoxed<Box2d>;
        private static readonly Func<Stream, object> DecodeBox2dArray = s => DecodeArray<Box2d>(s);

        private static readonly Func<Stream, object> DecodeBox3f = ReadBoxed<Box3f>;
        private static readonly Func<Stream, object> DecodeBox3fArray = s => DecodeArray<Box3f>(s);
        private static readonly Func<Stream, object> DecodeBox3d = ReadBoxed<Box3d>;
        private static readonly Func<Stream, object> DecodeBox3dArray = s => DecodeArray<Box3d>(s);

        private static readonly Func<Stream, object> DecodeC3b = ReadBoxed<C3b>;
        private static readonly Func<Stream, object> DecodeC3bArray = s => DecodeArray<C3b>(s);

        private static unsafe T[] DecodeArray<T>(Stream s) where T : struct
        {
            var count = s.Read<int>();
            var xs = new T[count];
            var p = MemoryMarshal.Cast<T, byte>(xs.AsSpan());
            if (s.Read(p) != p.Length) throw new Exception();
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
                    throw new InvalidOperationException($"Unknown definition {unknownDef}.");
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
                    throw new InvalidOperationException($"Unknown definition {unknownDef}.");
                }
            }
        }

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static (Durable.Def, object) Deserialize(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return Decode(stream);
        }

        /// <summary>
        /// Deserializes value from byte array.
        /// </summary>
        public static (Durable.Def, object) Deserialize(Stream stream)
            => Decode(stream);

        #endregion
    }
}


#endif