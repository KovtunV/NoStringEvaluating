using System;
using System.Collections.Generic;
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
        /// Remove function
        /// </summary>
        public void RemoveFunction(string functionName);

        /// <summary>
        /// Read function name
        /// </summary>
        bool TryProceedFunction(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index);
    }
}
