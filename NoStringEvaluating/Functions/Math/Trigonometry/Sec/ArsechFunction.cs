using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using static System.Math;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec;

/// <summary>
/// Function - arsech
/// </summary>
public sealed class ArsechFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ARSECH";

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
        var a = Sqrt(-x * x + 1) + 1;
        return Log(a / x);
    }
}
