using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cosec
{
    /// <summary>
    /// Function - cosec
    /// </summary>
    public class CosecFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "COSEC";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return 1 / System.Math.Sin(args[0]);
        }
    }
}
