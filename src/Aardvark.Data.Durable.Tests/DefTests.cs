using Aardvark.Base;
using System;
using Xunit;

namespace Aardvark.Data.Tests
{
    public class DefTests
    {
        [Fact]
        public void EqualsTest()
        {
            var a = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);
            var b = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);

            Assert.True(a.Equals((object)a));
            Assert.True(b.Equals((object)b));

            Assert.False(a.Equals((object)b));
            Assert.False(a.Equals((object)null));
            Assert.False(b.Equals((object)a));
            Assert.False(b.Equals((object)null));
        }

        [Fact]
        public void EquatableTest()
        {
            var a = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);
            var b = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);

            Assert.True(a.Equals((Durable.Def)a));
            Assert.True(b.Equals((Durable.Def)b));

            Assert.False(a.Equals((Durable.Def)b));
            Assert.False(a.Equals((Durable.Def)null));
            Assert.False(b.Equals((Durable.Def)a));
            Assert.False(b.Equals((Durable.Def)null));
        }

        [Fact]
        public void ComparableTest()
        {
            var a = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);
            var b = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);

            Assert.True(a.CompareTo(a) == 0);
            Assert.True(a.CompareTo(b).Sign() == b.CompareTo(a).Sign() * -1);

            Assert.True(a.CompareTo(null) == 1);
        }

        [Fact]
        public void AliasTest()
        {
            var a = new Durable.Def(Guid.NewGuid(), "test", "test", Durable.Primitives.Unit.Id, false);
            var alias = Guid.NewGuid();
            Durable.Def.AddAlias(alias, a);

            Assert.True(a == Durable.Def.Get(alias));
        }
    }
}
