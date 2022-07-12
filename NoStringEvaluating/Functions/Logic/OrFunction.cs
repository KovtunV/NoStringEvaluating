using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - or
/// </summary>
public class OrFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "OR";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        for (int i = 0; i < args.Count; i++)
        {
            if (System.Math.Abs(args[i]) > NoStringEvaluatorConstants.FloatingTolerance)
                return 1;
        }

        return 0;
    }
}
