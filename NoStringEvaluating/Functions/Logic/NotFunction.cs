using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - not
/// </summary>
public sealed class NotFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "NOT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return factory.Boolean.Create(!args[0].Boolean);
    }
}
