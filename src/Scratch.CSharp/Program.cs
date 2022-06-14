using Aardvark.Base;

class Program
{
    public static void Main()
    {

        var child = new Durable.Codec2.Entry[]
        {
            new(Durable.Primitives.UInt8Array,      () => new byte  [] { 1, 2, 3, 4, 5 }),
            new(Durable.Primitives.Int32Array,      () => new int   [] { 1, 2, 3, 4, 5, 6, 7 }),
            new(Durable.Primitives.Float64Array,    () => new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 3.1415 }),
        };

        var m = new Durable.Codec2.Entry[]
        {
            new(Durable.Primitives.UInt8Array,      () => new byte  [] { 1, 2, 3, 4, 5 }),
            new(Durable.Primitives.DurableMap2,     () => child),
            new(Durable.Primitives.Float64Array,    () => new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 3.1415 }),
        };

        for (var _ = 0; _ < 5; _++)
        {
            Report.BeginTimed("roundtrip");
            for (var i = 0; i < 1000000; i++)
            {
                var ms = new MemoryStream();
                Durable.Codec2.Encode(ms, Durable.Primitives.DurableMap2, m);
                var buffer = ms.ToArray();
                var (def, o) = Durable.Codec2.Decode(buffer);
            }
            Report.EndTimed();
        }

        Console.WriteLine("done");

    }
}