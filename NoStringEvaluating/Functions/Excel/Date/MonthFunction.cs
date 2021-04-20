using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date
{
    /// <summary>
    /// Returns a month from dateTime
    /// <para>Month(Now())</para>
    /// </summary>
    public class MonthFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MONTH";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var arg = args[0];
            if (arg.IsDateTime)
            {
                return arg.GetDateTime().Month;
            }

            return double.NaN;
        }
    }
}
