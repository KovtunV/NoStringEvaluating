using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos
{
    /// <summary>
    /// Function - cos
    /// </summary>
    public class CosFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "COS";

        /// <summary>
        /// Evaluate va;ue
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Cos(args[0]);
        }
    }
}
