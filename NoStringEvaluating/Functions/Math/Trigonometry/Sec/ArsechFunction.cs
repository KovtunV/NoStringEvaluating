using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec
{
    /// <summary>
    /// Function - arsech
    /// </summary>
    public class ArsechFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARSECH";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var x = args[0];
            var a = Sqrt(-x * x + 1) + 1;
            return Log(a / x);
        }
    }
}
