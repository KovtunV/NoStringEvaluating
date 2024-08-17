using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// List reader
/// </summary>
public static class ListReader
{
    /// <summary>
    /// Read list
    /// </summary>
    public static bool TryProceedList(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out _);

        // Check out of range
        if (localIndex >= formula.Length)
        {
            return false;
        }

        // Read list
        if (formula[localIndex] != OPEN_BRACKET)
        {
            return false;
        }

        // Skip start char
        localIndex++;

        // List nodes buffer
        var listNodes = new List<BaseFormulaNode>();

        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];

            if (ch == CLOSE_BRACKET)
            {
                var wordNodesCount = listNodes.Count(c => c.TypeKey == NodeTypeEnum.Word);
                var numberNodesCount = listNodes.Count(c => c.TypeKey == NodeTypeEnum.Number);

                if (numberNodesCount >= wordNodesCount)
                {
                    var numberList = listNodes
                        .Where(c => c.TypeKey == NodeTypeEnum.Number).Cast<NumberNode>()
                        .Select(s => s.Number).ToList();

                    var listNode = new NumberListNode(numberList);
                    nodes.Add(listNode);
                }
                else
                {
                    var wordList = listNodes
                        .Where(c => c.TypeKey == NodeTypeEnum.Word).Cast<WordNode>()
                        .Select(s => s.Word).ToList();

                    var listNode = new WordListNode(wordList);
                    nodes.Add(listNode);
                }

                index = i;
                return true;
            }

            if (NumberReader.TryProceedNumber(listNodes, formula, ref i))
            {
                continue;
            }

            if (WordReader.TryProceedWord(listNodes, formula, ref i))
            {
                continue;
            }
        }

        return false;
    }

    private const char OPEN_BRACKET = '{';
    private const char CLOSE_BRACKET = '}';
}
