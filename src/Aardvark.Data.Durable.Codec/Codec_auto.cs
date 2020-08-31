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
using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Aardvark.Data
{
        /// <summary></summary>
        public static partial class DurableCodec
        {
            private static void Init()
            {

                #region Durable.Aardvark.V2f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV2f = (s, o) => { var x = (V2f)o; s.Write(x.X); s.Write(x.Y); };
                Action<BinaryWriter, object> EncodeV2fArray = (s, o) => EncodeArray(s, (V2f[])o);
                Func<BinaryReader, object> DecodeV2f = s => new V2f(s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeV2fArray = DecodeArray<V2f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV2f = Write<V2f>;
                Action<Stream, object> EncodeV2fArray = (s, o) => EncodeArray(s, (V2f[])o);
                Func<Stream, object> DecodeV2f = ReadBoxed<V2f>;
                Func<Stream, object> DecodeV2fArray = DecodeArray<V2f>;
                #endif
                s_encoders[Durable.Aardvark.V2f.Id] = EncodeV2f;
                s_decoders[Durable.Aardvark.V2f.Id] = DecodeV2f;
                s_encoders[Durable.Aardvark.V2fArray.Id] = EncodeV2fArray;
                s_decoders[Durable.Aardvark.V2fArray.Id] = DecodeV2fArray;

                #endregion

                #region Durable.Aardvark.V2d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV2d = (s, o) => { var x = (V2d)o; s.Write(x.X); s.Write(x.Y); };
                Action<BinaryWriter, object> EncodeV2dArray = (s, o) => EncodeArray(s, (V2d[])o);
                Func<BinaryReader, object> DecodeV2d = s => new V2d(s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeV2dArray = DecodeArray<V2d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV2d = Write<V2d>;
                Action<Stream, object> EncodeV2dArray = (s, o) => EncodeArray(s, (V2d[])o);
                Func<Stream, object> DecodeV2d = ReadBoxed<V2d>;
                Func<Stream, object> DecodeV2dArray = DecodeArray<V2d>;
                #endif
                s_encoders[Durable.Aardvark.V2d.Id] = EncodeV2d;
                s_decoders[Durable.Aardvark.V2d.Id] = DecodeV2d;
                s_encoders[Durable.Aardvark.V2dArray.Id] = EncodeV2dArray;
                s_decoders[Durable.Aardvark.V2dArray.Id] = DecodeV2dArray;

                #endregion

                #region Durable.Aardvark.V3f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV3f = (s, o) => { var x = (V3f)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
                Action<BinaryWriter, object> EncodeV3fArray = (s, o) => EncodeArray(s, (V3f[])o);
                Func<BinaryReader, object> DecodeV3f = s => new V3f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeV3fArray = DecodeArray<V3f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV3f = Write<V3f>;
                Action<Stream, object> EncodeV3fArray = (s, o) => EncodeArray(s, (V3f[])o);
                Func<Stream, object> DecodeV3f = ReadBoxed<V3f>;
                Func<Stream, object> DecodeV3fArray = DecodeArray<V3f>;
                #endif
                s_encoders[Durable.Aardvark.V3f.Id] = EncodeV3f;
                s_decoders[Durable.Aardvark.V3f.Id] = DecodeV3f;
                s_encoders[Durable.Aardvark.V3fArray.Id] = EncodeV3fArray;
                s_decoders[Durable.Aardvark.V3fArray.Id] = DecodeV3fArray;

                #endregion

                #region Durable.Aardvark.V3d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV3d = (s, o) => { var x = (V3d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
                Action<BinaryWriter, object> EncodeV3dArray = (s, o) => EncodeArray(s, (V3d[])o);
                Func<BinaryReader, object> DecodeV3d = s => new V3d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeV3dArray = DecodeArray<V3d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV3d = Write<V3d>;
                Action<Stream, object> EncodeV3dArray = (s, o) => EncodeArray(s, (V3d[])o);
                Func<Stream, object> DecodeV3d = ReadBoxed<V3d>;
                Func<Stream, object> DecodeV3dArray = DecodeArray<V3d>;
                #endif
                s_encoders[Durable.Aardvark.V3d.Id] = EncodeV3d;
                s_decoders[Durable.Aardvark.V3d.Id] = DecodeV3d;
                s_encoders[Durable.Aardvark.V3dArray.Id] = EncodeV3dArray;
                s_decoders[Durable.Aardvark.V3dArray.Id] = DecodeV3dArray;

                #endregion

                #region Durable.Aardvark.V4f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV4f = (s, o) => { var x = (V4f)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
                Action<BinaryWriter, object> EncodeV4fArray = (s, o) => EncodeArray(s, (V4f[])o);
                Func<BinaryReader, object> DecodeV4f = s => new V4f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeV4fArray = DecodeArray<V4f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV4f = Write<V4f>;
                Action<Stream, object> EncodeV4fArray = (s, o) => EncodeArray(s, (V4f[])o);
                Func<Stream, object> DecodeV4f = ReadBoxed<V4f>;
                Func<Stream, object> DecodeV4fArray = DecodeArray<V4f>;
                #endif
                s_encoders[Durable.Aardvark.V4f.Id] = EncodeV4f;
                s_decoders[Durable.Aardvark.V4f.Id] = DecodeV4f;
                s_encoders[Durable.Aardvark.V4fArray.Id] = EncodeV4fArray;
                s_decoders[Durable.Aardvark.V4fArray.Id] = DecodeV4fArray;

                #endregion

                #region Durable.Aardvark.V4d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV4d = (s, o) => { var x = (V4d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
                Action<BinaryWriter, object> EncodeV4dArray = (s, o) => EncodeArray(s, (V4d[])o);
                Func<BinaryReader, object> DecodeV4d = s => new V4d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeV4dArray = DecodeArray<V4d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV4d = Write<V4d>;
                Action<Stream, object> EncodeV4dArray = (s, o) => EncodeArray(s, (V4d[])o);
                Func<Stream, object> DecodeV4d = ReadBoxed<V4d>;
                Func<Stream, object> DecodeV4dArray = DecodeArray<V4d>;
                #endif
                s_encoders[Durable.Aardvark.V4d.Id] = EncodeV4d;
                s_decoders[Durable.Aardvark.V4d.Id] = DecodeV4d;
                s_encoders[Durable.Aardvark.V4dArray.Id] = EncodeV4dArray;
                s_decoders[Durable.Aardvark.V4dArray.Id] = DecodeV4dArray;

                #endregion

                #region Durable.Aardvark.M22f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM22f = (s, o) => { var x = (M22f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M10); s.Write(x.M11); };
                Action<BinaryWriter, object> EncodeM22fArray = (s, o) => EncodeArray(s, (M22f[])o);
                Func<BinaryReader, object> DecodeM22f = s => new M22f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeM22fArray = DecodeArray<M22f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM22f = Write<M22f>;
                Action<Stream, object> EncodeM22fArray = (s, o) => EncodeArray(s, (M22f[])o);
                Func<Stream, object> DecodeM22f = ReadBoxed<M22f>;
                Func<Stream, object> DecodeM22fArray = DecodeArray<M22f>;
                #endif
                s_encoders[Durable.Aardvark.M22f.Id] = EncodeM22f;
                s_decoders[Durable.Aardvark.M22f.Id] = DecodeM22f;
                s_encoders[Durable.Aardvark.M22fArray.Id] = EncodeM22fArray;
                s_decoders[Durable.Aardvark.M22fArray.Id] = DecodeM22fArray;

                #endregion

                #region Durable.Aardvark.M22d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM22d = (s, o) => { var x = (M22d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M10); s.Write(x.M11); };
                Action<BinaryWriter, object> EncodeM22dArray = (s, o) => EncodeArray(s, (M22d[])o);
                Func<BinaryReader, object> DecodeM22d = s => new M22d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeM22dArray = DecodeArray<M22d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM22d = Write<M22d>;
                Action<Stream, object> EncodeM22dArray = (s, o) => EncodeArray(s, (M22d[])o);
                Func<Stream, object> DecodeM22d = ReadBoxed<M22d>;
                Func<Stream, object> DecodeM22dArray = DecodeArray<M22d>;
                #endif
                s_encoders[Durable.Aardvark.M22d.Id] = EncodeM22d;
                s_decoders[Durable.Aardvark.M22d.Id] = DecodeM22d;
                s_encoders[Durable.Aardvark.M22dArray.Id] = EncodeM22dArray;
                s_decoders[Durable.Aardvark.M22dArray.Id] = DecodeM22dArray;

                #endregion

                #region Durable.Aardvark.M33f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM33f = (s, o) => { var x = (M33f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); };
                Action<BinaryWriter, object> EncodeM33fArray = (s, o) => EncodeArray(s, (M33f[])o);
                Func<BinaryReader, object> DecodeM33f = s => new M33f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeM33fArray = DecodeArray<M33f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM33f = Write<M33f>;
                Action<Stream, object> EncodeM33fArray = (s, o) => EncodeArray(s, (M33f[])o);
                Func<Stream, object> DecodeM33f = ReadBoxed<M33f>;
                Func<Stream, object> DecodeM33fArray = DecodeArray<M33f>;
                #endif
                s_encoders[Durable.Aardvark.M33f.Id] = EncodeM33f;
                s_decoders[Durable.Aardvark.M33f.Id] = DecodeM33f;
                s_encoders[Durable.Aardvark.M33fArray.Id] = EncodeM33fArray;
                s_decoders[Durable.Aardvark.M33fArray.Id] = DecodeM33fArray;

                #endregion

                #region Durable.Aardvark.M33d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM33d = (s, o) => { var x = (M33d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); };
                Action<BinaryWriter, object> EncodeM33dArray = (s, o) => EncodeArray(s, (M33d[])o);
                Func<BinaryReader, object> DecodeM33d = s => new M33d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeM33dArray = DecodeArray<M33d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM33d = Write<M33d>;
                Action<Stream, object> EncodeM33dArray = (s, o) => EncodeArray(s, (M33d[])o);
                Func<Stream, object> DecodeM33d = ReadBoxed<M33d>;
                Func<Stream, object> DecodeM33dArray = DecodeArray<M33d>;
                #endif
                s_encoders[Durable.Aardvark.M33d.Id] = EncodeM33d;
                s_decoders[Durable.Aardvark.M33d.Id] = DecodeM33d;
                s_encoders[Durable.Aardvark.M33dArray.Id] = EncodeM33dArray;
                s_decoders[Durable.Aardvark.M33dArray.Id] = DecodeM33dArray;

                #endregion

                #region Durable.Aardvark.M44f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM44f = (s, o) => { var x = (M44f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M03); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M13); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); s.Write(x.M23); s.Write(x.M30); s.Write(x.M31); s.Write(x.M32); s.Write(x.M33); };
                Action<BinaryWriter, object> EncodeM44fArray = (s, o) => EncodeArray(s, (M44f[])o);
                Func<BinaryReader, object> DecodeM44f = s => new M44f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeM44fArray = DecodeArray<M44f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM44f = Write<M44f>;
                Action<Stream, object> EncodeM44fArray = (s, o) => EncodeArray(s, (M44f[])o);
                Func<Stream, object> DecodeM44f = ReadBoxed<M44f>;
                Func<Stream, object> DecodeM44fArray = DecodeArray<M44f>;
                #endif
                s_encoders[Durable.Aardvark.M44f.Id] = EncodeM44f;
                s_decoders[Durable.Aardvark.M44f.Id] = DecodeM44f;
                s_encoders[Durable.Aardvark.M44fArray.Id] = EncodeM44fArray;
                s_decoders[Durable.Aardvark.M44fArray.Id] = DecodeM44fArray;

                #endregion

                #region Durable.Aardvark.M44d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeM44d = (s, o) => { var x = (M44d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M03); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M13); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); s.Write(x.M23); s.Write(x.M30); s.Write(x.M31); s.Write(x.M32); s.Write(x.M33); };
                Action<BinaryWriter, object> EncodeM44dArray = (s, o) => EncodeArray(s, (M44d[])o);
                Func<BinaryReader, object> DecodeM44d = s => new M44d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeM44dArray = DecodeArray<M44d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeM44d = Write<M44d>;
                Action<Stream, object> EncodeM44dArray = (s, o) => EncodeArray(s, (M44d[])o);
                Func<Stream, object> DecodeM44d = ReadBoxed<M44d>;
                Func<Stream, object> DecodeM44dArray = DecodeArray<M44d>;
                #endif
                s_encoders[Durable.Aardvark.M44d.Id] = EncodeM44d;
                s_decoders[Durable.Aardvark.M44d.Id] = DecodeM44d;
                s_encoders[Durable.Aardvark.M44dArray.Id] = EncodeM44dArray;
                s_decoders[Durable.Aardvark.M44dArray.Id] = DecodeM44dArray;

                #endregion

                #region Durable.Aardvark.Range1f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1f = (s, o) => { var x = (Range1f)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1fArray = (s, o) => EncodeArray(s, (Range1f[])o);
                Func<BinaryReader, object> DecodeRange1f = s => new Range1f(s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeRange1fArray = DecodeArray<Range1f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1f = Write<Range1f>;
                Action<Stream, object> EncodeRange1fArray = (s, o) => EncodeArray(s, (Range1f[])o);
                Func<Stream, object> DecodeRange1f = ReadBoxed<Range1f>;
                Func<Stream, object> DecodeRange1fArray = DecodeArray<Range1f>;
                #endif
                s_encoders[Durable.Aardvark.Range1f.Id] = EncodeRange1f;
                s_decoders[Durable.Aardvark.Range1f.Id] = DecodeRange1f;
                s_encoders[Durable.Aardvark.Range1fArray.Id] = EncodeRange1fArray;
                s_decoders[Durable.Aardvark.Range1fArray.Id] = DecodeRange1fArray;

                #endregion

                #region Durable.Aardvark.Range1d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1d = (s, o) => { var x = (Range1d)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1dArray = (s, o) => EncodeArray(s, (Range1d[])o);
                Func<BinaryReader, object> DecodeRange1d = s => new Range1d(s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeRange1dArray = DecodeArray<Range1d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1d = Write<Range1d>;
                Action<Stream, object> EncodeRange1dArray = (s, o) => EncodeArray(s, (Range1d[])o);
                Func<Stream, object> DecodeRange1d = ReadBoxed<Range1d>;
                Func<Stream, object> DecodeRange1dArray = DecodeArray<Range1d>;
                #endif
                s_encoders[Durable.Aardvark.Range1d.Id] = EncodeRange1d;
                s_decoders[Durable.Aardvark.Range1d.Id] = DecodeRange1d;
                s_encoders[Durable.Aardvark.Range1dArray.Id] = EncodeRange1dArray;
                s_decoders[Durable.Aardvark.Range1dArray.Id] = DecodeRange1dArray;

                #endregion

                #region Durable.Aardvark.C3f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC3f = (s, o) => { var x = (C3f)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
                Action<BinaryWriter, object> EncodeC3fArray = (s, o) => EncodeArray(s, (C3f[])o);
                Func<BinaryReader, object> DecodeC3f = s => new C3f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeC3fArray = DecodeArray<C3f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC3f = Write<C3f>;
                Action<Stream, object> EncodeC3fArray = (s, o) => EncodeArray(s, (C3f[])o);
                Func<Stream, object> DecodeC3f = ReadBoxed<C3f>;
                Func<Stream, object> DecodeC3fArray = DecodeArray<C3f>;
                #endif
                s_encoders[Durable.Aardvark.C3f.Id] = EncodeC3f;
                s_decoders[Durable.Aardvark.C3f.Id] = DecodeC3f;
                s_encoders[Durable.Aardvark.C3fArray.Id] = EncodeC3fArray;
                s_decoders[Durable.Aardvark.C3fArray.Id] = DecodeC3fArray;

                #endregion

                #region Durable.Aardvark.C3d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC3d = (s, o) => { var x = (C3d)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
                Action<BinaryWriter, object> EncodeC3dArray = (s, o) => EncodeArray(s, (C3d[])o);
                Func<BinaryReader, object> DecodeC3d = s => new C3d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeC3dArray = DecodeArray<C3d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC3d = Write<C3d>;
                Action<Stream, object> EncodeC3dArray = (s, o) => EncodeArray(s, (C3d[])o);
                Func<Stream, object> DecodeC3d = ReadBoxed<C3d>;
                Func<Stream, object> DecodeC3dArray = DecodeArray<C3d>;
                #endif
                s_encoders[Durable.Aardvark.C3d.Id] = EncodeC3d;
                s_decoders[Durable.Aardvark.C3d.Id] = DecodeC3d;
                s_encoders[Durable.Aardvark.C3dArray.Id] = EncodeC3dArray;
                s_decoders[Durable.Aardvark.C3dArray.Id] = DecodeC3dArray;

                #endregion

                #region Durable.Aardvark.C4f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC4f = (s, o) => { var x = (C4f)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
                Action<BinaryWriter, object> EncodeC4fArray = (s, o) => EncodeArray(s, (C4f[])o);
                Func<BinaryReader, object> DecodeC4f = s => new C4f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeC4fArray = DecodeArray<C4f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC4f = Write<C4f>;
                Action<Stream, object> EncodeC4fArray = (s, o) => EncodeArray(s, (C4f[])o);
                Func<Stream, object> DecodeC4f = ReadBoxed<C4f>;
                Func<Stream, object> DecodeC4fArray = DecodeArray<C4f>;
                #endif
                s_encoders[Durable.Aardvark.C4f.Id] = EncodeC4f;
                s_decoders[Durable.Aardvark.C4f.Id] = DecodeC4f;
                s_encoders[Durable.Aardvark.C4fArray.Id] = EncodeC4fArray;
                s_decoders[Durable.Aardvark.C4fArray.Id] = DecodeC4fArray;

                #endregion

                #region Durable.Aardvark.C4d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC4d = (s, o) => { var x = (C4d)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
                Action<BinaryWriter, object> EncodeC4dArray = (s, o) => EncodeArray(s, (C4d[])o);
                Func<BinaryReader, object> DecodeC4d = s => new C4d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeC4dArray = DecodeArray<C4d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC4d = Write<C4d>;
                Action<Stream, object> EncodeC4dArray = (s, o) => EncodeArray(s, (C4d[])o);
                Func<Stream, object> DecodeC4d = ReadBoxed<C4d>;
                Func<Stream, object> DecodeC4dArray = DecodeArray<C4d>;
                #endif
                s_encoders[Durable.Aardvark.C4d.Id] = EncodeC4d;
                s_decoders[Durable.Aardvark.C4d.Id] = DecodeC4d;
                s_encoders[Durable.Aardvark.C4dArray.Id] = EncodeC4dArray;
                s_decoders[Durable.Aardvark.C4dArray.Id] = DecodeC4dArray;

                #endregion

                #region Durable.Aardvark.CieLabf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeCieLabf = (s, o) => { var x = (CieLabf)o; s.Write(x.L); s.Write(x.a); s.Write(x.b); };
                Action<BinaryWriter, object> EncodeCieLabfArray = (s, o) => EncodeArray(s, (CieLabf[])o);
                Func<BinaryReader, object> DecodeCieLabf = s => new CieLabf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeCieLabfArray = DecodeArray<CieLabf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeCieLabf = Write<CieLabf>;
                Action<Stream, object> EncodeCieLabfArray = (s, o) => EncodeArray(s, (CieLabf[])o);
                Func<Stream, object> DecodeCieLabf = ReadBoxed<CieLabf>;
                Func<Stream, object> DecodeCieLabfArray = DecodeArray<CieLabf>;
                #endif
                s_encoders[Durable.Aardvark.CieLabf.Id] = EncodeCieLabf;
                s_decoders[Durable.Aardvark.CieLabf.Id] = DecodeCieLabf;
                s_encoders[Durable.Aardvark.CieLabfArray.Id] = EncodeCieLabfArray;
                s_decoders[Durable.Aardvark.CieLabfArray.Id] = DecodeCieLabfArray;

                #endregion

                #region Durable.Aardvark.CIeLuvf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeCIeLuvf = (s, o) => { var x = (CIeLuvf)o; s.Write(x.L); s.Write(x.u); s.Write(x.v); };
                Action<BinaryWriter, object> EncodeCIeLuvfArray = (s, o) => EncodeArray(s, (CIeLuvf[])o);
                Func<BinaryReader, object> DecodeCIeLuvf = s => new CIeLuvf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeCIeLuvfArray = DecodeArray<CIeLuvf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeCIeLuvf = Write<CIeLuvf>;
                Action<Stream, object> EncodeCIeLuvfArray = (s, o) => EncodeArray(s, (CIeLuvf[])o);
                Func<Stream, object> DecodeCIeLuvf = ReadBoxed<CIeLuvf>;
                Func<Stream, object> DecodeCIeLuvfArray = DecodeArray<CIeLuvf>;
                #endif
                s_encoders[Durable.Aardvark.CIeLuvf.Id] = EncodeCIeLuvf;
                s_decoders[Durable.Aardvark.CIeLuvf.Id] = DecodeCIeLuvf;
                s_encoders[Durable.Aardvark.CIeLuvfArray.Id] = EncodeCIeLuvfArray;
                s_decoders[Durable.Aardvark.CIeLuvfArray.Id] = DecodeCIeLuvfArray;

                #endregion

                #region Durable.Aardvark.CieXYZf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeCieXYZf = (s, o) => { var x = (CieXYZf)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
                Action<BinaryWriter, object> EncodeCieXYZfArray = (s, o) => EncodeArray(s, (CieXYZf[])o);
                Func<BinaryReader, object> DecodeCieXYZf = s => new CieXYZf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeCieXYZfArray = DecodeArray<CieXYZf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeCieXYZf = Write<CieXYZf>;
                Action<Stream, object> EncodeCieXYZfArray = (s, o) => EncodeArray(s, (CieXYZf[])o);
                Func<Stream, object> DecodeCieXYZf = ReadBoxed<CieXYZf>;
                Func<Stream, object> DecodeCieXYZfArray = DecodeArray<CieXYZf>;
                #endif
                s_encoders[Durable.Aardvark.CieXYZf.Id] = EncodeCieXYZf;
                s_decoders[Durable.Aardvark.CieXYZf.Id] = DecodeCieXYZf;
                s_encoders[Durable.Aardvark.CieXYZfArray.Id] = EncodeCieXYZfArray;
                s_decoders[Durable.Aardvark.CieXYZfArray.Id] = DecodeCieXYZfArray;

                #endregion

                #region Durable.Aardvark.CieYxyf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeCieYxyf = (s, o) => { var x = (CieYxyf)o; s.Write(x.Y); s.Write(x.x); s.Write(x.y); };
                Action<BinaryWriter, object> EncodeCieYxyfArray = (s, o) => EncodeArray(s, (CieYxyf[])o);
                Func<BinaryReader, object> DecodeCieYxyf = s => new CieYxyf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeCieYxyfArray = DecodeArray<CieYxyf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeCieYxyf = Write<CieYxyf>;
                Action<Stream, object> EncodeCieYxyfArray = (s, o) => EncodeArray(s, (CieYxyf[])o);
                Func<Stream, object> DecodeCieYxyf = ReadBoxed<CieYxyf>;
                Func<Stream, object> DecodeCieYxyfArray = DecodeArray<CieYxyf>;
                #endif
                s_encoders[Durable.Aardvark.CieYxyf.Id] = EncodeCieYxyf;
                s_decoders[Durable.Aardvark.CieYxyf.Id] = DecodeCieYxyf;
                s_encoders[Durable.Aardvark.CieYxyfArray.Id] = EncodeCieYxyfArray;
                s_decoders[Durable.Aardvark.CieYxyfArray.Id] = DecodeCieYxyfArray;

                #endregion

                #region Durable.Aardvark.CMYKf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeCMYKf = (s, o) => { var x = (CMYKf)o; s.Write(x.C); s.Write(x.M); s.Write(x.Y); s.Write(x.K); };
                Action<BinaryWriter, object> EncodeCMYKfArray = (s, o) => EncodeArray(s, (CMYKf[])o);
                Func<BinaryReader, object> DecodeCMYKf = s => new CMYKf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeCMYKfArray = DecodeArray<CMYKf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeCMYKf = Write<CMYKf>;
                Action<Stream, object> EncodeCMYKfArray = (s, o) => EncodeArray(s, (CMYKf[])o);
                Func<Stream, object> DecodeCMYKf = ReadBoxed<CMYKf>;
                Func<Stream, object> DecodeCMYKfArray = DecodeArray<CMYKf>;
                #endif
                s_encoders[Durable.Aardvark.CMYKf.Id] = EncodeCMYKf;
                s_decoders[Durable.Aardvark.CMYKf.Id] = DecodeCMYKf;
                s_encoders[Durable.Aardvark.CMYKfArray.Id] = EncodeCMYKfArray;
                s_decoders[Durable.Aardvark.CMYKfArray.Id] = DecodeCMYKfArray;

                #endregion

                #region Durable.Aardvark.HSLf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeHSLf = (s, o) => { var x = (HSLf)o; s.Write(x.H); s.Write(x.S); s.Write(x.L); };
                Action<BinaryWriter, object> EncodeHSLfArray = (s, o) => EncodeArray(s, (HSLf[])o);
                Func<BinaryReader, object> DecodeHSLf = s => new HSLf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeHSLfArray = DecodeArray<HSLf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeHSLf = Write<HSLf>;
                Action<Stream, object> EncodeHSLfArray = (s, o) => EncodeArray(s, (HSLf[])o);
                Func<Stream, object> DecodeHSLf = ReadBoxed<HSLf>;
                Func<Stream, object> DecodeHSLfArray = DecodeArray<HSLf>;
                #endif
                s_encoders[Durable.Aardvark.HSLf.Id] = EncodeHSLf;
                s_decoders[Durable.Aardvark.HSLf.Id] = DecodeHSLf;
                s_encoders[Durable.Aardvark.HSLfArray.Id] = EncodeHSLfArray;
                s_decoders[Durable.Aardvark.HSLfArray.Id] = DecodeHSLfArray;

                #endregion

                #region Durable.Aardvark.HSVf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeHSVf = (s, o) => { var x = (HSVf)o; s.Write(x.H); s.Write(x.S); s.Write(x.V); };
                Action<BinaryWriter, object> EncodeHSVfArray = (s, o) => EncodeArray(s, (HSVf[])o);
                Func<BinaryReader, object> DecodeHSVf = s => new HSVf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeHSVfArray = DecodeArray<HSVf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeHSVf = Write<HSVf>;
                Action<Stream, object> EncodeHSVfArray = (s, o) => EncodeArray(s, (HSVf[])o);
                Func<Stream, object> DecodeHSVf = ReadBoxed<HSVf>;
                Func<Stream, object> DecodeHSVfArray = DecodeArray<HSVf>;
                #endif
                s_encoders[Durable.Aardvark.HSVf.Id] = EncodeHSVf;
                s_decoders[Durable.Aardvark.HSVf.Id] = DecodeHSVf;
                s_encoders[Durable.Aardvark.HSVfArray.Id] = EncodeHSVfArray;
                s_decoders[Durable.Aardvark.HSVfArray.Id] = DecodeHSVfArray;

                #endregion

                #region Durable.Aardvark.Yuvf

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeYuvf = (s, o) => { var x = (Yuvf)o; s.Write(x.Y); s.Write(x.u); s.Write(x.v); };
                Action<BinaryWriter, object> EncodeYuvfArray = (s, o) => EncodeArray(s, (Yuvf[])o);
                Func<BinaryReader, object> DecodeYuvf = s => new Yuvf(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeYuvfArray = DecodeArray<Yuvf>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeYuvf = Write<Yuvf>;
                Action<Stream, object> EncodeYuvfArray = (s, o) => EncodeArray(s, (Yuvf[])o);
                Func<Stream, object> DecodeYuvf = ReadBoxed<Yuvf>;
                Func<Stream, object> DecodeYuvfArray = DecodeArray<Yuvf>;
                #endif
                s_encoders[Durable.Aardvark.Yuvf.Id] = EncodeYuvf;
                s_decoders[Durable.Aardvark.Yuvf.Id] = DecodeYuvf;
                s_encoders[Durable.Aardvark.YuvfArray.Id] = EncodeYuvfArray;
                s_decoders[Durable.Aardvark.YuvfArray.Id] = DecodeYuvfArray;

                #endregion


            }
        }
}

