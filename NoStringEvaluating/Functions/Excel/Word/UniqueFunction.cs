using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Unique(myList) or Unique(myList; true)
/// <para>if second parameter is true then returns only qnique</para>
/// <para>if second parameter is false then returns list without doubles</para>
/// </summary>
public class UniqueFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "UNIQUE";

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordListFactory = factory.WordList();

        var wordList = args[0].GetWordList();
        var qniqueOnly = args.Count > 1 && args[^1].Number != 0;

        var group = wordList.GroupBy(q => q);
        if (qniqueOnly)
        {
            var resQniqueOnly = group.Where(w => w.Count() == 1).SelectMany(s => s).ToList();
            return wordListFactory.Create(resQniqueOnly);
        }

        var resAllQnique = group.Select(s => s.First()).ToList();
        return wordListFactory.Create(resAllQnique);
    }
}
