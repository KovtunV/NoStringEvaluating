using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Number reader
    /// </summary>
    public static class NumberReader
    {
        /// <summary>
        /// Read number
        /// </summary>
        public static bool TryProceedNumber(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
        {
            // Read unary minus
            var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

            var numberBuilder = new IndexWatcher();
            for (int i = localIndex; i < formula.Length; i++)
            {
                var ch = formula[i];
                var isLastChar = i + 1 == formula.Length;

                if (ch.IsFloatingNumber())
                {
                    numberBuilder.Remember(i);

                    if (isLastChar && TryAddNumber(nodes, formula, numberBuilder, isNegativeLocal))
                    {
                        index = i;
                        return true;
                    }
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

        private static bool TryAddNumber(ICollection<IFormulaNode> nodes, ReadOnlySpan<char> formula, IndexWatcher nodeBuilder, bool isNegative)
        {
            if (nodeBuilder.InProcess)
            {
                var valueSpan = formula.Slice(nodeBuilder.StartIndex.GetValueOrDefault(), nodeBuilder.Length);
                var value = GetDouble(valueSpan);

                if (isNegative)
                {
                    value *= -1;
                }

                var valNode = new ValueNode(value);
                nodes.Add(valNode);

                return true;
            }

            return false;
        }

        private static double GetDouble(ReadOnlySpan<char> value)
        {
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var res))
            {
                double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out res);
            }

            return res;
        }
    }
}
