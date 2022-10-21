using Aardvark.Base;
using Aardvark.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Aardworx.Data;

public static class DurableCodec2
{
    public readonly record struct Item(Durable.Def Def, object Data)
    {
        public bool IsMap => Data is IReadOnlyDictionary<Durable.Def, object>;
        public IReadOnlyDictionary<Durable.Def, object> AsMap() => (IReadOnlyDictionary<Durable.Def, object>)Data;
    }

    public static void Encode(Stream stream, Durable.Def def, object o) => Encode(stream, new(def, o));
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
        return Decode(stream, id);
    }
    public static Item Decode(Stream stream, Guid id)
    {
        if (Durable.Def.TryGet(id, out var def))
        {
            if (_decoders.TryGetValue(def.PrimitiveType, out var decoder))
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
    private delegate Item Decoder(Stream stream, in Durable.Def def);

    private static readonly Dictionary<Guid, Encoder> _encoders = new()
    {
        { Durable.Primitives.DurableMap2.Id         , EncodeDurableMap2     },

        { Durable.Primitives.StringUTF8.Id          , EncodeString          },
        { Durable.Primitives.StringUTF8Array.Id     , EncodeStringArray     },

        { Durable.Primitives.Int8.Id                , EncodeValue<sbyte>    },
        { Durable.Primitives.Int16.Id               , EncodeValue<short>    },
        { Durable.Primitives.Int32.Id               , EncodeValue<int>      },
        { Durable.Primitives.Int64.Id               , EncodeValue<long>     },
        { Durable.Primitives.UInt8.Id               , EncodeValue<byte>     },
        { Durable.Primitives.UInt16.Id              , EncodeValue<ushort>   },
        { Durable.Primitives.UInt32.Id              , EncodeValue<uint>     },
        { Durable.Primitives.UInt64.Id              , EncodeValue<ulong>    },
        { Durable.Primitives.Float32.Id             , EncodeValue<float>    },
        { Durable.Primitives.Float64.Id             , EncodeValue<double>   },
        { Durable.Primitives.DecimalDotnet.Id       , EncodeValue<decimal>  },
        { Durable.Primitives.GuidDef.Id             , EncodeValue<Guid>     },

        { Durable.Primitives.Int8Array.Id           , EncodeArray<sbyte>    },
        { Durable.Primitives.Int16Array.Id          , EncodeArray<short>    },
        { Durable.Primitives.Int32Array.Id          , EncodeArray<int>      },
        { Durable.Primitives.Int64Array.Id          , EncodeArray<long>     },
        { Durable.Primitives.UInt8Array.Id          , EncodeArray<byte>     },
        { Durable.Primitives.UInt16Array.Id         , EncodeArray<ushort>   },
        { Durable.Primitives.UInt32Array.Id         , EncodeArray<uint>     },
        { Durable.Primitives.UInt64Array.Id         , EncodeArray<ulong>    },
        { Durable.Primitives.Float32Array.Id        , EncodeArray<float>    },
        { Durable.Primitives.Float64Array.Id        , EncodeArray<double>   },
        { Durable.Primitives.DecimalDotnetArray.Id  , EncodeArray<decimal>  },
        { Durable.Primitives.GuidArray.Id           , EncodeArray<Guid>     },


        { Durable.Aardvark.V2d.Id                   , EncodeValue<V2d>      },
        { Durable.Aardvark.V3d.Id                   , EncodeValue<V3d>      },
        { Durable.Aardvark.V4d.Id                   , EncodeValue<V4d>      },
        { Durable.Aardvark.V2f.Id                   , EncodeValue<V2f>      },
        { Durable.Aardvark.V3f.Id                   , EncodeValue<V3f>      },
        { Durable.Aardvark.V4f.Id                   , EncodeValue<V4f>      },
        { Durable.Aardvark.V2i.Id                   , EncodeValue<V2i>      },
        { Durable.Aardvark.V3i.Id                   , EncodeValue<V3i>      },
        { Durable.Aardvark.V4i.Id                   , EncodeValue<V4i>      },
        { Durable.Aardvark.V2l.Id                   , EncodeValue<V2l>      },
        { Durable.Aardvark.V3l.Id                   , EncodeValue<V3l>      },
        { Durable.Aardvark.V4l.Id                   , EncodeValue<V4l>      },
        { Durable.Aardvark.V2dArray.Id              , EncodeArray<V2d>      },
        { Durable.Aardvark.V3dArray.Id              , EncodeArray<V3d>      },
        { Durable.Aardvark.V4dArray.Id              , EncodeArray<V4d>      },
        { Durable.Aardvark.V2fArray.Id              , EncodeArray<V2f>      },
        { Durable.Aardvark.V3fArray.Id              , EncodeArray<V3f>      },
        { Durable.Aardvark.V4fArray.Id              , EncodeArray<V4f>      },
        { Durable.Aardvark.V2iArray.Id              , EncodeArray<V2i>      },
        { Durable.Aardvark.V3iArray.Id              , EncodeArray<V3i>      },
        { Durable.Aardvark.V4iArray.Id              , EncodeArray<V4i>      },
        { Durable.Aardvark.V2lArray.Id              , EncodeArray<V2l>      },
        { Durable.Aardvark.V3lArray.Id              , EncodeArray<V3l>      },
        { Durable.Aardvark.V4lArray.Id              , EncodeArray<V4l>      },

        { Durable.Aardvark.C3b.Id                   , EncodeValue<C4b>      },
        { Durable.Aardvark.C4b.Id                   , EncodeValue<C4b>      },
        { Durable.Aardvark.C3bArray.Id              , EncodeArray<C4b>      },
        { Durable.Aardvark.C4bArray.Id              , EncodeArray<C4b>      },

        { Durable.Aardvark.Cell.Id                  , EncodeValue<Cell>     },
        { Durable.Aardvark.Cell2d.Id                , EncodeValue<Cell2d>   },
        { Durable.Aardvark.CellArray.Id             , EncodeArray<Cell>     },
        { Durable.Aardvark.Cell2dArray.Id           , EncodeArray<Cell2d>   },

        { Durable.Aardvark.Box2d.Id                 , EncodeValue<Box2d>    },
        { Durable.Aardvark.Box2f.Id                 , EncodeValue<Box2f>    },
        { Durable.Aardvark.Box2i.Id                 , EncodeValue<Box2i>    },
        { Durable.Aardvark.Box2l.Id                 , EncodeValue<Box2l>    },
        { Durable.Aardvark.Box3d.Id                 , EncodeValue<Box3d>    },
        { Durable.Aardvark.Box3f.Id                 , EncodeValue<Box3f>    },
        { Durable.Aardvark.Box3i.Id                 , EncodeValue<Box3i>    },
        { Durable.Aardvark.Box3l.Id                 , EncodeValue<Box3l>    },
        { Durable.Aardvark.Box2dArray.Id            , EncodeArray<Box2d>    },
        { Durable.Aardvark.Box2fArray.Id            , EncodeArray<Box2f>    },
        { Durable.Aardvark.Box2iArray.Id            , EncodeArray<Box2i>    },
        { Durable.Aardvark.Box2lArray.Id            , EncodeArray<Box2l>    },
        { Durable.Aardvark.Box3dArray.Id            , EncodeArray<Box3d>    },
        { Durable.Aardvark.Box3fArray.Id            , EncodeArray<Box3f>    },
        { Durable.Aardvark.Box3iArray.Id            , EncodeArray<Box3i>    },
        { Durable.Aardvark.Box3lArray.Id            , EncodeArray<Box3l>    },

        { Durable.Aardvark.Range1b.Id               , EncodeValue<Range1b>  },
        { Durable.Aardvark.Range1d.Id               , EncodeValue<Range1d>  },
        { Durable.Aardvark.Range1f.Id               , EncodeValue<Range1f>  },
        { Durable.Aardvark.Range1i.Id               , EncodeValue<Range1i>  },
        { Durable.Aardvark.Range1l.Id               , EncodeValue<Range1l>  },
        { Durable.Aardvark.Range1s.Id               , EncodeValue<Range1s>  },
        { Durable.Aardvark.Range1sb.Id              , EncodeValue<Range1sb> },
        { Durable.Aardvark.Range1ui.Id              , EncodeValue<Range1ui> },
        { Durable.Aardvark.Range1ul.Id              , EncodeValue<Range1ul> },
        { Durable.Aardvark.Range1us.Id              , EncodeValue<Range1us> },
    };

    private static void EncodeDurableMap2(Stream stream, in Item item)
    {
        if (!stream.CanWrite) throw new Exception($"Stream must be writeable. Error e75f9f7e-499f-4198-bea7-32153a25c214.");
        if (!stream.CanSeek) throw new Exception($"Stream must be seekable. Error d47180d9-90e3-4013-8da4-73e1ab130782.");

        Entry[] MapToEntryArray(IReadOnlyCollection<KeyValuePair<Durable.Def, object>> collection)
        {
            var result = new Entry[collection.Count];
            var i = 0;
            foreach (var x in collection) result[i++] = new Entry(x.Key, x.Value);
            return result;
        }

        IReadOnlyList<Entry> data = item.Data switch
        {
            IReadOnlyCollection<KeyValuePair<Durable.Def, object>> xs => MapToEntryArray(xs),
            IEnumerable<(Durable.Def def, object o)> xs => xs.Select(x => new Entry(x.def, x.o)).ToArray(),
            _ => throw new NotImplementedException($"Unable to encode {item.Data.GetType()} as durable map. Error cd3ec158-68a4-4c83-94e7-fe231a08b403.")
        };

        // Sets stream.Position to next multiple of 16.
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
            var headerSizeInBytes = 16 + 8 + 4 + 4 + data.Count * sizeof(TocEntry);
            var header = new byte[headerSizeInBytes];
            fixed (byte* h = header)
            {
                *(Guid*)(h + 0) = item.Def.Id;      // [origin +  0]   Guid       16 bytes
                var pTotalBytes = (long*)(h + 16);  // [       + 16]   uint64      8 bytes      // to skip this map goto [origin + totalBytes]
                *(int*)(h + 24) = data.Count;       // [       + 24]   int32       4 bytes      // number of TOC entries
                *(int*)(h + 28) = 0;                // [       + 28]   int32       4 bytes      // not used
                var toc = (TocEntry*)(h + 32);

                stream.Position += headerSizeInBytes;
                for (var i = 0; i < data.Count; i++)
                {
                    var e = data[i];

                    // align to 16-byte boundary
                    AlignStreamToMultipleOf16();

                    // write lookup table entry (key, offset)
                    toc[i] = new(e.Key.Id, stream.Position - origin);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EncodeValue<T>(this Stream s, in Item item) where T : struct
    {
        var x = (T)item.Data;
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        var ros = MemoryMarshal.CreateReadOnlySpan(ref x, 1);
        var p = MemoryMarshal.Cast<T, byte>(ros);
        s.Write(p);
#elif NETSTANDARD2_0
        var buffer = new byte[Marshal.SizeOf<T>()];
        MemoryMarshal.Write(buffer, ref x);
        s.Write(buffer, 0, buffer.Length);
#endif
    }

    private static void EncodeArray<T>(Stream stream, in Item item) where T : unmanaged
    {
        var xs = (T[])item.Data;

        //var id = item.Def.Id.ToByteArray();
        //stream.Write(id, 0, id.Length);

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EncodeString(this Stream s, in Item item)
    {
        var utf8 = Encoding.UTF8.GetBytes((string)item.Data);
        EncodeArray<byte>(s, new Item(Durable.Primitives.StringUTF8Array, utf8));
    }

    private static void EncodeStringArray(this Stream s, in Item item)
    {
        var xs = (string[])item.Data;
        EncodeValue<int>(s, new(Durable.Primitives.Int32, xs.Length));
        for (int i = 0; i < xs.Length; i++) EncodeString(s, new(Durable.Primitives.StringUTF8, xs[i]));
    }

    private static readonly byte[] _padding = new byte[16];

#endregion

        #region Decoders

    private static readonly Dictionary<Guid, Decoder> _decoders = new()
    {
        { Durable.Primitives.DurableMap2.Id         , DecodeDurableMap2     },

        { Durable.Primitives.StringUTF8.Id          , DecodeString          },
        { Durable.Primitives.StringUTF8Array.Id     , DecodeStringArray     },

        { Durable.Primitives.Int8.Id                , DecodeValue<sbyte>    },
        { Durable.Primitives.Int16.Id               , DecodeValue<short>    },
        { Durable.Primitives.Int32.Id               , DecodeValue<int>      },
        { Durable.Primitives.Int64.Id               , DecodeValue<long>     },
        { Durable.Primitives.UInt8.Id               , DecodeValue<byte>     },
        { Durable.Primitives.UInt16.Id              , DecodeValue<ushort>   },
        { Durable.Primitives.UInt32.Id              , DecodeValue<uint>     },
        { Durable.Primitives.UInt64.Id              , DecodeValue<ulong>    },
        { Durable.Primitives.Float32.Id             , DecodeValue<float>    },
        { Durable.Primitives.Float64.Id             , DecodeValue<double>   },
        { Durable.Primitives.DecimalDotnet.Id       , DecodeValue<decimal>  },
        { Durable.Primitives.GuidDef.Id             , DecodeValue<Guid>     },

        { Durable.Primitives.Int8Array.Id           , DecodeArray<sbyte>    },
        { Durable.Primitives.Int16Array.Id          , DecodeArray<short>    },
        { Durable.Primitives.Int32Array.Id          , DecodeArray<int>      },
        { Durable.Primitives.Int64Array.Id          , DecodeArray<long>     },
        { Durable.Primitives.UInt8Array.Id          , DecodeArray<byte>     },
        { Durable.Primitives.UInt16Array.Id         , DecodeArray<ushort>   },
        { Durable.Primitives.UInt32Array.Id         , DecodeArray<uint>     },
        { Durable.Primitives.UInt64Array.Id         , DecodeArray<ulong>    },
        { Durable.Primitives.Float32Array.Id        , DecodeArray<float>    },
        { Durable.Primitives.Float64Array.Id        , DecodeArray<double>   },
        { Durable.Primitives.DecimalDotnetArray.Id  , DecodeArray<decimal>  },
        { Durable.Primitives.GuidArray.Id           , DecodeArray<Guid>     },

        { Durable.Aardvark.V2d.Id                   , DecodeValue<V2d>      },
        { Durable.Aardvark.V3d.Id                   , DecodeValue<V3d>      },
        { Durable.Aardvark.V4d.Id                   , DecodeValue<V4d>      },
        { Durable.Aardvark.V2f.Id                   , DecodeValue<V2f>      },
        { Durable.Aardvark.V3f.Id                   , DecodeValue<V3f>      },
        { Durable.Aardvark.V4f.Id                   , DecodeValue<V4f>      },
        { Durable.Aardvark.V2i.Id                   , DecodeValue<V2i>      },
        { Durable.Aardvark.V3i.Id                   , DecodeValue<V3i>      },
        { Durable.Aardvark.V4i.Id                   , DecodeValue<V4i>      },
        { Durable.Aardvark.V2l.Id                   , DecodeValue<V2l>      },
        { Durable.Aardvark.V3l.Id                   , DecodeValue<V3l>      },
        { Durable.Aardvark.V4l.Id                   , DecodeValue<V4l>      },
        { Durable.Aardvark.V2dArray.Id              , DecodeArray<V2d>      },
        { Durable.Aardvark.V3dArray.Id              , DecodeArray<V3d>      },
        { Durable.Aardvark.V4dArray.Id              , DecodeArray<V4d>      },
        { Durable.Aardvark.V2fArray.Id              , DecodeArray<V2f>      },
        { Durable.Aardvark.V3fArray.Id              , DecodeArray<V3f>      },
        { Durable.Aardvark.V4fArray.Id              , DecodeArray<V4f>      },
        { Durable.Aardvark.V2iArray.Id              , DecodeArray<V2i>      },
        { Durable.Aardvark.V3iArray.Id              , DecodeArray<V3i>      },
        { Durable.Aardvark.V4iArray.Id              , DecodeArray<V4i>      },
        { Durable.Aardvark.V2lArray.Id              , DecodeArray<V2l>      },
        { Durable.Aardvark.V3lArray.Id              , DecodeArray<V3l>      },
        { Durable.Aardvark.V4lArray.Id              , DecodeArray<V4l>      },

        { Durable.Aardvark.C3b.Id                   , DecodeValue<C4b>      },
        { Durable.Aardvark.C4b.Id                   , DecodeValue<C4b>      },
        { Durable.Aardvark.C3bArray.Id              , DecodeArray<C4b>      },
        { Durable.Aardvark.C4bArray.Id              , DecodeArray<C4b>      },

        { Durable.Aardvark.Cell.Id                  , DecodeValue<Cell>     },
        { Durable.Aardvark.Cell2d.Id                , DecodeValue<Cell2d>   },
        { Durable.Aardvark.CellArray.Id             , DecodeArray<Cell>     },
        { Durable.Aardvark.Cell2dArray.Id           , DecodeArray<Cell2d>   },

        { Durable.Aardvark.Box2d.Id                 , DecodeValue<Box2d>    },
        { Durable.Aardvark.Box2f.Id                 , DecodeValue<Box2f>    },
        { Durable.Aardvark.Box2i.Id                 , DecodeValue<Box2i>    },
        { Durable.Aardvark.Box2l.Id                 , DecodeValue<Box2l>    },
        { Durable.Aardvark.Box3d.Id                 , DecodeValue<Box3d>    },
        { Durable.Aardvark.Box3f.Id                 , DecodeValue<Box3f>    },
        { Durable.Aardvark.Box3i.Id                 , DecodeValue<Box3i>    },
        { Durable.Aardvark.Box3l.Id                 , DecodeValue<Box3l>    },
        { Durable.Aardvark.Box2dArray.Id            , DecodeArray<Box2d>    },
        { Durable.Aardvark.Box2fArray.Id            , DecodeArray<Box2f>    },
        { Durable.Aardvark.Box2iArray.Id            , DecodeArray<Box2i>    },
        { Durable.Aardvark.Box2lArray.Id            , DecodeArray<Box2l>    },
        { Durable.Aardvark.Box3dArray.Id            , DecodeArray<Box3d>    },
        { Durable.Aardvark.Box3fArray.Id            , DecodeArray<Box3f>    },
        { Durable.Aardvark.Box3iArray.Id            , DecodeArray<Box3i>    },
        { Durable.Aardvark.Box3lArray.Id            , DecodeArray<Box3l>    },

        { Durable.Aardvark.Range1b.Id               , DecodeValue<Range1b>  },
        { Durable.Aardvark.Range1d.Id               , DecodeValue<Range1d>  },
        { Durable.Aardvark.Range1f.Id               , DecodeValue<Range1f>  },
        { Durable.Aardvark.Range1i.Id               , DecodeValue<Range1i>  },
        { Durable.Aardvark.Range1l.Id               , DecodeValue<Range1l>  },
        { Durable.Aardvark.Range1s.Id               , DecodeValue<Range1s>  },
        { Durable.Aardvark.Range1sb.Id              , DecodeValue<Range1sb> },
        { Durable.Aardvark.Range1ui.Id              , DecodeValue<Range1ui> },
        { Durable.Aardvark.Range1ul.Id              , DecodeValue<Range1ul> },
        { Durable.Aardvark.Range1us.Id              , DecodeValue<Range1us> },
    };

    private readonly record struct Entry(Durable.Def Key, object Value);
    private readonly record struct TocEntry(Guid Key, long RelativeOffset);

    /// <summary>
    /// Stream is positioned at (origin + 16), that means AFTER the durable type guid.
    /// </summary>
    private static Item DecodeDurableMap2(Stream stream, in Durable.Def def)
    {
        var origin = stream.Position - 16;

        unsafe
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP

            // stream is positioned at [origin + 16], which is after the DurableMap2 Def (16 bytes, GUID).
            // next up:
            //   8 bytes    int64   totalBytes              
            //   4 bytes    int32   number of toc entries   
            //   4 bytes            reserved
            var h = stackalloc byte[16];
            var header = new Span<byte>(h, 16);
            if (stream.Read(header) != 16) throw new Exception($"Failed to read header from DurableMap2. Error 6aec9296-6cf2-4322-9049-313794aad1cf.");

            var totalBytes = *(long*)h;
            var tocEntryCount = *(int*)(h + 8);
            var toc = new TocEntry[tocEntryCount];
            var tocBuffer = MemoryMarshal.Cast<TocEntry, byte>(toc);
            if (stream.Read(tocBuffer) != tocBuffer.Length) throw new Exception($"Failed to read lookup table from DurableMap2. Error 75c27a6d-1a07-4e8b-b7fa-041e8d727ac4.");

            var data = new Dictionary<Durable.Def, object>();
            for (var i = 0; i < toc.Length; i++)
            {
                stream.Position = origin + toc[i].RelativeOffset;
                var (k, v) = Decode(stream, toc[i].Key);
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
                var tocBuffer = new byte[entryCount * sizeof(TocEntry)];
                if (stream.Read(tocBuffer, 0, tocBuffer.Length) != tocBuffer.Length) throw new Exception($"Failed to read lookup table from DurableMap2. Error 71d5d0a4-0be5-4eb8-9d00-c3e7c4d9a517.");

                var toc = MemoryMarshal.Cast<byte, TocEntry>(tocBuffer);

                var data = new Dictionary<Durable.Def, object>();
                for (var i = 0; i < toc.Length; i++)
                {
                    stream.Position = origin + toc[i].RelativeOffset;
                    var (k, v) = Decode(stream);
                    data[k] = v;
                }

                return new(def, data);
            }

#endif
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Item DecodeValue<T>(this Stream s, in Durable.Def def) where T : struct
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        T x = default;
        var span = MemoryMarshal.CreateSpan(ref x, 1);
        var p = MemoryMarshal.Cast<T, byte>(span);
        if (s.Read(p) != p.Length) throw new Exception($"Invariant f96c6373-e9c1-4af2-b253-efa048cfbb2d. {typeof(T)}.");
        return new(def, x);
#elif NETSTANDARD2_0
        var remaining = Marshal.SizeOf<T>();
        var buffer = new byte[remaining];
        var i = 0;
        while (remaining > 0)
        {
            var count = s.Read(buffer, i, remaining);
            i += count;
            remaining -= count;
        }
        return new(def, MemoryMarshal.Read<T>(buffer));
#endif
    }

    /// <summary>
    /// Stream is positioned at (origin + 16), that means AFTER the durable type guid.
    /// </summary>
    private static Item DecodeArray<T>(Stream stream, in Durable.Def def) where T : unmanaged
    {
        unsafe
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Item DecodeString(Stream stream, in Durable.Def def)
    {
        var utf8 = DecodeArray<byte>(stream, Durable.Primitives.StringUTF8Array);
        var s = Encoding.UTF8.GetString((byte[])utf8.Data);
        return new(def, s);
    }

    private static Item DecodeStringArray(Stream stream, in Durable.Def def)
    {
        var count = (int)DecodeValue<int>(stream, Durable.Primitives.Int32).Data;
        var xs = new string[count];
        for (int i = 0; i < xs.Length; i++) xs[i] = (string)(DecodeString(stream, Durable.Primitives.StringUTF8).Data);
        return new(def, xs);
    }

#endregion
}