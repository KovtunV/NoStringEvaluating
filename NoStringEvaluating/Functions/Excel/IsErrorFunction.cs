using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using System.Collections.Generic;

namespace NoStringEvaluating.Functions.Excel
{
    /// <summary>
    /// IsError(ToNumber('Text'))
    /// </summary>
    public class IsErrorFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ISERROR";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return double.IsNaN(args[0]) ? 1 : 0;
        }
    }
}
