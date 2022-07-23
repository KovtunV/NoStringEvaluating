using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - if
/// </summary>
public sealed class IfFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "IF";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = false;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        if (System.Math.Abs(args[0]) > NoStringEvaluatorConstants.FloatingTolerance)
        {
            return args[1];
        }

        return args[2];
    }
}
