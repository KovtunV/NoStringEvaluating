using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes;

/// <summary>
/// Formula node - FunctionChar
/// </summary>
public class FunctionCharNode(FunctionChar functionChar) : BaseFormulaNode(NodeTypeEnum.FunctionChar)
{
    /// <summary>
    /// FunctionChar
    /// </summary>
    public FunctionChar FunctionChar { get; } = functionChar;

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
