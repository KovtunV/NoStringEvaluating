using System.Globalization;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Number reader
/// </summary>
public static class NumberReader
{
    /// <summary>
    /// Read number
    /// </summary>
    public static bool TryProceedNumber(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

        var isReadedFloatingPoint = false;
        var numberBuilder = default(IndexWatcher);
        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];
            var isLastChar = i + 1 == formula.Length;

            if (ch.IsDigit())
            {
                numberBuilder.Remember(i);

                if (isLastChar && TryAddNumber(nodes, formula, numberBuilder, isNegativeLocal))
                {
                    index = i;
                    return true;
                }
            }
            else if (!isReadedFloatingPoint && ch.IsFloatingPointSymbol() && IsDigitNext(formula, i))
            {
                isReadedFloatingPoint = true;
                numberBuilder.Remember(i);
            }
            else if (TryAddNumber(nodes, formula, numberBuilder, isNegativeLocal))
            {
                index = i - 1;
                return true;
            }
            else
            {
                break;
            }
        }

        return false;
    }

    private static bool TryAddNumber(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, IndexWatcher nodeBuilder, bool isNegative)
    {
        if (nodeBuilder.InProcess)
        {
            var valueSpan = formula.Slice(nodeBuilder.StartIndex.GetValueOrDefault(), nodeBuilder.Length);
            var value = GetDouble(valueSpan);

            if (isNegative)
            {
                value *= -1;
            }

            var valNode = new NumberNode(value);
            nodes.Add(valNode);

            return true;
        }

        return false;
    }

    private static double GetDouble(ReadOnlySpan<char> value)
    {
        if (double.TryParse(value, NumberStyles.Any, RusCulture, out var res))
        {
            return res;
        }

        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
        {
            return res;
        }

        return default;
    }

    private static bool IsDigitNext(ReadOnlySpan<char> formula, int index)
    {
        var nextIndex = index + 1;
        if (nextIndex == formula.Length)
        {
            return false;
        }

        return formula[nextIndex].IsDigit();
    }

    private static CultureInfo RusCulture { get; } = CultureInfo.GetCultureInfo("ru-RU");
}
