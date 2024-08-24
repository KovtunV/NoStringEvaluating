using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes.Common;

/// <summary>
/// Formula nodes
/// </summary>
public class FormulaNodes(List<BaseFormulaNode> nodes)
{
    /// <summary>
    /// Nodes
    /// </summary>
    public List<BaseFormulaNode> Nodes { get; } = nodes;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return string.Join(" ", Nodes);
    }
}
