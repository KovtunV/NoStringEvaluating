using NoStringEvaluating.Contract;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.FormulaChecker;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Checking;

/// <summary>
/// Syntax checker
/// </summary>
public class FormulaChecker(IFormulaParser formulaParser) : IFormulaChecker
{
    private readonly IFormulaParser _formulaParser = formulaParser;

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

        CheckBracketsCount(mistakes, nodes, 0, nodes.Count);
        CheckEmptyBrackets(mistakes, nodes, 0, nodes.Count);
        CheckNotOperatorableNodes(mistakes, nodes, 0, nodes.Count);

        // Check
        CheckSyntaxInternal(mistakes, nodes, 0, nodes.Count);

        return new CheckFormulaResult(mistakes);
    }

    private void CheckSyntaxInternal(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            if (TryCheckFunction(mistakes, nodes, ref i))
            {
                continue;
            }

            var nextIndex = GetNextIndex(nodes, i, end);
            CheckMissedOperator(mistakes, nodes, i, nextIndex);
            CheckNodeBetweenNumbers(mistakes, nodes, i, nextIndex);
            CheckMissedNumber(mistakes, nodes, i, nextIndex);
            CheckFunctionBody(mistakes, nodes, i, nextIndex);

            i = nextIndex - 1;
        }
    }

    private static int GetNextIndex(List<BaseFormulaNode> nodes, int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            if (nodes[i] is FunctionNode)
            {
                return i;
            }
        }

        return end;
    }

    #region Function

    private bool TryCheckFunction(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, ref int index)
    {
        var localIndex = index;

        if (nodes[localIndex] is not FunctionNode)
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
                {
                    break;
                }

                CheckSyntaxInternal(mistakes, nodes, partIndexStart, localIndex);

                // The end of function
                break;
            }
        }

        index = localIndex;
        return true;
    }

    #endregion

    #region Bracket

    private static void CheckBracketsCount(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        var openBracketCount = 0;
        var closeBracketCount = 0;

        for (int i = start; i < end; i++)
        {
            if (nodes[i] is not BracketNode bracketNode)
            {
                continue;
            }

            if (bracketNode.Bracket == Bracket.Open)
            {
                openBracketCount++;
            }
            else if (bracketNode.Bracket == Bracket.Close)
            {
                closeBracketCount++;
            }
        }

        if (openBracketCount != closeBracketCount)
        {
            var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.WrongBracketsNumber, "Wrong number of brackets");
            mistakes.Add(mistakeItem);
        }
    }

    private static void CheckEmptyBrackets(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        for (int i = start + 1; i < end; i++)
        {
            var prevNode = nodes[i - 1];
            var node = nodes[i];
            var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

            var whenPrevNodeIsNotFunction = prevNode is not FunctionNode && IsOpenBracket(node) && IsCloseBracket(nextNode);
            var whenStartFormula = i == 1 && IsOpenBracket(prevNode) && IsCloseBracket(node);

            if (whenPrevNodeIsNotFunction || whenStartFormula)
            {
                var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.EmptyBrackets, "Empty brackets");
                mistakes.Add(mistakeItem);
            }
        }
    }

    #endregion

    #region Checkers

    private static void CheckMissedOperator(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
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

    private static void CheckNodeBetweenNumbers(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            var prevNode = i is 0 ? null : nodes[i - 1];
            var node = nodes[i];
            var nextNode = i + 1 < nodes.Count ? nodes[i + 1] : null;

            if (IsOperatorableNode(prevNode) && IsOperatorableNode(nextNode) && node is not OperatorNode)
            {
                var msg = $"Between \"{prevNode}\" and \"{nextNode}\" must be an operator, not \"{node}\"";
                var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.OperatorBetweenPrevAndNextNode, msg, prevNode.ToString(), nextNode.ToString(), node.ToString());
                mistakes.Add(mistakeItem);
            }
        }
    }

    private static void CheckMissedNumber(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
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

    private static void CheckFunctionBody(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        if (start + 1 != end)
        {
            return;
        }

        var node = nodes[start];
        if (node is FunctionCharNode)
        {
            var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.EmptyFunctionBody, "Empty function's body");
            mistakes.Add(mistakeItem);
        }
    }

    private static void CheckNotOperatorableNodes(List<FormulaCheckerModel> mistakes, List<BaseFormulaNode> nodes, int start, int end)
    {
        for (int i = start + 1; i < end; i++)
        {
            var prevNode = nodes[i - 1];
            var node = nodes[i];

            if (prevNode is FunctionCharNode && node is FunctionCharNode)
            {
                var mistakeItem = CreateMistakeModel(FormulaCheckerMistakeType.DoubledFunctionCharNodes, "Two or more function chars in a sequence");
                mistakes.Add(mistakeItem);
                break;
            }
        }
    }

    #endregion

    private static FormulaCheckerModel CreateMistakeModel(FormulaCheckerMistakeType key, string message, params string[] parts)
    {
        return new FormulaCheckerModel(key, message, parts);
    }

    private static bool IsOpenBracket(BaseFormulaNode node)
    {
        return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Open;
    }

    private static bool IsCloseBracket(BaseFormulaNode node)
    {
        return node is BracketNode bracketNode && bracketNode.Bracket == Bracket.Close;
    }

    private static bool IsOperatorableNode(BaseFormulaNode node)
    {
        return node is NumberNode
            || node is BooleanNode
            || node is VariableNode
            || node is WordNode
            || node is FunctionNode
            || node is WordListNode
            || node is NumberListNode;
    }
}