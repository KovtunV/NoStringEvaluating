using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - fact
    /// </summary>
    public class FactFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "FACT";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var n = args[0];

            var res = 1d;
            for (int i = 2; i <= n; i++)
            {
                res *= i;
            }

            return res;
        }
    }
}
