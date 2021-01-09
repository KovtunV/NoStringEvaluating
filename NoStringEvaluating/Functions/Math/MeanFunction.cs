using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - mean
    /// </summary>
    public class MeanFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MEAN";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var sum = 0d;
            for (int i = 0; i < args.Count; i++)
            {
                sum += args[i];
            }

            return sum / args.Count;
        }
    }
}
