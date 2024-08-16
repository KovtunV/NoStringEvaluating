namespace NoStringEvaluating.Nodes.Base;

/// <summary>
/// Formula node
/// </summary>
public abstract class BaseFormulaNode(NodeTypeEnum typeKey)
{
    /// <summary>
    /// Type key
    /// </summary>
    public NodeTypeEnum TypeKey { get; } = typeKey;
}
