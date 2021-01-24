using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry
{
    /// <summary>
    /// Function - deg
    /// </summary>
    public class DegFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "DEG";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            // 180 / Math.PI == 57.295779513082323
            return 57.295779513082323 * args[0];
        }
    }
}
