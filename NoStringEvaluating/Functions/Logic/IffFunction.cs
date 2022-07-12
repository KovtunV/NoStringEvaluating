using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - iff
/// </summary>
public class IffFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "IFF";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        for (int i = 0; i < args.Count - 1; i += 2)
        {
            if (System.Math.Abs(args[i]) > NoStringEvaluatorConstants.FloatingTolerance)
            {
                return args[i + 1];
            }
        }

        return double.NaN;
    }
}
