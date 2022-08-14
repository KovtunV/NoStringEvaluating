using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Null
/// </summary>
public class NullNode : BaseFormulaNode
{
    /// <summary>
    /// Null Const
    /// </summary>
    public NullNode() : base(NodeTypeEnum.Null)
    {
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return "NULL";
    }
}
