using System.Collections.Generic;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Read function's char
    /// </summary>
    public static class FunctionCharReader
    {
        /// <summary>
        /// Read function's char
        /// </summary>
        public static bool TryProceedFunctionChar(List<BaseFormulaNode> nodes, char ch)
        {
            if (FUNCTION_SEPARATOR == ch)
            {
                var node = new FunctionCharNode(FunctionChar.Semicolon);
                nodes.Add(node);
                return true;
            }

            return false;
        }

        private const char FUNCTION_SEPARATOR = ';';
    }
}
