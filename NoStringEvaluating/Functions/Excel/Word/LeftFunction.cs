using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// Searches a string from left to right and returns the leftmost characters of the string
    /// <para>Left(myWord) or Left(myWord; numberOfChars) or Left(myWord; subWord) </para>
    /// </summary>
    public class LeftFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "LEFT";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var wordFactory = factory.Word();
            var word = args[0].GetWord();
            if (args.Count == 1)
            {
                var wordRes = word.Length > 1 ? word[..1] : string.Empty;
                return wordFactory.Create(wordRes);
            }

            var pattern = args[1];

            // Number
            if (pattern.IsNumber)
            {
                if (pattern.Number < 0)
                {
                    return wordFactory.Empty();
                }

                if (pattern.Number >= word.Length)
                {
                    return wordFactory.Create(word);
                }

                var wordRes = word[..(int)pattern.Number];
                return wordFactory.Create(wordRes);
            }

            // Word
            var patternWord = pattern.GetWord();
            if (patternWord.Length == 0)
            {
                return wordFactory.Empty();
            }

            var subWordIndex = word.IndexOf(patternWord);
            if(subWordIndex == -1)
            {
                return wordFactory.Create(word);
            }

            return wordFactory.Create(word[..subWordIndex]);
        }
    }
}
