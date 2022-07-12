using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel;

/// <summary>
/// Returns a number of elements
/// <para>Count(1; 2; myList)</para>
/// </summary>
public class CountFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; } = "COUNT";

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
                res += arg.GetWordList().Count;
                continue;
            }

            if (arg.IsNumberList)
            {
                res += arg.GetNumberList().Count;
                continue;
            }

            res++;
        }

        return res;
    }
}
