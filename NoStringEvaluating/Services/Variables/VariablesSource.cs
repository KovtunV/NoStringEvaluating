using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Services.Variables;

internal readonly struct VariablesSource
{
    private readonly IVariablesContainer _variablesContainer;
    private readonly IDictionary<string, EvaluatorValue> _variablesDict;

    private VariablesSource(IVariablesContainer variablesContainer, IDictionary<string, EvaluatorValue> variablesDict)
    {
        _variablesContainer = variablesContainer;
        _variablesDict = variablesDict;
    }

    internal EvaluatorValue GetValue(string name)
    {
        // Check an implemented container
        if (_variablesContainer != null) return GetValueFromContainer(name);
        // Check a dictionary
        if (_variablesDict != null && _variablesDict.TryGetValue(name, out var value)) return value;
        // Not in there, return null value
        return default(InternalEvaluatorValue);
    }

    private EvaluatorValue GetValueFromContainer(string name)
    {
        if (_variablesContainer.TryGetValue(name, out var value)) return value;
        // Not in there return null value
        return default(InternalEvaluatorValue);
    }

    internal static VariablesSource Create(IVariablesContainer variablesContainer)
    {
        return new VariablesSource(variablesContainer, variablesDict: null);
    }

    internal static VariablesSource Create(IDictionary<string, EvaluatorValue> variablesDict)
    {
        return new VariablesSource(variablesContainer: null, variablesDict);
    }
}
