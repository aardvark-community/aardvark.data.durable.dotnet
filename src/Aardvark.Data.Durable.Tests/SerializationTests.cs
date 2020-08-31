using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class SerializationTests
    {
        private void Primitive<T>(Durable.Def def, T value, int size, Func<T, T, bool> eq)
        {
            var buffer = DurableCodec.Serialize(def, value);
            if (size > 0) Assert.True(buffer.Length == 16 + size); // guid (16 bytes) + value (size bytes)
            var (d, o) = DurableCodec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T || def == Durable.Primitives.Unit);
            Assert.True(eq(value, (T)o));
        }
        private void PrimitiveArray<T>(Durable.Def def, T[] value, int size, Func<T, T, bool> eq)
        {
            var buffer = DurableCodec.Serialize(def, value);
            if (size > 0) Assert.True(buffer.Length == 16 + size); // guid (16 bytes) + value (size bytes)

            var (d, o) = DurableCodec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T[]);
            var xs = (T[])o;
            for (var i = 0; i < value.Length; i++)
                Assert.True(eq(value[i], xs[i]));
        }
        private void Primitive<T>(Durable.Def def, T value, int size) where T : IEquatable<T>
            => Primitive(def, value, size, (a, b) => a.Equals(b));
        private void PrimitiveArray<T>(Durable.Def def, T[] value, int size) where T : IEquatable<T>
            => PrimitiveArray(def, value, size, (a, b) => a.Equals(b));

        [Fact]
        public void Mmmh()
        {
            var map = new Dictionary<Durable.Def, object>() {
                { Durable.Aardvark.V2i, new V2i(17, 42) }
            };

            var buffer = DurableCodec.Serialize(Durable.Primitives.DurableMap, map);
            var (d, o) = DurableCodec.Deserialize(buffer);

            Assert.True(d == Durable.Primitives.DurableMap);

            var map2 = o as ImmutableDictionary<Durable.Def, object>;
            Assert.True(map2.ContainsKey(Durable.Aardvark.V2i));
            Assert.True((V2i)map2[Durable.Aardvark.V2i] == new V2i(17, 42));
        }

        [Fact] public void Primitive_Unit() => Primitive<object>(Durable.Primitives.Unit, default, 0, (a, b) => a == b);
        [Fact] public void Primitive_StringUTF8() => Primitive(Durable.Primitives.StringUTF8, "foo bar woohoo", 18);

        [Fact] public void Primitive_Guid() => Primitive(Durable.Primitives.GuidDef, Guid.NewGuid(), 16);
        [Fact] public void Primitive_Int8() => Primitive(Durable.Primitives.Int8, (sbyte)42, 1);
        [Fact] public void Primitive_UInt8() => Primitive(Durable.Primitives.UInt8, (byte)42, 1);
        [Fact] public void Primitive_Int16() => Primitive(Durable.Primitives.Int16, (short)42, 2);
        [Fact] public void Primitive_UInt16() => Primitive(Durable.Primitives.UInt16, (ushort)42, 2);
        [Fact] public void Primitive_Int32() => Primitive(Durable.Primitives.Int32, (int)42, 4);
        [Fact] public void Primitive_UInt32() => Primitive(Durable.Primitives.UInt32, (uint)42, 4);
        [Fact] public void Primitive_Int64() => Primitive(Durable.Primitives.Int64, (long)42, 8);
        [Fact] public void Primitive_UInt64() => Primitive(Durable.Primitives.UInt64, (ulong)42, 8);
        [Fact] public void Primitive_Float32() => Primitive(Durable.Primitives.Float32, 3.1415926f, 4);
        [Fact] public void Primitive_Float64() => Primitive(Durable.Primitives.Float64, 3.1415926, 8);

        [Fact] public void Aardvark_Cell() => Primitive(Durable.Aardvark.Cell, new Cell(2,11,-5,-6), 28);
        [Fact] public void Aardvark_CellPadded32() => Primitive(Durable.Aardvark.CellPadded32, new Cell(2, 11, -5, -6), 32);
        [Fact] public void Aardvark_Cell2d() => Primitive(Durable.Aardvark.Cell2d, new Cell2d(2, 11, -6), 20);
        [Fact] public void Aardvark_Cell2dPadded24() => Primitive(Durable.Aardvark.Cell2dPadded24, new Cell2d(2, 11, -6), 24);

        [Fact] public void Aardvark_V2i() => Primitive(Durable.Aardvark.V2i, new V2i(4, -5), 8);
        [Fact] public void Aardvark_V3i() => Primitive(Durable.Aardvark.V3i, new V3i(2, 6, -4), 12);
        [Fact] public void Aardvark_V4i() => Primitive(Durable.Aardvark.V4i, new V4i(5, -3, 8, 34), 16);
        [Fact] public void Aardvark_V2l() => Primitive(Durable.Aardvark.V2l, new V2l(4, -5), 16);
        [Fact] public void Aardvark_V3l() => Primitive(Durable.Aardvark.V3l, new V3l(2, 6, -4), 24);
        [Fact] public void Aardvark_V4l() => Primitive(Durable.Aardvark.V4l, new V4l(5, -3, 8, 34), 32);
        [Fact] public void Aardvark_V2f() => Primitive(Durable.Aardvark.V2f, new V2f(1.2f, 3.4f), 8);
        [Fact] public void Aardvark_V3f() => Primitive(Durable.Aardvark.V3f, new V3f(1.2f, 3.4f, 5.6f), 12);
        [Fact] public void Aardvark_V4f() => Primitive(Durable.Aardvark.V4f, new V4f(1.2f, 3.4f, 5.6f, 7.8f), 16);
        [Fact] public void Aardvark_V2d() => Primitive(Durable.Aardvark.V2d, new V2d(1.2, 3.4), 16);
        [Fact] public void Aardvark_V3d() => Primitive(Durable.Aardvark.V3d, new V3d(1.2, 3.4, 5.6), 24);
        [Fact] public void Aardvark_V4d() => Primitive(Durable.Aardvark.V4d, new V4d(1.2, 3.4, 5.6, 7.8), 32);


        [Fact] public void Aardvark_Range1b()  => Primitive(Durable.Aardvark.Range1b,  new Range1b(byte.MinValue, byte.MaxValue), 2);
        [Fact] public void Aardvark_Range1d()  => Primitive(Durable.Aardvark.Range1d,  new Range1d(double.MinValue, double.MaxValue), 16);
        [Fact] public void Aardvark_Range1f()  => Primitive(Durable.Aardvark.Range1f,  new Range1f(float.MinValue, float.MaxValue), 8);
        [Fact] public void Aardvark_Range1i()  => Primitive(Durable.Aardvark.Range1i,  new Range1i(int.MinValue, int.MaxValue), 8);
        [Fact] public void Aardvark_Range1l()  => Primitive(Durable.Aardvark.Range1l,  new Range1l(long.MinValue, long.MaxValue), 16);
        [Fact] public void Aardvark_Range1s()  => Primitive(Durable.Aardvark.Range1s,  new Range1s(short.MinValue, short.MaxValue), 4);
        [Fact] public void Aardvark_Range1sb() => Primitive(Durable.Aardvark.Range1sb, new Range1sb(sbyte.MinValue, sbyte.MaxValue), 2);
        [Fact] public void Aardvark_Rangeui()  => Primitive(Durable.Aardvark.Range1ui, new Range1ui(uint.MinValue, uint.MaxValue), 8);
        [Fact] public void Aardvark_Rangeul()  => Primitive(Durable.Aardvark.Range1ul, new Range1ul(ulong.MinValue, ulong.MaxValue), 16);
        [Fact] public void Aardvark_Rangeus()  => Primitive(Durable.Aardvark.Range1us, new Range1us(ushort.MinValue, ushort.MaxValue), 4);

        [Fact] public void Aardvark_Box2i() => Primitive(Durable.Aardvark.Box2i, new Box2i(new V2i(1, 3), new V2i(5, 7)), 16);
        [Fact] public void Aardvark_Box2l() => Primitive(Durable.Aardvark.Box2l, new Box2l(new V2l(1, 3), new V2l(5, 7)), 32);
        [Fact] public void Aardvark_Box2f() => Primitive(Durable.Aardvark.Box2f, new Box2f(new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f)), 16);
        [Fact] public void Aardvark_Box2d() => Primitive(Durable.Aardvark.Box2d, new Box2d(new V2d(1.2, 3.4), new V2d(5.6, 7.8)), 32);
        [Fact] public void Aardvark_Box3i() => Primitive(Durable.Aardvark.Box3i, new Box3i(new V3i(1, 3, 5), new V3i(6, 8, 9)), 24);
        [Fact] public void Aardvark_Box3l() => Primitive(Durable.Aardvark.Box3l, new Box3l(new V3l(1, 3, 5), new V3l(6, 8, 9)), 48);
        [Fact] public void Aardvark_Box3f() => Primitive(Durable.Aardvark.Box3f, new Box3f(new V3f(1.2f, 3.4f, 5.6f), new V3f(6.7f, 8.9f, 9.0f)), 24);
        [Fact] public void Aardvark_Box3d() => Primitive(Durable.Aardvark.Box3d, new Box3d(new V3d(1.2, 3.4, 5.6), new V3d(6.7, 8.9, 9.0)), 48);


        [Fact] public void Aardvark_C3b() => Primitive(Durable.Aardvark.C3b, new C3b(17, 42, 253), 3);
        [Fact] public void Aardvark_C3d() => Primitive(Durable.Aardvark.C3d, new C3d(0.1, 0.2, 0.3), 24);
        [Fact] public void Aardvark_C3f() => Primitive(Durable.Aardvark.C3f, new C3f(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_C3ui() => Primitive(Durable.Aardvark.C3ui, new C3ui(5, 15, 155), 12);
        [Fact] public void Aardvark_C3us() => Primitive(Durable.Aardvark.C3us, new C3us(5, 15, 155), 6);

        [Fact] public void Aardvark_C4b() => Primitive(Durable.Aardvark.C4b, new C4b(17, 42, 253, 128), 4);
        [Fact] public void Aardvark_C4d() => Primitive(Durable.Aardvark.C4d, new C4d(0.3, 0.4, 0.5, 0.6), 32);
        [Fact] public void Aardvark_C4f() => Primitive(Durable.Aardvark.C4f, new C4f(0.3f, 0.4f, 0.5f, 0.6f), 16);
        [Fact] public void Aardvark_C4ui() => Primitive(Durable.Aardvark.C4ui, new C4ui(5, 15, 155, 127), 16);
        [Fact] public void Aardvark_C4us() => Primitive(Durable.Aardvark.C4us, new C4us(5, 15, 155, 127), 8);

        [Fact] public void Aardvark_CieLabf() => Primitive(Durable.Aardvark.CieLabf, new CieLabf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_CIeLuvf() => Primitive(Durable.Aardvark.CIeLuvf, new CIeLuvf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_CieXYZf() => Primitive(Durable.Aardvark.CieXYZf, new CieXYZf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_CieYxyf() => Primitive(Durable.Aardvark.CieYxyf, new CieYxyf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_CMYKf() => Primitive(Durable.Aardvark.CMYKf, new CMYKf(0.1f, 0.2f, 0.3f, 0.4f), 16);
        [Fact] public void Aardvark_HSLf() => Primitive(Durable.Aardvark.HSLf, new HSLf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_HSVf() => Primitive(Durable.Aardvark.HSVf, new HSVf(0.1f, 0.2f, 0.3f), 12);
        [Fact] public void Aardvark_Yuvf() => Primitive(Durable.Aardvark.Yuvf, new Yuvf(0.1f, 0.2f, 0.3f), 12);



        [Fact]
        public void Aardvark_Capsule3d() => Primitive(Durable.Aardvark.Capsule3d,
            new Capsule3d(p0: new V3d(3.14, 2.71, 1.23), p1: new V3d(13.14, 22.71, 31.23), radius: 0.99),
            (3 + 3 + 1) * 8, (a, b) => a.P0 == b.P0 && a.P1 == b.P1 && a.Radius == b.Radius
            );
        [Fact] public void Aardvark_Circle2d() => Primitive(Durable.Aardvark.Circle2d,
            new Circle2d(center: new V2d(3.14, 2.71), radius: 0.99),
            24, (a, b) => a == b
            );
        [Fact] public void Aardvark_Circle3d() => Primitive(Durable.Aardvark.Circle3d, 
            new Circle3d(center: new V3d(3.14, 2.71, 0.123), normal: new V3d(2,3,4).Normalized, radius: 0.99),
            (3 + 3 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_ObliqueCone3d() => Primitive(Durable.Aardvark.ObliqueCone3d,
            new ObliqueCone3d(
                o: new V3d(101.2, 202.3, 303.4),
                c: new Circle3d(center: new V3d(3.14, 2.71, 0.123), normal: new V3d(2, 3, 4).Normalized, radius: 0.99)
                ),
            3 * 8 + (3 + 3 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Cone3d() => Primitive(Durable.Aardvark.Cone3d,
            new Cone3d(
                origin: new V3d(101.2, 202.3, 303.4),
                dir: new V3d(2, 3, 4).Normalized,
                angle: 0.12345
                ),
            (3 + 3 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Cylinder3d() => Primitive(Durable.Aardvark.Cylinder3d,
           new Cylinder3d(
               p0: new V3d(3.14, 2.71, 1.23), p1: new V3d(13.14, 22.71, 31.23),
               radius: 0.12345, distanceScale: 0.555
               ),
           (3 + 3 + 1 + 1) * 8, (a, b) => a == b
           );
        [Fact]
        public void Aardvark_Sphere3d() => Primitive(Durable.Aardvark.Sphere3d,
            new Sphere3d(center: new V3d(3.14, 2.71, 0.123), radius: 0.99),
            (3 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Line2d() => Primitive(Durable.Aardvark.Line2d,
            new Line2d(p0: new V2d(3.14, 2.71), p1: new V2d(1003.14, 1002.71)),
            2 * 2 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Line3d() => Primitive(Durable.Aardvark.Line3d,
            new Line3d(p0: new V3d(3.14, 2.71, 123.456), p1: new V3d(1003.14, 1002.71, 789.012)),
            2 * 3 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Triangle2d() => Primitive(Durable.Aardvark.Triangle2d,
            new Triangle2d(p0: new V2d(3.14, 2.71), p1: new V2d(1003.14, 1002.71), p2: new V2d(-1003.14, -1002.71)),
            3 * 2 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Triangle3d() => Primitive(Durable.Aardvark.Triangle3d,
           new Triangle3d(p0: new V3d(3.14, 2.71, 123.456), p1: new V3d(1003.14, 1002.71, 789.012), p2: new V3d(-1003.14, -1002.71, -789.012)),
           3 * 3 * 8, (a, b) => a == b
           );
        [Fact]
        public void Aardvark_Quad2d() => Primitive(Durable.Aardvark.Quad2d,
            new Quad2d(p0: new V2d(3.14, 2.71), p1: new V2d(1003.14, 1002.71), p2: new V2d(-1003.14, -1002.71), p3: new V2d(345, -435)),
            4 * 2 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Quad3d() => Primitive(Durable.Aardvark.Quad3d,
            new Quad3d(p0: new V3d(3.14, 2.71, 123.456), p1: new V3d(1003.14, 1002.71, 789.012), p2: new V3d(-1003.14, -1002.71, -789.012), p3: new V3d(345, -435, -0.123)),
            4 * 3 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Plane2d() => Primitive(Durable.Aardvark.Plane2d,
            new Plane2d(new V2d(3.14, 2.71), distance: 1003.14),
            (2 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Plane3d() => Primitive(Durable.Aardvark.Plane3d,
            new Plane3d(new V3d(3.14, 2.71, -123), distance: 1003.14),
            (3 + 1) * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Ray2d() => Primitive(Durable.Aardvark.Ray2d,
            new Ray2d(new V2d(3.14, 2.71), new V2d(1003.14, 1002.71)),
            2 * 2 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Ray3d() => Primitive(Durable.Aardvark.Ray3d,
            new Ray3d(new V3d(3.14, 2.71, -123), new V3d(1003.14, 1002.71, 456)),
            2 * 3 * 8, (a, b) => a == b
            );
        [Fact]
        public void Aardvark_Torus3d() => Primitive(Durable.Aardvark.Torus3d,
            new Torus3d(position: new V3d(3.14, 2.71, -123), direction: new V3d(1003.14, 1002.71, 456), majorRadius: 500.005, minorRadius: 200.002),
            (3 + 3 + 1 + 1) * 8, (a, b) => a == b
            );

        [Fact]
        public void Primitive_M22f() => Primitive(Durable.Aardvark.M22f, new M22f(1, 2, 3, 4), 16);
        [Fact]
        public void Primitive_M33f() => Primitive(Durable.Aardvark.M33f, new M33f(1, 2, 3, 4, 5, 6, 7, 8, 9), 36);
        [Fact]
        public void Primitive_M44f() => Primitive(Durable.Aardvark.M44f, new M44f(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16), 64);
        [Fact]
        public void Primitive_M22d() => Primitive(Durable.Aardvark.M22d, new M22d(1, 2, 3, 4), 32);
        [Fact]
        public void Primitive_M33d() => Primitive(Durable.Aardvark.M33d, new M33d(1, 2, 3, 4, 5, 6, 7, 8, 9), 72);
        [Fact]
        public void Primitive_M44d() => Primitive(Durable.Aardvark.M44d, new M44d(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), 128);


        [Fact] public void Primitive_StringUTF8Array() => PrimitiveArray(Durable.Primitives.StringUTF8Array, 
            new[] { "foo", "bar", "woohoo" }, 4 + 4+3 + 4+3 + 4+6
            );

        [Fact] public void Primitive_GuidArray() => PrimitiveArray(Durable.Primitives.GuidArray, 
                new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }, 4 + 3 * 16
                );

        [Fact]
        public void Primitive_Int8Array() => PrimitiveArray(Durable.Primitives.Int8Array,
                new[] { (sbyte)42, (sbyte)43, (sbyte)44 }, 4 + 3 * 1
                );
        [Fact]
        public void Primitive_UInt8Array() => PrimitiveArray(Durable.Primitives.UInt8Array,
                new[] { (byte)42, (byte)43, (byte)44 }, 4 + 3 * 1
                );
        [Fact]
        public void Primitive_Int16Array() => PrimitiveArray(Durable.Primitives.Int16Array,
                new[] { (short)42, (short)43, (short)44 }, 4 + 3 * 2
                );
        [Fact]
        public void Primitive_UInt16Array() => PrimitiveArray(Durable.Primitives.UInt16Array,
                new[] { (ushort)42, (ushort)43, (ushort)44 }, 4 + 3 * 2
                );
        [Fact]
        public void Primitive_Int32Array() => PrimitiveArray(Durable.Primitives.Int32Array,
                new[] { (int)42, (int)43, (int)44 }, 4 + 3 * 4
                );
        [Fact]
        public void Primitive_UInt32Array() => PrimitiveArray(Durable.Primitives.UInt32Array,
                new[] { (uint)42, (uint)43, (uint)44 }, 4 + 3 * 4
                );
        [Fact]
        public void Primitive_Int64Array() => PrimitiveArray(Durable.Primitives.Int64Array,
                new[] { (long)42, (long)43, (long)44 }, 4 + 3 * 8
                );
        [Fact]
        public void Primitive_UInt64Array() => PrimitiveArray(Durable.Primitives.UInt64Array,
                new[] { (ulong)42, (ulong)43, (ulong)44 }, 4 + 3 * 8
                );
        [Fact]
        public void Primitive_Float32Array() => PrimitiveArray(Durable.Primitives.Float32Array,
                new[] { (float)42, (float)43, (float)44 }, 4 + 3 * 4
                );
        [Fact]
        public void Primitive_Float64Array() => PrimitiveArray(Durable.Primitives.Float64Array,
                new[] { (double)42, (double)43, (double)44 }, 4 + 3 * 8
                );

        [Fact]
        public void Primitive_CellArray() => PrimitiveArray(Durable.Aardvark.CellArray,
                new[] { new Cell(1,2,3,4), new Cell(-5, 42, -17, -10) }, 4 + 2 * Marshal.SizeOf<Cell>()
                );
        [Fact]
        public void Primitive_CellPadded32Array() => PrimitiveArray(Durable.Aardvark.CellPadded32Array,
                new[] { new Cell(1, 2, 3, 4), new Cell(-5, 42, -17, -10) }, 4 + 2 * Marshal.SizeOf<Cell>()
                );
        [Fact]
        public void Primitive_Cell2dArray() => PrimitiveArray(Durable.Aardvark.Cell2dArray,
                new[] { new Cell2d(1, 2, 4), new Cell2d(-5, 42, -10) }, 4 + 2 * Marshal.SizeOf<Cell2d>()
                );
        [Fact]
        public void Primitive_Cell2dPadded24Array() => PrimitiveArray(Durable.Aardvark.Cell2dPadded24Array,
                new[] { new Cell2d(1, 2, 4), new Cell2d(-5, 42, -10) }, 4 + 2 * Marshal.SizeOf<Cell2d>()
                );

        [Fact]
        public void Primitive_V2iArray() => PrimitiveArray(Durable.Aardvark.V2iArray,
                new[] { new V2i(1, 3), new V2i(5, 7) }, 4 + 2 * 2 * 4
                );
        [Fact]
        public void Primitive_V3iArray() => PrimitiveArray(Durable.Aardvark.V3iArray,
                new[] { new V3i(1, 3, -2), new V3i(5, 7, -6) }, 4 + 2 * 3 * 4
                );
        [Fact]
        public void Primitive_V4iArray() => PrimitiveArray(Durable.Aardvark.V4iArray,
                new[] { new V4i(1, 3, -2, 0), new V4i(5, 7, -6, -0) }, 4 + 2 * 4 * 4
                );
        [Fact]
        public void Primitive_V2lArray() => PrimitiveArray(Durable.Aardvark.V2lArray,
                new[] { new V2l(1, 3), new V2l(5, 7) }, 4 + 2 * 2 * 8
                );
        [Fact]
        public void Primitive_V3lArray() => PrimitiveArray(Durable.Aardvark.V3lArray,
                new[] { new V3l(1, 3, -2), new V3l(5, 7, -6) }, 4 + 2 * 3 * 8
                );
        [Fact]
        public void Primitive_V4lArray() => PrimitiveArray(Durable.Aardvark.V4lArray,
                new[] { new V4l(1, 3, -2, 0), new V4l(5, 7, -6, -0) }, 4 + 2 * 4 * 8
                );
        [Fact]
        public void Primitive_V2fArray() => PrimitiveArray(Durable.Aardvark.V2fArray,
                new[] { new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f) }, 4 + 2 * 2 * 4
                );
        [Fact]
        public void Primitive_V3fArray() => PrimitiveArray(Durable.Aardvark.V3fArray,
                new[] { new V3f(1.2f, 3.4f, -2.3f), new V3f(5.6f, 7.8f, -6.7f) }, 4 + 2 * 3 * 4
                );
        [Fact]
        public void Primitive_V4fArray() => PrimitiveArray(Durable.Aardvark.V4fArray,
                new[] { new V4f(1.2f, 3.4f, -2.3f, 0.1f), new V4f(5.6f, 7.8f, -6.7f, -0.1f) }, 4 + 2 * 4 * 4
                );
        [Fact]
        public void Primitive_V2dArray() => PrimitiveArray(Durable.Aardvark.V2dArray,
                new[] { new V2d(1.2, 3.4), new V2d(5.6, 7.8) }, 4 + 2 * 2 * 8
                );
        [Fact]
        public void Primitive_V3dArray() => PrimitiveArray(Durable.Aardvark.V3dArray,
                new[] { new V3d(1.2, 3.4, -2.3), new V3d(5.6, 7.8, -6.7) }, 4 + 2 * 3 * 8
                );
        [Fact]
        public void Primitive_V4dArray() => PrimitiveArray(Durable.Aardvark.V4dArray,
                new[] { new V4d(1.2, 3.4, -2.3, 0.1), new V4d(5.6, 7.8, -6.7, -0.1) }, 4 + 2 * 4 * 8
                );


        [Fact]
        public void Primitive_Range1bArray() => PrimitiveArray(Durable.Aardvark.Range1bArray,
                new[] { new Range1b(7, 42), new Range1b(17, 101) }, 4 + 2 * 2
                );
        [Fact]
        public void Primitive_Range1dArray() => PrimitiveArray(Durable.Aardvark.Range1dArray,
                new[] { new Range1d(7, 42), new Range1d(17, 101) }, 4 + 2 * 16
                );
        [Fact]
        public void Primitive_Range1fArray() => PrimitiveArray(Durable.Aardvark.Range1fArray,
                new[] { new Range1f(7, 42), new Range1f(17, 101) }, 4 + 2 * 8
                );
        [Fact]
        public void Primitive_Range1iArray() => PrimitiveArray(Durable.Aardvark.Range1iArray,
                new[] { new Range1i(7, 42), new Range1i(17, 101) }, 4 + 2 * 8
                );
        [Fact]
        public void Primitive_Range1lArray() => PrimitiveArray(Durable.Aardvark.Range1lArray,
                new[] { new Range1l(7, 42), new Range1l(17, 101) }, 4 + 2 * 16
                );
        [Fact]
        public void Primitive_Range1sArray() => PrimitiveArray(Durable.Aardvark.Range1sArray,
                new[] { new Range1s(7, 42), new Range1s(17, 101) }, 4 + 2 * 4
                );
        [Fact]
        public void Primitive_Range1sbArray() => PrimitiveArray(Durable.Aardvark.Range1sbArray,
                new[] { new Range1sb(7, 42), new Range1sb(17, 101) }, 4 + 2 * 2
                );
        [Fact]
        public void Primitive_Range1uiArray() => PrimitiveArray(Durable.Aardvark.Range1uiArray,
                new[] { new Range1ui(7, 42), new Range1ui(17, 101) }, 4 + 2 * 8
                );
        [Fact]
        public void Primitive_Range1ulArray() => PrimitiveArray(Durable.Aardvark.Range1ulArray,
                new[] { new Range1ul(7, 42), new Range1ul(17, 101) }, 4 + 2 * 16
                );
        [Fact]
        public void Primitive_Range1usArray() => PrimitiveArray(Durable.Aardvark.Range1usArray,
                new[] { new Range1us(7, 42), new Range1us(17, 101) }, 4 + 2 * 4
                );



        [Fact]
        public void Primitive_Box2iArray() => PrimitiveArray(Durable.Aardvark.Box2iArray,
            new[] {
                new Box2i(new V2i(1, 3), new V2i(5, 7)),
                new Box2i(new V2i(2, 4), new V2i(6, 8))
                },
            4 + 2 * 2 * 2 * 4
            );
        [Fact]
        public void Primitive_Box3iArray() => PrimitiveArray(Durable.Aardvark.Box3iArray,
            new[] {
                new Box3i(new V3i(1, 3, 5), new V3i(5, 7, -213)),
                new Box3i(new V3i(2, 4, 6), new V3i(6, 8, 234))
                },
            4 + 2 * 2 * 3 * 4
            );
        [Fact]
        public void Primitive_Box2lArray() => PrimitiveArray(Durable.Aardvark.Box2lArray,
            new[] {
                new Box2l(new V2l(1, 3), new V2l(5, 7)),
                new Box2l(new V2l(2, 4), new V2l(6, 8))
                },
            4 + 2 * 2 * 2 * 8
            );
        [Fact]
        public void Primitive_Box3lArray() => PrimitiveArray(Durable.Aardvark.Box3lArray,
            new[] {
                new Box3l(new V3l(1, 3, 5), new V3l(5, 7, -213)),
                new Box3l(new V3l(2, 4, 6), new V3l(6, 8, 234))
                },
            4 + 2 * 2 * 3 * 8
            );
        [Fact]
        public void Primitive_Box2fArray() => PrimitiveArray(Durable.Aardvark.Box2fArray,
            new[] {
                new Box2f(new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f)),
                new Box2f(new V2f(2.3f, 4.5f), new V2f(6.7f, 8.9f))
                },
            4 + 2 * 2 * 2 * 4
            );
        [Fact]
        public void Primitive_Box3fArray() => PrimitiveArray(Durable.Aardvark.Box3fArray,
            new[] {
                new Box3f(new V3f(1.2f, 3.4f, 5.6f), new V3f(5.6f, 7.8f, -213.5f)),
                new Box3f(new V3f(2.3f, 4.5f, 6.7f), new V3f(6.7f, 8.9f, 234.67f))
                },
            4 + 2 * 2 * 3 * 4
            );
        [Fact]
        public void Primitive_Box2dArray() => PrimitiveArray(Durable.Aardvark.Box2dArray,
            new[] {
                new Box2d(new V2d(1.2, 3.4), new V2d(5.6f, 7.8)),
                new Box2d(new V2d(2.3, 4.5), new V2d(6.7f, 8.9))
                },
            4 + 2 * 2 * 2 * 8
            );
        [Fact]
        public void Primitive_Box3dArray() => PrimitiveArray(Durable.Aardvark.Box3dArray,
            new[] {
                new Box3d(new V3d(1.2, 3.4, 5.6), new V3d(5.6, 7.8, 2354.567)),
                new Box3d(new V3d(2.3, 4.5, 6.7), new V3d(6.7, 8.9, -456.345))
                },
            4 + 2 * 2 * 3 * 8
            );


        [Fact]
        public void Primitive_C3bArray() => PrimitiveArray(Durable.Aardvark.C3bArray,
               new[] { C3b.Red, C3b.Magenta, C3b.DarkGreen }, 4 + 3 * 3
               );
        [Fact]
        public void Primitive_C3dArray() => PrimitiveArray(Durable.Aardvark.C3dArray,
                new[] { C3d.Red, C3d.Magenta, C3d.DarkGreen }, 4 + 3 * 24
                );
        [Fact]
        public void Primitive_C3fArray() => PrimitiveArray(Durable.Aardvark.C3fArray,
                new[] { C3f.Red, C3f.Magenta, C3f.DarkGreen }, 4 + 3 * 12
                );
        [Fact]
        public void Primitive_C3uiArray() => PrimitiveArray(Durable.Aardvark.C3uiArray,
                new[] { C3ui.Red, C3ui.Magenta, C3ui.DarkGreen }, 4 + 3 * 12
                );
        [Fact]
        public void Primitive_C3usArray() => PrimitiveArray(Durable.Aardvark.C3usArray,
                new[] { C3us.Red, C3us.Magenta, C3us.DarkGreen }, 4 + 3 * 6
                );

        [Fact]
        public void Primitive_C4bArray() => PrimitiveArray(Durable.Aardvark.C4bArray,
                new[] { C4b.Red, C4b.Magenta, C4b.DarkGreen }, 4 + 3 * 4
                );
        [Fact]
        public void Primitive_C4dArray() => PrimitiveArray(Durable.Aardvark.C4dArray,
                new[] { C4d.Red, C4d.Magenta, C4d.DarkGreen }, 4 + 3 * 32
                );
        [Fact]
        public void Primitive_C4fArray() => PrimitiveArray(Durable.Aardvark.C4fArray,
                new[] { C4f.Red, C4f.Magenta, C4f.DarkGreen }, 4 + 3 * 16
                );
        [Fact]
        public void Primitive_C4uiArray() => PrimitiveArray(Durable.Aardvark.C4uiArray,
                new[] { C4ui.Red, C4ui.Magenta, C4ui.DarkGreen }, 4 + 3 * 16
                );
        [Fact]
        public void Primitive_C4usArray() => PrimitiveArray(Durable.Aardvark.C4usArray,
                new[] { C4us.Red, C4us.Magenta, C4us.DarkGreen }, 4 + 3 * 8
                );

        [Fact]
        public void Primitive_CieLabfArray() => PrimitiveArray(Durable.Aardvark.CieLabfArray,
               new[] { new CieLabf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_CieLuvfArray() => PrimitiveArray(Durable.Aardvark.CIeLuvfArray,
               new[] { new CIeLuvf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_CieXYZfArray() => PrimitiveArray(Durable.Aardvark.CieXYZfArray,
               new[] { new CieXYZf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_CieYxyfArray() => PrimitiveArray(Durable.Aardvark.CieYxyfArray,
               new[] { new CieYxyf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_CMYKfArray() => PrimitiveArray(Durable.Aardvark.CMYKfArray,
               new[] { new CMYKf(0.1f, 0.2f, 0.3f, 0.4f) }, 4 + 1 * 16
               );
        [Fact]
        public void Primitive_HSLfArray() => PrimitiveArray(Durable.Aardvark.HSLfArray,
               new[] { new HSLf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_HSVfArray() => PrimitiveArray(Durable.Aardvark.HSVfArray,
               new[] { new HSVf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );
        [Fact]
        public void Primitive_YuvfArray() => PrimitiveArray(Durable.Aardvark.YuvfArray,
               new[] { new Yuvf(0.1f, 0.2f, 0.3f) }, 4 + 1 * 12
               );


        [Fact]
        public void Primitive_M22fArray() => PrimitiveArray(Durable.Aardvark.M22fArray,
            new[] {
                new M22f(1, 2, 3, 4),
                new M22f(0, 1, 2, 3),
                },
            4 + 2 * 4 * 4
            );
        [Fact]
        public void Primitive_M33fArray() => PrimitiveArray(Durable.Aardvark.M33fArray,
            new[] {
                new M33f(1, 2, 3, 4, 5, 6, 7, 8, 9),
                new M33f(0, 1, 2, 3, 4, 5, 6, 7, 8),
                },
            4 + 2 * 9 * 4
            );
        [Fact]
        public void Primitive_M44fArray() => PrimitiveArray(Durable.Aardvark.M44fArray,
            new[] {
                new M44f(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16),
                new M44f(0, 1, 2, 3, 4, 5, 6, 7, 8,  9, 10, 11, 12, 13, 14, 15),
                },
            4 + 2 * 16 * 4
            );
        [Fact]
        public void Primitive_M22dArray() => PrimitiveArray(Durable.Aardvark.M22dArray,
            new[] {
                new M22d(1, 2, 3, 4),
                new M22d(0, 1, 2, 3),
                },
            4 + 2 * 4 * 8
            );
        [Fact]
        public void Primitive_M33dArray() => PrimitiveArray(Durable.Aardvark.M33dArray,
            new[] {
                new M33d(1, 2, 3, 4, 5, 6, 7, 8, 9),
                new M33d(0, 1, 2, 3, 4, 5, 6, 7, 8),
                },
            4 + 2 * 9 * 8
            );
        [Fact]
        public void Primitive_M44dArray() => PrimitiveArray(Durable.Aardvark.M44dArray,
            new[] {
                new M44d(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16),
                new M44d(0, 1, 2, 3, 4, 5, 6, 7, 8,  9, 10, 11, 12, 13, 14, 15),
                },
            4 + 2 * 16 * 8
            );



        [Fact]
        public void Primitive_DurableMap()
        {
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                .Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                .Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
                ;
            var buffer = DurableCodec.Serialize(Durable.Primitives.DurableMap, map);
            var l = 16 + 4 + (16 + 16) + (16 + 8) + (16 + 4 + 3 * 3) + (16 + 4 + 2 * 12);
            Assert.True(buffer.Length == l);

            var (def, o) = DurableCodec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.DurableMap);

            var m = o as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 4);
            
            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);
           
            Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);
            
            var cs = (C3b[])m[Durable.Octree.Colors3b];
            Assert.True(cs.Length == 3);
            Assert.True(cs[0] == C3b.Red);
            Assert.True(cs[1] == C3b.Green);
            Assert.True(cs[2] == C3b.Blue); 
            
            var ps = (V3f[])m[Durable.Octree.PositionsLocal3f];
            Assert.True(ps.Length == 2);
            Assert.True(ps[0] == V3f.IOO);
            Assert.True(ps[1] == V3f.OIO);
        }

        [Fact]
        public void Primitive_DurableMapAligned8()
        {
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                .Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                .Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
                ;
            var buffer = DurableCodec.Serialize(Durable.Primitives.DurableMapAligned8, map);
            var l = 16 + 4 + 4 + (16 + 16) + (16 + 8) + (16 + 4 + 3 * 3 + 3) + (16 + 4 + 2 * 12 + 4);
            Assert.True(buffer.Length == l);
            Assert.True(buffer.Length % 8 == 0);

            var (def, o) = DurableCodec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.DurableMapAligned8);

            var m = o as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 4);

            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);

            Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);

            var cs = (C3b[])m[Durable.Octree.Colors3b];
            Assert.True(cs.Length == 3);
            Assert.True(cs[0] == C3b.Red);
            Assert.True(cs[1] == C3b.Green);
            Assert.True(cs[2] == C3b.Blue);

            var ps = (V3f[])m[Durable.Octree.PositionsLocal3f];
            Assert.True(ps.Length == 2);
            Assert.True(ps[0] == V3f.IOO);
            Assert.True(ps[1] == V3f.OIO);
        }

        [Fact]
        public void Primitive_DurableMapAligned16()
        {
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                .Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                .Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
                ;
            var buffer = DurableCodec.Serialize(Durable.Primitives.DurableMapAligned16, map);
            var l = 16 + 4 + 12 + (16 + 16) + (16 + 8 + 8) + (16 + 4 + 3 * 3 + 3) + (16 + 4 + 2 * 12 + 4);
            Assert.True(buffer.Length == l);
            Assert.True(buffer.Length % 16 == 0);

            var (def, o) = DurableCodec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.DurableMapAligned16);

            var m = o as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 4);
            
            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);
            
            Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);
            
            var cs = (C3b[])m[Durable.Octree.Colors3b];
            Assert.True(cs.Length == 3);
            Assert.True(cs[0] == C3b.Red);
            Assert.True(cs[1] == C3b.Green);
            Assert.True(cs[2] == C3b.Blue);

            var ps = (V3f[])m[Durable.Octree.PositionsLocal3f];
            Assert.True(ps.Length == 2);
            Assert.True(ps[0] == V3f.IOO);
            Assert.True(ps[1] == V3f.OIO);
        }



        [Fact]
        public void Primitive_GZipped_DurableMap()
        {
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                .Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                .Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
                ;
            var value = new DurableGZipped(Durable.Primitives.DurableMap, map);
            var buffer = DurableCodec.Serialize(Durable.Primitives.GZipped, value);

            var (def, o) = DurableCodec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.GZipped);

            var gz = o as DurableGZipped;
            Assert.True(gz != null);
            Assert.True(gz.Def == Durable.Primitives.DurableMap);

            var m = gz.Value as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 4);

            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);

            Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);

            var cs = (C3b[])m[Durable.Octree.Colors3b];
            Assert.True(cs.Length == 3);
            Assert.True(cs[0] == C3b.Red);
            Assert.True(cs[1] == C3b.Green);
            Assert.True(cs[2] == C3b.Blue);

            var ps = (V3f[])m[Durable.Octree.PositionsLocal3f];
            Assert.True(ps.Length == 2);
            Assert.True(ps[0] == V3f.IOO);
            Assert.True(ps[1] == V3f.OIO);
        }

        [Fact]
        public void Primitive_GZipped_DurableMapAligned16()
        {
            var id = new Guid("a5221f7a-3594-4079-b390-84f261b47420");

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                //.Add(Durable.Octree.NodeCountTotal, 123L)
                //.Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                //.Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
                ;
            var value = new DurableGZipped(Durable.Primitives.DurableMapAligned16, map);
            var buffer = DurableCodec.Serialize(Durable.Primitives.GZipped, value);

            var (def, o) = DurableCodec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.GZipped);

            var gz = o as DurableGZipped;
            Assert.True(gz != null);
            Assert.True(gz.Def == Durable.Primitives.DurableMapAligned16);

            var m = gz.Value as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 1);

            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);

            //Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);

            //var cs = (C3b[])m[Durable.Octree.Colors3b];
            //Assert.True(cs.Length == 3);
            //Assert.True(cs[0] == C3b.Red);
            //Assert.True(cs[1] == C3b.Green);
            //Assert.True(cs[2] == C3b.Blue);

            //var ps = (V3f[])m[Durable.Octree.PositionsLocal3f];
            //Assert.True(ps.Length == 2);
            //Assert.True(ps[0] == V3f.IOO);
            //Assert.True(ps[1] == V3f.OIO);
        }
    }
}
