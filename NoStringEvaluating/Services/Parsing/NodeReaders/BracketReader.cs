using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Bracket reader
    /// </summary>
    public static class BracketReader
    {
        /// <summary>
        /// Read open bracket
        /// </summary>
        public static bool TryProceedOpenBracket(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int isNegativeCount, ref int index)
        {
            // Read unary minus
            var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

            // Check out of range
            if (localIndex >= formula.Length)
                return false;

            // Check open bracket
            if (formula[localIndex] == OPEN_BRACKET_CHAR)
            {
                if (isNegativeLocal)
                {
                    AddAdditionalNodesForUnaryMinus(nodes);
                }

                // Add bracket
                AddAddOpenBracket(nodes);

                // Is unary minus
                if (isNegativeLocal)
                {
                    isNegativeCount++;
                }

                index = localIndex;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Read close bracket
        /// </summary>
        public static bool TryProceedCloseBracket(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int isNegativeCount, ref int index)
        {
            if (formula[index] != CLOSE_BRACKET_CHAR)
            {
                return false;
            }

            // Add bracket
            AddAddCloseBracket(nodes);

            // If there is unary minus, we must add close bracket
            if (isNegativeCount > 0)
            {
                AddAddCloseBracket(nodes);
                isNegativeCount--;
            }

            return true;
        }

        private static void AddAdditionalNodesForUnaryMinus(ICollection<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Open);
            var value = new ValueNode(0);
            var minus = new OperatorNode(Operator.Minus);

            nodes.Add(bracket);
            nodes.Add(value);
            nodes.Add(minus);
        }

        private static void AddAddCloseBracket(ICollection<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Close);
            nodes.Add(bracket);
        }

        private static void AddAddOpenBracket(ICollection<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Open);
            nodes.Add(bracket);
        }

        private const char OPEN_BRACKET_CHAR = '(';
        private const char CLOSE_BRACKET_CHAR = ')';
    }
}
