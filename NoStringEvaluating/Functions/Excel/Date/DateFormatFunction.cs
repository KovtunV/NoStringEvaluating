using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Date;

/// <summary>
/// DateFormat(Now(); 'yyyy MMMM')
/// </summary>
public sealed class DateFormatFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "DATEFORMAT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var dateArg = args[0];
        var formatArg = args[1];

        var strRes = dateArg.DateTime.ToString(formatArg.Word);
        return factory.Word.Create(strRes);
    }
}
