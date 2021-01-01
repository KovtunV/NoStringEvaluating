using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluating.Services
{
    /// <summary>
    /// Word index tracker
    /// </summary>
    public struct IndexWatcher
    {
        /// <summary>
        /// Start word index
        /// </summary>
        public int? StartIndex { get; private set; }

        /// <summary>
        /// Word length
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Has start index
        /// </summary>
        public bool InProcess => StartIndex.HasValue;

        /// <summary>
        /// Remember index
        /// </summary>
        public void Remember(int index)
        {
            if (!StartIndex.HasValue)
                StartIndex = index;

            Length++;
        }
    }
}
