using System;
using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel;

/// <summary>
/// SORT(myList; sortType)
/// <para>sortType: true - asc, false - desc</para> 
/// </summary>
public sealed class SortFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "SORT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var ascSort = args.Count <= 1 || args[1].Boolean;
        var list = args[0];

        if (list.IsWordList)
        {
            // Copy list
            var wordList = list.GetWordList().ToList();

            if (ascSort)
                wordList.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));
            else
                wordList.Sort((a, b) => string.Compare(b, a, StringComparison.Ordinal));

            return factory.WordList.Create(wordList);
        }

        if (list.IsNumberList)
        {
            // Copy list
            var numberList = list.GetNumberList().ToList();

            if (ascSort)
                numberList.Sort((a, b) => a.CompareTo(b));
            else
                numberList.Sort((a, b) => b.CompareTo(a));

            return factory.NumberList.Create(numberList);
        }

        return default;
    }
}
