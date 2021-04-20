using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Factories
{
    /// <summary>
    /// WordFactory
    /// </summary>
    public readonly struct WordFactory
    {
        private readonly List<int> _ids;

        /// <summary>
        /// WordFactory
        /// </summary>
        public WordFactory(List<int> ids)
        {
            _ids = ids;
        }

        /// <summary>
        /// Concates two values to word
        /// </summary>
        public InternalEvaluatorValue Concat(InternalEvaluatorValue a, InternalEvaluatorValue b)
        {
            var word = $"{a}{b}";
            return Create(word);
        }

        /// <summary>
        /// Creates default
        /// </summary>
        public InternalEvaluatorValue Empty()
        {
            return Create(string.Empty);
        }

        /// <summary>
        /// Creates word value
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public InternalEvaluatorValue Create(string word)
        {
            // Save to keeper
            var id = WordKeeper.Instance.Save(word);

            // Save to scouped list
            _ids.Add(id);

            // Create value
            return new InternalEvaluatorValue(id, ValueTypeKey.Word);
        }
    }
}