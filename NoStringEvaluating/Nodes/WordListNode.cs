using System.Collections.Generic;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - WordList
/// </summary>
public class WordListNode : BaseFormulaNode
{
    /// <summary>
    /// WordList
    /// </summary>
    public List<string> WordList { get; }

    /// <summary>
    /// Formula node - WordList
    /// </summary>
    public WordListNode(List<string> wordList) : base(NodeTypeEnum.WordList)
    {
        WordList = wordList;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return string.Join(", ", WordList);
    }
}
