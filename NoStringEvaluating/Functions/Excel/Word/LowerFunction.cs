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
public class LowerFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "LOWER";

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var arg = args[0];

        if (arg.IsWord)
        {
            var res = arg.GetWord().ToLowerInvariant();
            return factory.Word().Create(res);
        }

        if (arg.IsWordList)
        {
            var wordList = arg.GetWordList();
            var wordListRes = wordList.Select(s => s.ToLowerInvariant()).ToList();
            return factory.WordList().Create(wordListRes);
        }

        return factory.Word().Empty();
    }
}
