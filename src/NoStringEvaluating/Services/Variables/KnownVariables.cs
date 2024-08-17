namespace NoStringEvaluating.Services.Variables;

internal static class KnownVariables
{
    private static readonly Dictionary<string, double> _numberVariables;
    private static readonly Dictionary<string, bool> _booleanVariables;

    static KnownVariables()
    {
        _numberVariables = new Dictionary<string, double>
        {
            ["PI"] = Math.PI,
            ["TAU"] = Math.PI * 2,
            ["E"] = Math.E,
        };

        _booleanVariables = new Dictionary<string, bool>
        {
            ["TRUE"] = true,
            ["FALSE"] = false,
            ["ASC"] = true,
            ["DESC"] = false,
        };
    }

    internal static bool TryGetNumberValue(string name, out double val)
    {
        return _numberVariables.TryGetValue(name.ToUpperInvariant(), out val);
    }

    internal static bool TryGetBooleanValue(string name, out bool val)
    {
        return _booleanVariables.TryGetValue(name.ToUpperInvariant(), out val);
    }

    internal static bool IsNull(string name)
    {
        return name.Equals("NULL", StringComparison.OrdinalIgnoreCase);
    }
}
