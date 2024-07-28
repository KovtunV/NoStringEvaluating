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

    private const char NEGATION_CHAR = '!';
}
