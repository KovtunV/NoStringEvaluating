using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos;

/// <summary>
/// Function - arccos
/// </summary>
public class ArccosFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "ARCCOS";

    /// <summary>
    /// Evaluate value
    /// </summary>
    /// <param name="args"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Acos(args[0]);
    }
}
