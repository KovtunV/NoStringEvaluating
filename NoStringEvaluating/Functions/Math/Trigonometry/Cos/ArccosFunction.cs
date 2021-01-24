using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos
{
    /// <summary>
    /// Function - arccos
    /// </summary>
    public class ArccosFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ARCCOS";

        /// <summary>
        /// Evaluate value
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public double Execute(List<double> args)
        {
            return System.Math.Acos(args[0]);
        }
    }
}
