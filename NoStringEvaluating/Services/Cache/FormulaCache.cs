using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Services.Cache
{
    /// <summary>
    /// Parsed formula cache
    /// </summary>
    public class FormulaCache : IFormulaCache
    {
        private readonly ConcurrentDictionary<string, FormulaNodes> _formulaNodes;
        private readonly IFormulaParser _formulaParser;

        /// <summary>
        /// Parsed formula cache
        /// </summary>
        public FormulaCache(IFormulaParser formulaParser)
        {
            _formulaNodes = new ConcurrentDictionary<string, FormulaNodes>();

            _formulaParser = formulaParser;
        }

        /// <summary>
        /// Return cached formula nodes 
        /// </summary>
        public FormulaNodes GetFormulaNodes(string formula)
        {
            if (!_formulaNodes.TryGetValue(formula, out var formulaNodes))
            {
                formulaNodes = _formulaParser.Parse(formula);

                if (!_formulaNodes.TryAdd(formula, formulaNodes))
                {
                    return GetFormulaNodes(formula);
                }
            }

            return formulaNodes;
        }
    }
}
