using System.Globalization;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Number
    /// </summary>
    public class NumberNode : BaseFormulaNode
    {
        /// <summary>
        /// Number
        /// </summary>
        public double Number { get; }

        /// <summary>
        /// Formula node - Number
        /// </summary>
        public NumberNode(double number) : base(NodeTypeEnum.Number)
        {
            Number = number;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return Number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
