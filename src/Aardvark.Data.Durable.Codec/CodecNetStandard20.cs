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
using System.Runtime.CompilerServices;
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

        private static readonly Action<BinaryWriter, object> EncodeV2i =
            (s, o) => { var x = (V2i)o; s.Write(x.X); s.Write(x.Y); };
        private static readonly Action<BinaryWriter, object> EncodeV2iArray =
            (s, o) => EncodeArray(s, (V2i[])o);
        private static readonly Action<BinaryWriter, object> EncodeV3i =
            (s, o) => { var x = (V3i)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
        private static readonly Action<BinaryWriter, object> EncodeV3iArray =
            (s, o) => EncodeArray(s, (V3i[])o);
        private static readonly Action<BinaryWriter, object> EncodeV4i =
            (s, o) => { var x = (V4i)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
        private static readonly Action<BinaryWriter, object> EncodeV4iArray =
            (s, o) => EncodeArray(s, (V4i[])o);

        private static readonly Action<BinaryWriter, object> EncodeV2l =
            (s, o) => { var x = (V2l)o; s.Write(x.X); s.Write(x.Y); };
        private static readonly Action<BinaryWriter, object> EncodeV2lArray =
            (s, o) => EncodeArray(s, (V2l[])o);
        private static readonly Action<BinaryWriter, object> EncodeV3l =
            (s, o) => { var x = (V3l)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
        private static readonly Action<BinaryWriter, object> EncodeV3lArray =
            (s, o) => EncodeArray(s, (V3l[])o);
        private static readonly Action<BinaryWriter, object> EncodeV4l =
            (s, o) => { var x = (V4l)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
        private static readonly Action<BinaryWriter, object> EncodeV4lArray =
            (s, o) => EncodeArray(s, (V4l[])o);

        private static readonly Action<BinaryWriter, object> EncodeV2f =
            (s, o) => { var x = (V2f)o; s.Write(x.X); s.Write(x.Y); };
        private static readonly Action<BinaryWriter, object> EncodeV2fArray =
            (s, o) => EncodeArray(s, (V2f[])o);
        private static readonly Action<BinaryWriter, object> EncodeV3f =
            (s, o) => { var x = (V3f)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
        private static readonly Action<BinaryWriter, object> EncodeV3fArray =
            (s, o) => EncodeArray(s, (V3f[])o);
        private static readonly Action<BinaryWriter, object> EncodeV4f =
            (s, o) => { var x = (V4f)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
        private static readonly Action<BinaryWriter, object> EncodeV4fArray =
            (s, o) => EncodeArray(s, (V4f[])o);


        private static readonly Action<BinaryWriter, object> EncodeV2d =
            (s, o) => { var x = (V2d)o; s.Write(x.X); s.Write(x.Y); };
        private static readonly Action<BinaryWriter, object> EncodeV2dArray =
            (s, o) => EncodeArray(s, (V2d[])o);
        private static readonly Action<BinaryWriter, object> EncodeV3d =
            (s, o) => { var x = (V3d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); };
        private static readonly Action<BinaryWriter, object> EncodeV3dArray =
            (s, o) => EncodeArray(s, (V3d[])o);
        private static readonly Action<BinaryWriter, object> EncodeV4d =
            (s, o) => { var x = (V4d)o; s.Write(x.X); s.Write(x.Y); s.Write(x.Z); s.Write(x.W); };
        private static readonly Action<BinaryWriter, object> EncodeV4dArray =
            (s, o) => EncodeArray(s, (V4d[])o);

        private static readonly Action<BinaryWriter, object> EncodeRange1b =
            (s, o) => { var x = (Range1b)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1bArray =
            (s, o) => EncodeArray(s, (Range1b[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1d =
            (s, o) => { var x = (Range1d)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1dArray =
            (s, o) => EncodeArray(s, (Range1d[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1f =
            (s, o) => { var x = (Range1f)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1fArray =
            (s, o) => EncodeArray(s, (Range1f[])o); 
        private static readonly Action<BinaryWriter, object> EncodeRange1i =
             (s, o) => { var x = (Range1i)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1iArray =
            (s, o) => EncodeArray(s, (Range1i[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1l =
             (s, o) => { var x = (Range1l)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1lArray =
            (s, o) => EncodeArray(s, (Range1l[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1s =
             (s, o) => { var x = (Range1s)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1sArray =
            (s, o) => EncodeArray(s, (Range1s[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1sb =
             (s, o) => { var x = (Range1sb)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1sbArray =
            (s, o) => EncodeArray(s, (Range1sb[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1ui =
             (s, o) => { var x = (Range1ui)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1uiArray =
            (s, o) => EncodeArray(s, (Range1ui[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1ul =
             (s, o) => { var x = (Range1ul)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1ulArray =
            (s, o) => EncodeArray(s, (Range1ul[])o);
        private static readonly Action<BinaryWriter, object> EncodeRange1us =
             (s, o) => { var x = (Range1us)o; s.Write(x.Min); s.Write(x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeRange1usArray =
            (s, o) => EncodeArray(s, (Range1us[])o);

        private static readonly Action<BinaryWriter, object> EncodeBox2i =
            (s, o) => { var x = (Box2i)o; EncodeV2i(s, x.Min); EncodeV2i(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox2iArray =
            (s, o) => EncodeArray(s, (Box2i[])o);
        private static readonly Action<BinaryWriter, object> EncodeBox3i =
            (s, o) => { var x = (Box3i)o; EncodeV3i(s, x.Min); EncodeV3i(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox3iArray =
            (s, o) => EncodeArray(s, (Box3i[])o);

        private static readonly Action<BinaryWriter, object> EncodeBox2l =
            (s, o) => { var x = (Box2l)o; EncodeV2l(s, x.Min); EncodeV2l(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox2lArray =
            (s, o) => EncodeArray(s, (Box2l[])o);
        private static readonly Action<BinaryWriter, object> EncodeBox3l =
            (s, o) => { var x = (Box3l)o; EncodeV3l(s, x.Min); EncodeV3l(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox3lArray =
            (s, o) => EncodeArray(s, (Box3l[])o);

        private static readonly Action<BinaryWriter, object> EncodeBox2f =
            (s, o) => { var x = (Box2f)o; EncodeV2f(s, x.Min); EncodeV2f(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox2fArray =
            (s, o) => EncodeArray(s, (Box2f[])o);
        private static readonly Action<BinaryWriter, object> EncodeBox3f =
            (s, o) => { var x = (Box3f)o; EncodeV3f(s, x.Min); EncodeV3f(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox3fArray =
            (s, o) => EncodeArray(s, (Box3f[])o);

        private static readonly Action<BinaryWriter, object> EncodeBox2d =
            (s, o) => { var x = (Box2d)o; EncodeV2d(s, x.Min); EncodeV2d(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox2dArray =
            (s, o) => EncodeArray(s, (Box2d[])o);
        private static readonly Action<BinaryWriter, object> EncodeBox3d =
            (s, o) => { var x = (Box3d)o; EncodeV3d(s, x.Min); EncodeV3d(s, x.Max); };
        private static readonly Action<BinaryWriter, object> EncodeBox3dArray =
            (s, o) => EncodeArray(s, (Box3d[])o);


        private static readonly Action<BinaryWriter, object> EncodeM22f =
            (s, o) => { var x = (M22f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M10); s.Write(x.M11); };
        private static readonly Action<BinaryWriter, object> EncodeM22fArray =
            (s, o) => EncodeArray(s, (M22f[])o);
        private static readonly Action<BinaryWriter, object> EncodeM33f =
            (s, o) => { var x = (M33f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); };
        private static readonly Action<BinaryWriter, object> EncodeM33fArray =
            (s, o) => EncodeArray(s, (M33f[])o);
        private static readonly Action<BinaryWriter, object> EncodeM44f =
            (s, o) => { var x = (M44f)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M03); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M13); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); s.Write(x.M23); s.Write(x.M30); s.Write(x.M31); s.Write(x.M32); s.Write(x.M33); };
        private static readonly Action<BinaryWriter, object> EncodeM44fArray =
            (s, o) => EncodeArray(s, (M44f[])o);

        private static readonly Action<BinaryWriter, object> EncodeM22d =
            (s, o) => { var x = (M22d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M10); s.Write(x.M11); };
        private static readonly Action<BinaryWriter, object> EncodeM22dArray =
            (s, o) => EncodeArray(s, (M22d[])o);
        private static readonly Action<BinaryWriter, object> EncodeM33d =
            (s, o) => { var x = (M33d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); };
        private static readonly Action<BinaryWriter, object> EncodeM33dArray =
            (s, o) => EncodeArray(s, (M33d[])o);
        private static readonly Action<BinaryWriter, object> EncodeM44d =
            (s, o) => { var x = (M44d)o; s.Write(x.M00); s.Write(x.M01); s.Write(x.M02); s.Write(x.M03); s.Write(x.M10); s.Write(x.M11); s.Write(x.M12); s.Write(x.M13); s.Write(x.M20); s.Write(x.M21); s.Write(x.M22); s.Write(x.M23); s.Write(x.M30); s.Write(x.M31); s.Write(x.M32); s.Write(x.M33); };
        private static readonly Action<BinaryWriter, object> EncodeM44dArray =
            (s, o) => EncodeArray(s, (M44d[])o);


        private static readonly Action<BinaryWriter, object> EncodeC3b =
            (s, o) => { var x = (C3b)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
        private static readonly Action<BinaryWriter, object> EncodeC3bArray =
            (s, o) => EncodeArray(s, (C3b[])o);
        private static readonly Action<BinaryWriter, object> EncodeC3d =
            (s, o) => { var x = (C3d)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
        private static readonly Action<BinaryWriter, object> EncodeC3dArray =
            (s, o) => EncodeArray(s, (C3d[])o);
        private static readonly Action<BinaryWriter, object> EncodeC3f =
            (s, o) => { var x = (C3f)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
        private static readonly Action<BinaryWriter, object> EncodeC3fArray =
            (s, o) => EncodeArray(s, (C3f[])o);
        private static readonly Action<BinaryWriter, object> EncodeC3ui =
            (s, o) => { var x = (C3ui)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
        private static readonly Action<BinaryWriter, object> EncodeC3uiArray =
            (s, o) => EncodeArray(s, (C3ui[])o);
        private static readonly Action<BinaryWriter, object> EncodeC3us =
            (s, o) => { var x = (C3us)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); };
        private static readonly Action<BinaryWriter, object> EncodeC3usArray =
            (s, o) => EncodeArray(s, (C3us[])o);

        private static readonly Action<BinaryWriter, object> EncodeC4b =
            (s, o) => { var x = (C4b)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4bArray =
            (s, o) => EncodeArray(s, (C4b[])o);
        private static readonly Action<BinaryWriter, object> EncodeC4d =
            (s, o) => { var x = (C4d)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4dArray =
            (s, o) => EncodeArray(s, (C4d[])o);
        private static readonly Action<BinaryWriter, object> EncodeC4f =
            (s, o) => { var x = (C4f)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4fArray =
            (s, o) => EncodeArray(s, (C4f[])o);
        private static readonly Action<BinaryWriter, object> EncodeC4ui =
            (s, o) => { var x = (C4ui)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4uiArray =
            (s, o) => EncodeArray(s, (C4ui[])o);
        private static readonly Action<BinaryWriter, object> EncodeC4us =
            (s, o) => { var x = (C4us)o; s.Write(x.R); s.Write(x.G); s.Write(x.B); s.Write(x.A); };
        private static readonly Action<BinaryWriter, object> EncodeC4usArray =
            (s, o) => EncodeArray(s, (C4us[])o);


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

        private static readonly Func<BinaryReader, object> DecodeV2i = s => new V2i(s.ReadInt32(), s.ReadInt32());
        private static readonly Func<BinaryReader, object> DecodeV2iArray = DecodeArray<V2i>;
        private static readonly Func<BinaryReader, object> DecodeV3i = s => new V3i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
        private static readonly Func<BinaryReader, object> DecodeV3iArray = DecodeArray<V3i>;
        private static readonly Func<BinaryReader, object> DecodeV4i = s => new V4i(s.ReadInt32(), s.ReadInt32(), s.ReadInt32(), s.ReadInt32());
        private static readonly Func<BinaryReader, object> DecodeV4iArray = DecodeArray<V4i>;

        private static readonly Func<BinaryReader, object> DecodeV2l = s => new V2l(s.ReadInt64(), s.ReadInt64());
        private static readonly Func<BinaryReader, object> DecodeV2lArray = DecodeArray<V2l>;
        private static readonly Func<BinaryReader, object> DecodeV3l = s => new V3l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
        private static readonly Func<BinaryReader, object> DecodeV3lArray = DecodeArray<V3l>;
        private static readonly Func<BinaryReader, object> DecodeV4l = s => new V4l(s.ReadInt64(), s.ReadInt64(), s.ReadInt64(), s.ReadInt64());
        private static readonly Func<BinaryReader, object> DecodeV4lArray = DecodeArray<V4l>;

        private static readonly Func<BinaryReader, object> DecodeV2f = s => new V2f(s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeV2fArray = DecodeArray<V2f>;
        private static readonly Func<BinaryReader, object> DecodeV3f = s => new V3f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeV3fArray = DecodeArray<V3f>;
        private static readonly Func<BinaryReader, object> DecodeV4f = s => new V4f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeV4fArray = DecodeArray<V4f>;

        private static readonly Func<BinaryReader, object> DecodeV2d = s => new V2d(s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeV2dArray = DecodeArray<V2d>;
        private static readonly Func<BinaryReader, object> DecodeV3d = s => new V3d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeV3dArray = DecodeArray<V3d>;
        private static readonly Func<BinaryReader, object> DecodeV4d = s => new V4d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeV4dArray = DecodeArray<V4d>;

        private static readonly Func<BinaryReader, object> DecodeRange1b = s => new Range1b(s.ReadByte(), s.ReadByte());
        private static readonly Func<BinaryReader, object> DecodeRange1bArray = DecodeArray<Range1b>;
        private static readonly Func<BinaryReader, object> DecodeRange1d = s => new Range1d(s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeRange1dArray = DecodeArray<Range1d>;
        private static readonly Func<BinaryReader, object> DecodeRange1f = s => new Range1f(s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeRange1fArray = DecodeArray<Range1f>;
        private static readonly Func<BinaryReader, object> DecodeRange1i = s => new Range1i(s.ReadInt32(), s.ReadInt32());
        private static readonly Func<BinaryReader, object> DecodeRange1iArray = DecodeArray<Range1i>;
        private static readonly Func<BinaryReader, object> DecodeRange1l = s => new Range1l(s.ReadInt64(), s.ReadInt64());
        private static readonly Func<BinaryReader, object> DecodeRange1lArray = DecodeArray<Range1l>;
        private static readonly Func<BinaryReader, object> DecodeRange1s = s => new Range1s(s.ReadInt16(), s.ReadInt16());
        private static readonly Func<BinaryReader, object> DecodeRange1sArray = DecodeArray<Range1s>;
        private static readonly Func<BinaryReader, object> DecodeRange1sb = s => new Range1sb(s.ReadSByte(), s.ReadSByte());
        private static readonly Func<BinaryReader, object> DecodeRange1sbArray = DecodeArray<Range1sb>;
        private static readonly Func<BinaryReader, object> DecodeRange1ui = s => new Range1ui(s.ReadUInt32(), s.ReadUInt32());
        private static readonly Func<BinaryReader, object> DecodeRange1uiArray = DecodeArray<Range1ui>;
        private static readonly Func<BinaryReader, object> DecodeRange1ul = s => new Range1ul(s.ReadUInt64(), s.ReadUInt64());
        private static readonly Func<BinaryReader, object> DecodeRange1ulArray = DecodeArray<Range1ul>;
        private static readonly Func<BinaryReader, object> DecodeRange1us = s => new Range1us(s.ReadUInt16(), s.ReadUInt16());
        private static readonly Func<BinaryReader, object> DecodeRange1usArray = DecodeArray<Range1us>;

        private static readonly Func<BinaryReader, object> DecodeBox2i = s => new Box2i((V2i)DecodeV2i(s), (V2i)DecodeV2i(s));
        private static readonly Func<BinaryReader, object> DecodeBox2iArray = DecodeArray<Box2i>;
        private static readonly Func<BinaryReader, object> DecodeBox2l = s => new Box2l((V2l)DecodeV2l(s), (V2l)DecodeV2l(s));
        private static readonly Func<BinaryReader, object> DecodeBox2lArray = DecodeArray<Box2l>;
        private static readonly Func<BinaryReader, object> DecodeBox2f = s => new Box2f((V2f)DecodeV2f(s), (V2f)DecodeV2f(s));
        private static readonly Func<BinaryReader, object> DecodeBox2fArray = DecodeArray<Box2f>;
        private static readonly Func<BinaryReader, object> DecodeBox2d = s => new Box2d((V2d)DecodeV2d(s), (V2d)DecodeV2d(s));
        private static readonly Func<BinaryReader, object> DecodeBox2dArray = DecodeArray<Box2d>;

        private static readonly Func<BinaryReader, object> DecodeBox3i = s => new Box3i((V3i)DecodeV3i(s), (V3i)DecodeV3i(s));
        private static readonly Func<BinaryReader, object> DecodeBox3iArray = DecodeArray<Box3i>;
        private static readonly Func<BinaryReader, object> DecodeBox3l = s => new Box3l((V3l)DecodeV3l(s), (V3l)DecodeV3l(s));
        private static readonly Func<BinaryReader, object> DecodeBox3lArray = DecodeArray<Box3l>;
        private static readonly Func<BinaryReader, object> DecodeBox3f = s => new Box3f((V3f)DecodeV3f(s), (V3f)DecodeV3f(s));
        private static readonly Func<BinaryReader, object> DecodeBox3fArray = DecodeArray<Box3f>;
        private static readonly Func<BinaryReader, object> DecodeBox3d = s => new Box3d((V3d)DecodeV3d(s), (V3d)DecodeV3d(s));
        private static readonly Func<BinaryReader, object> DecodeBox3dArray = DecodeArray<Box3d>;

        private static readonly Func<BinaryReader, object> DecodeM22f = s => new M22f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeM22fArray = DecodeArray<M22f>;
        private static readonly Func<BinaryReader, object> DecodeM33f = s => new M33f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeM33fArray = DecodeArray<M33f>;
        private static readonly Func<BinaryReader, object> DecodeM44f = s => new M44f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeM44fArray = DecodeArray<M44f>;

        private static readonly Func<BinaryReader, object> DecodeM22d = s => new M22d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeM22dArray = DecodeArray<M22d>;
        private static readonly Func<BinaryReader, object> DecodeM33d = s => new M33d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeM33dArray = DecodeArray<M33d>;
        private static readonly Func<BinaryReader, object> DecodeM44d = s => new M44d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeM44dArray = DecodeArray<M44d>;

        private static readonly Func<BinaryReader, object> DecodeC3b = s => new C3b(s.ReadByte(), s.ReadByte(), s.ReadByte());
        private static readonly Func<BinaryReader, object> DecodeC3bArray = DecodeArray<C3b>;
        private static readonly Func<BinaryReader, object> DecodeC3d = s => new C3d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeC3dArray = DecodeArray<C3d>;
        private static readonly Func<BinaryReader, object> DecodeC3f = s => new C3f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeC3fArray = DecodeArray<C3f>;
        private static readonly Func<BinaryReader, object> DecodeC3ui = s => new C3ui(s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32());
        private static readonly Func<BinaryReader, object> DecodeC3uiArray = DecodeArray<C3ui>;
        private static readonly Func<BinaryReader, object> DecodeC3us = s => new C3us(s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16());
        private static readonly Func<BinaryReader, object> DecodeC3usArray = DecodeArray<C3us>;

        private static readonly Func<BinaryReader, object> DecodeC4b = s => new C4b(s.ReadByte(), s.ReadByte(), s.ReadByte(), s.ReadByte());
        private static readonly Func<BinaryReader, object> DecodeC4bArray = DecodeArray<C4b>;
        private static readonly Func<BinaryReader, object> DecodeC4d = s => new C4d(s.ReadDouble(), s.ReadDouble(), s.ReadDouble(), s.ReadDouble());
        private static readonly Func<BinaryReader, object> DecodeC4dArray = DecodeArray<C4d>;
        private static readonly Func<BinaryReader, object> DecodeC4f = s => new C4f(s.ReadSingle(), s.ReadSingle(), s.ReadSingle(), s.ReadSingle());
        private static readonly Func<BinaryReader, object> DecodeC4fArray = DecodeArray<C4f>;
        private static readonly Func<BinaryReader, object> DecodeC4ui = s => new C4ui(s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32(), s.ReadUInt32());
        private static readonly Func<BinaryReader, object> DecodeC4uiArray = DecodeArray<C4ui>;
        private static readonly Func<BinaryReader, object> DecodeC4us = s => new C4us(s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16(), s.ReadUInt16());
        private static readonly Func<BinaryReader, object> DecodeC4usArray = DecodeArray<C4us>;

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