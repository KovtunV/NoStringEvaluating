using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sin;

/// <summary>
/// Function - asin
/// </summary>
public sealed class AsinFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ASIN";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Asin(args[0]);
    }
}
