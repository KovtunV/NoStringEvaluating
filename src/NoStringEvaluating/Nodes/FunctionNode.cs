using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Function
/// </summary>
public class FunctionNode(IFunction function, bool isNegative, bool isNegation) : BaseFormulaNode(NodeTypeEnum.Function)
{
    /// <summary>
    /// Function
    /// </summary>
    public IFunction Function { get; } = function;

    /// <summary>
    /// Has unary minus
    /// </summary>
    public bool IsNegative { get; } = isNegative;

    /// <summary>
    /// Has negation in boolean
    /// </summary>
    public bool IsNegation { get; } = isNegation;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Function.Name;
    }
}
