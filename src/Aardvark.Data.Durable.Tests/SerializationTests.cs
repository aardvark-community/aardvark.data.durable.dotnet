using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class SerializationTests
    {
        private void Primitive<T>(Durable.Def def, T value, Func<T, T, bool> eq)
        {
            var buffer = DurableCodec.Serialize(def, value);

            var (d, o) = DurableCodec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T);
            Assert.True(eq(value, (T)o));
        }
        private void PrimitiveArray<T>(Durable.Def def, T[] value, Func<T, T, bool> eq)
        {
            var buffer = DurableCodec.Serialize(def, value);

            var (d, o) = DurableCodec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T[]);
            var xs = (T[])o;
            for (var i = 0; i < value.Length; i++)
                Assert.True(eq(value[i], xs[i]));
        }
        private void Primitive<T>(Durable.Def def, T value) where T : IEquatable<T>
            => Primitive(def, value, (a, b) => a.Equals(b));
        private void PrimitiveArray<T>(Durable.Def def, T[] value) where T : IEquatable<T>
            => PrimitiveArray(def, value, (a, b) => a.Equals(b));

        [Fact]
        public void Mmmh()
        {
            //var buffer = File.ReadAllBytes(@"T:\buffer.bin");
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

        [Fact] public void Primitive_StringUTF8() => Primitive(Durable.Primitives.StringUTF8, "foo bar woohoo");

        [Fact] public void Primitive_Guid() => Primitive(Durable.Primitives.GuidDef, Guid.NewGuid());
        [Fact] public void Primitive_Int8() => Primitive(Durable.Primitives.Int8, (sbyte)42);
        [Fact] public void Primitive_UInt8() => Primitive(Durable.Primitives.UInt8, (byte)42);
        [Fact] public void Primitive_Int16() => Primitive(Durable.Primitives.Int16, (short)42);
        [Fact] public void Primitive_UInt16() => Primitive(Durable.Primitives.UInt16, (ushort)42);
        [Fact] public void Primitive_Int32() => Primitive(Durable.Primitives.Int32, (int)42);
        [Fact] public void Primitive_UInt32() => Primitive(Durable.Primitives.UInt32, (uint)42);
        [Fact] public void Primitive_Int64() => Primitive(Durable.Primitives.Int64, (long)42);
        [Fact] public void Primitive_UInt64() => Primitive(Durable.Primitives.UInt64, (ulong)42);
        [Fact] public void Primitive_Float32() => Primitive(Durable.Primitives.Float32, 3.1415926f);
        [Fact] public void Primitive_Float64() => Primitive(Durable.Primitives.Float64, 3.1415926);

        [Fact] public void Primitive_Cell() => Primitive(Durable.Aardvark.Cell, new Cell(2,11,-5,-6));
        [Fact] public void Primitive_Cell2d() => Primitive(Durable.Aardvark.Cell2d, new Cell2d(2, 11, -6));

        [Fact] public void Primitive_V2i() => Primitive(Durable.Aardvark.V2i, new V2i(4, -5));
        [Fact] public void Primitive_V3i() => Primitive(Durable.Aardvark.V3i, new V3i(2, 6, -4));
        [Fact] public void Primitive_V4i() => Primitive(Durable.Aardvark.V4i, new V4i(5, -3, 8, 34));
        [Fact] public void Primitive_V2l() => Primitive(Durable.Aardvark.V2l, new V2l(4, -5));
        [Fact] public void Primitive_V3l() => Primitive(Durable.Aardvark.V3l, new V3l(2, 6, -4));
        [Fact] public void Primitive_V4l() => Primitive(Durable.Aardvark.V4l, new V4l(5, -3, 8, 34));
        [Fact] public void Primitive_V2f() => Primitive(Durable.Aardvark.V2f, new V2f(1.2f, 3.4f));
        [Fact] public void Primitive_V3f() => Primitive(Durable.Aardvark.V3f, new V3f(1.2f, 3.4f, 5.6f));
        [Fact] public void Primitive_V4f() => Primitive(Durable.Aardvark.V4f, new V4f(1.2f, 3.4f, 5.6f, 7.8f));
        [Fact] public void Primitive_V2d() => Primitive(Durable.Aardvark.V2d, new V2d(1.2, 3.4));
        [Fact] public void Primitive_V3d() => Primitive(Durable.Aardvark.V3d, new V3d(1.2, 3.4, 5.6));
        [Fact] public void Primitive_V4d() => Primitive(Durable.Aardvark.V4d, new V4d(1.2, 3.4, 5.6, 7.8));

        [Fact] public void Primitive_C3b() => Primitive(Durable.Aardvark.C3b, new C3b(17, 42, 253));
        [Fact] public void Primitive_C4b() => Primitive(Durable.Aardvark.C4b, new C4b(17, 42, 253, 128));
        [Fact] public void Primitive_C3f() => Primitive(Durable.Aardvark.C3f, new C3f(0.1f, 0.2f, 0.3f));
        [Fact] public void Primitive_C4f() => Primitive(Durable.Aardvark.C4f, new C4f(0.3f, 0.4f, 0.5f, 0.6f));

        [Fact] public void Primitive_Box2i() => Primitive(Durable.Aardvark.Box2i,
            new Box2i(new V2i(1, 3), new V2i(5, 7)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box2l() => Primitive(Durable.Aardvark.Box2l,
            new Box2l(new V2l(1, 3), new V2l(5, 7)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box2f() => Primitive(Durable.Aardvark.Box2f, 
            new Box2f(new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box2d() => Primitive(Durable.Aardvark.Box2d,
            new Box2d(new V2d(1.2, 3.4), new V2d(5.6, 7.8)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3i() => Primitive(Durable.Aardvark.Box3i,
            new Box3i(new V3i(1, 3, 5), new V3i(6, 8, 9)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3l() => Primitive(Durable.Aardvark.Box3l,
            new Box3l(new V3l(1, 3, 5), new V3l(6, 8, 9)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3f() => Primitive(Durable.Aardvark.Box3f,
            new Box3f(new V3f(1.2f, 3.4f, 5.6f), new V3f(6.7f, 8.9f, 9.0f)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3d() => Primitive(Durable.Aardvark.Box3d,
            new Box3d(new V3d(1.2, 3.4, 5.6), new V3d(6.7, 8.9, 9.0)), (a, b) => a == b
            );

        [Fact]
        public void Primitive_M22f() => Primitive(Durable.Aardvark.M22f,
            new M22f(1, 2, 3, 4), (a, b) => a == b
            );
        [Fact]
        public void Primitive_M33f() => Primitive(Durable.Aardvark.M33f,
            new M33f(1, 2, 3, 4, 5, 6, 7, 8, 9), (a, b) => a == b
            );
        [Fact]
        public void Primitive_M44f() => Primitive(Durable.Aardvark.M44f,
            new M44f(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16), (a, b) => a == b
            );
        [Fact]
        public void Primitive_M22d() => Primitive(Durable.Aardvark.M22d,
            new M22d(1, 2, 3, 4), (a, b) => a == b
            );
        [Fact]
        public void Primitive_M33d() => Primitive(Durable.Aardvark.M33d,
            new M33d(1, 2, 3, 4, 5, 6, 7, 8, 9), (a, b) => a == b
            );
        [Fact]
        public void Primitive_M44d() => Primitive(Durable.Aardvark.M44d,
            new M44d(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), (a, b) => a == b
            );

        [Fact] public void Primitive_StringUTF8Array() => PrimitiveArray(Durable.Primitives.StringUTF8Array, 
            new[] { "foo", "bar", "woohoo" }
            );

        [Fact] public void Primitive_GuidArray() => PrimitiveArray(Durable.Primitives.GuidArray, 
                new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
                );

        [Fact]
        public void Primitive_Int8Array() => PrimitiveArray(Durable.Primitives.Int8Array,
                new[] { (sbyte)42, (sbyte)43, (sbyte)44 }
                );
        [Fact]
        public void Primitive_UInt8Array() => PrimitiveArray(Durable.Primitives.UInt8Array,
                new[] { (byte)42, (byte)43, (byte)44 }
                );
        [Fact]
        public void Primitive_Int16Array() => PrimitiveArray(Durable.Primitives.Int16Array,
                new[] { (short)42, (short)43, (short)44 }
                );
        [Fact]
        public void Primitive_UInt16Array() => PrimitiveArray(Durable.Primitives.UInt16Array,
                new[] { (ushort)42, (ushort)43, (ushort)44 }
                );
        [Fact]
        public void Primitive_Int32Array() => PrimitiveArray(Durable.Primitives.Int32Array,
                new[] { (int)42, (int)43, (int)44 }
                );
        [Fact]
        public void Primitive_UInt32Array() => PrimitiveArray(Durable.Primitives.UInt32Array,
                new[] { (uint)42, (uint)43, (uint)44 }
                );
        [Fact]
        public void Primitive_Int64Array() => PrimitiveArray(Durable.Primitives.Int64Array,
                new[] { (long)42, (long)43, (long)44 }
                );
        [Fact]
        public void Primitive_UInt64Array() => PrimitiveArray(Durable.Primitives.UInt64Array,
                new[] { (ulong)42, (ulong)43, (ulong)44 }
                );
        [Fact]
        public void Primitive_Float32Array() => PrimitiveArray(Durable.Primitives.Float32Array,
                new[] { (float)42, (float)43, (float)44 }
                );
        [Fact]
        public void Primitive_Float64Array() => PrimitiveArray(Durable.Primitives.Float64Array,
                new[] { (double)42, (double)43, (double)44 }
                );

        [Fact]
        public void Primitive_CellArray() => PrimitiveArray(Durable.Aardvark.CellArray,
                new[] { new Cell(1,2,3,4), new Cell(-5, 42, -17, -10) }
                );
        [Fact]
        public void Primitive_Cell2dArray() => PrimitiveArray(Durable.Aardvark.Cell2dArray,
                new[] { new Cell2d(1, 2, 4), new Cell2d(-5, 42, -10) }
                );

        [Fact]
        public void Primitive_V2iArray() => PrimitiveArray(Durable.Aardvark.V2iArray,
                new[] { new V2i(1, 3), new V2i(5, 7) }
                );
        [Fact]
        public void Primitive_V3iArray() => PrimitiveArray(Durable.Aardvark.V3iArray,
                new[] { new V3i(1, 3, -2), new V3i(5, 7, -6) }
                );
        [Fact]
        public void Primitive_V4iArray() => PrimitiveArray(Durable.Aardvark.V4iArray,
                new[] { new V4i(1, 3, -2, 0), new V4i(5, 7, -6, -0) }
                );
        [Fact]
        public void Primitive_V2lArray() => PrimitiveArray(Durable.Aardvark.V2lArray,
                new[] { new V2l(1, 3), new V2l(5, 7) }
                );
        [Fact]
        public void Primitive_V3lArray() => PrimitiveArray(Durable.Aardvark.V3lArray,
                new[] { new V3l(1, 3, -2), new V3l(5, 7, -6) }
                );
        [Fact]
        public void Primitive_V4lArray() => PrimitiveArray(Durable.Aardvark.V4lArray,
                new[] { new V4l(1, 3, -2, 0), new V4l(5, 7, -6, -0) }
                );
        [Fact]
        public void Primitive_V2fArray() => PrimitiveArray(Durable.Aardvark.V2fArray,
                new[] { new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f) }
                );
        [Fact]
        public void Primitive_V3fArray() => PrimitiveArray(Durable.Aardvark.V3fArray,
                new[] { new V3f(1.2f, 3.4f, -2.3f), new V3f(5.6f, 7.8f, -6.7f) }
                );
        [Fact]
        public void Primitive_V4fArray() => PrimitiveArray(Durable.Aardvark.V4fArray,
                new[] { new V4f(1.2f, 3.4f, -2.3f, 0.1f), new V4f(5.6f, 7.8f, -6.7f, -0.1f) }
                );
        [Fact]
        public void Primitive_V2dArray() => PrimitiveArray(Durable.Aardvark.V2dArray,
                new[] { new V2d(1.2, 3.4), new V2d(5.6, 7.8) }
                );
        [Fact]
        public void Primitive_V3dArray() => PrimitiveArray(Durable.Aardvark.V3dArray,
                new[] { new V3d(1.2, 3.4, -2.3), new V3d(5.6, 7.8, -6.7) }
                );
        [Fact]
        public void Primitive_V4dArray() => PrimitiveArray(Durable.Aardvark.V4dArray,
                new[] { new V4d(1.2, 3.4, -2.3, 0.1), new V4d(5.6, 7.8, -6.7, -0.1) }
                );

        [Fact]
        public void Primitive_C3bArray() => PrimitiveArray(Durable.Aardvark.C3bArray,
                new[] { C3b.Red, C3b.Magenta, C3b.DarkGreen }
                );
        [Fact]
        public void Primitive_C4bArray() => PrimitiveArray(Durable.Aardvark.C4bArray,
                new[] { C4b.Red, C4b.Magenta, C4b.DarkGreen }
                );
        [Fact]
        public void Primitive_C3fArray() => PrimitiveArray(Durable.Aardvark.C3fArray,
                new[] { C3f.Red, C3f.Magenta, C3f.DarkGreen }
                );
        [Fact]
        public void Primitive_C4fArray() => PrimitiveArray(Durable.Aardvark.C4fArray,
                new[] { C4f.Red, C4f.Magenta, C4f.DarkGreen }
                );

        [Fact]
        public void Primitive_Box2iArray() => PrimitiveArray(Durable.Aardvark.Box2iArray,
            new[] {
                new Box2i(new V2i(1, 3), new V2i(5, 7)),
                new Box2i(new V2i(2, 4), new V2i(6, 8))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box3iArray() => PrimitiveArray(Durable.Aardvark.Box3iArray,
            new[] {
                new Box3i(new V3i(1, 3, 5), new V3i(5, 7, -213)),
                new Box3i(new V3i(2, 4, 6), new V3i(6, 8, 234))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box2lArray() => PrimitiveArray(Durable.Aardvark.Box2lArray,
            new[] {
                new Box2l(new V2l(1, 3), new V2l(5, 7)),
                new Box2l(new V2l(2, 4), new V2l(6, 8))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box3lArray() => PrimitiveArray(Durable.Aardvark.Box3lArray,
            new[] {
                new Box3l(new V3l(1, 3, 5), new V3l(5, 7, -213)),
                new Box3l(new V3l(2, 4, 6), new V3l(6, 8, 234))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box2fArray() => PrimitiveArray(Durable.Aardvark.Box2fArray,
            new[] {
                new Box2f(new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f)),
                new Box2f(new V2f(2.3f, 4.5f), new V2f(6.7f, 8.9f))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box3fArray() => PrimitiveArray(Durable.Aardvark.Box3fArray,
            new[] {
                new Box3f(new V3f(1.2f, 3.4f, 5.6f), new V3f(5.6f, 7.8f, -213.5f)),
                new Box3f(new V3f(2.3f, 4.5f, 6.7f), new V3f(6.7f, 8.9f, 234.67f))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box2dArray() => PrimitiveArray(Durable.Aardvark.Box2dArray,
            new[] {
                new Box2d(new V2d(1.2, 3.4), new V2d(5.6f, 7.8)),
                new Box2d(new V2d(2.3, 4.5), new V2d(6.7f, 8.9))
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_Box3dArray() => PrimitiveArray(Durable.Aardvark.Box3dArray,
            new[] {
                new Box3d(new V3d(1.2, 3.4, 5.6), new V3d(5.6, 7.8, 2354.567)),
                new Box3d(new V3d(2.3, 4.5, 6.7), new V3d(6.7, 8.9, -456.345))
                },
            (a, b) => a == b
            );


        [Fact]
        public void Primitive_M22fArray() => PrimitiveArray(Durable.Aardvark.M22fArray,
            new[] {
                new M22f(1, 2, 3, 4),
                new M22f(0, 1, 2, 3),
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_M33fArray() => PrimitiveArray(Durable.Aardvark.M33fArray,
            new[] {
                new M33f(1, 2, 3, 4, 5, 6, 7, 8, 9),
                new M33f(0, 1, 2, 3, 4, 5, 6, 7, 8),
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_M44fArray() => PrimitiveArray(Durable.Aardvark.M44fArray,
            new[] {
                new M44f(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16),
                new M44f(0, 1, 2, 3, 4, 5, 6, 7, 8,  9, 10, 11, 12, 13, 14, 15),
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_M22dArray() => PrimitiveArray(Durable.Aardvark.M22dArray,
            new[] {
                new M22d(1, 2, 3, 4),
                new M22d(0, 1, 2, 3),
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_M33dArray() => PrimitiveArray(Durable.Aardvark.M33dArray,
            new[] {
                new M33d(1, 2, 3, 4, 5, 6, 7, 8, 9),
                new M33d(0, 1, 2, 3, 4, 5, 6, 7, 8),
                },
            (a, b) => a == b
            );
        [Fact]
        public void Primitive_M44dArray() => PrimitiveArray(Durable.Aardvark.M44dArray,
            new[] {
                new M44d(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16),
                new M44d(0, 1, 2, 3, 4, 5, 6, 7, 8,  9, 10, 11, 12, 13, 14, 15),
                },
            (a, b) => a == b
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
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                .Add(Durable.Octree.Colors3b, new[] { C3b.Red, C3b.Green, C3b.Blue })
                .Add(Durable.Octree.PositionsLocal3f, new[] { V3f.IOO, V3f.OIO })
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
    }
}
