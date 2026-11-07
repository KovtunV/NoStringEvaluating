using NoStringEvaluating.Extensions;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Negation Reader
/// </summary>
public static class NegationReader
{
    /// <summary>
    /// Read negation
    /// </summary>
    public static int ReadNegation(ReadOnlySpan<char> formula, int index, out bool isNegationed)
    {
        var isNegationedLocal = false;
        var localIndex = index;

        for (; localIndex < formula.Length; localIndex++)
        {
            var ch = formula[localIndex];

            if (ch.IsWhiteSpace())
            {
                continue;
            }

            if (IsOpenBracketNext(formula, localIndex))
            {
                isNegationed = isNegationedLocal;
                return localIndex;
            }

            if (ch == NEGATION_CHAR)
            {
                isNegationedLocal = !isNegationedLocal;
            }
            else if (IsNegationKeyword(formula, localIndex))
            {
                isNegationedLocal = !isNegationedLocal;
                localIndex += NEGATION_KEYWORD.Length - 1;
            }
            else
            {
                break;
            }
        }

        isNegationed = isNegationedLocal;
        return localIndex;
    }

    private static bool IsOpenBracketNext(ReadOnlySpan<char> formula, int index)
    {
        return (index + 1 < formula.Length ? formula[index + 1] : ' ') == '(';
    }

    /// <summary>
    /// The keyword not (case insensitive) before an operand it can negate: a variable, a function, another negation. Before an opening bracket it is the
    /// function: not(x), not (x). Before anything else, or at the end, it is no negation: the variable reader then makes it a KeywordNode, a wrong formula
    /// </summary>
    private static bool IsNegationKeyword(ReadOnlySpan<char> formula, int index)
    {
        if (!GlobalOptions.UseKeywordOperators || !formula.IsWholeWordAt(index, NEGATION_KEYWORD))
        {
            return false;
        }

        for (int i = index + NEGATION_KEYWORD.Length; i < formula.Length; i++)
        {
            var ch = formula[i];

            if (!ch.IsWhiteSpace())
            {
                return ch.IsSimpleVariable() || ch == '[' || ch == NEGATION_CHAR;
            }
        }

        return false;
    }

    private const char NEGATION_CHAR = '!';
    private const string NEGATION_KEYWORD = "not";
}
