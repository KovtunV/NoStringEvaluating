﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel;

/// <summary>
/// Returns a number of elements
/// <para>Count(1; 2; myList)</para>
/// </summary>
public sealed class CountFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "COUNT";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Execute value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var res = 0;
        for (int i = 0; i < args.Count; i++)
        {
            var arg = args[i];
            if (arg.IsWordList)
            {
                res += arg.WordList.Count;
                continue;
            }

            if (arg.IsNumberList)
            {
                res += arg.NumberList.Count;
                continue;
            }

            res++;
        }

        return res;
    }
}
