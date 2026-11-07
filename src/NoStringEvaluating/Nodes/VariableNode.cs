using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Variable
/// </summary>
public class VariableNode(string name, bool isNegative, bool isNegation) : BaseFormulaNode(NodeTypeEnum.Variable)
{
    /// <summary>
    /// Formula node - Variable bound to an index of an <see cref="Contract.Variables.IIndexedVariablesContainer"/>
    /// </summary>
    public VariableNode(string name, bool isNegative, bool isNegation, int index)
        : this(name, isNegative, isNegation)
    {
        Index = index;
    }

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
    /// The index the variable is bound to in an <see cref="Contract.Variables.IIndexedVariablesContainer"/>, -1 when it is read by name
    /// </summary>
    public int Index { get; } = -1;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return $"[{Name}]";
    }
}
