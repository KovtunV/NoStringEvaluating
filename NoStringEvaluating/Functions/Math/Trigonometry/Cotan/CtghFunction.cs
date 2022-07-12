using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cotan;

/// <summary>
/// Function - ctgh
/// </summary>
public class CtghFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "CTGH";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return 1 / System.Math.Tanh(args[0]);
    }
}
