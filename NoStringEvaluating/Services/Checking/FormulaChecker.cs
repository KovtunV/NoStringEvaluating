using System;
using System.Collections.Generic;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.FormulaChecker;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Checking
{
    /// <summary>
    /// Syntax checker
    /// </summary>
    public class FormulaChecker : IFormulaChecker
    {
        private readonly IFormulaParser _formulaParser;

        /// <summary>
        /// Syntax checker
        /// </summary>
        public FormulaChecker(IFormulaParser formulaParser)
        {
            _formulaParser = formulaParser;
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
            var mistakes = new List<FormulaCheckerModel>();
            var nodes = _formulaParser.ParseWithoutRpn(formula);

            // Check brackets for whole formula
            CheckBracketsCount(mistakes, nodes, 0, nodes.Count);
            CheckEmptyBrackets(mistakes, nodes, 0, nodes.Count);

            // Check
            CheckSyntaxInternal(mistakes, nodes, 0, nodes.Count);

            return new CheckFormulaResult(mistakes);
        }

        private void CheckSyntaxInternal(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (TryCheckFunction(mistakes, nodes, ref i))
                    continue;

                var nextIndex = GetNextIndex(nodes, i, end);
                CheckMissedOperator(mistakes, nodes, i, nextIndex);
                CheckNodeBetweenNumbers(mistakes, nodes, i, nextIndex);
                CheckMissedNumber(mistakes, nodes, i, nextIndex);
                CheckFunctionBody(mistakes, nodes, i, nextIndex);

                i = nextIndex - 1;
            }
        }

        private int GetNextIndex(List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (nodes[i] is FunctionNode)
                    return i;
            }

            return end;
        }

        #region Function

        private bool TryCheckFunction(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, ref int index)
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
                    CheckSyntaxInternal(mistakes, nodes, partIndexStart, localIndex);

                    partIndexStart = localIndex + 1;
                }

                else if (node is BracketNode bracketNode && bracketCounter.Proceed(bracketNode))
                {
                    if (!canReadPart)
                        break;

                    CheckSyntaxInternal(mistakes, nodes, partIndexStart, localIndex);

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

        private static bool IsNextSemicolon(List<BaseFormulaNode> nodes, int index)
        {
            var nextNode = index + 1 < nodes.Count ? nodes[index + 1] : null;
            var nextFunctionChar = nextNode as FunctionCharNode;
            var nextIsSemicolon = nextFunctionChar?.FunctionChar == FunctionChar.Semicolon;
            return nextIsSemicolon;
        }

        #endregion

        #region Bracket

        private void CheckBracketsCount(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
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
                var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.WrongBracketsNumber, "Wrong number of brackets");
                mistakes.Add(mistakeItem);
            }
        }



        private void CheckEmptyBrackets(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start + 1; i < end; i++)
            {
                var prevNode = nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (!(prevNode is FunctionNode) && IsOpenBracket(node) && IsCloseBracket(nextNode))
                {
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.EmptyBrackets, "Empty brackets");
                    mistakes.Add(mistakeItem);
                }
            }
        }



        #endregion

        #region Checkers

        private void CheckMissedOperator(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var isLast = i + 1 == end;

                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if ((IsOperatorableNode(prevNode) || IsCloseBracket(prevNode)) && IsOperatorableNode(node))
                {
                    var msg = $"Between \"{prevNode}\" and \"{node}\" must be an operator";
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.OperatorBetweenPrevAndCurrentNode, msg, prevNode.ToString(), node.ToString());
                    mistakes.Add(mistakeItem);
                }

                if (isLast && IsOperatorableNode(node) && (IsOperatorableNode(nextNode) || IsOpenBracket(nextNode)))
                {
                    var msg = $"Between \"{node}\" and \"{nextNode}\" must be an operator";
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.OperatorBetweenCurrentAndNextNode, msg, node.ToString(), nextNode.ToString());
                    mistakes.Add(mistakeItem);
                }
            }
        }

        private void CheckNodeBetweenNumbers(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (IsOperatorableNode(prevNode) && IsOperatorableNode(nextNode) && !(node is OperatorNode))
                {
                    var msg = $"Between \"{prevNode}\" and \"{nextNode}\" must be an operator, not \"{node}\"";
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.OperatorBetweenPrevAndNextNode, msg, prevNode.ToString(), nextNode.ToString(), node.ToString());
                    mistakes.Add(mistakeItem);
                }
            }
        }

        private void CheckMissedNumber(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var prevNode = i is 0 ? null : nodes[i - 1];
                var node = nodes[i];
                var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

                if (!(IsOperatorableNode(prevNode) || IsCloseBracket(prevNode)) && node is OperatorNode)
                {
                    var prevNodeName = prevNode?.ToString() ?? "NULL";
                    var msg = $"Before \"{node}\" must be a number or a closed bracket, not \"{prevNodeName}\"";
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.NumberBeforeNode, msg, node.ToString(), prevNodeName);
                    mistakes.Add(mistakeItem);
                }

                if (node is OperatorNode && !(IsOperatorableNode(nextNode) || IsOpenBracket(nextNode)))
                {
                    var nextNodeName = nextNode?.ToString() ?? "NULL";
                    var msg = $"After \"{node}\" must be a number or an opened bracket, not \"{nextNodeName}\"";
                    var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.NumberAfterNode, msg, node.ToString(), nextNodeName);
                    mistakes.Add(mistakeItem);
                }
            }
        }

        private void CheckFunctionBody(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
        {
            if (start + 1 != end)
                return;

            var node = nodes[start];
            if (node is FunctionCharNode)
            {
                var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.EmptyFunctionBody, "Empty function's body");
                mistakes.Add(mistakeItem);
            }
        }

        #endregion

        private FormulaCheckerModel CreateMistakeModel(FormulaCheckerMistakeType key, string message, params string[] parts)
        {
            if (parts != null)
            {
                return new FormulaCheckerModel(key, message, parts);
            }

            return new FormulaCheckerModel(key, message);
        }

        private bool IsOpenBracket(BaseFormulaNode node)
        {
            return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Open;
        }

        private bool IsCloseBracket(BaseFormulaNode node)
        {
            return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Close;
        }

        private bool IsOperatorableNode(BaseFormulaNode node)
        {
            return node is ValueNode
                || node is VariableNode
                || node is WordNode
                || node is FunctionNode;
        }
    }
}