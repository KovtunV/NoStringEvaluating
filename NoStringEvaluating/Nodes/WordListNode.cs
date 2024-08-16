using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - WordList
/// </summary>
public class WordListNode(List<string> wordList) : BaseFormulaNode(NodeTypeEnum.WordList)
{
    /// <summary>
    /// WordList
    /// </summary>
    public List<string> WordList { get; } = wordList;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return string.Join(", ", WordList);
    }
}
