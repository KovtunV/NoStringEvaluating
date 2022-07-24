using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Concates values
/// <para>Concat(56; '_myWord') or Concat(myList; myArg; 45; myList2)</para>
/// </summary>
public sealed class ConcatFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "CONCAT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var firstArg = args[0];
        string res;

        if (firstArg.IsWordList)
        {
            var wordList = firstArg.GetWordList();
            res = string.Join(string.Empty, wordList);
        }
        else if (firstArg.IsNumberList)
        {
            var numberList = firstArg.GetNumberList();
            res = string.Join(string.Empty, numberList);
        }
        else
        {
            res = firstArg.ToString();
        }

        for (int i = 1; i < args.Count; i++)
        {
            var arg = args[i];
            if (arg.IsWordList)
            {
                var wordList = arg.GetWordList();
                res += string.Join(string.Empty, wordList);
            }
            else if (arg.IsNumberList)
            {
                var numberList = arg.GetNumberList();
                res += string.Join(string.Empty, numberList);
            }
            else
            {
                res += arg.ToString();
            }
        }

        return factory.Word().Create(res);
    }
}
