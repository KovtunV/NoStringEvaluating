using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Contract
{
    /// <summary>
    /// Math expression evaluator
    /// </summary>
    public interface INoStringEvaluator
    {
        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(string formula, IVariablesContainer variables);
        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(FormulaNodes formulaNodes, IVariablesContainer variables);

        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(string formula, IDictionary<string, double> variables);
        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(FormulaNodes formulaNodes, IDictionary<string, double> variables);

        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(string formula);
        /// <summary>
        /// Calculate formula
        /// </summary>
        double Calc(FormulaNodes formulaNodes);
    }
}
