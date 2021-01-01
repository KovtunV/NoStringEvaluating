using System.Collections.Generic;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes.Common
{
    /// <summary>
    /// Formula nodes
    /// </summary>
    public class FormulaNodes
    {
        /// <summary>
        /// Nodes
        /// </summary>
        public List<IFormulaNode> Nodes { get; }

        /// <summary>
        /// Formula nodes
        /// </summary>
        public FormulaNodes(List<IFormulaNode> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return string.Join(" ", Nodes);
        }
    }
}
