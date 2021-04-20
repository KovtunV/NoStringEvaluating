using System;
using System.Collections.Generic;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using static NoStringEvaluating.NoStringFunctionsInitializer;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Function reader
    /// </summary>
    public class FunctionReader : IFunctionReader
    {
        private readonly List<IFunction> _functions;

        /// <summary>
        /// Function reader
        /// </summary>
        public FunctionReader()
        {
            _functions = new List<IFunction>();

            // Initialize functions
            InitializeFunctions(this, typeof(FunctionReader));
        }

        /// <summary>
        /// Add function
        /// </summary>
        /// <exception cref="NoStringFunctionException">if exists</exception>
        public void AddFunction(IFunction func, bool replace = false)
        {
            var existedFunc = _functions.Find(f => string.Equals(f.Name, func.Name, StringComparison.OrdinalIgnoreCase));
            if (existedFunc != null)
            {
                if (replace)
                {
                    _functions.Remove(existedFunc);
                }
                else
                {
                    throw new NoStringFunctionException(func.Name);
                }
            }

            _functions.Add(func);
        }

        /// <summary>
        /// Remove function
        /// </summary>
        public void RemoveFunction(string functionName)
        {
            _functions.RemoveAll(q => string.Equals(q.Name, functionName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Read function name
        /// </summary>
        public bool TryProceedFunction(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
        {
            // Read unary minus
            var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

            // Read function
            var functionNameBuilder = new NameBuilder();
            for (int fInd = 0; fInd < _functions.Count; fInd++)
            {
                var function = _functions[fInd];

                // Set function name
                functionNameBuilder.Reset(function.Name);

                for (int i = localIndex; i < formula.Length; i++)
                {
                    var ch = formula[i];

                    if (functionNameBuilder.TryRemember(ch))
                    {
                        if (functionNameBuilder.IsFinished && IsBracketNext(formula, i + 1))
                        {
                            var functionNode = new FunctionNode(function, isNegativeLocal);
                            nodes.Add(functionNode);

                            index = i;
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return false;
        }

        private static bool IsBracketNext(ReadOnlySpan<char> formula, int index)
        {
            for (int i = index; i < formula.Length; i++)
            {
                var ch = formula[i];

                if (ch.IsWhiteSpace())
                    continue;

                return ch is OPEN_BRACKET;
            }

            return false;
        }

        private const char OPEN_BRACKET = '(';
    }
}
