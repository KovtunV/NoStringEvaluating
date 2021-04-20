using System;
using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Factories
{
    /// <summary>
    /// DateTimeFactory
    /// </summary>
    public readonly struct DateTimeFactory
    {
        private readonly List<int> _ids;

        /// <summary>
        /// DateTimeFactory
        /// </summary>
        public DateTimeFactory(List<int> ids)
        {
            _ids = ids;
        }

        /// <summary>
        /// Creates default
        /// </summary>
        public InternalEvaluatorValue Empty()
        {
            return Create(DateTime.MinValue);
        }

        /// <summary>
        /// Creates dateTime value
        /// </summary>
        public InternalEvaluatorValue Create(DateTime date)
        {
            // Save to keeper
            var id = DateTimeKeeper.Instance.Save(date);

            // Save to scouped list
            _ids.Add(id);

            // Create value
            return new InternalEvaluatorValue(id, ValueTypeKey.DateTime);
        }
    }
}
