using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cotan
{
    /// <summary>
    /// Function - coth
    /// </summary>
    public class CothFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "COTH";

        /// <summary>
        /// Evaluate function
        /// </summary>
        public double Execute(List<double> args)
        {
            return 1 / System.Math.Tanh(args[0]);
        }
    }
}
