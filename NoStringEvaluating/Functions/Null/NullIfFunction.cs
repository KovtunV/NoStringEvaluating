using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Null;

/// <summary>
/// Function - NullIf(x,3) => Returns NULL if x == 3
/// </summary>
public sealed class NullIfFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "NULLIF";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = true;

    /// <summary>
    /// Evaluate function
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        // Wrong input, output Null
        if (args.Count != 2) return default(InternalEvaluatorValue);

        // If first argument equals second we output Null
        if (args[0].Equals(args[1])) return default(InternalEvaluatorValue);

        // different argumens, keep the first
        return args[0];

    }
}
