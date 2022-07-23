using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Base
{
    /// <summary>
    /// Function
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Does the function allow for input arguments to be null? Return false if NULL input arguments are not supported. Your function can also return  default(InternalEvaluatorValue)  to return a NULL result
        /// </summary>
        bool CanHandleNullArguments { get; } 

        /// <summary>
        /// Evaluate value
        /// </summary>
        InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory);
    }
}
