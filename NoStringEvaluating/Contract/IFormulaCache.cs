using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Contract
{
    /// <summary>
    /// Parsed formula cache
    /// </summary>
    public interface IFormulaCache
    {
        /// <summary>
        /// Return cached formula nodes 
        /// </summary>
        FormulaNodes GetFormulaNodes(string formula);
    }
}
