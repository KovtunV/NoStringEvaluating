using System.Collections.Generic;

namespace NoStringEvaluating.Functions.Base
{
    /// <summary>
    /// Function
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Evaluate value
        /// </summary>
        double Execute(IList<double> args);
    }
}
