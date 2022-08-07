using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - max
/// <para>Max(1; 2; 3) or Max(myList; 2; 3)</para>
/// </summary>
public sealed class MaxFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "MAX";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var firstArg = args[0];
        var max = firstArg.Number;

        if (firstArg.IsNumberList)
        {
            max = Max(firstArg.GetNumberList());
        }

        for (int i = 1; i < args.Count; i++)
        {
            var arg = args[i];
            var current = arg.Number;

            if (arg.IsNumberList)
            {
                current = Max(arg.GetNumberList());
            }

            if (current > max)
                max = current;
        }

        return max;
    }

    private static double Max(List<double> list)
    {
        var max = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            var current = list[i];

            if (current > max)
                max = current;
        }

        return max;
    }
}
