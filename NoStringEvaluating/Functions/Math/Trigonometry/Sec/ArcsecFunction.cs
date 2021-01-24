using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec
{
    /// <summary>
    /// Function - arcsec
    /// </summary>
    public class ArcsecFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCSEC";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];

            // 2 * Atan(1) == 1.5707963267948966
            return 1.5707963267948966 - Atan(Sign(x) / Sqrt(x * x - 1));
        }
    }
}
