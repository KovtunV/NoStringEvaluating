using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Reader for unary minus
    /// </summary>
    public static class UnaryMinusReader
    {
        /// <summary>
        /// Read unary minus and return next index
        /// </summary>
        public static int ReadUnaryMinus(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, int index, out bool isNegative)
        {
            var isNegativeLocal = false;
            var localIndex = index;
            for (; localIndex < formula.Length; localIndex++)
            {
                var ch = formula[localIndex];

                if (!TryProceedUnaryMinus(nodes, ch, ref isNegativeLocal))
                {
                    break;
                }
            }

            isNegative = isNegativeLocal;
            return localIndex;
        }

        /// <summary>
        /// Read unary minus
        /// </summary>
        private static bool TryProceedUnaryMinus(IList<IFormulaNode> nodes, char ch, ref bool isNegative)
        {
            var prevNode = nodes.Count > 0 ? nodes[^1] : null;

            // Unary minus
            if (ch == MINUS_CHAR && !(prevNode is ValueNode) && !(prevNode is VariableNode) && !(prevNode is BracketNode br && br.Bracket == Bracket.Close))
            {
                isNegative = !(isNegative is true);
                return true;
            }

            return false;
        }

        private const char MINUS_CHAR = '-';
    }
}
