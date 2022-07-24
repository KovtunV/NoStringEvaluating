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
    public bool CanHandleNullArguments { get; }

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
            return factory.Boolean().Create(wordList.Contains(argItem.GetWord()));
        }

        if (argList.IsNumberList)
        {
            if (!argItem.IsNumber) return 0;

            var numberList = argList.GetNumberList();
            return factory.Boolean().Create(numberList.Contains(argItem.Number));
        }

        return 0;
    }
}
