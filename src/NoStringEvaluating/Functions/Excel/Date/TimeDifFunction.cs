﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Calculates the number of hours, minutes, or seconds between two dates
/// <para>TimeDif(dateTime1; dateTime2; 'H'), can be: H, M, S</para>
/// </summary>
public sealed class TimeDifFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "TIMEDIF";

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

        if (format == "H")
        {
            return dateEnd.Subtract(dateStart).TotalHours;
        }

        if (format == "M")
        {
            return dateEnd.Subtract(dateStart).TotalMinutes;
        }

        if (format == "S")
        {
            return dateEnd.Subtract(dateStart).TotalSeconds;
        }

        return default;
    }
}
