using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - FunctionChar
    /// </summary>
    public class FunctionCharNode : IFormulaNode
    {
        /// <summary>
        /// FunctionChar
        /// </summary>
        public FunctionChar FunctionChar { get; }

        /// <summary>
        /// Formula node - FunctionChar
        /// </summary>
        public FunctionCharNode(FunctionChar functionChar)
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
                FunctionChar.Undefined => "ERROR",
                _ => "ERROR"
            };
        }
    }
}
