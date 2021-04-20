using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date
{
    /// <summary>
    /// Returns datetime value from string
    /// <para>ToDateTime('8/15/2002')</para>
    /// </summary>
    public class ToDateTimeFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "TODATETIME";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var dateTimeFactory = factory.DateTime();
  
            var dateStr = args[0].GetWord();
            if (!DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out var res))
            {
                if (!DateTime.TryParse(dateStr, out res))
                {
                    dateTimeFactory.Empty();
                }
            }

            return dateTimeFactory.Create(res);
        }
    }
}
