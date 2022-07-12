using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - log10
/// </summary>
public class Log10Function : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "LOG10";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Log10(args[0]);
    }
}
