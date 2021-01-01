using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - add
    /// </summary>
    public class AddFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "ADD";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(IList<double> args)
        {
            var sum = 0d;
            for (int i = 0; i < args.Count; i++)
            {
                sum += args[i];
            }

            return sum;
        }
    }
}
