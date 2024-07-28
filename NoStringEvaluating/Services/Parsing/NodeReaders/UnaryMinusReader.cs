﻿using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Reader for unary minus
/// </summary>
public static class UnaryMinusReader
{
    /// <summary>
    /// Read unary minus and return next index
    /// </summary>
    public static int ReadUnaryMinus(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, int index, out bool isNegative)
    {
        var isNegativeLocal = false;
        var localIndex = index;
        for (; localIndex < formula.Length; localIndex++)
        {
            var ch = formula[localIndex];

            if (ch.IsWhiteSpace())
            {
                continue;
            }

            if (!TryProceedUnaryMinus(nodes, ch, ref isNegativeLocal))
            {
                break;
            }
        }

        isNegative = isNegativeLocal;
        return localIndex;
    }

    /// <summary>
    /// Read unary minus
    /// </summary>
    private static bool TryProceedUnaryMinus(List<BaseFormulaNode> nodes, char ch, ref bool isNegative)
    {
        var prevNode = nodes.Count > 0 ? nodes[^1] : null;

        // Unary minus
        if (ch == MINUS_CHAR && !IsOperatorableNode(prevNode) && !(prevNode is BracketNode br && br.Bracket == Bracket.Close))
        {
            isNegative = !isNegative;
            return true;
        }

        return false;
    }

    private static bool IsOperatorableNode(BaseFormulaNode node)
    {
        return node is NumberNode
            || node is VariableNode
            || node is WordNode
            || node is FunctionNode
            || node is WordListNode
            || node is NumberListNode;
    }

    private const char MINUS_CHAR = '-';
}
