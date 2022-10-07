using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Adds a number of hours to a datetime
/// </summary>
public sealed class AddHoursFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ADDHOURS";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var dateVal = args[0].DateTime;

        if (args.Count == 2 && args[1].IsNumber)
        {
            return factory.DateTime.Create(dateVal.AddHours(args[1].Number));
        }

        // If we get weird input we just return the first argument
        return args[0];
    }
}
