using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;

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
            return _variablesContainer?.GetValue(name) ?? _variablesDict[name];
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
