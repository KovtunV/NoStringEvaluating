using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - fib
/// </summary>
public sealed class FibFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "FIB";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var n = args[0].Number;
        var a = 0d;
        var b = 1d;

        for (int i = 0; i < n; i++)
        {
            var temp = a;
            a = b;
            b = temp + b;
        }

        return a;
    }
}
