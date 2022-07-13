using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using System.Collections.Generic;

namespace NoStringEvaluating.Functions.Excel
{
    /// <summary>
    /// IsNumber(5)
    /// </summary>
    public sealed class IsNumberFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "ISNUMBER";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return args[0].IsNumber ? 1 : 0;
        }
    }
}
