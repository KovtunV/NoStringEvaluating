using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Replaces characters within text
/// <para>Replace(myWord; oldPart; newPart) or Replace(myList; oldPart; newPart)</para>
/// </summary>
public class ReplaceFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "REPLACE";

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordFactory = factory.Word();

        var sourseArg = args[0];
        var oldWord = args[1].GetWord();
        var newWord = args[2].GetWord();

        // Word
        if (sourseArg.IsWord)
        {
            var word = sourseArg.GetWord();
            var res = word.Replace(oldWord, newWord);
            return wordFactory.Create(res);
        }

        if (sourseArg.IsWordList)
        {
            var sourseList = sourseArg.GetWordList();

            var resList = new List<string>(sourseList.Count);
            for (int i = 0; i < sourseList.Count; i++)
            {
                resList.Add(sourseList[i].Replace(oldWord, newWord));
            }

            return factory.WordList().Create(resList);
        }

        return wordFactory.Empty();
    }
}
