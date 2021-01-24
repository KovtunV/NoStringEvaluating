using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sin
{
    /// <summary>
    /// Function - arcsinh
    /// </summary>
    public class ArcsinhFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCSINH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Asinh(args[0]);
        }
    }
}
