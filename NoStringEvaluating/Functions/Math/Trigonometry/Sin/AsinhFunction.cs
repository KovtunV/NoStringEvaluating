using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sin
{
    /// <summary>
    /// Function - asinh
    /// </summary>
    public class AsinhFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ASINH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Asinh(args[0]);
        }
    }
}
