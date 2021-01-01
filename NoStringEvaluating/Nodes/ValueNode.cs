using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Value
    /// </summary>
    public class ValueNode : IFormulaNode
    {
        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Formula node - Value
        /// </summary>
        public ValueNode(double value)
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
