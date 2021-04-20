using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Factories
{
    /// <summary>
    /// NumberListFactory
    /// </summary>
    public readonly struct NumberListFactory
    {
        private readonly List<int> _ids;

        /// <summary>
        /// NumberListFactory
        /// </summary>
        public NumberListFactory(List<int> ids)
        {
            _ids = ids;
        }

        /// <summary>
        /// Creates default
        /// </summary>
        public InternalEvaluatorValue Empty()
        {
            return Create(new List<double>());
        }

        /// <summary>
        /// Creates double List value
        /// </summary>
        public InternalEvaluatorValue Create(List<double> numberList)
        {
            // Save to keeper
            var id = NumberListKeeper.Instance.Save(numberList);

            // Save to scouped list
            _ids.Add(id);

            // Create value
            return new InternalEvaluatorValue(id, ValueTypeKey.NumberList);
        }
    }
}
