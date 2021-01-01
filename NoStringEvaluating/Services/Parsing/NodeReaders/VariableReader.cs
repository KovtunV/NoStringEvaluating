using System;
using System.Collections.Generic;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Variable reader
    /// </summary>
    public static class VariableReader
    {
        /// <summary>
        /// Read variable
        /// </summary>
        public static bool TryProceedVariable(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
        {
            // Read unary minus
            var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

            // Check out of range
            if (localIndex >= formula.Length)
                return false;

            // Read variable
            if (formula[localIndex] != START_CHAR)
            {
                return false;
            }

            // Skip start char
            localIndex++;

            var variableBuilder = new IndexWatcher();
            for (int i = localIndex; i < formula.Length; i++)
            {
                var ch = formula[i];
   
                if (ch == END_CHAR)
                {
                    var variableSpan = formula.Slice(variableBuilder.StartIndex.GetValueOrDefault(), variableBuilder.Length);
                    var variableName = variableSpan.ToString();
                    var valNode = new VariableNode(variableName, isNegativeLocal);
                    nodes.Add(valNode);

                    index = i;
                    return true;
                }

                variableBuilder.Remember(i);
            }

            return false;
        }

        private const char START_CHAR = '[';
        private const char END_CHAR = ']';
    }
}
