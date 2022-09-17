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
    /// Has negation in boolean
    /// </summary>
    public bool IsNegation { get; }

    /// <summary>
    /// Formula node - Function
    /// </summary>
    public FunctionNode(IFunction function, bool isNegative, bool isNegation)
        : base(NodeTypeEnum.Function)
    {
        Function = function;
        IsNegative = isNegative;
        IsNegation = isNegation;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Function.Name;
    }
}
