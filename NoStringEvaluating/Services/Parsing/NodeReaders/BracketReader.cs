using System;
using System.Collections.Generic;
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
        public static bool TryProceedOpenBracket(List<IFormulaNode> nodes, ReadOnlySpan<char> formula, BracketCounters negativeBracketCounters, ref int index)
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
                var bracket = AddOpenBracket(nodes);

                // Is unary minus
                if (isNegativeLocal)
                {
                    negativeBracketCounters.CreateNewCounter();
                }
                else
                {
                    negativeBracketCounters.Proceed(bracket);
                }

                index = localIndex;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Read close bracket
        /// </summary>
        public static bool TryProceedCloseBracket(List<IFormulaNode> nodes, ReadOnlySpan<char> formula, BracketCounters negativeBracketCounters, ref int index)
        {
            if (formula[index] != CLOSE_BRACKET_CHAR)
            {
                return false;
            }

            // Add bracket
            var bracket = AddCloseBracket(nodes);

            // If there is unary minus, we must add close bracket
            if (negativeBracketCounters.Proceed(bracket))
            {
                AddCloseBracket(nodes);
            }

            return true;
        }

        private static void AddAdditionalNodesForUnaryMinus(List<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Open);
            var value = new ValueNode(0);
            var minus = new OperatorNode(Operator.Minus);

            nodes.Add(bracket);
            nodes.Add(value);
            nodes.Add(minus);
        }

        private static BracketNode AddCloseBracket(List<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Close);
            nodes.Add(bracket);

            return bracket;
        }

        private static BracketNode AddOpenBracket(List<IFormulaNode> nodes)
        {
            var bracket = new BracketNode(Bracket.Open);
            nodes.Add(bracket);

            return bracket;
        }

        private const char OPEN_BRACKET_CHAR = '(';
        private const char CLOSE_BRACKET_CHAR = ')';
    }
}
