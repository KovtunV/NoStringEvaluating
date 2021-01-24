using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - fib
    /// </summary>
    public class FibFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "FIB";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public double Execute(List<double> args)
        {
            var n = args[0];
            var a = 0d;
            var b = 1d;

            for (int i = 0; i < n; i++)
            {
                var temp = a;
                a = b;
                b = temp + b;
            }
            
            return a;
        }
    }
}
