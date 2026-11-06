using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - a keyword (and, or, not) where it is no operator, so the formula is wrong: the checker reports it, the evaluator throws on it
/// </summary>
public class KeywordNode(string keyword) : BaseFormulaNode(NodeTypeEnum.Keyword)
{
    /// <summary>
    /// Keyword
    /// </summary>
    public string Keyword { get; } = keyword;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Keyword;
    }
}
