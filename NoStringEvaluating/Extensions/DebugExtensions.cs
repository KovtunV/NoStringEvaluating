using System.Collections.Generic;
using System.Reflection;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Variables;

namespace NoStringEvaluating.Extensions;

/// <summary>
/// Extension for debugging
/// </summary>
public static class DebugExtensions
{
    #region VariableValuesUsed

    /// <summary>
    /// Return list of variable values used by formula
    /// </summary>
    public static List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(
        this NoStringEvaluator eval,
        string formula,
        IVariablesContainer variableContainer = null)
    {
        var formulaNodes = ExtractFormulaCache(eval).GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variableContainer);
        return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Return list of variable values used by formula
    /// </summary>
    public static List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(
        this NoStringEvaluator eval,
        FormulaNodes formulaNodes,
        IVariablesContainer variableContainer = null)
    {
        var wrapper = VariablesSource.Create(variableContainer);
        return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Return list of variable values used by formula
    /// </summary>
    public static List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(
        this NoStringEvaluator eval,
        string formula,
        IDictionary<string, EvaluatorValue> variables = null)
    {
        var formulaNodes = ExtractFormulaCache(eval).GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Return list of variable values used by formula
    /// </summary>
    public static List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(
        this NoStringEvaluator eval,
        FormulaNodes formulaNodes,
        IDictionary<string, EvaluatorValue> variables = null)
    {
        var wrapper = VariablesSource.Create(variables);
        return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Routine to check what variables the calculation actually receives and uses. Without doing the actual calculation. Usefull for debugging and logging
    /// </summary>
    private static List<(string name, EvaluatorValue value)> VariableValuesUsedByFormulaInternal(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        var res = new List<(string, EvaluatorValue)>();

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            if (node.TypeKey == NodeTypeEnum.Variable)
            {
                var variableNode = (VariableNode)node;
                res.Add((variableNode.Name, variables.GetValue(variableNode.Name)));
            }
            else if (node.TypeKey == NodeTypeEnum.FunctionWrapper)
            {
                // add the list of variables used by the function
                var functionWrapper = (FunctionWrapperNode)node;
                for (int f = 0; f < functionWrapper.FunctionArgumentNodes.Count; f++)
                {
                    res.AddRange(VariableValuesUsedByFormulaInternal(functionWrapper.FunctionArgumentNodes[f], variables));
                }
            }
        }

        return res;
    }

    private static IFormulaCache ExtractFormulaCache(NoStringEvaluator eval)
    {
        return typeof(NoStringEvaluator)
            .GetField("_formulaCache", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(eval) as IFormulaCache;
    }

    #endregion

}
