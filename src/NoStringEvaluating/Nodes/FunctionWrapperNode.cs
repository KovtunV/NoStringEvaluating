using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Function wrapper
/// </summary>
public class FunctionWrapperNode(FunctionNode functionNode) : BaseFormulaNode(NodeTypeEnum.FunctionWrapper)
{
    /// <summary>
    /// Function node
    /// </summary>
    public FunctionNode FunctionNode { get; } = functionNode;

    /// <summary>
    /// Function arguments
    /// </summary>
    public List<List<BaseFormulaNode>> FunctionArgumentNodes { get; } = [];

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        var joined1 = FunctionArgumentNodes.Select(s => string.Join(" ", s));
        var joined = string.Join("; ", joined1);
        return $"{FunctionNode}({joined})";
    }
}
