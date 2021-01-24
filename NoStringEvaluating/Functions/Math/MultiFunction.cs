using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - multi
    /// </summary>
    public class MultiFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MULTI";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            if (args.Count is 0)
                return double.NaN;

            var res = 1d;
            for (int i = 0; i < args.Count; i++)
            {
                res *= args[i];
            }

            return res;
        }
    }
}
