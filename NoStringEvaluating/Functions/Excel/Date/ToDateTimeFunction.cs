using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns datetime value from string
/// <para>ToDateTime('8/15/2002')</para>
/// </summary>
public sealed class ToDateTimeFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "TODATETIME";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var dateTimeFactory = factory.DateTime;

        var dateStr = args[0].Word;
        if (!DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out var res))
        {
            if (!DateTime.TryParse(dateStr, out res))
            {
                return dateTimeFactory.Empty;
            }
        }

        return dateTimeFactory.Create(res);
    }
}
