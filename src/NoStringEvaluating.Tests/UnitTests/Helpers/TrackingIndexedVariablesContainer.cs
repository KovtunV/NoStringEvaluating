using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Variables;

namespace NoStringEvaluating.Tests.UnitTests.Helpers;

// Adds read counters and custom binding layouts to the production container for binder tests
internal class TrackingIndexedVariablesContainer : IIndexedVariablesContainer
{
    private readonly IndexedVariablesContainer _container;
    private readonly bool _indexed;

    public TrackingIndexedVariablesContainer(IDictionary<string, EvaluatorValue> variables = null, bool indexed = true)
    {
        _container = new(variables);
        _indexed = indexed;
        BindingKey = this;
    }

    public object BindingKey { get; set; }

    public int IndexReads { get; private set; }

    public int NameReads { get; private set; }

    public int IndexOf(string name)
    {
        return _indexed ? _container.IndexOf(name) : -1;
    }

    public bool TryGetValue(int index, out EvaluatorValue value)
    {
        IndexReads++;
        return _container.TryGetValue(index, out value);
    }

    public IVariable AddOrUpdate(string name, EvaluatorValue value)
    {
        return _container.AddOrUpdate(name, value);
    }

    public EvaluatorValue GetValue(string name)
    {
        return TryGetValue(name, out var value) ? value : default;
    }

    public bool TryGetValue(string name, out EvaluatorValue value)
    {
        NameReads++;
        return _container.TryGetValue(name, out value);
    }
}
