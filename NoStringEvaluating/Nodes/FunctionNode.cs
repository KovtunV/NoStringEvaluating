using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Function
    /// </summary>
    public class FunctionNode : IFormulaNode
    {
        /// <summary>
        /// Function
        /// </summary>
        public IFunction Function { get; }

        /// <summary>
        /// Has unary minus
        /// </summary>
        public bool IsNegative { get; }

        /// <summary>
        /// Formula node - Function
        /// </summary>
        public FunctionNode(IFunction function, bool isNegative)
        {
            Function = function;
            IsNegative = isNegative;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return Function.Name;
        }
    }
}
