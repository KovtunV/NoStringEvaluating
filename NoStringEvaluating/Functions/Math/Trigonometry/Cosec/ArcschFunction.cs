using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cosec
{
    /// <summary>
    /// Function - arcsch
    /// </summary>
    public class ArcschFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCSCH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];
            var a = Sign(x) * Sqrt(x * x + 1) + 1;
            return Log(a / x);
        }
    }
}
