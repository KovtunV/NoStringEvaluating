using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Factories
{
    /// <summary>
    /// WordListFactory
    /// </summary>
    public readonly struct WordListFactory
    {
        private readonly List<int> _ids;

        /// <summary>
        /// WordListFactory
        /// </summary>
        public WordListFactory(List<int> ids)
        {
            _ids = ids;
        }

        /// <summary>
        /// Creates default
        /// </summary>
        /// <returns></returns>
        public InternalEvaluatorValue Empty()
        {
            return Create(new List<string>());
        }

        /// <summary>
        /// Creates string List value
        /// </summary>
        public InternalEvaluatorValue Create(List<string> wordList)
        {
            // Save to keeper
            var id = WordListKeeper.Instance.Save(wordList);

            // Save to scouped list
            _ids.Add(id);

            // Create value
            return new InternalEvaluatorValue(id, ValueTypeKey.WordList);
        }
    }
}