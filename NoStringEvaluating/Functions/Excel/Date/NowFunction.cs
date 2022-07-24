using System;
using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns Datetime.Now
/// <para>Now()</para>
/// </summary>
public sealed class NowFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "NOW";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return factory.DateTime.Create(DateTime.Now);
    }
}
