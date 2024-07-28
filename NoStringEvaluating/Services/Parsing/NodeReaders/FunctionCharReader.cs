﻿using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Read function's char
/// </summary>
public static class FunctionCharReader
{
    /// <summary>
    /// Read function's char
    /// </summary>
    public static bool TryProceedFunctionChar(List<BaseFormulaNode> nodes, char ch)
    {
        if (ch == FUNCTION_SEPARATOR_SEMICOLON)
        {
            var node = new FunctionCharNode(FunctionChar.Semicolon);
            nodes.Add(node);
            return true;
        }
        else if (ch == FUNCTION_SEPARATOR_COMMA)
        {
            var node = new FunctionCharNode(FunctionChar.Comma);
            nodes.Add(node);
            return true;
        }

        return false;
    }

    private const char FUNCTION_SEPARATOR_SEMICOLON = ';';
    private const char FUNCTION_SEPARATOR_COMMA = ',';
}
