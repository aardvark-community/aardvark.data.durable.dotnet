using System;
using System.Collections.Immutable;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void CanSerializePrimitive_Guid()
        {
            var x = Guid.NewGuid();
            var buffer = Codec.Serialize(Durable.Primitives.GuidDef, x);
            Assert.True(buffer.Length > 0);
        }
        [Fact]
        public void CanDeserializePrimitive_Guid()
        {
            var x = Guid.NewGuid();
            var buffer = Codec.Serialize(Durable.Primitives.GuidDef, x);

            var (def, o) = Codec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.GuidDef);
            Assert.True(o is Guid);
            Assert.True((Guid)o == x);
        }

        [Fact]
        public void CanSerializePrimitive_int32()
        {
            var x = 42;
            var buffer = Codec.Serialize(Durable.Primitives.Int32, x);
            Assert.True(buffer.Length == 20);
        }
        [Fact]
        public void CanDeserializePrimitive_int32()
        {
            var x = 42;
            var buffer = Codec.Serialize(Durable.Primitives.Int32, x);

            var (def, o) = Codec.Deserialize(buffer);
            Assert.True(def == Durable.Primitives.Int32);
            Assert.True(o is int);
            Assert.True((int)o == x);
        }


        [Fact]
        public void CanSerializeDurableMap()
        {
            var map = ImmutableDictionary<Durable.Def, object>.Empty
                .Add(Durable.Octree.NodeId, Guid.NewGuid())
                .Add(Durable.Octree.NodeCountTotal, 123L)
                ;
            var buffer = Codec.Serialize(Durable.Primitives.DurableMap, map);
            Assert.True(buffer.Length > 0);
        }

        [Fact]
        public void CanDeserializeDurableMap()
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
