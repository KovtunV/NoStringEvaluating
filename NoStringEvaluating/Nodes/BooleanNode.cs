using System.Globalization;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - Boolean
/// </summary>
public class BooleanNode : BaseFormulaNode
{
    /// <summary>
    /// Boolean
    /// </summary>
    public bool Value { get; }

    /// <summary>
    /// Formula node - Boolean
    /// </summary>
    public BooleanNode(bool value) : base(NodeTypeEnum.Boolean)
    {
        Value = value;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
