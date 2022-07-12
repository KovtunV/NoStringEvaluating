using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - sign
/// </summary>
public class SignFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "SIGN";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Sign(args[0]);
    }
}
