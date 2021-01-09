using System;
using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

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

            RegisterInternalFunctions();
        }

        private void RegisterInternalFunctions() // TODO: move to static service with type to look in custom assembly
        {
            var funcInterfaceType = typeof(IFunction);

            var types = typeof(FunctionReader).Assembly.GetTypes();
            var filteredTypes = types
                .Where(w => w.IsClass)
                .Where(w => !w.IsAbstract)
                .Where(w => funcInterfaceType.IsAssignableFrom(w))
                .ToArray();

            for (int i = 0; i < filteredTypes.Length; i++)
            {
                var func = (IFunction)Activator.CreateInstance(filteredTypes[i]);
                AddFunction(func);
            }
        }

        /// <summary>
        /// Add function
        /// </summary>
        /// <exception cref="NoStringFunctionException">if exists</exception>
        public void AddFunction(IFunction func, bool replace = false)
        {
            var existedFunc = _functions.Find(f => string.Equals(f.Name, func.Name, StringComparison.InvariantCultureIgnoreCase));
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
        /// Read function name
        /// </summary>
        public bool TryProceedFunction(List<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
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
