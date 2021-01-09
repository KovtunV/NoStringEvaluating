using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - or
    /// </summary>
    public class OrFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "OR";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public virtual double Execute(List<double> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (System.Math.Abs(args[i]) > NoStringEvaluatorConstants.FloatingTolerance)
                    return 1;
            }

            return 0;
        }
    }
}
