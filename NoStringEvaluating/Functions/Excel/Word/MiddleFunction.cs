using System;
using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// Returns any substring from the middle of a string
    /// <para>Middle(myWord; indexStart; numberChars) or Middle(myWord; indexStart; wordEnd)  or Middle(myWord; wordStart; numberChars) or Middle(myWord; wordStart; wordEnd)</para>
    /// </summary>
    public class MiddleFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MIDDLE";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var wordFactory = factory.Word();

            var word = args[0].GetWord();
            var argStart = args[1];
            var argEnd = args[2];

            if (argStart.IsNumber && argEnd.IsNumber)
            {
                return CropNumberNumber(word, argStart, argEnd, wordFactory);
            }

            if (argStart.IsNumber && argEnd.IsWord)
            {
                return CropNumberWord(word, argStart, argEnd, wordFactory);
            }

            if (argStart.IsWord && argEnd.IsNumber)
            {
                return CropWordNumber(word, argStart, argEnd, wordFactory);
            }

            if (argStart.IsWord && argEnd.IsWord)
            {
                return CropWordWord(word, argStart, argEnd, wordFactory);
            }

            return wordFactory.Empty();
        }

        private InternalEvaluatorValue CropWordWord(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd, WordFactory wordFactory)
        {
            var wordStart = argStart.GetWord();
            var wordEnd = argEnd.GetWord();

            var wordStartIndex = word.IndexOf(wordStart, StringComparison.Ordinal);
            if (wordStartIndex == -1)
                return wordFactory.Empty();
            wordStartIndex += wordStart.Length;

            var wordEndIndex = word.AsSpan().Slice(wordStartIndex).IndexOf(wordEnd, StringComparison.Ordinal);
            if (wordEndIndex == -1)
                return wordFactory.Empty();
            wordEndIndex += wordStartIndex;

            return wordFactory.Create(word[wordStartIndex..wordEndIndex]);
        }

        private InternalEvaluatorValue CropWordNumber(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd, WordFactory wordFactory)
        {
            var wordStart = argStart.GetWord();
            var argEndInt = (int)argEnd.Number;

            var wordStartIndex = word.IndexOf(wordStart, StringComparison.Ordinal);
            if (wordStartIndex == -1)
                return wordFactory.Empty();

            wordStartIndex += wordStart.Length;

            if (wordStartIndex + argEndInt > word.Length)
                return wordFactory.Create(word[wordStartIndex..]);

            return wordFactory.Create(word[wordStartIndex..(wordStartIndex + argEndInt)]);
        }

        private InternalEvaluatorValue CropNumberWord(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd, WordFactory wordFactory)
        {
            var argStartInt = (int)argStart.Number;
            var wordEnd = argEnd.GetWord();

            if (argStartInt < 0 || argStartInt > word.Length)
                return wordFactory.Empty();

            var wordEndIndex = word.AsSpan().Slice(argStartInt).IndexOf(wordEnd, StringComparison.Ordinal);
            if (wordEndIndex == -1)
                return wordFactory.Empty();

            return wordFactory.Create(word[argStartInt..(argStartInt + wordEndIndex)]);
        }

        private InternalEvaluatorValue CropNumberNumber(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd, WordFactory wordFactory)
        {
            var argStartInt = (int)argStart.Number;
            var argEndInt = (int)argEnd.Number;

            if (argStartInt < 0 || argEndInt < 0)
                return wordFactory.Empty();

            if (argStartInt > word.Length)
                return wordFactory.Empty();

            if (argStartInt + argEndInt > word.Length)
                return wordFactory.Create(word[argStartInt..]);

            return wordFactory.Create(word[argStartInt..(argStartInt + argEndInt)]);
        }
    }
}
