using System;
using System.Collections.Generic;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing
{
    /// <summary>
    /// Reverse Polish notation
    /// </summary>
    public static class PolishNotationService
    {
        /// <summary>
        /// Return reversed nodes
        /// </summary>
        public static List<IFormulaNode> GetReversedNodes(List<IFormulaNode> nodes)
        {
            return GetReversedNodes(nodes.ToArray());
        }

        private static List<IFormulaNode> GetReversedNodes(ReadOnlySpan<IFormulaNode> nodes)
        {
            var outExpression = new List<IFormulaNode>();
            var stack = new Stack<IFormulaNode>();

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

        private static bool TryProceedFunction(List<IFormulaNode> outExpression, ReadOnlySpan<IFormulaNode> nodes, ref int index)
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

            // Skip next semicolon after function
            if (IsNextSemicolon(nodes, localIndex))
                localIndex++;

            index = localIndex;
            return true;
        }

        private static bool IsNextSemicolon(ReadOnlySpan<IFormulaNode> nodes, int index)
        {
            var nextNode = index + 1 < nodes.Length ? nodes[index + 1] : null;
            var nextFunctionChar = nextNode as FunctionCharNode;
            var nextIsSemicolon = nextFunctionChar?.FunctionChar == FunctionChar.Semicolon;
            return nextIsSemicolon;
        }
    }
}
