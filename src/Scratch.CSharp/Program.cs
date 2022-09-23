using Aardvark.Base;
using Aardvark.Data;

class Program
{
    public static void Main()
    {
        var foo0 = Durable.Aardvark.ChunkClassifications1b;
        static Durable.Def Def(string id, string name, string description, Durable.Def type)
                => new(new Guid(id), name, description, type.Id, true);
        var foo1 = Def("3cf3a1b8-1000-4b2f-a674-f0718c60de72", "Aardvark.Chunk.Classifications1b", "Classifications. byte[].", Durable.Primitives.UInt8Array);

        //var buffer = File.ReadAllBytes(@"E:\e57tests\stores\20220917\tmp\chunk-00001.gz");
        //var map = DurableCodec.DeserializeDurableMap(buffer);

        //var child = new (Durable.Def, object)[]
        //{
        //   (Durable.Primitives.UInt8Array,      new byte  [] { 1, 2, 3, 4, 5 }),
        //   (Durable.Primitives.Int32Array,      new int   [] { 1, 2, 3, 4, 5, 6, 7 }),
        //   (Durable.Primitives.Float64Array,    new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 3.1415 }),
        //};

        //var m = new (Durable.Def, object)[]
        //{
        //    (Durable.Primitives.UInt8Array,     new byte  [] { 1, 2, 3, 4, 5 }),
        //    (Durable.Primitives.DurableMap2,    child),
        //    (Durable.Primitives.Float64Array,   new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 3.1415 }),
        //};

        //for (var t = 0; t < 5; t++)
        //{
        //    Report.BeginTimed("roundtrip");
        //    for (var i = 0; i < 1000000; i++)
        //    {
        //        var ms = new MemoryStream();
        //        Durable.Codec2.Encode(ms, Durable.Primitives.DurableMap2, m);
        //        var buffer = ms.ToArray();
        //        _ = Durable.Codec2.Decode(buffer);
        //    }
        //    Report.EndTimed();
        //}

        //Console.WriteLine("done");

    }
}