using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Unique(myList) or Unique(myList; true)
/// <para>if second parameter is true then returns only unique</para>
/// <para>if second parameter is false then returns list without doubles</para>
/// </summary>
public sealed class UniqueFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "UNIQUE";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordListFactory = factory.WordList;

        var wordList = args[0].GetWordList();
        var uniqueOnly = args.Count > 1 && args[^1];

        var group = wordList.GroupBy(q => q);
        if (uniqueOnly)
        {
            var resUniqueOnly = group.Where(w => w.Count() == 1).SelectMany(s => s).ToList();
            return wordListFactory.Create(resUniqueOnly);
        }

        var resAllUnique = group.Select(s => s.First()).ToList();
        return wordListFactory.Create(resAllUnique);
    }
}
