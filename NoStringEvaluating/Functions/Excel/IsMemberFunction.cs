using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel;

/// <summary>
/// IsMember(myList; item)
/// </summary>
public sealed class IsMemberFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ISMEMBER";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var argList = args[0];
        var argItem = args[1];

        if (argList.IsWordList)
        {
            if (!argItem.IsWord) return 0;

            var wordList = argList.GetWordList();
            return wordList.Contains(argItem.GetWord()) ? 1 : 0;
        }

        if (argList.IsNumberList)
        {
            if (!argItem.IsNumber) return 0;

            var numberList = argList.GetNumberList();
            return numberList.Contains(argItem.Number) ? 1 : 0;
        }

        return 0;
    }
}
