using System;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories
{
    /// <summary>
    /// Factory fro values
    /// </summary>
    public readonly struct ValueFactory
    {
        private readonly ExtraTypeIdContainer _idContainer;

        internal ValueFactory(ExtraTypeIdContainer idContainer)
        {
            _idContainer = idContainer;
        }

        /// <summary>
        /// Returns word factory
        /// </summary>
        public WordFactory Word()
        {
            return new WordFactory(_idContainer.WordIds);
        }

        /// <summary>
        /// Returns dateTime factory
        /// </summary>
        public DateTimeFactory DateTime()
        {
            return new DateTimeFactory(_idContainer.DateTimeIds);
        }

        /// <summary>
        /// Returns wordList factory
        /// </summary>
        public WordListFactory WordList()
        {
            return new WordListFactory(_idContainer.WordListIds);
        }

        /// <summary>
        /// Returns numberList factory
        /// </summary>
        public NumberListFactory NumberList()
        {
            return new NumberListFactory(_idContainer.NumberListIds);
        }
        
        internal InternalEvaluatorValue Create(EvaluatorValue val)
        {
            if (val.TypeKey == ValueTypeKey.Number)
            {
                return val.Number;
            }

            if (val.TypeKey == ValueTypeKey.Word)
            {
                return Word().Create(val.Word);
            }

            if (val.TypeKey == ValueTypeKey.DateTime)
            {
                return DateTime().Create(val.DateTime);
            }

            if (val.TypeKey == ValueTypeKey.WordList)
            {
                return WordList().Create(val.WordList);
            }

            if (val.TypeKey == ValueTypeKey.NumberList)
            {
                return NumberList().Create(val.NumberList);
            }

            throw new InvalidCastException($"Can't cast {nameof(EvaluatorValue)} with the typeKey = \"{val.TypeKey}\" to {nameof(InternalEvaluatorValue)}");
        }
    }

}
