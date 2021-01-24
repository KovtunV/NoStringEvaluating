using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec
{
    /// <summary>
    /// Function - arcsech
    /// </summary>
    public class ArcsechFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCSECH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];
            var a = Sqrt(-x * x + 1) + 1;
            return Log(a / x);
        }
    }
}
