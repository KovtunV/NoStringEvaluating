using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Logic
{
    /// <summary>
    /// Function - if
    /// </summary>
    public class IfFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "IF";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            if (System.Math.Abs(args[0]) > NoStringEvaluatorConstants.FloatingTolerance)
            {
                return args[1];
            }
        
            return args[2];
        }
    }
}
