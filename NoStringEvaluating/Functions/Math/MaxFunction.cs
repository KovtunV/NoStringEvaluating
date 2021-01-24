using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - max
    /// </summary>
    public class MaxFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MAX";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var max = args[0];

            for (int i = 1; i < args.Count; i++)
            {
                var current = args[i];

                if (current > max)
                    max = current;
            }

            return max;
        }
    }
}
