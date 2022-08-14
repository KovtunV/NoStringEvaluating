using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Concatenates all members of a text list and returns a text string
/// <para>Implode(myList) or Implode(myList; separator) or Implode(myList; 5; 'my wordd'; separator) last value is separator</para>
/// <para>separator by default is empty ""</para>
/// </summary>
public sealed class ImplodeFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "IMPLODE";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var separator = args.Count > 1 ? args[^1].GetWord() : string.Empty;
        var res = string.Join(separator, GetLoop(args));
        return factory.Word.Create(res);
    }

    private static IEnumerable<string> GetLoop(List<InternalEvaluatorValue> args)
    {
        var n = args.Count == 1 ? 1 : args.Count - 1;
        for (int i = 0; i < n; i++)
        {
            var arg = args[i];

            if (arg.IsWordList)
            {
                var wordList = arg.GetWordList();
                for (int j = 0; j < wordList.Count; j++)
                {
                    yield return wordList[j];
                }
            }
            else if (arg.IsNumberList)
            {
                var numberList = arg.GetNumberList();
                for (int j = 0; j < numberList.Count; j++)
                {
                    yield return numberList[j].ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                yield return arg.ToString();
            }
        }
    }
}
