using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry;

/// <summary>
/// Function - rad
/// </summary>
public sealed class RadFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "RAD";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        // Math.PI / 180 == 0.017453292519943295
        return 0.017453292519943295 * args[0].Number;
    }
}
