using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date
{
    /// <summary>
    /// Returnts a day from dateTime
    /// <para>Day(Now())</para>
    /// </summary>
    public class DayFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "DAY";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var arg = args[0];
            if (arg.IsDateTime)
            {
                return arg.GetDateTime().Day;
            }

            return double.NaN;
        }
    }
}
