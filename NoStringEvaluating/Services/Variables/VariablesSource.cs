using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
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

    private static void ThrowExceptionIfConfigured(string name)
    {
        if (!GlobalOptions.ThrowIfVariableNotFound)
        {
            return;
        }

        var msg = $"Variable \"{name}\" not found";
        throw new VariableNotFoundException(name, msg);
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
