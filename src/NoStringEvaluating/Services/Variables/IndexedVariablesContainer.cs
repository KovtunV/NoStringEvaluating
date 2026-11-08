using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Services.Variables;

/// <summary>
/// Variables with stable, zero-based indexes assigned in insertion order. Names are case-sensitive
/// </summary>
/// <remarks>
/// Bind a formula once with <see cref="Binding.FormulaBinder"/>, then update values with <see cref="AddOrUpdate"/>.
/// Adding or updating variables preserves existing indexes. Concurrent reads are supported only while the container is not being modified
/// </remarks>
public sealed class IndexedVariablesContainer : IIndexedVariablesContainer
{
    private readonly Dictionary<string, int> _indexes = new(StringComparer.Ordinal);
    private readonly List<EvaluatorValue> _values = [];

    /// <summary>
    /// Creates a container, optionally copying initial variables in their enumeration order
    /// </summary>
    public IndexedVariablesContainer(IDictionary<string, EvaluatorValue> variables = null)
    {
        if (variables is null)
        {
            return;
        }

        foreach (var variable in variables)
        {
            AddOrUpdate(variable.Key, variable.Value);
        }
    }

    /// <summary>
    /// Indexes belong to this container; another container is read by name even if its names are the same
    /// </summary>
    public object BindingKey => this;

    /// <inheritdoc/>
    public int IndexOf(string name)
    {
        return _indexes.TryGetValue(name, out var index) ? index : -1;
    }

    /// <inheritdoc/>
    public bool TryGetValue(int index, out EvaluatorValue value)
    {
        if (index >= 0 && index < _values.Count)
        {
            value = _values[index];
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Adds or updates a value without changing existing indexes. Returns a snapshot of the variable;
    /// use this method to make subsequent updates to the container
    /// </summary>
    public IVariable AddOrUpdate(string name, EvaluatorValue value)
    {
        if (_indexes.TryGetValue(name, out var index))
        {
            _values[index] = value;
        }
        else
        {
            _indexes.Add(name, _values.Count);
            _values.Add(value);
        }

        return new Variable { Name = name, Value = value };
    }

    /// <summary>
    /// Returns the value, or a null evaluator value when the name is not present
    /// </summary>
    public EvaluatorValue GetValue(string name)
    {
        return TryGetValue(name, out var value) ? value : default;
    }

    /// <inheritdoc/>
    public bool TryGetValue(string name, out EvaluatorValue value)
    {
        if (_indexes.TryGetValue(name, out var index))
        {
            value = _values[index];
            return true;
        }

        value = default;
        return false;
    }

    private struct Variable : IVariable
    {
        public string Name { get; set; }

        public EvaluatorValue Value { get; set; }
    }
}
