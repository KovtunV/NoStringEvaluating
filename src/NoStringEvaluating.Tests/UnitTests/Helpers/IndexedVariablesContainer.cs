using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.UnitTests.Helpers;

// Variables with an index each, in the order they were added (none when indexed is false); counts the reads by index and by name
internal class IndexedVariablesContainer : IIndexedVariablesContainer
{
    private readonly Dictionary<string, int> _indexes = [];
    private readonly List<EvaluatorValue> _values = [];
    private readonly bool _indexed;

    public IndexedVariablesContainer(IDictionary<string, EvaluatorValue> variables = null, bool indexed = true)
    {
        _indexed = indexed;
        BindingKey = this;

        if (variables is null)
        {
            return;
        }

        foreach (var variable in variables)
        {
            AddOrUpdate(variable.Key, variable.Value);
        }
    }

    public object BindingKey { get; set; }

    public int IndexReads { get; private set; }

    public int NameReads { get; private set; }

    public int IndexOf(string name)
    {
        return _indexed && _indexes.TryGetValue(name, out var index) ? index : -1;
    }

    public bool TryGetValue(int index, out EvaluatorValue value)
    {
        IndexReads++;

        if (index >= 0 && index < _values.Count)
        {
            value = _values[index];
            return true;
        }

        value = default;
        return false;
    }

    public IVariable AddOrUpdate(string name, EvaluatorValue value)
    {
        if (_indexes.TryGetValue(name, out var index))
        {
            _values[index] = value;
        }
        else
        {
            _indexes[name] = _values.Count;
            _values.Add(value);
        }

        return new Variable { Name = name, Value = value };
    }

    public EvaluatorValue GetValue(string name)
    {
        return TryGetValue(name, out var value) ? value : default;
    }

    public bool TryGetValue(string name, out EvaluatorValue value)
    {
        NameReads++;

        if (_indexes.TryGetValue(name, out var index))
        {
            value = _values[index];
            return true;
        }

        value = default;
        return false;
    }

    internal struct Variable : IVariable
    {
        public string Name { get; set; }

        public EvaluatorValue Value { get; set; }
    }
}
