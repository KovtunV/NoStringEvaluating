using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Takes a date and returns a number between 1-7 representing the day of week
/// <para>WeekDay(Today())</para>
/// </summary>
public sealed class WeekDayFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "WEEKDAY";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var weekDay = (int)args[0].DateTime.DayOfWeek + 1;
        return weekDay;
    }
}
