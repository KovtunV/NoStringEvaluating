using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - log2
/// </summary>
public sealed class Log2Function : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "LOG2";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Log2(args[0]);
    }
}
