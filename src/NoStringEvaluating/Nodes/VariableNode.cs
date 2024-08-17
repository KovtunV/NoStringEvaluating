using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Variable
/// </summary>
public class VariableNode(string name, bool isNegative, bool isNegation) : BaseFormulaNode(NodeTypeEnum.Variable)
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = name;

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
        return $"[{Name}]";
    }
}
