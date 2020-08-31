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

                #region Durable.Aardvark.V2i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV2i = (s, o) => { var x = (V2i)o; s.Write(x.X); s.Write(x.Y); };
                Action<BinaryWriter, object> EncodeV2iArray = (s, o) => EncodeArray(s, (V2i[])o);
                Func<BinaryReader, object> DecodeV2i = s => new V2i(s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeV2iArray = DecodeArray<V2i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV2i = Write<V2i>;
                Action<Stream, object> EncodeV2iArray = (s, o) => EncodeArray(s, (V2i[])o);
                Func<Stream, object> DecodeV2i = ReadBoxed<V2i>;
                Func<Stream, object> DecodeV2iArray = DecodeArray<V2i>;
                #endif
                s_encoders[Durable.Aardvark.V2i.Id] = EncodeV2i;
                s_decoders[Durable.Aardvark.V2i.Id] = DecodeV2i;
                s_encoders[Durable.Aardvark.V2iArray.Id] = EncodeV2iArray;
                s_decoders[Durable.Aardvark.V2iArray.Id] = DecodeV2iArray;

                #endregion

                #region Durable.Aardvark.V2l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV2l = (s, o) => { var x = (V2l)o; s.Write(x.X); s.Write(x.Y); };
                Action<BinaryWriter, object> EncodeV2lArray = (s, o) => EncodeArray(s, (V2l[])o);
                Func<BinaryReader, object> DecodeV2l = s => new V2l(s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeV2lArray = DecodeArray<V2l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV2l = Write<V2l>;
                Action<Stream, object> EncodeV2lArray = (s, o) => EncodeArray(s, (V2l[])o);
                Func<Stream, object> DecodeV2l = ReadBoxed<V2l>;
                Func<Stream, object> DecodeV2lArray = DecodeArray<V2l>;
                #endif
                s_encoders[Durable.Aardvark.V2l.Id] = EncodeV2l;
                s_decoders[Durable.Aardvark.V2l.Id] = DecodeV2l;
                s_encoders[Durable.Aardvark.V2lArray.Id] = EncodeV2lArray;
                s_decoders[Durable.Aardvark.V2lArray.Id] = DecodeV2lArray;

                #endregion

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

                #region Durable.Aardvark.V3i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV3i = (s, o) => { var x = (V3i)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
                Action<BinaryWriter, object> EncodeV3iArray = (s, o) => EncodeArray(s, (V3i[])o);
                Func<BinaryReader, object> DecodeV3i = s => new V3i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeV3iArray = DecodeArray<V3i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV3i = Write<V3i>;
                Action<Stream, object> EncodeV3iArray = (s, o) => EncodeArray(s, (V3i[])o);
                Func<Stream, object> DecodeV3i = ReadBoxed<V3i>;
                Func<Stream, object> DecodeV3iArray = DecodeArray<V3i>;
                #endif
                s_encoders[Durable.Aardvark.V3i.Id] = EncodeV3i;
                s_decoders[Durable.Aardvark.V3i.Id] = DecodeV3i;
                s_encoders[Durable.Aardvark.V3iArray.Id] = EncodeV3iArray;
                s_decoders[Durable.Aardvark.V3iArray.Id] = DecodeV3iArray;

                #endregion

                #region Durable.Aardvark.V3l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV3l = (s, o) => { var x = (V3l)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
                Action<BinaryWriter, object> EncodeV3lArray = (s, o) => EncodeArray(s, (V3l[])o);
                Func<BinaryReader, object> DecodeV3l = s => new V3l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeV3lArray = DecodeArray<V3l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV3l = Write<V3l>;
                Action<Stream, object> EncodeV3lArray = (s, o) => EncodeArray(s, (V3l[])o);
                Func<Stream, object> DecodeV3l = ReadBoxed<V3l>;
                Func<Stream, object> DecodeV3lArray = DecodeArray<V3l>;
                #endif
                s_encoders[Durable.Aardvark.V3l.Id] = EncodeV3l;
                s_decoders[Durable.Aardvark.V3l.Id] = DecodeV3l;
                s_encoders[Durable.Aardvark.V3lArray.Id] = EncodeV3lArray;
                s_decoders[Durable.Aardvark.V3lArray.Id] = DecodeV3lArray;

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

                #region Durable.Aardvark.V4i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV4i = (s, o) => { var x = (V4i)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
                Action<BinaryWriter, object> EncodeV4iArray = (s, o) => EncodeArray(s, (V4i[])o);
                Func<BinaryReader, object> DecodeV4i = s => new V4i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeV4iArray = DecodeArray<V4i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV4i = Write<V4i>;
                Action<Stream, object> EncodeV4iArray = (s, o) => EncodeArray(s, (V4i[])o);
                Func<Stream, object> DecodeV4i = ReadBoxed<V4i>;
                Func<Stream, object> DecodeV4iArray = DecodeArray<V4i>;
                #endif
                s_encoders[Durable.Aardvark.V4i.Id] = EncodeV4i;
                s_decoders[Durable.Aardvark.V4i.Id] = DecodeV4i;
                s_encoders[Durable.Aardvark.V4iArray.Id] = EncodeV4iArray;
                s_decoders[Durable.Aardvark.V4iArray.Id] = DecodeV4iArray;

                #endregion

                #region Durable.Aardvark.V4l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeV4l = (s, o) => { var x = (V4l)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
                Action<BinaryWriter, object> EncodeV4lArray = (s, o) => EncodeArray(s, (V4l[])o);
                Func<BinaryReader, object> DecodeV4l = s => new V4l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeV4lArray = DecodeArray<V4l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeV4l = Write<V4l>;
                Action<Stream, object> EncodeV4lArray = (s, o) => EncodeArray(s, (V4l[])o);
                Func<Stream, object> DecodeV4l = ReadBoxed<V4l>;
                Func<Stream, object> DecodeV4lArray = DecodeArray<V4l>;
                #endif
                s_encoders[Durable.Aardvark.V4l.Id] = EncodeV4l;
                s_decoders[Durable.Aardvark.V4l.Id] = DecodeV4l;
                s_encoders[Durable.Aardvark.V4lArray.Id] = EncodeV4lArray;
                s_decoders[Durable.Aardvark.V4lArray.Id] = DecodeV4lArray;

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

                #region Durable.Aardvark.Range1b

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1b = (s, o) => { var x = (Range1b)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1bArray = (s, o) => EncodeArray(s, (Range1b[])o);
                Func<BinaryReader, object> DecodeRange1b = s => new Range1b(s.ReadByte(), s.ReadByte());
                Func<BinaryReader, object> DecodeRange1bArray = DecodeArray<Range1b>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1b = Write<Range1b>;
                Action<Stream, object> EncodeRange1bArray = (s, o) => EncodeArray(s, (Range1b[])o);
                Func<Stream, object> DecodeRange1b = ReadBoxed<Range1b>;
                Func<Stream, object> DecodeRange1bArray = DecodeArray<Range1b>;
                #endif
                s_encoders[Durable.Aardvark.Range1b.Id] = EncodeRange1b;
                s_decoders[Durable.Aardvark.Range1b.Id] = DecodeRange1b;
                s_encoders[Durable.Aardvark.Range1bArray.Id] = EncodeRange1bArray;
                s_decoders[Durable.Aardvark.Range1bArray.Id] = DecodeRange1bArray;

                #endregion

                #region Durable.Aardvark.Range1sb

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1sb = (s, o) => { var x = (Range1sb)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1sbArray = (s, o) => EncodeArray(s, (Range1sb[])o);
                Func<BinaryReader, object> DecodeRange1sb = s => new Range1sb(s.ReadSByte(), s.ReadSByte());
                Func<BinaryReader, object> DecodeRange1sbArray = DecodeArray<Range1sb>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1sb = Write<Range1sb>;
                Action<Stream, object> EncodeRange1sbArray = (s, o) => EncodeArray(s, (Range1sb[])o);
                Func<Stream, object> DecodeRange1sb = ReadBoxed<Range1sb>;
                Func<Stream, object> DecodeRange1sbArray = DecodeArray<Range1sb>;
                #endif
                s_encoders[Durable.Aardvark.Range1sb.Id] = EncodeRange1sb;
                s_decoders[Durable.Aardvark.Range1sb.Id] = DecodeRange1sb;
                s_encoders[Durable.Aardvark.Range1sbArray.Id] = EncodeRange1sbArray;
                s_decoders[Durable.Aardvark.Range1sbArray.Id] = DecodeRange1sbArray;

                #endregion

                #region Durable.Aardvark.Range1s

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1s = (s, o) => { var x = (Range1s)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1sArray = (s, o) => EncodeArray(s, (Range1s[])o);
                Func<BinaryReader, object> DecodeRange1s = s => new Range1s(s.ReadInt16(), s.ReadInt16());
                Func<BinaryReader, object> DecodeRange1sArray = DecodeArray<Range1s>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1s = Write<Range1s>;
                Action<Stream, object> EncodeRange1sArray = (s, o) => EncodeArray(s, (Range1s[])o);
                Func<Stream, object> DecodeRange1s = ReadBoxed<Range1s>;
                Func<Stream, object> DecodeRange1sArray = DecodeArray<Range1s>;
                #endif
                s_encoders[Durable.Aardvark.Range1s.Id] = EncodeRange1s;
                s_decoders[Durable.Aardvark.Range1s.Id] = DecodeRange1s;
                s_encoders[Durable.Aardvark.Range1sArray.Id] = EncodeRange1sArray;
                s_decoders[Durable.Aardvark.Range1sArray.Id] = DecodeRange1sArray;

                #endregion

                #region Durable.Aardvark.Range1us

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1us = (s, o) => { var x = (Range1us)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1usArray = (s, o) => EncodeArray(s, (Range1us[])o);
                Func<BinaryReader, object> DecodeRange1us = s => new Range1us(s.ReadUInt16(), s.ReadUInt16());
                Func<BinaryReader, object> DecodeRange1usArray = DecodeArray<Range1us>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1us = Write<Range1us>;
                Action<Stream, object> EncodeRange1usArray = (s, o) => EncodeArray(s, (Range1us[])o);
                Func<Stream, object> DecodeRange1us = ReadBoxed<Range1us>;
                Func<Stream, object> DecodeRange1usArray = DecodeArray<Range1us>;
                #endif
                s_encoders[Durable.Aardvark.Range1us.Id] = EncodeRange1us;
                s_decoders[Durable.Aardvark.Range1us.Id] = DecodeRange1us;
                s_encoders[Durable.Aardvark.Range1usArray.Id] = EncodeRange1usArray;
                s_decoders[Durable.Aardvark.Range1usArray.Id] = DecodeRange1usArray;

                #endregion

                #region Durable.Aardvark.Range1i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1i = (s, o) => { var x = (Range1i)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1iArray = (s, o) => EncodeArray(s, (Range1i[])o);
                Func<BinaryReader, object> DecodeRange1i = s => new Range1i(s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeRange1iArray = DecodeArray<Range1i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1i = Write<Range1i>;
                Action<Stream, object> EncodeRange1iArray = (s, o) => EncodeArray(s, (Range1i[])o);
                Func<Stream, object> DecodeRange1i = ReadBoxed<Range1i>;
                Func<Stream, object> DecodeRange1iArray = DecodeArray<Range1i>;
                #endif
                s_encoders[Durable.Aardvark.Range1i.Id] = EncodeRange1i;
                s_decoders[Durable.Aardvark.Range1i.Id] = DecodeRange1i;
                s_encoders[Durable.Aardvark.Range1iArray.Id] = EncodeRange1iArray;
                s_decoders[Durable.Aardvark.Range1iArray.Id] = DecodeRange1iArray;

                #endregion

                #region Durable.Aardvark.Range1ui

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1ui = (s, o) => { var x = (Range1ui)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1uiArray = (s, o) => EncodeArray(s, (Range1ui[])o);
                Func<BinaryReader, object> DecodeRange1ui = s => new Range1ui(s.ReadUInt32(), s.ReadUInt32());
                Func<BinaryReader, object> DecodeRange1uiArray = DecodeArray<Range1ui>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1ui = Write<Range1ui>;
                Action<Stream, object> EncodeRange1uiArray = (s, o) => EncodeArray(s, (Range1ui[])o);
                Func<Stream, object> DecodeRange1ui = ReadBoxed<Range1ui>;
                Func<Stream, object> DecodeRange1uiArray = DecodeArray<Range1ui>;
                #endif
                s_encoders[Durable.Aardvark.Range1ui.Id] = EncodeRange1ui;
                s_decoders[Durable.Aardvark.Range1ui.Id] = DecodeRange1ui;
                s_encoders[Durable.Aardvark.Range1uiArray.Id] = EncodeRange1uiArray;
                s_decoders[Durable.Aardvark.Range1uiArray.Id] = DecodeRange1uiArray;

                #endregion

                #region Durable.Aardvark.Range1l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1l = (s, o) => { var x = (Range1l)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1lArray = (s, o) => EncodeArray(s, (Range1l[])o);
                Func<BinaryReader, object> DecodeRange1l = s => new Range1l(s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeRange1lArray = DecodeArray<Range1l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1l = Write<Range1l>;
                Action<Stream, object> EncodeRange1lArray = (s, o) => EncodeArray(s, (Range1l[])o);
                Func<Stream, object> DecodeRange1l = ReadBoxed<Range1l>;
                Func<Stream, object> DecodeRange1lArray = DecodeArray<Range1l>;
                #endif
                s_encoders[Durable.Aardvark.Range1l.Id] = EncodeRange1l;
                s_decoders[Durable.Aardvark.Range1l.Id] = DecodeRange1l;
                s_encoders[Durable.Aardvark.Range1lArray.Id] = EncodeRange1lArray;
                s_decoders[Durable.Aardvark.Range1lArray.Id] = DecodeRange1lArray;

                #endregion

                #region Durable.Aardvark.Range1ul

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeRange1ul = (s, o) => { var x = (Range1ul)o; s.Write(x.Min); s.Write(x.Max); };
                Action<BinaryWriter, object> EncodeRange1ulArray = (s, o) => EncodeArray(s, (Range1ul[])o);
                Func<BinaryReader, object> DecodeRange1ul = s => new Range1ul(s.ReadUInt64(), s.ReadUInt64());
                Func<BinaryReader, object> DecodeRange1ulArray = DecodeArray<Range1ul>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeRange1ul = Write<Range1ul>;
                Action<Stream, object> EncodeRange1ulArray = (s, o) => EncodeArray(s, (Range1ul[])o);
                Func<Stream, object> DecodeRange1ul = ReadBoxed<Range1ul>;
                Func<Stream, object> DecodeRange1ulArray = DecodeArray<Range1ul>;
                #endif
                s_encoders[Durable.Aardvark.Range1ul.Id] = EncodeRange1ul;
                s_decoders[Durable.Aardvark.Range1ul.Id] = DecodeRange1ul;
                s_encoders[Durable.Aardvark.Range1ulArray.Id] = EncodeRange1ulArray;
                s_decoders[Durable.Aardvark.Range1ulArray.Id] = DecodeRange1ulArray;

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

                #region Durable.Aardvark.Box2i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox2i = (s, o) => { var x = (Box2i)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Max.X); s.Write(x.Max.Y); };
                Action<BinaryWriter, object> EncodeBox2iArray = (s, o) => EncodeArray(s, (Box2i[])o);
                Func<BinaryReader, object> DecodeBox2i = s => new Box2i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeBox2iArray = DecodeArray<Box2i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox2i = Write<Box2i>;
                Action<Stream, object> EncodeBox2iArray = (s, o) => EncodeArray(s, (Box2i[])o);
                Func<Stream, object> DecodeBox2i = ReadBoxed<Box2i>;
                Func<Stream, object> DecodeBox2iArray = DecodeArray<Box2i>;
                #endif
                s_encoders[Durable.Aardvark.Box2i.Id] = EncodeBox2i;
                s_decoders[Durable.Aardvark.Box2i.Id] = DecodeBox2i;
                s_encoders[Durable.Aardvark.Box2iArray.Id] = EncodeBox2iArray;
                s_decoders[Durable.Aardvark.Box2iArray.Id] = DecodeBox2iArray;

                #endregion

                #region Durable.Aardvark.Box2l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox2l = (s, o) => { var x = (Box2l)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Max.X); s.Write(x.Max.Y); };
                Action<BinaryWriter, object> EncodeBox2lArray = (s, o) => EncodeArray(s, (Box2l[])o);
                Func<BinaryReader, object> DecodeBox2l = s => new Box2l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeBox2lArray = DecodeArray<Box2l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox2l = Write<Box2l>;
                Action<Stream, object> EncodeBox2lArray = (s, o) => EncodeArray(s, (Box2l[])o);
                Func<Stream, object> DecodeBox2l = ReadBoxed<Box2l>;
                Func<Stream, object> DecodeBox2lArray = DecodeArray<Box2l>;
                #endif
                s_encoders[Durable.Aardvark.Box2l.Id] = EncodeBox2l;
                s_decoders[Durable.Aardvark.Box2l.Id] = DecodeBox2l;
                s_encoders[Durable.Aardvark.Box2lArray.Id] = EncodeBox2lArray;
                s_decoders[Durable.Aardvark.Box2lArray.Id] = DecodeBox2lArray;

                #endregion

                #region Durable.Aardvark.Box2f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox2f = (s, o) => { var x = (Box2f)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Max.X); s.Write(x.Max.Y); };
                Action<BinaryWriter, object> EncodeBox2fArray = (s, o) => EncodeArray(s, (Box2f[])o);
                Func<BinaryReader, object> DecodeBox2f = s => new Box2f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeBox2fArray = DecodeArray<Box2f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox2f = Write<Box2f>;
                Action<Stream, object> EncodeBox2fArray = (s, o) => EncodeArray(s, (Box2f[])o);
                Func<Stream, object> DecodeBox2f = ReadBoxed<Box2f>;
                Func<Stream, object> DecodeBox2fArray = DecodeArray<Box2f>;
                #endif
                s_encoders[Durable.Aardvark.Box2f.Id] = EncodeBox2f;
                s_decoders[Durable.Aardvark.Box2f.Id] = DecodeBox2f;
                s_encoders[Durable.Aardvark.Box2fArray.Id] = EncodeBox2fArray;
                s_decoders[Durable.Aardvark.Box2fArray.Id] = DecodeBox2fArray;

                #endregion

                #region Durable.Aardvark.Box2d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox2d = (s, o) => { var x = (Box2d)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Max.X); s.Write(x.Max.Y); };
                Action<BinaryWriter, object> EncodeBox2dArray = (s, o) => EncodeArray(s, (Box2d[])o);
                Func<BinaryReader, object> DecodeBox2d = s => new Box2d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeBox2dArray = DecodeArray<Box2d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox2d = Write<Box2d>;
                Action<Stream, object> EncodeBox2dArray = (s, o) => EncodeArray(s, (Box2d[])o);
                Func<Stream, object> DecodeBox2d = ReadBoxed<Box2d>;
                Func<Stream, object> DecodeBox2dArray = DecodeArray<Box2d>;
                #endif
                s_encoders[Durable.Aardvark.Box2d.Id] = EncodeBox2d;
                s_decoders[Durable.Aardvark.Box2d.Id] = DecodeBox2d;
                s_encoders[Durable.Aardvark.Box2dArray.Id] = EncodeBox2dArray;
                s_decoders[Durable.Aardvark.Box2dArray.Id] = DecodeBox2dArray;

                #endregion

                #region Durable.Aardvark.Box3i

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox3i = (s, o) => { var x = (Box3i)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Min.Z); s.Write(x.Max.X); s.Write(x.Max.Y); s.Write(x.Max.Z); };
                Action<BinaryWriter, object> EncodeBox3iArray = (s, o) => EncodeArray(s, (Box3i[])o);
                Func<BinaryReader, object> DecodeBox3i = s => new Box3i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
                Func<BinaryReader, object> DecodeBox3iArray = DecodeArray<Box3i>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox3i = Write<Box3i>;
                Action<Stream, object> EncodeBox3iArray = (s, o) => EncodeArray(s, (Box3i[])o);
                Func<Stream, object> DecodeBox3i = ReadBoxed<Box3i>;
                Func<Stream, object> DecodeBox3iArray = DecodeArray<Box3i>;
                #endif
                s_encoders[Durable.Aardvark.Box3i.Id] = EncodeBox3i;
                s_decoders[Durable.Aardvark.Box3i.Id] = DecodeBox3i;
                s_encoders[Durable.Aardvark.Box3iArray.Id] = EncodeBox3iArray;
                s_decoders[Durable.Aardvark.Box3iArray.Id] = DecodeBox3iArray;

                #endregion

                #region Durable.Aardvark.Box3l

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox3l = (s, o) => { var x = (Box3l)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Min.Z); s.Write(x.Max.X); s.Write(x.Max.Y); s.Write(x.Max.Z); };
                Action<BinaryWriter, object> EncodeBox3lArray = (s, o) => EncodeArray(s, (Box3l[])o);
                Func<BinaryReader, object> DecodeBox3l = s => new Box3l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
                Func<BinaryReader, object> DecodeBox3lArray = DecodeArray<Box3l>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox3l = Write<Box3l>;
                Action<Stream, object> EncodeBox3lArray = (s, o) => EncodeArray(s, (Box3l[])o);
                Func<Stream, object> DecodeBox3l = ReadBoxed<Box3l>;
                Func<Stream, object> DecodeBox3lArray = DecodeArray<Box3l>;
                #endif
                s_encoders[Durable.Aardvark.Box3l.Id] = EncodeBox3l;
                s_decoders[Durable.Aardvark.Box3l.Id] = DecodeBox3l;
                s_encoders[Durable.Aardvark.Box3lArray.Id] = EncodeBox3lArray;
                s_decoders[Durable.Aardvark.Box3lArray.Id] = DecodeBox3lArray;

                #endregion

                #region Durable.Aardvark.Box3f

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox3f = (s, o) => { var x = (Box3f)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Min.Z); s.Write(x.Max.X); s.Write(x.Max.Y); s.Write(x.Max.Z); };
                Action<BinaryWriter, object> EncodeBox3fArray = (s, o) => EncodeArray(s, (Box3f[])o);
                Func<BinaryReader, object> DecodeBox3f = s => new Box3f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
                Func<BinaryReader, object> DecodeBox3fArray = DecodeArray<Box3f>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox3f = Write<Box3f>;
                Action<Stream, object> EncodeBox3fArray = (s, o) => EncodeArray(s, (Box3f[])o);
                Func<Stream, object> DecodeBox3f = ReadBoxed<Box3f>;
                Func<Stream, object> DecodeBox3fArray = DecodeArray<Box3f>;
                #endif
                s_encoders[Durable.Aardvark.Box3f.Id] = EncodeBox3f;
                s_decoders[Durable.Aardvark.Box3f.Id] = DecodeBox3f;
                s_encoders[Durable.Aardvark.Box3fArray.Id] = EncodeBox3fArray;
                s_decoders[Durable.Aardvark.Box3fArray.Id] = DecodeBox3fArray;

                #endregion

                #region Durable.Aardvark.Box3d

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeBox3d = (s, o) => { var x = (Box3d)o; s.Write(x.Min.X); s.Write(x.Min.Y); s.Write(x.Min.Z); s.Write(x.Max.X); s.Write(x.Max.Y); s.Write(x.Max.Z); };
                Action<BinaryWriter, object> EncodeBox3dArray = (s, o) => EncodeArray(s, (Box3d[])o);
                Func<BinaryReader, object> DecodeBox3d = s => new Box3d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
                Func<BinaryReader, object> DecodeBox3dArray = DecodeArray<Box3d>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeBox3d = Write<Box3d>;
                Action<Stream, object> EncodeBox3dArray = (s, o) => EncodeArray(s, (Box3d[])o);
                Func<Stream, object> DecodeBox3d = ReadBoxed<Box3d>;
                Func<Stream, object> DecodeBox3dArray = DecodeArray<Box3d>;
                #endif
                s_encoders[Durable.Aardvark.Box3d.Id] = EncodeBox3d;
                s_decoders[Durable.Aardvark.Box3d.Id] = DecodeBox3d;
                s_encoders[Durable.Aardvark.Box3dArray.Id] = EncodeBox3dArray;
                s_decoders[Durable.Aardvark.Box3dArray.Id] = DecodeBox3dArray;

                #endregion

                #region Durable.Aardvark.C3us

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC3us = (s, o) => { var x = (C3us)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
                Action<BinaryWriter, object> EncodeC3usArray = (s, o) => EncodeArray(s, (C3us[])o);
                Func<BinaryReader, object> DecodeC3us = s => new C3us(s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16());
                Func<BinaryReader, object> DecodeC3usArray = DecodeArray<C3us>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC3us = Write<C3us>;
                Action<Stream, object> EncodeC3usArray = (s, o) => EncodeArray(s, (C3us[])o);
                Func<Stream, object> DecodeC3us = ReadBoxed<C3us>;
                Func<Stream, object> DecodeC3usArray = DecodeArray<C3us>;
                #endif
                s_encoders[Durable.Aardvark.C3us.Id] = EncodeC3us;
                s_decoders[Durable.Aardvark.C3us.Id] = DecodeC3us;
                s_encoders[Durable.Aardvark.C3usArray.Id] = EncodeC3usArray;
                s_decoders[Durable.Aardvark.C3usArray.Id] = DecodeC3usArray;

                #endregion

                #region Durable.Aardvark.C3ui

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC3ui = (s, o) => { var x = (C3ui)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
                Action<BinaryWriter, object> EncodeC3uiArray = (s, o) => EncodeArray(s, (C3ui[])o);
                Func<BinaryReader, object> DecodeC3ui = s => new C3ui(s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32());
                Func<BinaryReader, object> DecodeC3uiArray = DecodeArray<C3ui>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC3ui = Write<C3ui>;
                Action<Stream, object> EncodeC3uiArray = (s, o) => EncodeArray(s, (C3ui[])o);
                Func<Stream, object> DecodeC3ui = ReadBoxed<C3ui>;
                Func<Stream, object> DecodeC3uiArray = DecodeArray<C3ui>;
                #endif
                s_encoders[Durable.Aardvark.C3ui.Id] = EncodeC3ui;
                s_decoders[Durable.Aardvark.C3ui.Id] = DecodeC3ui;
                s_encoders[Durable.Aardvark.C3uiArray.Id] = EncodeC3uiArray;
                s_decoders[Durable.Aardvark.C3uiArray.Id] = DecodeC3uiArray;

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

                #region Durable.Aardvark.C4us

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC4us = (s, o) => { var x = (C4us)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
                Action<BinaryWriter, object> EncodeC4usArray = (s, o) => EncodeArray(s, (C4us[])o);
                Func<BinaryReader, object> DecodeC4us = s => new C4us(s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16());
                Func<BinaryReader, object> DecodeC4usArray = DecodeArray<C4us>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC4us = Write<C4us>;
                Action<Stream, object> EncodeC4usArray = (s, o) => EncodeArray(s, (C4us[])o);
                Func<Stream, object> DecodeC4us = ReadBoxed<C4us>;
                Func<Stream, object> DecodeC4usArray = DecodeArray<C4us>;
                #endif
                s_encoders[Durable.Aardvark.C4us.Id] = EncodeC4us;
                s_decoders[Durable.Aardvark.C4us.Id] = DecodeC4us;
                s_encoders[Durable.Aardvark.C4usArray.Id] = EncodeC4usArray;
                s_decoders[Durable.Aardvark.C4usArray.Id] = DecodeC4usArray;

                #endregion

                #region Durable.Aardvark.C4ui

                #if NETSTANDARD2_0 || NET472 || NET48
                Action<BinaryWriter, object> EncodeC4ui = (s, o) => { var x = (C4ui)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
                Action<BinaryWriter, object> EncodeC4uiArray = (s, o) => EncodeArray(s, (C4ui[])o);
                Func<BinaryReader, object> DecodeC4ui = s => new C4ui(s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32());
                Func<BinaryReader, object> DecodeC4uiArray = DecodeArray<C4ui>;
                #endif
                #if NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP5_0
                Action<Stream, object> EncodeC4ui = Write<C4ui>;
                Action<Stream, object> EncodeC4uiArray = (s, o) => EncodeArray(s, (C4ui[])o);
                Func<Stream, object> DecodeC4ui = ReadBoxed<C4ui>;
                Func<Stream, object> DecodeC4uiArray = DecodeArray<C4ui>;
                #endif
                s_encoders[Durable.Aardvark.C4ui.Id] = EncodeC4ui;
                s_decoders[Durable.Aardvark.C4ui.Id] = DecodeC4ui;
                s_encoders[Durable.Aardvark.C4uiArray.Id] = EncodeC4uiArray;
                s_decoders[Durable.Aardvark.C4uiArray.Id] = DecodeC4uiArray;

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

