using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - sqrt
/// </summary>
public sealed class SqrtFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "SQRT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate function
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Sqrt(args[0].Number);
    }
}
