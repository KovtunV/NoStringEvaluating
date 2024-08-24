﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Returns any substring from the middle of a string
/// <para>Middle(myWord; indexStart; numberChars) or Middle(myWord; indexStart; wordEnd)  or Middle(myWord; wordStart; numberChars) or Middle(myWord; wordStart; wordEnd)</para>
/// </summary>
public sealed class MiddleFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "MIDDLE";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var valArg = args[0];
        var argStart = args[1];
        var argEnd = args[2];

        if (valArg.IsWord)
        {
            var word = valArg.Word;
            var wordRes = MiddleWord(argStart, argEnd, word);

            return factory.Word.Create(wordRes);
        }

        if (valArg.IsWordList)
        {
            var wordList = valArg.WordList.ToList();
            for (int i = 0; i < wordList.Count; i++)
            {
                wordList[i] = MiddleWord(argStart, argEnd, wordList[i]);
            }

            return factory.WordList.Create(wordList);
        }

        return default;
    }

    private static string MiddleWord(InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd, string word)
    {
        if (argStart.IsNumber && argEnd.IsNumber)
        {
            return CropNumberNumber(word, argStart, argEnd);
        }

        if (argStart.IsNumber && argEnd.IsWord)
        {
            return CropNumberWord(word, argStart, argEnd);
        }

        if (argStart.IsWord && argEnd.IsNumber)
        {
            return CropWordNumber(word, argStart, argEnd);
        }

        if (argStart.IsWord && argEnd.IsWord)
        {
            return CropWordWord(word, argStart, argEnd);
        }

        return string.Empty;
    }

    private static string CropWordWord(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd)
    {
        var wordStart = argStart.Word;
        var wordEnd = argEnd.Word;

        var wordStartIndex = word.IndexOf(wordStart, StringComparison.Ordinal);
        if (wordStartIndex == -1)
        {
            return string.Empty;
        }

        wordStartIndex += wordStart.Length;

        var wordEndIndex = word.AsSpan()[wordStartIndex..].IndexOf(wordEnd, StringComparison.Ordinal);
        if (wordEndIndex == -1)
        {
            return string.Empty;
        }

        wordEndIndex += wordStartIndex;

        return word[wordStartIndex..wordEndIndex];
    }

    private static string CropWordNumber(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd)
    {
        var wordStart = argStart.Word;
        var argEndInt = (int)argEnd.Number;

        var wordStartIndex = word.IndexOf(wordStart, StringComparison.Ordinal);
        if (wordStartIndex == -1)
        {
            return string.Empty;
        }

        wordStartIndex += wordStart.Length;

        if (wordStartIndex + argEndInt > word.Length)
        {
            return word[wordStartIndex..];
        }

        return word[wordStartIndex..(wordStartIndex + argEndInt)];
    }

    private static string CropNumberWord(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd)
    {
        var argStartInt = (int)argStart.Number;
        var wordEnd = argEnd.Word;

        if (argStartInt < 0 || argStartInt > word.Length)
        {
            return string.Empty;
        }

        var wordEndIndex = word.AsSpan()[argStartInt..].IndexOf(wordEnd, StringComparison.Ordinal);
        if (wordEndIndex == -1)
        {
            return string.Empty;
        }

        return word[argStartInt..(argStartInt + wordEndIndex)];
    }

    private static string CropNumberNumber(string word, InternalEvaluatorValue argStart, InternalEvaluatorValue argEnd)
    {
        var argStartInt = (int)argStart.Number;
        var argEndInt = (int)argEnd.Number;

        if (argStartInt < 0 || argEndInt < 0)
        {
            return string.Empty;
        }

        if (argStartInt > word.Length)
        {
            return string.Empty;
        }

        if (argStartInt + argEndInt > word.Length)
        {
            return word[argStartInt..];
        }

        return word[argStartInt..(argStartInt + argEndInt)];
    }
}
