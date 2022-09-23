/*
    MIT License

    Copyright (c) 2019-2022 Aardworx GmbH (https://aardworx.at). All rights reserved.

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
    public static partial class DurableCodec
    {
        /// <summary>
        /// Version.
        /// </summary>
        public const string Version = "0.3.2";
    }

    /// <summary>
    /// A gzipped element.
    /// </summary>
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
            //// force Durable.Octree initializer
            var _ = Durable.Octree.NodeId;

            s_encoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.Unit.Id] = EncodeUnit,

                [Durable.Primitives.DurableMap.Id] = EncodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned8.Id] = EncodeDurableMap8WithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = EncodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZipped.Id] = EncodeGZipped,

                [Durable.Primitives.DurableNamedMap.Id] = EncodeDurableNamedMap4WithoutHeader,

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

                // Cell and Cell2d structs are padded to 8-byte boundary by the compiler,
                // so there are some custom implementations to ensure compatibility
                // to data written (in the past) in different ways
                [Durable.Aardvark.Cell.Id] = EncodeCell,
                [Durable.Aardvark.CellArray.Id] = EncodeCellArray,
                [Durable.Aardvark.CellPadded32.Id] = EncodeCellPadded32,
                [Durable.Aardvark.CellPadded32Array.Id] = EncodeCellPadded32Array,
                [Durable.Aardvark.Cell2d.Id] = EncodeCell2d,
                [Durable.Aardvark.Cell2dArray.Id] = EncodeCell2dArray,
                [Durable.Aardvark.Cell2dPadded24.Id] = EncodeCell2dPadded24,
                [Durable.Aardvark.Cell2dPadded24Array.Id] = EncodeCell2dPadded24Array,

                // C3b and C4b have memory layout BGR(A) but constructors taking r,g,b,
                // so we need a custom implementation
                [Durable.Aardvark.C3b.Id] = EncodeC3b,
                [Durable.Aardvark.C3bArray.Id] = EncodeC3bArray,
                [Durable.Aardvark.C4b.Id] = EncodeC4b,
                [Durable.Aardvark.C4bArray.Id] = EncodeC4bArray,

                // polygons do not expose their underlying points array (only as IEnumerable)
                // so we need a custom implementation
                [Durable.Aardvark.Polygon2d.Id] = EncodePolygon2d,
                [Durable.Aardvark.Polygon2dArray.Id] = EncodePolygon2dArray,
                [Durable.Aardvark.Polygon3d.Id] = EncodePolygon3d,
                [Durable.Aardvark.Polygon3dArray.Id] = EncodePolygon3dArray,

                // Cylinder3d with distanceScale field is deprecated
                // custom implementation fails on encode but succeeds on decode when
                // distanceScale == 0. Otherwise fails as well.
                [Durable.Aardvark.Cylinder3dDeprecated20220302.Id] = EncodeCylinder3dDeprecated20220302,
                [Durable.Aardvark.Cylinder3dDeprecated20220302Array.Id] = EncodeCylinder3dDeprecated20220302Array,
            };

            s_decoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.Unit.Id] = DecodeUnit,

                [Durable.Primitives.DurableMap.Id] = DecodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned8.Id] = DecodeDurableMap8WithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = DecodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZipped.Id] = DecodeGZipped,

                [Durable.Primitives.DurableNamedMap.Id] = DecodeDurableNamedMap4WithoutHeader,

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
                [Durable.Aardvark.CellArray.Id] = DecodeCellPadded32Array,
                [Durable.Aardvark.CellPadded32.Id] = DecodeCellPadded32,
                [Durable.Aardvark.CellPadded32Array.Id] = DecodeCellPadded32Array,
                [Durable.Aardvark.Cell2d.Id] = DecodeCell2d,
                [Durable.Aardvark.Cell2dArray.Id] = DecodeCell2dPadded24Array,
                [Durable.Aardvark.Cell2dPadded24.Id] = DecodeCell2dPadded24,
                [Durable.Aardvark.Cell2dPadded24Array.Id] = DecodeCell2dPadded24Array,

                [Durable.Aardvark.C3b.Id] = DecodeC3b,
                [Durable.Aardvark.C3bArray.Id] = DecodeC3bArray,
                [Durable.Aardvark.C4b.Id] = DecodeC4b,
                [Durable.Aardvark.C4bArray.Id] = DecodeC4bArray,

                [Durable.Aardvark.Polygon2d.Id] = DecodePolygon2d,
                [Durable.Aardvark.Polygon2dArray.Id] = DecodePolygon2dArray,
                [Durable.Aardvark.Polygon3d.Id] = DecodePolygon3d,
                [Durable.Aardvark.Polygon3dArray.Id] = DecodePolygon3dArray,

                [Durable.Aardvark.Cylinder3dDeprecated20220302.Id] = DecodeCylinder3dDeprecated20220302,
                [Durable.Aardvark.Cylinder3dDeprecated20220302Array.Id] = DecodeCylinder3dDeprecated20220302Array,
            };

            Init();
        }

        #region GZip

        /// <summary>
        /// Returns decompressed buffer.
        /// </summary>
        public static byte[] Gzip(this byte[] buffer)
        {
            using var ms = new MemoryStream();
            using var gz = new GZipStream(ms, CompressionMode.Compress);
            gz.Write(buffer, 0, buffer.Length);
            gz.Flush();
            gz.Close();
            var compressedBuffer = ms.ToArray();
            return compressedBuffer;
        }

        /// <summary>
        /// Returns compressed buffer.
        /// </summary>
        public static byte[] Ungzip(this byte[] buffer, int uncompressedBufferLength)
        {
            using var stream = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress);

            int size = Math.Max(uncompressedBufferLength, 4096);
            byte[] bs = new byte[size];
            using var ms = new MemoryStream();

            int count = 0;
            do
            {
                count = stream.Read(bs, 0, size);
                if (count > 0) ms.Write(bs, 0, count);
            }
            while (count > 0);
            return ms.ToArray();
        }

        #endregion
    }
}
