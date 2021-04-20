using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - add
    /// </summary>
    public class AndFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "AND";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (System.Math.Abs(args[i]) < NoStringEvaluatorConstants.FloatingTolerance)
                    return 0;
            }

            return 1;
        }
    }
}
