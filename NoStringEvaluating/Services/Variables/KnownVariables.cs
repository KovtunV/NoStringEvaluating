using System;
using System.Collections.Generic;

namespace NoStringEvaluating.Services.Variables
{
    internal static class KnownVariables
    {
        private static readonly Dictionary<string, double> _variables;

        static KnownVariables()
        {
            _variables = new Dictionary<string, double>
            {
                ["PI"] = Math.PI,
                ["TAU"] = Math.PI * 2,
                ["E"] = Math.E,
                ["TRUE"] = 1,
                ["FALSE"] = 0
            };
        }

        internal static bool TryGetValue(string name, out double val)
        {
            return _variables.TryGetValue(name.ToUpperInvariant(), out val);
        }
    }
}
