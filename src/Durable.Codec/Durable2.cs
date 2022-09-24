﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static Aardvark.Data.Durable;

namespace Aardvark.Data;

public readonly record struct Item(Def Def, object Data)
{
    public bool IsMap => Data is IReadOnlyDictionary<Def, object>;
    public IReadOnlyDictionary<Def, object> AsMap() => (IReadOnlyDictionary<Def, object>)Data;
}

public static class DurableCodec2
{
    public static void Encode(Stream stream, Def def, object o) => Encode(stream, new(def, o));
    public static void Encode(Stream stream, Item item)
    {
        if (_encoders.TryGetValue(item.Def.PrimitiveType, out var encoder))
        {
            encoder(stream, item);
        }
        else
        {
            throw new NotImplementedException($"{item.Def}");
        }
    }

    public static Item Decode(byte[] buffer) => Decode(new MemoryStream(buffer));
    public static Item Decode(Stream stream)
    {
        var buffer = new byte[16];
        if (stream.Read(buffer, 0, 16) != 16) throw new Exception($"Failed to read durable def from stream. Error 095c9b0a-8cb6-4d58-b695-c2c30b6d88de.");
        var id = new Guid(buffer);
        if (Def.TryGet(id, out var def))
        {
            if (_decoders.TryGetValue(def.Id, out var decoder))
            {
                return decoder(stream, def);
            }
            else
            {
                throw new NotImplementedException($"No decoder for {def}. Error 1c0ac89d-019c-40c0-9657-f78f84a31db9.");
            }
        }
        else
        {
            throw new NotImplementedException($"Unknown durable id {id}. Error 1f2ee735-1c1d-46a4-aa7f-87b45948e314.");
        }
    }

    #region Encoders

    private delegate void Encoder(Stream stream, in Item item);
    private delegate Item Decoder(Stream stream, in Def def);

    private static readonly Dictionary<Guid, Encoder> _encoders = new()
    {
        { Primitives.DurableMap2.Id         , EncodeDurableMap2       },

        { Primitives.Int8Array.Id           , EncodeArray<sbyte>      },
        { Primitives.Int16Array.Id          , EncodeArray<short>      },
        { Primitives.Int32Array.Id          , EncodeArray<int>        },
        { Primitives.Int64Array.Id          , EncodeArray<long>       },
        { Primitives.UInt8Array.Id          , EncodeArray<byte>       },
        { Primitives.UInt16Array.Id         , EncodeArray<ushort>     },
        { Primitives.UInt32Array.Id         , EncodeArray<uint>       },
        { Primitives.UInt64Array.Id         , EncodeArray<ulong>      },
        { Primitives.Float32Array.Id        , EncodeArray<float>      },
        { Primitives.Float64Array.Id        , EncodeArray<double>     },
        { Primitives.DecimalDotnetArray.Id  , EncodeArray<decimal>    },
    };

    private static readonly Dictionary<Guid, Decoder> _decoders = new()
    {
        { Primitives.DurableMap2.Id         , DecodeDurableMap2       },

        { Primitives.Int8Array.Id           , DecodeArray<sbyte>      },
        { Primitives.Int16Array.Id          , DecodeArray<short>      },
        { Primitives.Int32Array.Id          , DecodeArray<int>        },
        { Primitives.Int64Array.Id          , DecodeArray<long>       },
        { Primitives.UInt8Array.Id          , DecodeArray<byte>       },
        { Primitives.UInt16Array.Id         , DecodeArray<ushort>     },
        { Primitives.UInt32Array.Id         , DecodeArray<uint>       },
        { Primitives.UInt64Array.Id         , DecodeArray<ulong>      },
        { Primitives.Float32Array.Id        , DecodeArray<float>      },
        { Primitives.Float64Array.Id        , DecodeArray<double>     },
        { Primitives.DecimalDotnetArray.Id  , DecodeArray<decimal>    },
    };

    private readonly record struct Entry(Def Key, object Value);
    private readonly record struct DurableMapLutEntry(Guid Key, long RelativeOffset);

    private static void EncodeDurableMap2(Stream stream, in Item item)
    {
        IReadOnlyList<Entry> data = item.Data switch
        {
            IEnumerable<(Def def, object o)> xs => xs.Select(x => new Entry(x.def, x.o)).ToList(),
            IReadOnlyDictionary<Def, object> xs => xs.Select(x => new Entry(x.Key, x.Value)).ToList(),
            _ => throw new NotImplementedException($"Unable to encode {item.Data.GetType()} as durable map. Error cd3ec158-68a4-4c83-94e7-fe231a08b403.")
        };

        // Sets stream.Position to next multiple of n, where n is one of [1, 2, 4, 8, 16].
        // Does nothing, if stream.Position is already a multiple of n.
        void AlignStreamToMultipleOf16()
        {
            var r = (int)(stream.Position % 16);
            if (r > 0) stream.Write(_padding, 0, 16 - r);

#if DEBUG
            if (stream.Position % 16 != 0) throw new Exception(
                $"Assertion failed. Expected stream position to be multiple of 16, but found {stream.Position}. " +
                $"Error 08384ba9-db78-4efb-bae4-02d6bd48967e."
                );
#endif
        }

        var origin = stream.Position;

        unsafe
        {
            var headerSizeInBytes = 16 + 8 + 4 + 4 + data.Count * sizeof(DurableMapLutEntry);
            var header = new byte[headerSizeInBytes];
            fixed (byte* h = header)
            {
                *(Guid*)(h + 0) = item.Def.Id;      // [origin +  0]   Guid       16 bytes
                var pTotalBytes = (long*)(h + 16);  // [       + 16]   uint64      8 bytes      // to skip this map goto [origin + totalBytes]
                *(int*)(h + 24) = data.Count;       // [       + 24]   int32       4 bytes
                *(int*)(h + 28) = 0;                // [       + 28]   int32       4 bytes      // not used
                var lut = (DurableMapLutEntry*)(h + 32);

                stream.Position += headerSizeInBytes;
                for (var i = 0; i < data.Count; i++)
                {
                    var e = data[i];

                    // align to 16-byte boundary
                    AlignStreamToMultipleOf16();

                    // write lookup table entry (key, offset)
                    lut[i] = new(e.Key.Id, stream.Position - origin);

                    // write child entry
                    Encode(stream, e.Key, e.Value);
                }

                // align to 16-byte boundary
                AlignStreamToMultipleOf16();

                // set total bytes
                *pTotalBytes = stream.Position - origin;

                // write final header
                var pos = stream.Position;
                stream.Position = origin;
                stream.Write(header, 0, header.Length);
                stream.Position = pos;
            }
        }
    }

    /// <summary>
    /// Stream is positioned at (origin + 16), that means AFTER the durable type guid.
    /// </summary>
    private static Item DecodeDurableMap2(Stream stream, in Def def)
    {
        var origin = stream.Position - 16;

        unsafe
        {
#if NETSTANDARD2_1_OR_GREATER

            var h = stackalloc byte[16];
            var header = new Span<byte>(h, 16);
            if (stream.Read(header) != 16) throw new Exception($"Failed to read header from DurableMap2. Error 6aec9296-6cf2-4322-9049-313794aad1cf.");

            var totalBytes = *(long*)h;
            var entryCount = *(int*)(h + 8);
            var lut = new DurableMapLutEntry[entryCount];
            var lutBuffer = MemoryMarshal.Cast<DurableMapLutEntry, byte>(lut);
            if (stream.Read(lutBuffer) != lutBuffer.Length) throw new Exception($"Failed to read lookup table from DurableMap2. Error 75c27a6d-1a07-4e8b-b7fa-041e8d727ac4.");

            var data = new Dictionary<Def, object>();
            for (var i = 0; i < lut.Length; i++)
            {
                stream.Position = origin + lut[i].RelativeOffset;
                var (k, v) = Decode(stream);
                data[k] = v;
            }

            return new(def, data);

#elif NETSTANDARD2_0

            var header = new byte[16];
            fixed (byte* h = header)
            {
                if (stream.Read(header, 0, 16) != 16) throw new Exception($"Failed to read header from DurableMap2. Error 2cd749d4-bb89-420b-bb1a-c483565a72a5.");

                var totalBytes = *(long*)h;
                var entryCount = *(int*)(h + 8);
                var lutBuffer = new byte[entryCount * sizeof(DurableMapLutEntry)];
                if (stream.Read(lutBuffer, 0, lutBuffer.Length) != lutBuffer.Length) throw new Exception($"Failed to read lookup table from DurableMap2. Error 71d5d0a4-0be5-4eb8-9d00-c3e7c4d9a517.");

                var lut = MemoryMarshal.Cast<byte, DurableMapLutEntry>(lutBuffer);

                var data = new Dictionary<Def, object>();
                for (var i = 0; i < lut.Length; i++)
                {
                    stream.Position = origin + lut[i].RelativeOffset;
                    var (k, v) = Decode(stream);
                    data[k] = v;
                }

                return new(def, data);
            }

#endif
        }
    }

    private static void EncodeArray<T>(Stream stream, in Item item) where T : unmanaged
    {
        var xs = (T[])item.Data;

        var id = item.Def.Id.ToByteArray();
        stream.Write(id, 0, id.Length);

#if NETSTANDARD2_1_OR_GREATER

        stream.Write(BitConverter.GetBytes(xs.Length));
        stream.Write(MemoryMarshal.Cast<T, byte>(xs));

#elif NETSTANDARD2_0

        stream.Write(BitConverter.GetBytes(xs.Length), 0, 4);
        unsafe
        {
            var buffer = new byte[xs.Length * sizeof(T)];
            Buffer.BlockCopy(xs, 0, buffer, 0, buffer.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

#endif
    }

    /// <summary>
    /// Stream is positioned at (origin + 16), that means AFTER the durable type guid.
    /// </summary>
    private static Item DecodeArray<T>(Stream stream, in Def def) where T : unmanaged
    {
        unsafe
        {
#if NETSTANDARD2_1_OR_GREATER

            var count = 0;
            if (stream.Read(new Span<byte>(&count, 4)) != 4) throw new Exception($"Failed to read array count. Error 86fddc90-6d73-4595-a8d0-28735e5785f8.");
            
            var xs = new T[count];
            var buffer = MemoryMarshal.Cast<T, byte>(xs);
            if (stream.Read(buffer) != buffer.Length) throw new Exception($"Failed to read array data. Error 051399a7-5a46-4819-b958-fbc88c260984.");

            return new(def, xs);

#elif NETSTANDARD2_0

            var countBuffer = new byte[4];
            if (stream.Read(countBuffer, 0, 4) != 4) throw new Exception($"Failed to read array count. Error 44791cad-2685-4d22-9a9f-a33c1ce9225e.");
            var count = BitConverter.ToInt32(countBuffer, 0);

            var buffer = new byte[count * sizeof(T)];
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length) throw new Exception($"Failed to read array data. Error bd050b9e-17cc-4b5f-a217-5d1990557d11.");
            var xs = new T[count];
            Buffer.BlockCopy(buffer, 0, xs, 0, buffer.Length);

            return new(def, xs);

#endif

        }
    }

    private static readonly byte[] _padding = new byte[16];

#endregion
}