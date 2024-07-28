﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Converts text to uppercase
/// <para>Upper(myWord) or Upper(myWordList)</para>
/// </summary>
public sealed class UpperFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "UPPER";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var arg = args[0];

        if (arg.IsWord)
        {
            var res = arg.Word.ToUpperInvariant();
            return factory.Word.Create(res);
        }

        if (arg.IsWordList)
        {
            var wordList = arg.WordList;
            var wordListRes = wordList.Select(s => s.ToUpperInvariant()).ToList();
            return factory.WordList.Create(wordListRes);
        }

        return default;
    }
}
