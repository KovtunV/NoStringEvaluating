using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec
{
    /// <summary>
    /// Function - asech
    /// </summary>
    public class AsechFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ASECH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];
            var a = Sqrt(-x * x + 1) + 1;
            return Log( a / x);
        }
    }
}
