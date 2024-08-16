using System.Globalization;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Boolean
/// </summary>
public class BooleanNode(bool value) : BaseFormulaNode(NodeTypeEnum.Boolean)
{
    /// <summary>
    /// Boolean
    /// </summary>
    public bool Value { get; } = value;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
