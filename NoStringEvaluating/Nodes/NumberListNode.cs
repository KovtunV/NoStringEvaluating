using NoStringEvaluating.Nodes.Base;
using System.Collections.Generic;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - NumberList
    /// </summary>
    public class NumberListNode : BaseFormulaNode
    {
        /// <summary>
        /// NumberList
        /// </summary>
        public List<double> NumberList { get; }

        /// <summary>
        /// Formula node - NumberList
        /// </summary>
        public NumberListNode(List<double> numberList) : base(NodeTypeEnum.NumberList)
        {
            NumberList = numberList;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return string.Join(", ", NumberList);
        }
    }
}
