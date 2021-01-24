using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - iff
    /// </summary>
    public class IffFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "IFF";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            for (int i = 0; i < args.Count - 1; i += 2)
            {
                if (System.Math.Abs(args[i]) > NoStringEvaluatorConstants.FloatingTolerance)
                {
                    return args[i + 1];
                }
            }

            return double.NaN;
        }
    }
}
