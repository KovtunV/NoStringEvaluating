using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date
{
    /// <summary>
    /// Calculates the number of days, months, or years between two dates
    /// <para>DateDif(date1; date2; 'Y'), can be: Y, M, D, MD, YM, YD</para>
    /// </summary>
    public class DateDifFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "DATEDIF";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var dateStart = args[0].GetDateTime();
            var dateEnd = args[1].GetDateTime();
            var format = args[2].GetWord().ToUpperInvariant();

            if (dateStart > dateEnd)
            {
                return double.NaN;
            }

            if (format == "Y")
            {
                var yearDiff = dateEnd.Year - dateStart.Year;

                if (dateStart.Month == dateEnd.Month && dateEnd.Day < dateStart.Day || dateEnd.Month < dateStart.Month)
                {
                    yearDiff--;
                }

                return yearDiff;
            }

            if (format == "M")
            {
                var monthDiff = 12 * (dateEnd.Year - dateStart.Year) + dateEnd.Month - dateStart.Month;

                if (dateEnd.Day < dateStart.Day)
                {
                    monthDiff--;
                }

                return monthDiff;
            }

            if (format == "D")
            {
                return dateEnd.Subtract(dateStart).Days;
            }

            if (format == "MD")
            {
                if (dateEnd.Day < dateStart.Day)
                {
                    return double.NaN;
                }

                return dateEnd.Day - dateStart.Day;
            }

            if (format == "YM")
            {
                dateStart = dateStart.AddYears(-dateStart.Year + 1);
                dateEnd = dateEnd.AddYears(-dateEnd.Year + 1);

                if (dateEnd.Month <= dateStart.Month && dateEnd.Day < dateStart.Day)
                {
                    return double.NaN;
                }

                return dateEnd.Month - dateStart.Month;
            }

            if (format == "YD")
            {
                dateStart = dateStart.AddYears(-dateStart.Year + 1);
                dateEnd = dateEnd.AddYears(-dateEnd.Year + 1);

                return dateEnd.Subtract(dateStart).Days;
            }

            return double.NaN;
        }
    }
}
