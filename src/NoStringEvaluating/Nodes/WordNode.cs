using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Word
/// </summary>
public class WordNode(string word) : BaseFormulaNode(NodeTypeEnum.Word)
{
    /// <summary>
    /// Word
    /// </summary>
    public string Word { get; } = word;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Word;
    }
}
