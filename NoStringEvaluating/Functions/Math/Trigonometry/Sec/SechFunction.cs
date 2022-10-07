using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sec;

/// <summary>
/// Function - sech
/// </summary>
public sealed class SechFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "SECH";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return 1 / System.Math.Cosh(args[0].Number);
    }
}
