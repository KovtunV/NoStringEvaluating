using System;
using System.Collections.Generic;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Parsing.NodeReaders;

namespace NoStringEvaluating.Services.Parsing
{
    /// <summary>
    /// Parser from string to object sequence
    /// </summary>
    public class FormulaParser : IFormulaParser
    {
        /// <summary>
        /// Function reader
        /// </summary>
        public IFunctionReader FunctionsReader { get; }

        /// <summary>
        /// Parser from string to object sequence
        /// </summary>
        public FormulaParser(IFunctionReader functionReader)
        {
            FunctionsReader = functionReader;
        }

        /// <summary>
        /// Return parsed formula nodes
        /// </summary>
        public FormulaNodes Parse(string formula)
        {
            return Parse(formula.AsSpan());
        }

        /// <summary>
        /// Return parsed formula nodes
        /// </summary>
        public FormulaNodes Parse(ReadOnlySpan<char> formula)
        {
            var nodes = ParseWithoutRpn(formula);
            var polish = PolishNotationService.GetReversedNodes(nodes);
            return new FormulaNodes(polish);
        }

        internal List<IFormulaNode> ParseWithoutRpn(ReadOnlySpan<char> formula)
        {
            var negativeBracketCounters = new BracketCounters();
            var nodes = new List<IFormulaNode>();

            for (int i = 0; i < formula.Length; i++)
            {
                var ch = formula[i];

                if (ch.IsWhiteSpace())
                    continue;

                if (FunctionCharReader.TryProceedFunctionChar(nodes, ch))
                    continue;

                if (BracketReader.TryProceedOpenBracket(nodes, formula, negativeBracketCounters, ref i))
                    continue;

                if (BracketReader.TryProceedCloseBracket(nodes, formula, negativeBracketCounters, ref i))
                    continue;

                if (VariableReader.TryProceedBorderedVariable(nodes, formula, ref i))
                    continue;

                if (NumberReader.TryProceedNumber(nodes, formula, ref i))
                    continue;

                if (FunctionsReader.TryProceedFunction(nodes, formula, ref i))
                    continue;

                if (VariableReader.TryProceedSimpleVariable(nodes, formula, ref i))
                    continue;

                if (OperatorReader.TryProceedOperator(nodes, formula, ref i))
                    continue;
            }

            return nodes;
        }
    }
}
