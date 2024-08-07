﻿using System.Globalization;
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
        var isScientificNotation = false;
        var isExponentPositive = false;
        var numberBuilder = default(IndexWatcher);
        var numberExponentBuilder = default(IndexWatcher);

        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];
            var isLastChar = i + 1 == formula.Length;

            if (ch.IsDigit())
            {
                if (isScientificNotation)
                {
                    numberExponentBuilder.Remember(i);
                }
                else
                {
                    numberBuilder.Remember(i);
                }

                if (isLastChar && TryAddNumber(nodes, formula, numberBuilder, numberExponentBuilder, isNegativeLocal, isScientificNotation, isExponentPositive))
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
            else if (!isScientificNotation && ch.IsScientificNotationSymbol())
            {
                isScientificNotation = true;
                isExponentPositive = IsExponentPositive(formula, i);

                if (!IsDigitNext(formula, i))
                {
                    i++;
                }
            }
            else if (TryAddNumber(nodes, formula, numberBuilder, numberExponentBuilder, isNegativeLocal, isScientificNotation, isExponentPositive))
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

    private static bool TryAddNumber(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, IndexWatcher numberBuilder, IndexWatcher numberExponentBuilder, bool isNegative, bool isScientificNotation, bool isExponentPositive)
    {
        if (numberBuilder.InProcess)
        {
            var valueSpan = formula.Slice(numberBuilder.StartIndex.GetValueOrDefault(), numberBuilder.Length);
            var value = GetDouble(valueSpan);

            if (isNegative)
            {
                value *= -1;
            }

            if (isScientificNotation)
            {
                var valueExponentSpan = formula.Slice(numberExponentBuilder.StartIndex.GetValueOrDefault(), numberExponentBuilder.Length);
                var valueExponent = GetDouble(valueExponentSpan);

                if (isExponentPositive)
                {
                    value *= Math.Pow(10, valueExponent);
                }
                else
                {
                    value /= Math.Pow(10, valueExponent);
                }
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

    private static bool IsScientificNotationSymbol(this char ch)
    {
        return ch == 'e' || ch == 'E';
    }

    private static bool IsExponentPositive(ReadOnlySpan<char> formula, int index)
    {
        var nextIndex = index + 1;
        return nextIndex == formula.Length || formula[nextIndex].IsDigit();
    }

    private static CultureInfo RusCulture { get; } = CultureInfo.GetCultureInfo("ru-RU");
}
