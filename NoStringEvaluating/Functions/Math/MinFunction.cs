using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - min
    /// </summary>
    public class MinFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MIN";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var min = args[0];

            for (int i = 1; i < args.Count; i++)
            {
                var current = args[i];

                if (current < min)
                    min = current;
            }

            return min;
        }
    }
}
