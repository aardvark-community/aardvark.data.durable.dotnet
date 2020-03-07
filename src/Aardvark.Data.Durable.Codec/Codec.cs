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
using System.IO;
using System.IO.Compression;

namespace Aardvark.Data
{
    /// <summary></summary>
    public class DurableGZipped
    {
        /// <summary></summary>
        public Durable.Def Def { get; }

        /// <summary></summary>
        public object Value { get; }

        /// <summary></summary>
        public DurableGZipped(Durable.Def def, object value)
        {
            Def = def ?? throw new ArgumentNullException(nameof(def));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    /// <summary>
    /// </summary>
    public static partial class DurableCodec
    {
        private static readonly Dictionary<Guid, object> s_encoders;
        private static readonly Dictionary<Guid, object> s_decoders;

        static DurableCodec()
        {
            // force Durable.Octree initializer
            if (Durable.Octree.NodeId == null) throw new InvalidOperationException("Invariant 98c78cd6-cef2-4f0b-bb8e-907064c305c4.");

            s_encoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.DurableMap.Id] = EncodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = EncodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZipped.Id] = EncodeGZipped,

                [Durable.Primitives.StringUTF8.Id] = EncodeStringUtf8,
                [Durable.Primitives.StringUTF8Array.Id] = EncodeStringUtf8Array,
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

                [Durable.Aardvark.Cell.Id] = EncodeCell,
                [Durable.Aardvark.CellArray.Id] = EncodeCellArray,
                [Durable.Aardvark.V2i.Id] = EncodeV2i,
                [Durable.Aardvark.V2iArray.Id] = EncodeV2iArray,
                [Durable.Aardvark.V3i.Id] = EncodeV3i,
                [Durable.Aardvark.V3iArray.Id] = EncodeV3iArray,
                [Durable.Aardvark.V4i.Id] = EncodeV4i,
                [Durable.Aardvark.V4iArray.Id] = EncodeV4iArray,
                [Durable.Aardvark.V2l.Id] = EncodeV2l,
                [Durable.Aardvark.V2lArray.Id] = EncodeV2lArray,
                [Durable.Aardvark.V3l.Id] = EncodeV3l,
                [Durable.Aardvark.V3lArray.Id] = EncodeV3lArray,
                [Durable.Aardvark.V4l.Id] = EncodeV4l,
                [Durable.Aardvark.V4lArray.Id] = EncodeV4lArray,
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

                [Durable.Aardvark.M22f.Id] = EncodeM22f,
                [Durable.Aardvark.M22fArray.Id] = EncodeM22fArray,
                [Durable.Aardvark.M33f.Id] = EncodeM33f,
                [Durable.Aardvark.M33fArray.Id] = EncodeM33fArray,
                [Durable.Aardvark.M44f.Id] = EncodeM44f,
                [Durable.Aardvark.M44fArray.Id] = EncodeM44fArray,
                [Durable.Aardvark.M22d.Id] = EncodeM22d,
                [Durable.Aardvark.M22dArray.Id] = EncodeM22dArray,
                [Durable.Aardvark.M33d.Id] = EncodeM33d,
                [Durable.Aardvark.M33dArray.Id] = EncodeM33dArray,
                [Durable.Aardvark.M44d.Id] = EncodeM44d,
                [Durable.Aardvark.M44dArray.Id] = EncodeM44dArray,


                [Durable.Aardvark.C3b.Id] = EncodeC3b,
                [Durable.Aardvark.C3bArray.Id] = EncodeC3bArray,
                [Durable.Aardvark.C4b.Id] = EncodeC4b,
                [Durable.Aardvark.C4bArray.Id] = EncodeC4bArray,
                [Durable.Aardvark.C3f.Id] = EncodeC3f,
                [Durable.Aardvark.C3fArray.Id] = EncodeC3fArray,
                [Durable.Aardvark.C4f.Id] = EncodeC4f,
                [Durable.Aardvark.C4fArray.Id] = EncodeC4fArray,
            };

            s_decoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.DurableMap.Id] = DecodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = DecodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZipped.Id] = DecodeGZipped,

                [Durable.Primitives.StringUTF8.Id] = DecodeStringUtf8,
                [Durable.Primitives.StringUTF8Array.Id] = DecodeStringUtf8Array,
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


                [Durable.Aardvark.Cell.Id] = DecodeCell,
                [Durable.Aardvark.CellArray.Id] = DecodeCellArray,
                [Durable.Aardvark.V2i.Id] = DecodeV2i,
                [Durable.Aardvark.V2iArray.Id] = DecodeV2iArray,
                [Durable.Aardvark.V3i.Id] = DecodeV3i,
                [Durable.Aardvark.V3iArray.Id] = DecodeV3iArray,
                [Durable.Aardvark.V4i.Id] = DecodeV4i,
                [Durable.Aardvark.V4iArray.Id] = DecodeV4iArray,
                [Durable.Aardvark.V2l.Id] = DecodeV2l,
                [Durable.Aardvark.V2lArray.Id] = DecodeV2lArray,
                [Durable.Aardvark.V3l.Id] = DecodeV3l,
                [Durable.Aardvark.V3lArray.Id] = DecodeV3lArray,
                [Durable.Aardvark.V4l.Id] = DecodeV4l,
                [Durable.Aardvark.V4lArray.Id] = DecodeV4lArray,
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

                [Durable.Aardvark.M22f.Id] = DecodeM22f,
                [Durable.Aardvark.M22fArray.Id] = DecodeM22fArray,
                [Durable.Aardvark.M33f.Id] = DecodeM33f,
                [Durable.Aardvark.M33fArray.Id] = DecodeM33fArray,
                [Durable.Aardvark.M44f.Id] = DecodeM44f,
                [Durable.Aardvark.M44fArray.Id] = DecodeM44fArray,
                [Durable.Aardvark.M22d.Id] = DecodeM22d,
                [Durable.Aardvark.M22dArray.Id] = DecodeM22dArray,
                [Durable.Aardvark.M33d.Id] = DecodeM33d,
                [Durable.Aardvark.M33dArray.Id] = DecodeM33dArray,
                [Durable.Aardvark.M44d.Id] = DecodeM44d,
                [Durable.Aardvark.M44dArray.Id] = DecodeM44dArray,

                [Durable.Aardvark.C3b.Id] = DecodeC3b,
                [Durable.Aardvark.C3bArray.Id] = DecodeC3bArray,
                [Durable.Aardvark.C4b.Id] = DecodeC4b,
                [Durable.Aardvark.C4bArray.Id] = DecodeC4bArray,
                [Durable.Aardvark.C3f.Id] = DecodeC3f,
                [Durable.Aardvark.C3fArray.Id] = DecodeC3fArray,
                [Durable.Aardvark.C4f.Id] = DecodeC4f,
                [Durable.Aardvark.C4fArray.Id] = DecodeC4fArray,
            };
        }

        private static byte[] GZipCompress(this byte[] buffer)
        {
            using var ms = new MemoryStream();
            using var gz = new GZipStream(ms, CompressionMode.Compress);
            gz.Write(buffer, 0, buffer.Length);
            gz.Flush();
            return ms.ToArray();
        }

        private static byte[] GZipDecompress(this byte[] buffer, int uncompressedLength)
        {
            using var ms = new MemoryStream(buffer);
            using var gz = new GZipStream(ms, CompressionMode.Decompress);
            var uncompressed = new byte[uncompressedLength];
            gz.Read(uncompressed, 0, uncompressedLength);
            return uncompressed;
        }
    }
}
