using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cos;

/// <summary>
/// Function - cosh
/// </summary>
public sealed class CoshFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "COSH";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Cosh(args[0].Number);
    }
}
