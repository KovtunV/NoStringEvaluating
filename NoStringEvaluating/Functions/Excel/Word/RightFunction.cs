using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// Searches a string from right to left and returns the rightmost characters of the string
    /// <para>Right(myWord) or Right(myWord; numberOfChars) or Right(myWord; wordNeededChars) </para>
    /// </summary>
    public class RightFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "RIGHT";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var wordFactory = factory.Word();

            var word = args[0].GetWord();
            if (args.Count == 1)
            {
                var wordRes = word.Length > 1 ? word[^1..] : string.Empty;
                return wordFactory.Create(wordRes);
            }

            var pattern = args[1];

            // Number
            if (pattern.IsNumber)
            {
                var numberInt = (int)pattern.Number;

                if (pattern.Number < 0)
                {
                    return wordFactory.Empty();
                }

                if (pattern.Number >= word.Length)
                {
                    return wordFactory.Create(word);
                }

                var wordRes = word[^numberInt..];
                return wordFactory.Create(wordRes);
            }

            // Word
            var patternWord = pattern.GetWord();
            if (patternWord.Length == 0)
            {
                return wordFactory.Empty();
            }

            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (!patternWord.Contains(word[i]))
                {
                    return wordFactory.Create(word[(i + 1)..]);
                } 
                
                if (i - 1 == 0)
                {
                    return wordFactory.Create(word);
                }
            }

            return wordFactory.Empty();
        }
    }
}
