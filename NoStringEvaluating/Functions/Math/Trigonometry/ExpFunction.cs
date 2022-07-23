using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry;

/// <summary>
/// Function - exp
/// </summary>
public sealed class ExpFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "EXP";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Exp(args[0]);
    }
}
