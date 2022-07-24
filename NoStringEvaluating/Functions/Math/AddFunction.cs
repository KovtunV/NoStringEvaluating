using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - add
/// <para>Add(1; 2; 3) or Add(myList; 1)</para>
/// </summary>
public sealed class AddFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "ADD";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var sum = 0d;

        for (int i = 0; i < args.Count; i++)
        {
            var arg = args[i];
            if (arg.IsNumberList)
            {
                var numberList = arg.GetNumberList();
                for (int j = 0; j < numberList.Count; j++)
                {
                    sum += numberList[j];
                }

                continue;
            }

            sum += args[i];
        }

        return sum;
    }
}
