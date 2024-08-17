using System.Collections.Concurrent;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.Helpers;

internal class VariablesContainer : IVariablesContainer
{
    private readonly ConcurrentDictionary<string, IVariable> _dict = new();

    public IVariable AddOrUpdate(string name, EvaluatorValue value)
    {
        var variable = new Variable { Name = name, Value = value };
        _dict[name] = variable;

        return variable;
    }

    public EvaluatorValue GetValue(string name)
    {
        return _dict[name].Value;
    }

    public bool TryGetValue(string name, out EvaluatorValue value)
    {
        var res = _dict.TryGetValue(name, out var variable);

        value = variable.Value;

        return res;
    }

    public IEnumerable<EvaluatorValue> GetAllValues()
    {
        return _dict.Select(x => x.Value.Value);
    }

    internal struct Variable : IVariable
    {
        public string Name { get; set; }

        public EvaluatorValue Value { get; set; }
    }
}
