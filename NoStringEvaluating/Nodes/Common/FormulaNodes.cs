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
            // I've removed ".AsReadOnly()" owing to the fact that it turned out to be slower then List ¯\_(ツ)_/¯
            // It was tested with Benchmark.One and Benchmark.Formula4. With AsReadOnly I got 300ms where as with List I got 200ms
            // Okay, if someone change collection it will be his mistake, let it be! 
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
