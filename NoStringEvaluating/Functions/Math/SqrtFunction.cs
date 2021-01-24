using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - sqrt
    /// </summary>
    public class SqrtFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "SQRT";

        /// <summary>
        /// Evaluate function
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Sqrt(args[0]);
        }
    }
}
