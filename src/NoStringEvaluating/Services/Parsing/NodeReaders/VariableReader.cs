﻿using NoStringEvaluating.Extensions;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Services.Variables;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Variable reader
/// </summary>
public static class VariableReader
{
    /// <summary>
    /// Read variable
    /// </summary>
    public static bool TryProceedBorderedVariable(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

        // Read negation
        localIndex = NegationReader.ReadNegation(formula, localIndex, out var isNegation);

        // Check out of range
        if (localIndex >= formula.Length)
        {
            return false;
        }

        // Read variable
        if (formula[localIndex] != START_CHAR)
        {
            return false;
        }

        // Skip start char
        localIndex++;

        var variableBuilder = default(IndexWatcher);
        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];

            if (ch == END_CHAR)
            {
                var variableSpan = formula.Slice(variableBuilder.StartIndex.GetValueOrDefault(), variableBuilder.Length);
                var variableName = variableSpan.ToString();
                AddFormulaNode(nodes, variableName, isNegativeLocal, isNegation);

                index = i;
                return true;
            }

            variableBuilder.Remember(i);
        }

        return false;
    }

    /// <summary>
    /// Read variable
    /// </summary>
    public static bool TryProceedSimpleVariable(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

        // Read negation
        localIndex = NegationReader.ReadNegation(formula, localIndex, out var isNegationLocal);

        var numberBuilder = default(IndexWatcher);
        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];
            var isLastChar = i + 1 == formula.Length;

            if (ch.IsSimpleVariable())
            {
                numberBuilder.Remember(i);

                if (isLastChar && TryAddSimpleVariable(nodes, formula, numberBuilder, isNegativeLocal, isNegationLocal))
                {
                    index = i;
                    return true;
                }
            }
            else if (TryAddSimpleVariable(nodes, formula, numberBuilder, isNegativeLocal, isNegationLocal))
            {
                index = i - 1;
                return true;
            }
            else
            {
                break;
            }
        }

        return false;
    }

    private static bool TryAddSimpleVariable(
        List<BaseFormulaNode> nodes,
        ReadOnlySpan<char> formula,
        IndexWatcher nodeBuilder,
        bool isNegative,
        bool isNegation)
    {
        if (nodeBuilder.InProcess)
        {
            var variableSpan = formula.Slice(nodeBuilder.StartIndex.GetValueOrDefault(), nodeBuilder.Length);
            var variableName = variableSpan.ToString();
            AddFormulaNode(nodes, variableName, isNegative, isNegation);

            return true;
        }

        return false;
    }

    private static void AddFormulaNode(
        List<BaseFormulaNode> nodes,
        string variableName,
        bool isNegative,
        bool isNegation)
    {
        // Known variable kinda Pi, E, etc...
        if (KnownVariables.TryGetNumberValue(variableName, out var numberValue))
        {
            if (isNegative)
            {
                numberValue *= -1;
            }

            var valNode = new NumberNode(numberValue);
            nodes.Add(valNode);
        }
        else if (KnownVariables.TryGetBooleanValue(variableName, out var boolValue))
        {
            if (isNegation)
            {
                boolValue = !boolValue;
            }

            var valNode = new BooleanNode(boolValue);
            nodes.Add(valNode);
        }
        else if (KnownVariables.IsNull(variableName))
        {
            var nullNode = new NullNode();
            nodes.Add(nullNode);
        }
        else
        {
            var varNode = new VariableNode(variableName, isNegative, isNegation);
            nodes.Add(varNode);
        }
    }

    private const char START_CHAR = '[';
    private const char END_CHAR = ']';
}
