using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Function
/// </summary>
public class FunctionNode : BaseFormulaNode
{
    /// <summary>
    /// Function
    /// </summary>
    public IFunction Function { get; }

    /// <summary>
    /// Has unary minus
    /// </summary>
    public bool IsNegative { get; }

    /// <summary>
    /// Formula node - Function
    /// </summary>
    public FunctionNode(IFunction function, bool isNegative) : base(NodeTypeEnum.Function)
    {
        Function = function;
        IsNegative = isNegative;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Function.Name;
    }
}
