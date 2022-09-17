using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - or
/// </summary>
public sealed class OrFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "OR";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        for (int i = 0; i < args.Count; i++)
        {
            if (args[i])
            {
                return true;
            }
        }

        return false;
    }
}
