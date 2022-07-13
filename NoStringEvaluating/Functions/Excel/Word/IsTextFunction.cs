using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using System.Collections.Generic;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// IsText('my text')
    /// </summary>
    public class IsTextFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get;} = "ISTEXT";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return args[0].IsWord ? 1 : 0;
        }
    }
}
