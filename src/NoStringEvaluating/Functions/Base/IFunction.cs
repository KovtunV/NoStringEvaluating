using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Base;

/// <summary>
/// Function
/// </summary>
public interface IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// If false and any argument is null - function wont be executed and null will be returned
    /// </summary>
    bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory);
}
