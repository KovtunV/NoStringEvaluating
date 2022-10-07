using System.Collections.Generic;
using System.Text;
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
        var sb = new StringBuilder();

        for (int i = 0; i < args.Count; i++)
        {
            var arg = args[i];
            if (arg.IsWordList)
            {
                var wordList = arg.WordList;
                sb.Append(string.Join(string.Empty, wordList));
            }
            else if (arg.IsNumberList)
            {
                var numberList = arg.NumberList;
                sb.Append(string.Join(string.Empty, numberList));
            }
            else
            {
                sb.Append(arg.ToString());
            }
        }

        return factory.Word.Create(sb.ToString());
    }
}
