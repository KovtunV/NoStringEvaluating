using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - min
/// </summary>
public sealed class MinFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "MIN";

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
        var min = firstArg.Number;

        if (firstArg.IsNumberList)
        {
            min = Min(firstArg.GetNumberList());
        }

        for (int i = 1; i < args.Count; i++)
        {
            var arg = args[i];
            var current = arg.Number;

            if (arg.IsNumberList)
            {
                current = Min(arg.GetNumberList());
            }

            if (current < min)
                min = current;
        }

        return min;
    }

    private static double Min(List<double> list)
    {
        var min = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            var current = list[i];

            if (current < min)
                min = current;
        }

        return min;
    }
}
