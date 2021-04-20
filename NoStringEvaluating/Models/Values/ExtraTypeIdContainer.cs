using System;
using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Models.Values
{
    /// <summary>
    /// Contains lists of ids for extra types
    /// </summary>
    public class ExtraTypeIdContainer : IDisposable
    {
        private ObjectPool<ExtraTypeIdContainer> _pool;

        /// <summary>
        /// WordIds
        /// </summary>
        public List<int> WordIds { get; }

        /// <summary>
        /// DateTimeIds
        /// </summary>
        public List<int> DateTimeIds { get; }

        /// <summary>
        /// WordListIds
        /// </summary>
        public List<int> WordListIds { get; }

        /// <summary>
        /// NumberListIds
        /// </summary>
        public List<int> NumberListIds { get; }

        /// <summary>
        /// Contains lists of ids for extra types
        /// </summary>
        public ExtraTypeIdContainer()
        {
            WordIds = new List<int>();
            DateTimeIds = new List<int>();
            WordListIds = new List<int>();
            NumberListIds = new List<int>();
        }

        internal ExtraTypeIdContainer SetPool(ObjectPool<ExtraTypeIdContainer> pool)
        {
            _pool = pool;

            return this;
        }

        internal ExtraTypeIdContainer Clear()
        {
            // Prevent dirty collection
            WordIds.Clear();
            DateTimeIds.Clear();
            WordListIds.Clear();
            NumberListIds.Clear();

            return this;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Word
            WordKeeper.Instance.Clear(WordIds);
            WordIds.Clear();

            // DateTime
            DateTimeKeeper.Instance.Clear(DateTimeIds);
            DateTimeIds.Clear();

            // WordList
            WordListKeeper.Instance.Clear(WordListIds);
            WordListIds.Clear();

            // NumberList
            NumberListKeeper.Instance.Clear(NumberListIds);
            NumberListIds.Clear();

            // Return itself
            _pool.Return(this);
        }
    }
}
