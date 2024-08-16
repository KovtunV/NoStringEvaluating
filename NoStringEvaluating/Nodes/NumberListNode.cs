using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - NumberList
/// </summary>
public class NumberListNode(List<double> numberList) : BaseFormulaNode(NodeTypeEnum.NumberList)
{
    /// <summary>
    /// NumberList
    /// </summary>
    public List<double> NumberList { get; } = numberList;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return string.Join(", ", NumberList);
    }
}
