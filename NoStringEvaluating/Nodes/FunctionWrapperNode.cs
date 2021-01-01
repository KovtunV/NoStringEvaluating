using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Function wrapper
    /// </summary>
    public class FunctionWrapperNode : IFormulaNode
    {
        /// <summary>
        /// Function node
        /// </summary>
        public FunctionNode FunctionNode { get; }

        /// <summary>
        /// Function arguments
        /// </summary>
        public List<List<IFormulaNode>> FunctionArgumentNodes { get; }

        /// <summary>
        /// Formula node - Function wrapper
        /// </summary>
        public FunctionWrapperNode(FunctionNode functionNode)
        {
            FunctionNode = functionNode;
            FunctionArgumentNodes = new List<List<IFormulaNode>>();
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            var joined1 = FunctionArgumentNodes.Select(s => string.Join(" ", s));
            var joined = string.Join("; ", joined1);
            return $"{FunctionNode}({joined})";
        }
    }
}
