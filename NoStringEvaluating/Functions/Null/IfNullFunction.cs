using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Null;

/// <summary>
/// Function - IfNull(x,3) => Returns 3 if x is Null
/// </summary>
public sealed class IfNullFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "IFNULL";

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
        if (args.Count != 2)
            return default;

        // If first argument is Null we output the second
        if (args[0].IsNull)
            return args[1];

        // First argument is not null, keep it
        return args[0];
    }
}
