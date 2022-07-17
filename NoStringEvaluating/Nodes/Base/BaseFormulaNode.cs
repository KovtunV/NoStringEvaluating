namespace NoStringEvaluating.Nodes.Base;

/// <summary>
/// Formula node
/// </summary>
public abstract class BaseFormulaNode
{
    /// <summary>
    /// Type key
    /// </summary>
    public NodeTypeEnum TypeKey { get; }

    /// <summary>
    /// Formula node
    /// </summary>
    protected BaseFormulaNode(NodeTypeEnum typeKey)
    {
        TypeKey = typeKey;
    }
}
