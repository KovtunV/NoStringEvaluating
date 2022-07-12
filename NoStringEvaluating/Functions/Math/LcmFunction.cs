using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using static System.Math;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - lcm
/// </summary>
public class LcmFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "LCM";

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        if (args.Count == 1)
            return args[0];

        var res = GetLcm(args[0], args[1]);
        for (int i = 2; i < args.Count; i++)
        {
            res = GetLcm(res, args[i]);
        }

        return Abs(res);
    }

    private double GetLcm(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return (a * b) / GetGcd(a, b);
    }

    private double GetGcd(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        while (Abs(b) > NoStringEvaluatorConstants.FloatingTolerance)
        {
            var tmp = b;
            b = a % b;
            a = tmp;
        }

        return a;
    }
}
