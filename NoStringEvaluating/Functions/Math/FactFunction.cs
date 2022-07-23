using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - fact
/// </summary>
public sealed class FactFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "FACT";

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

        var res = 1d;
        for (int i = 2; i <= n; i++)
        {
            res *= i;
        }

        return res;
    }
}
