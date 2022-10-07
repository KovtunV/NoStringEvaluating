using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Replaces characters within text
/// <para>Replace(myWord; oldPart; newPart) or Replace(myList; oldPart; newPart)</para>
/// </summary>
public sealed class ReplaceFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "REPLACE";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordFactory = factory.Word;

        var sourseArg = args[0];
        var oldWord = args[1].Word;
        var newWord = args[2].Word;

        // Word
        if (sourseArg.IsWord)
        {
            var word = sourseArg.Word;
            var res = word.Replace(oldWord, newWord);
            return wordFactory.Create(res);
        }

        if (sourseArg.IsWordList)
        {
            var sourseList = sourseArg.WordList;

            var resList = new List<string>(sourseList.Count);
            for (int i = 0; i < sourseList.Count; i++)
            {
                resList.Add(sourseList[i].Replace(oldWord, newWord));
            }

            return factory.WordList.Create(resList);
        }

        return default;
    }
}
