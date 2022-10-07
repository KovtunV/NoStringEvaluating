using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry;

/// <summary>
/// Function - deg
/// </summary>
public sealed class DegFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "DEG";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        // 180 / Math.PI == 57.295779513082323
        return 57.295779513082323 * args[0].Number;
    }
}
