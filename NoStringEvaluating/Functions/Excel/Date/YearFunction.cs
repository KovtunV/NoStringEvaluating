using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// Returns a year from dateTime
/// <para>Year(Now()) or Year(Now(); 'YY')</para>
/// </summary>
public sealed class YearFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "YEAR";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var wordFactory = factory.Word();
        var dateVal = args[0].GetDateTime();

        if (args.Count > 1 && args[1].IsWord && args[1].GetWord().Length == 2)
        {
            var strRes = dateVal.ToString("yy");
            return wordFactory.Create(strRes);
        }

        return wordFactory.Create(dateVal.Year.ToString());
    }
}
