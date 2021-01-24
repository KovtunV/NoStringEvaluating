using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec
{
    /// <summary>
    /// Function - sec
    /// </summary>
    public class SecFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "SEC";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
           return 1 / System.Math.Cos(args[0]);
        }
    }
}
