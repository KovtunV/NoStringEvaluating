using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos
{
    /// <summary>
    /// Function - cos
    /// </summary>
    public sealed class CosFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "COS";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Evaluate va;ue
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return System.Math.Cos(args[0]);
        }
    }
}
