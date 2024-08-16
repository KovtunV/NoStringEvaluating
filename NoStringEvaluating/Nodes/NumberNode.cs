using System.Globalization;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Number
/// </summary>
public class NumberNode(double number) : BaseFormulaNode(NodeTypeEnum.Number)
{
    /// <summary>
    /// Number
    /// </summary>
    public double Number { get; } = number;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Number.ToString(CultureInfo.InvariantCulture);
    }
}
