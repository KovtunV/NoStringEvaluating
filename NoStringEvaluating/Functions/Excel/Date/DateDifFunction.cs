using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Calculates the number of days, months, or years between two dates
/// <para>DateDif(date1; date2; 'Y'), can be: Y, M, D</para>
/// </summary>
public sealed class DateDifFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "DATEDIF";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var dateStart = args[0].DateTime;
        var dateEnd = args[1].DateTime;
        var format = args[2].Word.ToUpperInvariant();

        if (dateStart > dateEnd)
        {
            return default;
        }

        if (format == "Y")
        {
            var yearDiff = dateEnd.Year - dateStart.Year;

            if ((dateStart.Month == dateEnd.Month && dateEnd.Day < dateStart.Day) || dateEnd.Month < dateStart.Month)
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

        return default;
    }
}
