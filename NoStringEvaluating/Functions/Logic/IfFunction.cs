using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - if
    /// </summary>
    public class IfFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "IF";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            if (System.Math.Abs(args[0]) > NoStringEvaluatorConstants.FloatingTolerance)
            {
                return args[1];
            }
        
            return args[2];
        }
    }
}
