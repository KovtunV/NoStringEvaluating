using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// DateFormat(Now(); 'yyyy MMMM')
/// </summary>
public class DateFormatFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "DATEFORMAT";

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var dateArg = args[0];
        var formatArg = args[1];

        var strRes = dateArg.GetDateTime().ToString(formatArg.GetWord());
        return factory.Word().Create(strRes);
    }
}
