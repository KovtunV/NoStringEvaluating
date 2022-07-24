using System;
using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns the current date
/// <para>Today()</para>
/// </summary>
public sealed class TodayFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "TODAY";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return factory.DateTime().Create(DateTime.Today);
    }
}
