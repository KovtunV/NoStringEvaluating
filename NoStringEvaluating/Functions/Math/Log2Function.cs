using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - log2
    /// </summary>
    public class Log2Function : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "LOG2";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Log2(args[0]);
        }
    }
}
