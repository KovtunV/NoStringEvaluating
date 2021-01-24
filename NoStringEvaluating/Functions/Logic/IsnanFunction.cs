using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - isNaN
    /// </summary>
    public class IsnanFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ISNAN";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            return double.IsNaN(args[0]) ? 1 : 0;
        }
    }
}
