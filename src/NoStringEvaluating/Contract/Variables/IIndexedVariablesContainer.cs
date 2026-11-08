using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Contract.Variables;

/// <summary>
/// Container whose variables have a fixed index, so that a formula bound to it (<see cref="Services.Binding.FormulaBinder"/>) reads them by index instead of by name
/// </summary>
public interface IIndexedVariablesContainer : IVariablesContainer
{
    /// <summary>
    /// What the indexes belong to. A bound formula reads by index only from a container with the same key (reference equality), by name from any other.
    /// By default the container itself; containers that share one index layout can return one shared key
    /// </summary>
    object BindingKey => this;

    /// <summary>
    /// The index of a variable, or -1 when the container has no index for the name: a bound formula reads that variable by name
    /// </summary>
    int IndexOf(string name);

    /// <summary>
    /// Return variable's value by index if possible
    /// </summary>
    bool TryGetValue(int index, out EvaluatorValue value);
}
