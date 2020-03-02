using System;
using System.Collections.Immutable;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void Primitive_Guid()
        {
            var x = Guid.NewGuid();
            var buffer = Codec.Serialize(Durable.Primitives.GuidDef, x);

            var (def, o) = Codec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.GuidDef);
            Assert.True(o is Guid);
            Assert.True((Guid)o == x);
        }


        private void Primitive_Int<T>(Durable.Def def, T value) where T : IComparable<T>
        {
            var buffer = Codec.Serialize(def, value);

            var (d, o) = Codec.Deserialize(buffer);
            Assert.True(d == def);
            Assert.True(o is T);
            Assert.True(value.CompareTo((T)o) == 0);
        }

        [Fact] public void Primitive_Int8() => Primitive_Int(Durable.Primitives.Int8, (sbyte)42);
        [Fact] public void Primitive_UInt8() => Primitive_Int(Durable.Primitives.UInt8, (byte)42);
        [Fact] public void Primitive_Int16() => Primitive_Int(Durable.Primitives.Int16, (short)42);
        [Fact] public void Primitive_UInt16() => Primitive_Int(Durable.Primitives.UInt16, (ushort)42);
        [Fact] public void Primitive_Int32() => Primitive_Int(Durable.Primitives.Int32, (int)42);
        [Fact] public void Primitive_UInt32() => Primitive_Int(Durable.Primitives.UInt32, (uint)42);
        [Fact] public void Primitive_Int64() => Primitive_Int(Durable.Primitives.Int64, (long)42);
        [Fact] public void Primitive_UInt64() => Primitive_Int(Durable.Primitives.UInt64, (ulong)42);


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
