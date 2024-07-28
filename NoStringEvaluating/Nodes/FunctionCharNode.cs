using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - FunctionChar
/// </summary>
public class FunctionCharNode : BaseFormulaNode
{
    /// <summary>
    /// FunctionChar
    /// </summary>
    public FunctionChar FunctionChar { get; }

    /// <summary>
    /// Formula node - FunctionChar
    /// </summary>
    public FunctionCharNode(FunctionChar functionChar)
        : base(NodeTypeEnum.FunctionChar)
    {
        FunctionChar = functionChar;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return GetFunctionCharString(FunctionChar);
    }

    private static string GetFunctionCharString(FunctionChar functionChar)
    {
        return functionChar switch
        {
            FunctionChar.Semicolon => ";",
            FunctionChar.Comma => ",",
            FunctionChar.Undefined => "ERROR",
            _ => "ERROR",
        };
    }
}
