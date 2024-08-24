using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Bracket
/// </summary>
public class BracketNode(Bracket bracket) : BaseFormulaNode(NodeTypeEnum.Bracket)
{
    /// <summary>
    /// Bracket
    /// </summary>
    public Bracket Bracket { get; } = bracket;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return GetBracketString(Bracket);
    }

    private static string GetBracketString(Bracket bracket)
    {
        return bracket switch
        {
            Bracket.Open => "(",
            Bracket.Close => ")",
            Bracket.Undefined => "ERROR",
            _ => "ERROR",
        };
    }
}
