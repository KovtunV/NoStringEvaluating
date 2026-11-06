using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Services.Binding;

/// <summary>
/// Binds the variables of parsed formula nodes to the indexes of an <see cref="IIndexedVariablesContainer"/>: a copy of the nodes that reads those variables by index
/// </summary>
public static class FormulaBinder
{
    /// <summary>
    /// A copy of the formula nodes in which every variable the container indexes is bound to its index, keyed by the container's BindingKey. Evaluated with a
    /// container of that key the bound variables are read by index; with any other container, or a dictionary, every variable is read by name as before
    /// </summary>
    public static FormulaNodes Bind(FormulaNodes formulaNodes, IIndexedVariablesContainer container)
    {
        ArgumentNullException.ThrowIfNull(formulaNodes);
        ArgumentNullException.ThrowIfNull(container);

        return new FormulaNodes(Bind(formulaNodes.Nodes, container), container.BindingKey);
    }

    private static List<BaseFormulaNode> Bind(List<BaseFormulaNode> nodes, IIndexedVariablesContainer container)
    {
        var bound = new List<BaseFormulaNode>(nodes.Count);

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            if (node is VariableNode variableNode)
            {
                // bound to the new index; or unbound, which an earlier binding's node is not
                var index = container.IndexOf(variableNode.Name);
                bound.Add(index == variableNode.Index ? variableNode : new VariableNode(variableNode.Name, variableNode.IsNegative, variableNode.IsNegation, index));
            }
            else if (node is FunctionWrapperNode wrapper)
            {
                var boundWrapper = new FunctionWrapperNode(wrapper.FunctionNode);

                for (int a = 0; a < wrapper.FunctionArgumentNodes.Count; a++)
                {
                    boundWrapper.FunctionArgumentNodes.Add(Bind(wrapper.FunctionArgumentNodes[a], container));
                }

                bound.Add(boundWrapper);
            }
            else
            {
                bound.Add(node);
            }
        }

        return bound;
    }
}
