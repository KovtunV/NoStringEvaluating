using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Null;

/// <summary>
/// Function - IsNull(x) => Returns TRUE x is Null
/// </summary>
public sealed class IsNullFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ISNULL";

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
        if (args.Count != 1)
            return default;

        return factory.Boolean.Create(args[0].IsNull);
    }
}
