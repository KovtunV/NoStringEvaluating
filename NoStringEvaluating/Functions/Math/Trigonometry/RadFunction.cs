using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry
{
    /// <summary>
    /// Function - rad
    /// </summary>
    public class RadFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "RAD";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            // Math.PI / 180 == 0.017453292519943295
            return 0.017453292519943295 * args[0];
        }
    }
}
