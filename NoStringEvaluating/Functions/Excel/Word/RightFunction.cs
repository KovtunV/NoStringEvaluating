using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// Searches a string from right to left and returns the rightmost characters of the string
    /// <para>Right(myWord) or Right(myWord; numberOfChars) or Right(myWord; subWord) </para>
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
            var valArg = args[0];

            if (valArg.IsWord)
            {
                var word = valArg.GetWord();
                var wordRes = RightWord(args, word);

                return factory.Word().Create(wordRes);
            }

            if (valArg.IsWordList)
            {
                var wordList = valArg.GetWordList().ToList();
                for (int i = 0; i < wordList.Count; i++)
                {
                    wordList[i] = RightWord(args, wordList[i]);
                }

                return factory.WordList().Create(wordList);
            }

            return double.NaN;
        }

        private string RightWord(List<InternalEvaluatorValue> args, string word)
        {
            if (args.Count == 1)
            {
                var wordRes = word.Length > 1 ? word[^1..] : string.Empty;
                return wordRes;
            }

            var pattern = args[1];

            // Number
            if (pattern.IsNumber)
            {
                var numberInt = (int)pattern.Number;

                if (pattern.Number < 0)
                {
                    return string.Empty;
                }

                if (pattern.Number >= word.Length)
                {
                    return word;
                }

                var wordRes = word[^numberInt..];
                return wordRes;
            }

            // Word
            var patternWord = pattern.GetWord();
            if (patternWord.Length == 0)
            {
                return string.Empty;
            }

            var subWordIndex = word.IndexOf(patternWord);
            if (subWordIndex == -1)
            {
                return word;
            }

            return word[(subWordIndex + patternWord.Length)..];
        }

    }
}
