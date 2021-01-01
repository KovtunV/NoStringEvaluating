using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Contract
{
    /// <summary>
    /// Function reader
    /// </summary>
    public interface IFunctionReader
    {
        /// <summary>
        /// Add function
        /// </summary>
        /// <exception cref="NoStringFunctionException">if exists</exception>
        void AddFunction(IFunction func, bool replace = false);

        /// <summary>
        /// Read function name
        /// </summary>
        bool TryProceedFunction(IList<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index);
    }
}
