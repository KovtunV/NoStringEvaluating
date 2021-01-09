using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - add
    /// </summary>
    public class AndFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "AND";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public virtual double Execute(List<double> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (System.Math.Abs(args[i]) < NoStringEvaluatorConstants.FloatingTolerance)
                    return 0;
            }

            return 1;
        }
    }
}
