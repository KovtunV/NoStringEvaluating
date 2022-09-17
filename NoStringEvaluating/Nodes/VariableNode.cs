using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Variable
/// </summary>
public class VariableNode : BaseFormulaNode
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Has unary minus
    /// </summary>
    public bool IsNegative { get; }

    /// <summary>
    /// Has negation in boolean
    /// </summary>
    public bool IsNegation { get; }

    /// <summary>
    /// Formula node - Variable
    /// </summary>
    public VariableNode(string name, bool isNegative, bool isNegation)
        : base(NodeTypeEnum.Variable)
    {
        Name = name;
        IsNegative = isNegative;
        IsNegation = isNegation;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return $"[{Name}]";
    }
}
