﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Aardvark.Data
{
    /// <summary>
    /// </summary>
    public static partial class Durable
    {
        /// <summary>
        /// Version.
        /// </summary>
        public const string Version = "0.3.15";

        /// <summary></summary>
        public static void Init()
        {
            _ = Primitives.Int8;
            _ = Aardvark.C3ui;
            _ = Generic.Classifications1b;
            _ = Octree.AveragePointDistance;
        }

        /// <summary></summary>
        public static Def Get(Guid key) => Def.Get(key);

        /// <summary></summary>
        public static bool TryGet(Guid key, out Def def) => Def.TryGet(key, out def);

        /// <summary></summary>
        public class Def : IEquatable<Def>, IComparable, IComparable<Def>
        {
            /// <summary></summary>
            public readonly Guid Id;
            /// <summary></summary>
            public readonly string Name;
            /// <summary></summary>
            public readonly string Description;
            /// <summary></summary>
            public readonly Guid Type;
            /// <summary></summary>
            public readonly bool IsArray;

            /// <summary></summary>
            public Def(Guid id, string name, string description, Guid type, bool isArray)
                : this(id, name, description, type, isArray, verbose: false) { }

            /// <summary></summary>
            public Def(Guid id, string name, string description, Guid type, bool isArray, bool verbose)
            {
                lock (defs)
                {
                    if (defs.TryGetValue(id, out var x))
                    {
                        if (x.Id != id) throw new Exception("Invariant 70830314-1cda-4270-84db-eec0d1fca525.");

                        if (x.Type != type || x.IsArray != isArray)
                        {
                            throw new InvalidOperationException(
                                $"Conflicting definition of Durable.Def {id}.\n" +
                                $"NEW: Def(id: {id}, name: {name}, description: {description}, type: {type}, isArray: {isArray}).\n" +
                                $"OLD: Def(id: {x.Id}, name: {x.Name}, description: {x.Description}, type: {x.Type}, isArray: {x.IsArray}).\n"
                                );
                        }
                        else
                        {
                            if (verbose)
                            {
                                Console.WriteLine(
                                    $"[WARNING] Redefinition of existing Durable.Def {id}.\n" +
                                    $"          NEW: Def(id: {id}, name: {name}, description: {description}, type: {type}, isArray: {isArray}).\n" +
                                    $"          OLD: Def(id: {x.Id}, name: {x.Name}, description: {x.Description}, type: {x.Type}, isArray: {x.IsArray})."
                                    );
                            }
                        }
                    }

                    Id = id;
                    Name = name;
                    Description = description;
                    Type = type;
                    IsArray = isArray;

                    defs[id] = this;
                }
            }

            /// <summary></summary>
            public Guid PrimitiveType => Type == Guid.Empty
                ? Id
                : (defs.TryGetValue(Type, out var def) ? def.PrimitiveType : throw new Exception(
                    $"Unknown durable type {Type}. Error 7a5da687-38af-45c3-8872-b8cf4b93b2a5."
                    ));

            /// <summary></summary>
            public Def TypeDef => Get(Type);

            /// <summary></summary>
            public Def PrimitiveTypeDef => Get(PrimitiveType);

            /// <summary></summary>
            public override string ToString() => $"[{Name}, {Id}]";

            /// <summary></summary>
            public override int GetHashCode() => Id.GetHashCode();

            /// <summary></summary>
            public override bool Equals(object obj) => obj is Def other && Id == other.Id;

            /// <summary></summary>
            public bool Equals(Def other) => !(other is null || Id != other.Id);

            /// <summary></summary>
            public int CompareTo(object obj)
                => obj is Def other
                    ? CompareTo(other)
                    : throw new ArgumentException($"Can't compare Def with {obj?.GetType()}.", nameof(obj))
                    ;

            /// <summary></summary>
            public int CompareTo(Def other) => other is null ? 1 : Id.CompareTo(other.Id);

            /// <summary></summary>
            public static Def Get(Guid key) => defs[key];

            /// <summary></summary>
            public static bool TryGet(Guid key, out Def def) => defs.TryGetValue(key, out def);

            /// <summary></summary>
            public static Def[] AllDefs => defs.Values.ToArray();

            /// <summary>
            /// Adds an alias key for given definition.
            /// This allows apps to remap obsolete keys to the actual/current definition.
            /// </summary>
            /// <param name="key">Alias id.</param>
            /// <param name="definition">Aliased definition.</param>
            public static void AddAlias(Guid key, Def definition)
            {
                lock (defs)
                {
                    defs[key] = definition;
                }
            }

            private static readonly Dictionary<Guid, Def> defs = new();
        }
    }
}
