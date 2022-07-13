using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - not
    /// </summary>
    public sealed class NotFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "NOT";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return System.Math.Abs(args[0]) < NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0;
        }
    }
}
