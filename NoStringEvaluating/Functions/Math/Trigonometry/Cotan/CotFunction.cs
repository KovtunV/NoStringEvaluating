using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cotan;

/// <summary>
/// Function - cot
/// </summary>
public sealed class CotFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "COT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return 1 / System.Math.Tan(args[0].Number);
    }
}
