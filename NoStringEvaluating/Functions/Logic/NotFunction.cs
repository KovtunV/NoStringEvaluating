using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - not
    /// </summary>
    public class NotFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "NOT";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(IList<double> args)
        {
            return System.Math.Abs(args[0]) < NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0;
        }
    }
}
