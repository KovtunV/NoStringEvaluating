using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Services.Parsing;

namespace NoStringEvaluating.Services.Checking
{
    /// <summary>
    /// Syntax checker
    /// </summary>
    public class FormulaChecker : IFormulaChecker
    {
        private readonly FormulaParser _formulaParser;

        /// <summary>
        /// Syntax checker
        /// </summary>
        public FormulaChecker(IFunctionReader functionReader)
        {
            _formulaParser = new FormulaParser(functionReader);
        }

        /// <summary>
        /// Check syntax
        /// </summary>
        public CheckFormulaResult CheckSyntax(string formula)
        {
            return CheckSyntax(formula.AsSpan());
        }

        /// <summary>
        /// Check syntax
        /// </summary>
        public CheckFormulaResult CheckSyntax(ReadOnlySpan<char> formula)
        {
            var messages = new List<string>();
            var nodes = _formulaParser.ParseWithoutRpn(formula);

            // Check brackets for whole formula
            CheckBracketsCount(messages, nodes, 0, nodes.Count);
            CheckEmptyBrackets(messages, nodes, 0, nodes.Count);

            // Check
            CheckSyntaxInternal(messages, nodes, 0, nodes.Count);

            return new CheckFormulaResult(messages);
        }

        private void CheckSyntaxInternal(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (TryCheckFunction(messages, nodes, ref i))
                    continue;

                var nextIndex = GetNextIndex(nodes, i, end);
                CheckMissedOperator(messages, nodes, i, nextIndex);
                CheckNodeBetweenNumbers(messages, nodes, i, nextIndex);
                CheckMissedNumber(messages, nodes, i, nextIndex);
                CheckFunctionBody(messages, nodes, i, nextIndex);

                i = nextIndex - 1;
            }
        }

        private int GetNextIndex(IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (nodes[i] is FunctionNode)
                    return i;
            }

            return end;
        }

        #region Function

        private bool TryCheckFunction(ICollection<string> messages, IList<IFormulaNode> nodes, ref int index)
        {
            var localIndex = index;

            if (!(nodes[localIndex] is FunctionNode))
            {
                return false;
            }

            // Skip function name
            localIndex++;

            // Skip function open bracket
            localIndex++;

            // Bracket counter
            var bracketCounter = new BorderCounter<BracketNode>(f => f.Bracket == Bracket.Open);

            // Function's part index
            var partIndexStart = localIndex;

            // Find function's end
            for (; localIndex < nodes.Count; localIndex++)
            {
                var node = nodes[localIndex];

                var partLength = localIndex - partIndexStart;
                var canReadPart = partLength > 0;

                if (node is FunctionCharNode && canReadPart && bracketCounter.Count == 1)
                {
                    CheckSyntaxInternal(messages, nodes, partIndexStart, localIndex);

                    partIndexStart = localIndex + 1;
                }

                else if (node is BracketNode bracketNode && bracketCounter.Proceed(bracketNode))
                {
                    if (!canReadPart)
                        break;

                    CheckSyntaxInternal(messages, nodes, partIndexStart, localIndex);

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

        private static bool IsNextSemicolon(IList<IFormulaNode> nodes, int index)
        {
            var nextNode = index + 1 < nodes.Count ? nodes[index + 1] : null;
            var nextFunctionChar = nextNode as FunctionCharNode;
            var nextIsSemicolon = nextFunctionChar?.FunctionChar == FunctionChar.Semicolon;
            return nextIsSemicolon;
        }

        #endregion

        #region Bracket

        private void CheckBracketsCount(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            var openBracketCount = 0;
            var closeBracketCount = 0;

            for (int i = start; i < end; i++)
            {
                if (!(nodes[i] is BracketNode bracketNode))
                    continue;

                if (bracketNode.Bracket == Bracket.Open)
                    openBracketCount++;
                else if (bracketNode.Bracket == Bracket.Close)
                    closeBracketCount++;
            }

            if (openBracketCount != closeBracketCount)
            {
                messages.Add("Wrong number of brackets");
            }
        }

        private void CheckEmptyBrackets(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start + 1; i < end; i++)
            {
                var prevNode = nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (!(prevNode is FunctionNode) && IsOpenBracket(node) && IsCloseBracket(nextNode))
                {
                    messages.Add("Empty brackets");
                }
            }
        }
        #endregion

        #region Checkers

        private void CheckMissedOperator(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var isLast = i + 1 == end;

                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if ((IsOperatorableNode(prevNode) || IsCloseBracket(prevNode)) && IsOperatorableNode(node))
                {
                    messages.Add($"Between \"{prevNode}\" and \"{node}\" must be an operator");
                }

                if (isLast && IsOperatorableNode(node) && (IsOperatorableNode(nextNode) || IsOpenBracket(nextNode)))
                {
                    messages.Add($"Between \"{node}\" and \"{nextNode}\" must be an operator");
                }
            }
        }

        private void CheckNodeBetweenNumbers(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (IsOperatorableNode(prevNode) && IsOperatorableNode(nextNode) && !(node is OperatorNode))
                {
                    messages.Add($"Between \"{prevNode}\" and \"{nextNode}\" must be an operator, not \"{node}\"");
                }
            }
        }


        private void CheckMissedNumber(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (!(IsOperatorableNode(prevNode) || IsCloseBracket(prevNode)) && node is OperatorNode)
                {
                    messages.Add($"Before \"{node}\" must be a number or a closed bracket, not \"{prevNode?.ToString() ?? "NULL"}\"");
                }

                if (node is OperatorNode && !(IsOperatorableNode(nextNode) || IsOpenBracket(nextNode)))
                {
                    messages.Add($"After \"{node}\" must be a number or an opened bracket, not \"{nextNode?.ToString() ?? "NULL"}\"");
                }
            }
        }

        private void CheckFunctionBody(ICollection<string> messages, IList<IFormulaNode> nodes, int start, int end)
        {
            if (start + 1 != end)
                return;

            var node = nodes[start];
            if (node is FunctionCharNode)
            {
                messages.Add("Empty function's body");
            }
        }

        #endregion

        private bool IsOpenBracket(IFormulaNode node)
        {
            return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Open;
        }

        private bool IsCloseBracket(IFormulaNode node)
        {
            return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Close;
        }

        private bool IsOperatorableNode(IFormulaNode node)
        {
            return node is ValueNode
                || node is VariableNode
                || node is FunctionNode;
        }
    }
}
