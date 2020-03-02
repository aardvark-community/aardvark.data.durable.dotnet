using Aardvark.Base;
using System;
using System.Collections.Immutable;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class SerializationTests
    {
        private void Primitive<T>(Durable.Def def, T value, Func<T, T, bool> eq)
        {
            var buffer = Codec.Serialize(def, value);

            var (d, o) = Codec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T);
            Assert.True(eq(value, (T)o));
        }
        private void Primitive<T>(Durable.Def def, T value) where T : IEquatable<T>
            => Primitive(def, value, (a, b) => a.Equals(b));
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

        [Fact] public void Primitive_Box2f() => Primitive(Durable.Aardvark.Box2f, 
            new Box2f(new V2f(1.2f, 3.4f), new V2f(5.6f, 7.8f)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box2d() => Primitive(Durable.Aardvark.Box2d,
            new Box2d(new V2d(1.2, 3.4), new V2d(5.6, 7.8)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3f() => Primitive(Durable.Aardvark.Box3f,
            new Box3f(new V3f(1.2f, 3.4f, 5.6f), new V3f(6.7f, 8.9f, 9.0f)), (a, b) => a == b
            );
        [Fact] public void Primitive_Box3d() => Primitive(Durable.Aardvark.Box3d,
            new Box3d(new V3d(1.2, 3.4, 5.6), new V3d(6.7, 8.9, 9.0)), (a, b) => a == b
            );



        [Fact]
        public void Primitive_DurableMap()
        {
            var id = Guid.NewGuid();

            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, id)
                .Add(Durable.Octree.NodeCountTotal, 123L)
                ;
            var buffer = Codec.Serialize(Durable.Primitives.DurableMap, map);

            var (def, o) = Codec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.DurableMap);

            var m = o as ImmutableDictionary<Durable.Def, object>;
            Assert.True(m != null);
            Assert.True(m.Count == 2);
            Assert.True(((Guid)m[Durable.Octree.NodeId]) == id);
            Assert.True(((long)m[Durable.Octree.NodeCountTotal]) == 123L);
        }
    }
}
