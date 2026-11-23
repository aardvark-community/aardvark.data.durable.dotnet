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
        public void TimeDefinitionsKeepTheGuidsTheirWritersUse()
        {
            // Time on point data: data written with these Guids before they were upstream must read with these.
            Assert.Equal(new Guid("cd0fcb5f-f830-4df2-97a8-b3a7970e9ef8"), Durable.Octree.Times1d.Id);
            Assert.Equal(new Guid("dc861e30-6552-fcdd-146c-0437c1cd963e"), Durable.Octree.Times1dGz.Id);
            Assert.Equal(new Guid("38fee4cd-a795-41a2-9a42-6358fe044aaf"), Durable.Generic.TimeSystem.Id);
            Assert.Equal(Durable.Primitives.GuidDef.Id, Durable.Generic.TimeSystem.Type);
            // An enumerated value is a definition of its own, of type Unit, whose Guid the field holds.
            Assert.Equal(Durable.Primitives.Unit.Id, Durable.Generic.TimeSystemGps.Type);
            Assert.Equal(new Guid("2d860768-a853-4850-9ac8-20475382cd54"), Durable.Generic.TimeOriginKindGpsWeekStart.Id);
            Assert.Equal(Durable.Aardvark.Range1d.Id, Durable.Generic.TimeRange.Type);
        }

        [Fact]
        public void IntensityDefinitionsCoverEveryWidth()
        {
            // One octree intensity definition per element type, so a reader keeps a source's type.
            Assert.Equal(Durable.Primitives.UInt8Array.Id, Durable.Octree.Intensities1b.Type);
            Assert.Equal(Durable.Primitives.Int16Array.Id, Durable.Octree.Intensities1s.Type);
            Assert.Equal(Durable.Primitives.UInt16Array.Id, Durable.Octree.Intensities1us.Type);
            Assert.Equal(Durable.Primitives.Int32Array.Id, Durable.Octree.Intensities1i.Type);
            Assert.Equal(Durable.Primitives.UInt32Array.Id, Durable.Octree.Intensities1ui.Type);
            Assert.Equal(Durable.Primitives.Float32Array.Id, Durable.Octree.Intensities1f.Type);
            Assert.Equal(Durable.Primitives.Float64Array.Id, Durable.Octree.Intensities1d.Type);
            Assert.Equal(new Guid("3d70a06f-7e48-4b0a-a371-a4040999e98c"), Durable.Octree.Intensities1s.Id);
            Assert.Equal(new Guid("bf58adf5-43a6-41db-90f2-54f03ba85f66"), Durable.Octree.Intensities1us.Id);
            Assert.Equal(new Guid("9e64f8a0-17e7-43ac-9539-80e87f97d056"), Durable.Octree.Intensities1ui.Id);
            Assert.Equal(new Guid("447fd0be-0dc0-478f-b2bf-b8b3807c5553"), Durable.Octree.Intensities1d.Id);
            Assert.Equal(Durable.Primitives.UInt16ArrayGz.Id, Durable.Octree.Intensities1usGz.Type);
            Assert.Equal(Durable.Primitives.Float64ArrayGz.Id, Durable.Octree.Intensities1dGz.Type);
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
