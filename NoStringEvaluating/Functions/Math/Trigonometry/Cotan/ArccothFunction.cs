using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cotan
{
    /// <summary>
    /// Function - arccoth
    /// </summary>
    public class ArccothFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCCOTH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];
            return System.Math.Log((x + 1) / (x - 1)) / 2;
        }
    }
}
