using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word;

/// <summary>
/// Capitalizes the first letter in each word of a text
/// <para>Proper(myWord)</para>
/// </summary>
public sealed class ProperFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "PROPER";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var arg = args[0];

        if (arg.IsWord)
        {
            var word = arg.GetWord();

            var res = Proper(word);
            return factory.Word().Create(res);
        }

        if (arg.IsWordList)
        {
            // Copy
            var wordList = arg.GetWordList().ToList();
            for (int i = 0; i < wordList.Count; i++)
            {
                wordList[i] = Proper(wordList[i]);
            }

            return factory.WordList().Create(wordList);
        }

        return double.NaN;
    }

    private string Proper(string word)
    {
        if (HasCapital(word))
        {
            word = word.ToLowerInvariant();
        }

        var res = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word);
        return res;
    }

    private bool HasCapital(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
                return true;
        }

        return false;
    }
}
