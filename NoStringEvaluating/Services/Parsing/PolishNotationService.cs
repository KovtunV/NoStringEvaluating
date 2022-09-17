using System;
using System.Collections.Generic;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing;

/// <summary>
/// Reverse Polish notation
/// </summary>
public static class PolishNotationService
{
    /// <summary>
    /// Return reversed nodes
    /// </summary>
    public static List<BaseFormulaNode> GetReversedNodes(List<BaseFormulaNode> nodes)
    {
        return GetReversedNodes(nodes.ToArray());
    }

    private static List<BaseFormulaNode> GetReversedNodes(ReadOnlySpan<BaseFormulaNode> nodes)
    {
        var outExpression = new List<BaseFormulaNode>();
        var stack = new Stack<BaseFormulaNode>();

        for (int i = 0; i < nodes.Length; i++)
        {
            if (TryProceedFunction(outExpression, nodes, ref i))
                continue;

            var node = nodes[i];

            var operationNode = node as OperatorNode;
            var bracketNode = node as BracketNode;

            if (operationNode is null && bracketNode is null)
            {
                outExpression.Add(node);
                continue;
            }

            if (stack.Count == 0)
            {
                stack.Push(node);
                continue;
            }

            if (bracketNode != null && bracketNode.Bracket == Bracket.Close)
            {
                while (stack.Count > 0)
                {
                    var peekedBracket = stack.Peek() as BracketNode;
                    var peekedFunction = stack.Peek() as FunctionNode;

                    if (peekedBracket is null && peekedFunction is null)
                    {
                        outExpression.Add(stack.Pop());
                    }
                    else if (peekedBracket != null && peekedBracket.Bracket == Bracket.Open)
                    {
                        stack.Pop();
                        break;
                    }
                }
            }
            else
            {
                while (stack.Count > 0)
                {
                    if (bracketNode != null && bracketNode.Bracket == Bracket.Open)
                        break;

                    if (stack.Peek() is OperatorNode peekedOperation && peekedOperation.Priority >= operationNode.Priority)
                        outExpression.Add(stack.Pop());
                    else
                    {
                        break;
                    }
                }

                stack.Push(node);
            }
        }

        while (stack.Count > 0)
            outExpression.Add(stack.Pop());

        return outExpression;
    }

    private static bool TryProceedFunction(List<BaseFormulaNode> outExpression, ReadOnlySpan<BaseFormulaNode> nodes, ref int index)
    {
        var localIndex = index;

        if (!(nodes[localIndex] is FunctionNode funcNode))
        {
            return false;
        }

        var funcWrapperNode = new FunctionWrapperNode(funcNode);
        outExpression.Add(funcWrapperNode);

        // Skip function name
        localIndex++;

        // Skip function open bracket
        localIndex++;

        // Bracket counter
        var bracketCounter = new BorderCounter<BracketNode>(f => f.Bracket == Bracket.Open);

        // Function's part index
        var partIndexStart = localIndex;

        // Find function's end
        for (; localIndex < nodes.Length; localIndex++)
        {
            var node = nodes[localIndex];

            var partLength = localIndex - partIndexStart;
            var canReadPart = partLength > 0;

            if (node is FunctionCharNode && canReadPart && bracketCounter.Count == 1)
            {
                var part = nodes[partIndexStart..localIndex];
                var subPolish = GetReversedNodes(part);
                funcWrapperNode.FunctionArgumentNodes.Add(subPolish);
                partIndexStart = localIndex + 1;
            }

            else if (node is BracketNode bracketNode && bracketCounter.Proceed(bracketNode))
            {
                if (!canReadPart)
                    break;

                var part = nodes[partIndexStart..localIndex];
                var subPolish = GetReversedNodes(part);
                funcWrapperNode.FunctionArgumentNodes.Add(subPolish);

                // The end of function
                break;
            }
        }

        index = localIndex;
        return true;
    }
}
