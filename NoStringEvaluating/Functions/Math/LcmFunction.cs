using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using static System.Math;

namespace NoStringEvaluating.Functions.Math;

/// <summary>
/// Function - lcm
/// </summary>
public sealed class LcmFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "LCM";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var numbers = EnumerateNumbers(args).ToArray();
        if (numbers.Length == 1)
        {
            return numbers[0];
        }

        var res = GetLcm(numbers[0], numbers[1]);
        for (int i = 2; i < numbers.Length; i++)
        {
            res = GetLcm(res, numbers[i]);
        }

        return Abs(res);
    }

    private static IEnumerable<double> EnumerateNumbers(List<InternalEvaluatorValue> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            var arg = args[i];

            if (arg.IsNumberList)
            {
                var numbers = arg.NumberList;

                for (int j = 0; j < numbers.Count; j++)
                {
                    yield return numbers[j];
                }
            }

            if (arg.IsNumber)
            {
                yield return arg.Number;
            }
        }
    }

    private static double GetLcm(double a, double b)
    {
        return (a * b) / GetGcd(a, b);
    }

    private static double GetGcd(double a, double b)
    {
        while (Abs(b) > GlobalOptions.FloatingTolerance)
        {
            var tmp = b;
            b = a % b;
            a = tmp;
        }

        return a;
    }
}
