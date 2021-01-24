using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos
{
    /// <summary>
    /// Function - acosh
    /// </summary>
    public class AcoshFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ACOSH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return System.Math.Acosh(args[0]);
        }
    }
}
