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
            if (!argItem.IsWord)
            {
                return factory.Boolean.Create(false);
            }

            var wordList = argList.WordList;
            return factory.Boolean.Create(wordList.Contains(argItem.Word));
        }

        if (argList.IsNumberList)
        {
            if (!argItem.IsNumber)
            {
                return factory.Boolean.Create(false);
            }

            var numberList = argList.NumberList;
            return factory.Boolean.Create(numberList.Contains(argItem.Number));
        }

        return factory.Boolean.Create(false);
    }
}
