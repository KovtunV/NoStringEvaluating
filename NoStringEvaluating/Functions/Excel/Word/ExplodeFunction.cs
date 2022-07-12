using System;
using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Returns a text list composed of the elements of a text string
/// <para>Explode(myWord) or Explode(myWord; separator)</para>
/// <para>separator by default is white space " "</para>
/// </summary>
public class ExplodeFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "EXPLODE";

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var separator = args.Count > 1 ? args[1].GetWord() : " ";
        var res = args[0].GetWord().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
        return factory.WordList().Create(res);
    }
}
