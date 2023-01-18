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
using Aardvark.Base;
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
        public const string Version = "0.3.9";
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

#pragma warning disable CS0618 // Type or member is obsolete
            s_encoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.Unit.Id] = EncodeUnit,

                [Durable.Primitives.DurableMap.Id] = EncodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned8.Id] = EncodeDurableMap8WithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = EncodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZippedDeprecated20221021.Id] = EncodeGZipped,

                [Durable.Primitives.DurableNamedMapDeprecated20221021.Id] = EncodeDurableNamedMap4WithoutHeader,

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
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
            s_decoders = new Dictionary<Guid, object>
            {
                [Durable.Primitives.Unit.Id] = DecodeUnit,

                [Durable.Primitives.DurableMap.Id] = DecodeDurableMapWithoutHeader,
                [Durable.Primitives.DurableMapAligned8.Id] = DecodeDurableMap8WithoutHeader,
                [Durable.Primitives.DurableMapAligned16.Id] = DecodeDurableMap16WithoutHeader,
                [Durable.Primitives.GZippedDeprecated20221021.Id] = DecodeGZipped,

                [Durable.Primitives.DurableNamedMapDeprecated20221021.Id] = DecodeDurableNamedMap4WithoutHeader,

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
#pragma warning restore CS0618 // Type or member is obsolete

            Init();
        }

        #region GZip

        /// <summary>
        /// Returns compressed buffer.
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
        /// Returns uncompressed buffer.
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

        #region Primitive dotnet types

        private static readonly Dictionary<Durable.Def, Type> _primitveDotnetTypes = new()
        {
            { Durable.Primitives.GuidDef, typeof(Guid) },
            { Durable.Primitives.GuidArray, typeof(Guid[]) },

            { Durable.Primitives.UInt8      , typeof(byte    ) },
            { Durable.Primitives.UInt8Array , typeof(byte[]  ) },
            { Durable.Primitives.UInt16     , typeof(ushort  ) },
            { Durable.Primitives.UInt16Array, typeof(ushort[]) },
            { Durable.Primitives.UInt32     , typeof(uint    ) },
            { Durable.Primitives.UInt32Array, typeof(uint[]  ) },
            { Durable.Primitives.UInt64     , typeof(ulong   ) },
            { Durable.Primitives.UInt64Array, typeof(ulong[] ) },
            { Durable.Primitives.Int8       , typeof(sbyte   ) },
            { Durable.Primitives.Int8Array  , typeof(sbyte[] ) },
            { Durable.Primitives.Int16      , typeof(short  ) },
            { Durable.Primitives.Int16Array , typeof(short[]) },
            { Durable.Primitives.Int32      , typeof(int     ) },
            { Durable.Primitives.Int32Array , typeof(int[]   ) },
            { Durable.Primitives.Int64      , typeof(long    ) },
            { Durable.Primitives.Int64Array , typeof(long[]  ) },

            { Durable.Primitives.Float32     , typeof(float   ) },
            { Durable.Primitives.Float32Array, typeof(float[] ) },
            { Durable.Primitives.Float64     , typeof(double  ) },
            { Durable.Primitives.Float64Array, typeof(double[]) },

            { Durable.Primitives.DecimalDotnet, typeof(decimal) },
            { Durable.Primitives.StringUTF8, typeof(string) },
            { Durable.Primitives.StringUTF8Array, typeof(string[]) },


            { Durable.Aardvark.Affine2d     , typeof(Affine2d  ) },
            { Durable.Aardvark.Affine2dArray, typeof(Affine2d[]) },
            { Durable.Aardvark.Affine2f     , typeof(Affine2f  ) },
            { Durable.Aardvark.Affine2fArray, typeof(Affine2f[]) },
            { Durable.Aardvark.Affine3d     , typeof(Affine3d  ) },
            { Durable.Aardvark.Affine3dArray, typeof(Affine3d[]) },
            { Durable.Aardvark.Affine3f     , typeof(Affine3f  ) },
            { Durable.Aardvark.Affine3fArray, typeof(Affine3f[]) },

            { Durable.Aardvark.Box2d     , typeof(Box2d  ) },
            { Durable.Aardvark.Box2dArray, typeof(Box2d[]) },
            { Durable.Aardvark.Box2f     , typeof(Box2f  ) },
            { Durable.Aardvark.Box2fArray, typeof(Box2f[]) },
            { Durable.Aardvark.Box2i     , typeof(Box2i  ) },
            { Durable.Aardvark.Box2iArray, typeof(Box2i[]) },
            { Durable.Aardvark.Box2l     , typeof(Box2l  ) },
            { Durable.Aardvark.Box2lArray, typeof(Box2l[]) },
            { Durable.Aardvark.Box3d     , typeof(Box3d  ) },
            { Durable.Aardvark.Box3dArray, typeof(Box3d[]) },
            { Durable.Aardvark.Box3f     , typeof(Box3f  ) },
            { Durable.Aardvark.Box3fArray, typeof(Box3f[]) },
            { Durable.Aardvark.Box3i     , typeof(Box3i  ) },
            { Durable.Aardvark.Box3iArray, typeof(Box3i[]) },
            { Durable.Aardvark.Box3l     , typeof(Box3l  ) },
            { Durable.Aardvark.Box3lArray, typeof(Box3l[]) },

            { Durable.Aardvark.C3b      , typeof(C3b   ) },
            { Durable.Aardvark.C3bArray , typeof(C3b[] ) },
            { Durable.Aardvark.C3d      , typeof(C3d   ) },
            { Durable.Aardvark.C3dArray , typeof(C3d[] ) },
            { Durable.Aardvark.C3f      , typeof(C3f   ) },
            { Durable.Aardvark.C3fArray , typeof(C3f[] ) },
            { Durable.Aardvark.C3ui     , typeof(C3ui  ) },
            { Durable.Aardvark.C3uiArray, typeof(C3ui[]) },
            { Durable.Aardvark.C3us     , typeof(C3us  ) },
            { Durable.Aardvark.C3usArray, typeof(C3us[]) },
            { Durable.Aardvark.C4b      , typeof(C4b   ) },
            { Durable.Aardvark.C4bArray , typeof(C4b[] ) },
            { Durable.Aardvark.C4d      , typeof(C4d   ) },
            { Durable.Aardvark.C4dArray , typeof(C4d[] ) },
            { Durable.Aardvark.C4f      , typeof(C4f   ) },
            { Durable.Aardvark.C4fArray , typeof(C4f[] ) },
            { Durable.Aardvark.C4ui     , typeof(C4ui  ) },
            { Durable.Aardvark.C4uiArray, typeof(C4ui[]) },
            { Durable.Aardvark.C4us     , typeof(C4us  ) },
            { Durable.Aardvark.C4usArray, typeof(C4us[]) },
            { Durable.Aardvark.CieLabf     , typeof(CieLabf  ) },
            { Durable.Aardvark.CieLabfArray, typeof(CieLabf[]) },
            { Durable.Aardvark.CIeLuvf     , typeof(CIeLuvf  ) },
            { Durable.Aardvark.CIeLuvfArray, typeof(CIeLuvf[]) },
            { Durable.Aardvark.CieXYZf     , typeof(CieXYZf  ) },
            { Durable.Aardvark.CieXYZfArray, typeof(CieXYZf[]) },
            { Durable.Aardvark.CieYxyf     , typeof(CieYxyf  ) },
            { Durable.Aardvark.CieYxyfArray, typeof(CieYxyf[]) },
            { Durable.Aardvark.CMYKf       , typeof(CMYKf    ) },
            { Durable.Aardvark.CMYKfArray  , typeof(CMYKf[]  ) },
            { Durable.Aardvark.HSLf        , typeof(HSLf     ) },
            { Durable.Aardvark.HSLfArray   , typeof(HSLf[]   ) },
            { Durable.Aardvark.HSVf        , typeof(HSVf     ) },
            { Durable.Aardvark.HSVfArray   , typeof(HSVf[]   ) },

            { Durable.Aardvark.Capsule3d, typeof(Capsule3d) },
            { Durable.Aardvark.Capsule3dArray, typeof(Capsule3d[]) },
            { Durable.Aardvark.Cell, typeof(Cell) },
            { Durable.Aardvark.CellArray, typeof(Cell[]) },
            { Durable.Aardvark.Cell2d, typeof(Cell2d) },
            { Durable.Aardvark.Cell2dArray, typeof(Cell2d[]) },
            { Durable.Aardvark.Circle2d, typeof(Circle2d) },
            { Durable.Aardvark.Circle2dArray, typeof(Circle2d[]) },
            { Durable.Aardvark.Circle3d, typeof(Circle3d) },
            { Durable.Aardvark.Circle3dArray, typeof(Circle3d[]) },
            { Durable.Aardvark.Cone3d, typeof(Cone3d) },
            { Durable.Aardvark.Cone3dArray, typeof(Cone3d[]) },
            { Durable.Aardvark.Cylinder3d, typeof(Cylinder3d) },
            { Durable.Aardvark.Cylinder3dArray, typeof(Cylinder3d[]) },

            { Durable.Aardvark.Ellipse2d, typeof(Ellipse2d) },
            { Durable.Aardvark.Ellipse2dArray, typeof(Ellipse2d[]) },
            { Durable.Aardvark.Ellipse3d, typeof(Ellipse3d) },
            { Durable.Aardvark.Ellipse3dArray, typeof(Ellipse3d[]) },

            { Durable.Aardvark.Euclidean2d     , typeof(Euclidean2d  ) },
            { Durable.Aardvark.Euclidean2dArray, typeof(Euclidean2d[]) },
            { Durable.Aardvark.Euclidean2f     , typeof(Euclidean2f  ) },
            { Durable.Aardvark.Euclidean2fArray, typeof(Euclidean2f[]) },
            { Durable.Aardvark.Euclidean3d     , typeof(Euclidean3d  ) },
            { Durable.Aardvark.Euclidean3dArray, typeof(Euclidean3d[]) },
            { Durable.Aardvark.Euclidean3f     , typeof(Euclidean3f  ) },
            { Durable.Aardvark.Euclidean3fArray, typeof(Euclidean3f[]) },

            { Durable.Aardvark.Line2d, typeof(Line2d) },
            { Durable.Aardvark.Line2dArray, typeof(Line2d[]) },
            { Durable.Aardvark.Line3d, typeof(Line3d) },
            { Durable.Aardvark.Line3dArray, typeof(Line3d[]) },

            { Durable.Aardvark.M22d     , typeof(M22d  ) },
            { Durable.Aardvark.M22dArray, typeof(M22d[]) },
            { Durable.Aardvark.M22f     , typeof(M22f  ) },
            { Durable.Aardvark.M22fArray, typeof(M22f[]) },
            { Durable.Aardvark.M33d     , typeof(M33d  ) },
            { Durable.Aardvark.M33dArray, typeof(M33d[]) },
            { Durable.Aardvark.M33f     , typeof(M33f  ) },
            { Durable.Aardvark.M33fArray, typeof(M33f[]) },
            { Durable.Aardvark.M44d     , typeof(M44d  ) },
            { Durable.Aardvark.M44dArray, typeof(M44d[]) },
            { Durable.Aardvark.M44f     , typeof(M44f  ) },
            { Durable.Aardvark.M44fArray, typeof(M44f[]) },

            { Durable.Aardvark.ObliqueCone3d, typeof(ObliqueCone3d) },
            { Durable.Aardvark.ObliqueCone3dArray, typeof(ObliqueCone3d[]) },

            { Durable.Aardvark.Plane2d, typeof(Plane2d) },
            { Durable.Aardvark.Plane2dArray, typeof(Plane2d[]) },
            { Durable.Aardvark.Plane3d, typeof(Plane3d) },
            { Durable.Aardvark.Plane3dArray, typeof(Plane3d[]) },

            { Durable.Aardvark.Polygon2d, typeof(Polygon2d) },
            { Durable.Aardvark.Polygon2dArray, typeof(Polygon2d[]) },
            { Durable.Aardvark.Polygon3d, typeof(Polygon3d) },
            { Durable.Aardvark.Polygon3dArray, typeof(Polygon3d[]) },

            { Durable.Aardvark.Quad2d, typeof(Quad2d) },
            { Durable.Aardvark.Quad2dArray, typeof(Quad2d[]) },
            { Durable.Aardvark.Quad3d, typeof(Quad3d) },
            { Durable.Aardvark.Quad3dArray, typeof(Quad3d[]) },


            { Durable.Aardvark.Range1b      , typeof(Range1b   ) },
            { Durable.Aardvark.Range1bArray , typeof(Range1b[] ) },
            { Durable.Aardvark.Range1d      , typeof(Range1d   ) },
            { Durable.Aardvark.Range1dArray , typeof(Range1d[] ) },
            { Durable.Aardvark.Range1f      , typeof(Range1f   ) },
            { Durable.Aardvark.Range1fArray , typeof(Range1f[] ) },
            { Durable.Aardvark.Range1i      , typeof(Range1i   ) },
            { Durable.Aardvark.Range1iArray , typeof(Range1i[] ) },
            { Durable.Aardvark.Range1l      , typeof(Range1l   ) },
            { Durable.Aardvark.Range1lArray , typeof(Range1l[] ) },
            { Durable.Aardvark.Range1s      , typeof(Range1s   ) },
            { Durable.Aardvark.Range1sArray , typeof(Range1s[] ) },
            { Durable.Aardvark.Range1sb     , typeof(Range1sb  ) },
            { Durable.Aardvark.Range1sbArray, typeof(Range1sb[]) },
            { Durable.Aardvark.Range1ui     , typeof(Range1ui  ) },
            { Durable.Aardvark.Range1uiArray, typeof(Range1ui[]) },
            { Durable.Aardvark.Range1ul     , typeof(Range1ul  ) },
            { Durable.Aardvark.Range1ulArray, typeof(Range1ul[]) },
            { Durable.Aardvark.Range1us     , typeof(Range1us  ) },
            { Durable.Aardvark.Range1usArray, typeof(Range1us[]) },

            { Durable.Aardvark.Ray2d, typeof(Ray2d) },
            { Durable.Aardvark.Ray2dArray, typeof(Ray2d[]) },
            { Durable.Aardvark.Ray3d, typeof(Ray3d) },
            { Durable.Aardvark.Ray3dArray, typeof(Ray3d[]) },

            { Durable.Aardvark.Rot2d     , typeof(Rot2d  ) },
            { Durable.Aardvark.Rot2dArray, typeof(Rot2d[]) },
            { Durable.Aardvark.Rot2f     , typeof(Rot2f  ) },
            { Durable.Aardvark.Rot2fArray, typeof(Rot2f[]) },
            { Durable.Aardvark.Rot3d     , typeof(Rot3d  ) },
            { Durable.Aardvark.Rot3dArray, typeof(Rot3d[]) },
            { Durable.Aardvark.Rot3f     , typeof(Rot3f  ) },
            { Durable.Aardvark.Rot3fArray, typeof(Rot3f[]) },

            { Durable.Aardvark.Scale2d     , typeof(Scale2d  ) },
            { Durable.Aardvark.Scale2dArray, typeof(Scale2d[]) },
            { Durable.Aardvark.Scale2f     , typeof(Scale2f  ) },
            { Durable.Aardvark.Scale2fArray, typeof(Scale2f[]) },
            { Durable.Aardvark.Scale3d     , typeof(Scale3d  ) },
            { Durable.Aardvark.Scale3dArray, typeof(Scale3d[]) },
            { Durable.Aardvark.Scale3f     , typeof(Scale3f  ) },
            { Durable.Aardvark.Scale3fArray, typeof(Scale3f[]) },

            { Durable.Aardvark.Shift2d     , typeof(Shift2d  ) },
            { Durable.Aardvark.Shift2dArray, typeof(Shift2d[]) },
            { Durable.Aardvark.Shift2f     , typeof(Shift2f  ) },
            { Durable.Aardvark.Shift2fArray, typeof(Shift2f[]) },
            { Durable.Aardvark.Shift3d     , typeof(Shift3d  ) },
            { Durable.Aardvark.Shift3dArray, typeof(Shift3d[]) },
            { Durable.Aardvark.Shift3f     , typeof(Shift3f  ) },
            { Durable.Aardvark.Shift3fArray, typeof(Shift3f[]) },

            { Durable.Aardvark.Similarity2d     , typeof(Similarity2d  ) },
            { Durable.Aardvark.Similarity2dArray, typeof(Similarity2d[]) },
            { Durable.Aardvark.Similarity2f     , typeof(Similarity2f  ) },
            { Durable.Aardvark.Similarity2fArray, typeof(Similarity2f[]) },
            { Durable.Aardvark.Similarity3d     , typeof(Similarity3d  ) },
            { Durable.Aardvark.Similarity3dArray, typeof(Similarity3d[]) },
            { Durable.Aardvark.Similarity3f     , typeof(Similarity3f  ) },
            { Durable.Aardvark.Similarity3fArray, typeof(Similarity3f[]) },

            { Durable.Aardvark.Sphere3d, typeof(Sphere3d) },
            { Durable.Aardvark.Sphere3dArray, typeof(Sphere3d[]) },
            { Durable.Aardvark.Triangle2d, typeof(Triangle2d) },
            { Durable.Aardvark.Triangle2dArray, typeof(Triangle2d[]) },
            { Durable.Aardvark.Triangle3d, typeof(Triangle3d) },
            { Durable.Aardvark.Triangle3dArray, typeof(Triangle3d[]) },
            { Durable.Aardvark.Torus3d, typeof(Torus3d) },
            { Durable.Aardvark.Torus3dArray, typeof(Torus3d[]) },

            { Durable.Aardvark.Trafo2d     , typeof(Trafo2d  ) },
            { Durable.Aardvark.Trafo2dArray, typeof(Trafo2d[]) },
            { Durable.Aardvark.Trafo2f     , typeof(Trafo2f  ) },
            { Durable.Aardvark.Trafo2fArray, typeof(Trafo2f[]) },
            { Durable.Aardvark.Trafo3d     , typeof(Trafo3d  ) },
            { Durable.Aardvark.Trafo3dArray, typeof(Trafo3d[]) },
            { Durable.Aardvark.Trafo3f     , typeof(Trafo3f  ) },
            { Durable.Aardvark.Trafo3fArray, typeof(Trafo3f[]) },

            { Durable.Aardvark.V2d     , typeof(V2d  ) },
            { Durable.Aardvark.V2dArray, typeof(V2d[]) },
            { Durable.Aardvark.V2f     , typeof(V2f  ) },
            { Durable.Aardvark.V2fArray, typeof(V2f[]) },
            { Durable.Aardvark.V2i     , typeof(V2i  ) },
            { Durable.Aardvark.V2iArray, typeof(V2i[]) },
            { Durable.Aardvark.V2l     , typeof(V2l  ) },
            { Durable.Aardvark.V2lArray, typeof(V2l[]) },
            { Durable.Aardvark.V3d     , typeof(V3d  ) },
            { Durable.Aardvark.V3dArray, typeof(V3d[]) },
            { Durable.Aardvark.V3f     , typeof(V3f  ) },
            { Durable.Aardvark.V3fArray, typeof(V3f[]) },
            { Durable.Aardvark.V3i     , typeof(V3i  ) },
            { Durable.Aardvark.V3iArray, typeof(V3i[]) },
            { Durable.Aardvark.V3l     , typeof(V3l  ) },
            { Durable.Aardvark.V3lArray, typeof(V3l[]) },
            { Durable.Aardvark.V4d     , typeof(V4d  ) },
            { Durable.Aardvark.V4dArray, typeof(V4d[]) },
            { Durable.Aardvark.V4f     , typeof(V4f  ) },
            { Durable.Aardvark.V4fArray, typeof(V4f[]) },
            { Durable.Aardvark.V4i     , typeof(V4i  ) },
            { Durable.Aardvark.V4iArray, typeof(V4i[]) },
            { Durable.Aardvark.V4l     , typeof(V4l  ) },
            { Durable.Aardvark.V4lArray, typeof(V4l[]) },

            { Durable.Aardvark.Yuvf     , typeof(Yuvf  ) },
            { Durable.Aardvark.YuvfArray, typeof(Yuvf[]) },
        };

        /// <summary></summary>
        public static Type GetPrimitiveDotnetType(this Durable.Def def)
            => _primitveDotnetTypes.TryGetValue(def.PrimitiveTypeDef, out var t) ? t : null;

        #endregion
    }
}
