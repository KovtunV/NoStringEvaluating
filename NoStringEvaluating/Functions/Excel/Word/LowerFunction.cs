using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Converts text to lowercase
/// <para>Lower(myWord) or Lower(myWordList)</para>
/// </summary>
public sealed class LowerFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "LOWER";

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
            var res = arg.Word.ToLowerInvariant();
            return factory.Word.Create(res);
        }

        if (arg.IsWordList)
        {
            var wordList = arg.WordList;
            var wordListRes = wordList.Select(s => s.ToLowerInvariant()).ToList();
            return factory.WordList.Create(wordListRes);
        }

        return default;
    }
}
