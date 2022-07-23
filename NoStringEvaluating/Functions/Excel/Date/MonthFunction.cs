using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns a month from dateTime
/// <para>Month(Now()) or Month(Now(); 'MM')</para>
/// </summary>
public sealed class MonthFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "MONTH";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordFactory = factory.Word();
        var dateVal = args[0].GetDateTime();

        if (args.Count > 1 && args[1].IsWord)
        {
            var format = args[1].GetWord();
            var strRes = dateVal.Month.ToString().PadLeft(format.Length, '0');
            return wordFactory.Create(strRes);
        }

        return wordFactory.Create(dateVal.Month.ToString());
    }
}
