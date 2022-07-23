using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos;

/// <summary>
/// Function - acos
/// </summary>
public sealed class AcosFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ACOS";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Acos(args[0]);
    }
}
