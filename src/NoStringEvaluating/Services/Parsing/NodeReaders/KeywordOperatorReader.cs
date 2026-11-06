using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Keyword operator reader: and, or (case insensitive) between two operands
/// </summary>
public static class KeywordOperatorReader
{
    private static readonly (string Name, Operator Key)[] _keywords = [("and", Operator.And), ("or", Operator.Or)];

    /// <summary>
    /// Read keyword operator
    /// </summary>
    public static bool TryProceedKeywordOperator(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // A keyword is an operator after an operand only: where an operand is expected, and(...) and or(...) are the functions
        if (!GlobalOptions.UseKeywordOperators || !IsAfterOperand(nodes))
        {
            return false;
        }

        for (int k = 0; k < _keywords.Length; k++)
        {
            var (name, key) = _keywords[k];

            if (formula.IsWholeWordAt(index, name))
            {
                var operatorNode = new OperatorNode(key);
                nodes.Add(operatorNode);

                index += name.Length - 1;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// A keyword (and, or, not), which is no name of a simple variable while the keyword operators are on
    /// </summary>
    internal static bool IsReservedWord(string name)
    {
        return GlobalOptions.UseKeywordOperators
            && (name.Equals("and", StringComparison.OrdinalIgnoreCase)
                || name.Equals("or", StringComparison.OrdinalIgnoreCase)
                || name.Equals("not", StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsAfterOperand(List<BaseFormulaNode> nodes)
    {
        if (nodes.Count == 0)
        {
            return false;
        }

        var prevNode = nodes[^1];

        return prevNode is not OperatorNode
            && prevNode is not FunctionNode
            && prevNode is not FunctionCharNode
            && !(prevNode is BracketNode bracketNode && bracketNode.Bracket == Bracket.Open);
    }
}
