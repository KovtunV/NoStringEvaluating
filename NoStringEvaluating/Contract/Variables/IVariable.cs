using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Contract.Variables
{
    /// <summary>
    /// Variable model
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        EvaluatorValue Value { get; set; }
    }
}
