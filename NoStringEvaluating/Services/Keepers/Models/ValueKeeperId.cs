using NoStringEvaluating.Models.Values;
using System;

namespace NoStringEvaluating.Services.Keepers.Models
{
    /// <summary>
    /// Id struct for value keepers
    /// </summary>
    public readonly struct ValueKeeperId : IEquatable<ValueKeeperId>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Type key
        /// </summary>
        public ValueTypeKey TypeKey { get; }

        /// <summary>
        /// Id struct for value keepers
        /// </summary>
        public ValueKeeperId(int id, ValueTypeKey typeKey)
        {
            Id = id;
            TypeKey = typeKey;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id}__{TypeKey}";
        }

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is ValueKeeperId id && Equals(id);
        }

        /// <summary>
        /// Equals
        /// </summary>
        public bool Equals(ValueKeeperId other)
        {
            return Id == other.Id &&
                   TypeKey == other.TypeKey;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TypeKey);
        }
    }
}
