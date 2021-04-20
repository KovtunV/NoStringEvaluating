using System.Globalization;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Value
    /// </summary>
    public class ValueNode : BaseFormulaNode
    {
        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Formula node - Value
        /// </summary>
        public ValueNode(double value) : base(NodeTypeEnum.Value)
        {
            Value = value;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
