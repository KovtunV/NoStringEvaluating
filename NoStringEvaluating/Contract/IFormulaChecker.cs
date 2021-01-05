using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Models;

namespace NoStringEvaluating.Contract
{
    /// <summary>
    /// Syntax checker
    /// </summary>
    public interface IFormulaChecker
    {
        /// <summary>
        /// Check syntax
        /// </summary>
        CheckFormulaResult CheckSyntax(string formula);

        /// <summary>
        /// Check syntax
        /// </summary>
        CheckFormulaResult CheckSyntax(ReadOnlySpan<char> formula);
    }
}
