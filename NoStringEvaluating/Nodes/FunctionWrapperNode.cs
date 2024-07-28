using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Function wrapper
/// </summary>
public class FunctionWrapperNode : BaseFormulaNode
{
    /// <summary>
    /// Function node
    /// </summary>
    public FunctionNode FunctionNode { get; }

    /// <summary>
    /// Function arguments
    /// </summary>
    public List<List<BaseFormulaNode>> FunctionArgumentNodes { get; }

    /// <summary>
    /// Formula node - Function wrapper
    /// </summary>
    public FunctionWrapperNode(FunctionNode functionNode)
        : base(NodeTypeEnum.FunctionWrapper)
    {
        FunctionNode = functionNode;
        FunctionArgumentNodes = new List<List<BaseFormulaNode>>();
    }

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
