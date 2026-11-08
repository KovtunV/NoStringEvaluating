using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes.Common;

/// <summary>
/// Formula nodes
/// </summary>
public class FormulaNodes(List<BaseFormulaNode> nodes)
{
    /// <summary>
    /// Formula nodes whose variables are bound to the indexes of an <see cref="Contract.Variables.IIndexedVariablesContainer"/> with this BindingKey
    /// </summary>
    public FormulaNodes(List<BaseFormulaNode> nodes, object bindingKey)
        : this(nodes)
    {
        BindingKey = bindingKey;
    }

    /// <summary>
    /// Nodes
    /// </summary>
    public List<BaseFormulaNode> Nodes { get; } = nodes;

    /// <summary>
    /// The BindingKey of the <see cref="Contract.Variables.IIndexedVariablesContainer"/> the variables are bound to (<see cref="Services.Binding.FormulaBinder"/>), null when they are read by name
    /// </summary>
    public object BindingKey { get; }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return string.Join(" ", Nodes);
    }
}
