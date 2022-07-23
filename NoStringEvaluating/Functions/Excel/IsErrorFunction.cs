using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using System.Collections.Generic;

namespace NoStringEvaluating.Functions.Excel
{
    /// <summary>
    /// IsError(ToNumber('Text'))
    /// </summary>
    public sealed class IsErrorFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "ISERROR";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return double.IsNaN(args[0]) ? 1 : 0;
        }
    }
}
