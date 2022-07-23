using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cosec;

/// <summary>
/// Function - acsch
/// </summary>
public sealed class AcschFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ACSCH";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var x = args[0];
        var a = Sign(x) * Sqrt(x * x + 1) + 1;
        return Log(a / x);
    }
}
