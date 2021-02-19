using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;

namespace NoStringEvaluating.Services.Variables
{
    internal readonly struct VariablesSource
    {
        private readonly IVariablesContainer _variablesContainer;
        private readonly IDictionary<string, double> _variablesDict;

        private VariablesSource(IVariablesContainer variablesContainer, IDictionary<string, double> variablesDict)
        {
            _variablesContainer = variablesContainer;
            _variablesDict = variablesDict;
        }

        internal double GetValue(string name)
        {
            // Check null
            ThrowExceptionIfSourcesAreNull(name);

            // Check an implemented container
            if (_variablesContainer != null)
            {
                return GetValueFromContainer(name);
            }

            // Check a dictionary
            return GetValueFromDictionary(name);
        }

        private double GetValueFromContainer(string name)
        {
            if(!_variablesContainer.TryGetValue(name, out var value))
            {
                ThrowException(name);
            }

            return value;
        }

        private double GetValueFromDictionary(string name)
        {
            if (!_variablesDict.TryGetValue(name, out var value))
            {
                ThrowException(name);
            }

            return value;
        }

        private void ThrowExceptionIfSourcesAreNull(string name)
        {
            if (_variablesDict is null && _variablesContainer is null)
            {
                ThrowException(name);
            }
        }

        private void ThrowException(string name)
        {
            var msg = $"Variable \"{name}\" not found";
            throw new VariableNotFoundException(name, msg);
        }

        internal static VariablesSource Create(IVariablesContainer variablesContainer)
        {
            return new VariablesSource(variablesContainer, variablesDict: null);
        }

        internal static VariablesSource Create(IDictionary<string, double> variablesDict)
        {
            return new VariablesSource(variablesContainer: null, variablesDict);
        }
    }
}
