using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date
{
    /// <summary>
    /// Returns a year from dateTime
    /// <para>Year(Now())</para>
    /// </summary>
    public class YearFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "YEAR";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var arg = args[0];
            if (arg.IsDateTime)
            {
                return arg.GetDateTime().Year;
            }

            return double.NaN;
        }
    }
}
