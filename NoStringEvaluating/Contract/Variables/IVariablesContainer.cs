using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Contract.Variables
{
    /// <summary>
    /// Container with variables
    /// </summary>
    public interface IVariablesContainer
    {
        /// <summary>
        /// Add or update variable
        /// </summary>
        IVariable AddOrUpdate(string name, double value);

        /// <summary>
        /// Return variable's value
        /// </summary>
        EvaluatorValue GetValue(string name);

        /// <summary>
        /// Return variable's value if possible
        /// </summary>
        bool TryGetValue(string name, out EvaluatorValue value);
    }
}
