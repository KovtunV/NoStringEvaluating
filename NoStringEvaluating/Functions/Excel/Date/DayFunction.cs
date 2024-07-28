﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns a day from dateTime
/// <para>Day(Now()) or Day(Now(); 'DD')</para>
/// </summary>
public sealed class DayFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "DAY";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordFactory = factory.Word;
        var dateVal = args[0].DateTime;

        if (args.Count > 1 && args[1].IsWord)
        {
            var format = args[1].Word;
            var strRes = dateVal.Day.ToString().PadLeft(format.Length, '0');
            return wordFactory.Create(strRes);
        }

        return wordFactory.Create(dateVal.Day.ToString());
    }
}
