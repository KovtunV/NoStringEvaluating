using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cotan
{
    /// <summary>
    /// Function - ctanh
    /// </summary>
    public class CtanhFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "CTANH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return 1 / System.Math.Tanh(args[0]);
        }
    }
}
