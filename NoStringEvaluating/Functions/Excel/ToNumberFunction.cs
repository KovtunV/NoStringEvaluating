using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel;

/// <summary>
/// ToNumber('05')
/// </summary>
public sealed class ToNumberFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "TONUMBER";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = true;

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var arg = args[0];
        if (arg.IsNumber)
        {
            return arg;
        }

        if (arg.IsNull)
        {
            return double.NaN;
        }

        var numberWord = arg.Word;

        if (double.TryParse(numberWord, NumberStyles.Any, RusCulture, out var res))
            return res;

        if (double.TryParse(numberWord, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
            return res;

        return double.NaN;
    }

    private static CultureInfo RusCulture { get; } = CultureInfo.GetCultureInfo("ru-RU");
}
