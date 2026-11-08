using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Services.Variables;

internal readonly struct VariablesSource
{
    private readonly IVariablesContainer _variablesContainer;
    private readonly IIndexedVariablesContainer _indexedContainer;
    private readonly IDictionary<string, EvaluatorValue> _variablesDict;

    private VariablesSource(IVariablesContainer variablesContainer, IDictionary<string, EvaluatorValue> variablesDict, object bindingKey)
    {
        _variablesContainer = variablesContainer;
        _variablesDict = variablesDict;

        // The indexes of bound variables belong to one container (its BindingKey): any other container is read by name
        _indexedContainer = bindingKey is not null && variablesContainer is IIndexedVariablesContainer indexed && ReferenceEquals(indexed.BindingKey, bindingKey)
            ? indexed
            : null;
    }

    internal EvaluatorValue GetValue(string name)
    {
        if (_variablesDict is not null)
        {
            if (!_variablesDict.TryGetValue(name, out var variable))
            {
                ThrowExceptionIfConfigured(name);
                return default;
            }

            return variable;
        }

        if (_variablesContainer is not null)
        {
            if (!_variablesContainer.TryGetValue(name, out var variable))
            {
                ThrowExceptionIfConfigured(name);
                return default;
            }

            return variable;
        }

        ThrowExceptionIfConfigured(name);
        return default;
    }

    // By index when the variable is bound and this is the container it is bound to, by name otherwise
    internal EvaluatorValue GetValue(int index, string name)
    {
        if (index >= 0 && _indexedContainer is not null)
        {
            if (!_indexedContainer.TryGetValue(index, out var variable))
            {
                ThrowExceptionIfConfigured(name);
                return default;
            }

            return variable;
        }

        return GetValue(name);
    }

    private static void ThrowExceptionIfConfigured(string name)
    {
        if (!GlobalOptions.ThrowIfVariableNotFound)
        {
            return;
        }

        var msg = $"Variable \"{name}\" not found";
        throw new VariableNotFoundException(name, msg);
    }

    internal static VariablesSource Create(IVariablesContainer variablesContainer, object bindingKey)
    {
        return new VariablesSource(variablesContainer, variablesDict: null, bindingKey);
    }

    internal static VariablesSource Create(IDictionary<string, EvaluatorValue> variablesDict)
    {
        return new VariablesSource(variablesContainer: null, variablesDict, bindingKey: null);
    }
}
