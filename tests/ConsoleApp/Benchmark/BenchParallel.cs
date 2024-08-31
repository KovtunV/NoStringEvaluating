using System.Diagnostics;
using System.Globalization;
using BenchmarkDotNet.Attributes;
using ConsoleApp.Benchmark.Base;
using NoStringEvaluating.Models.Values;

namespace ConsoleApp.Benchmark;

public class BenchParallel : BenchBase
{
    public int Iterations { get; set; } = 300_000;

    public int ParallelDepth { get; set; } = 3;

    private Dictionary<string, EvaluatorValue> _variables;

    public override void Setup()
    {
        base.Setup();

        _variables = new()
        {
            { "A_bool", true },
            { "B_bool", true },
            { "C_bool", true },
            { "D_bool", false },
            { "A_number", 83 },
            { "B_number", 10 },
            { "A_datetime", DateTime.Parse("07/18/2005", CultureInfo.InvariantCulture) },
        };
    }

    [Benchmark]
    public void Eval_Boolean()
    {
        var trueCount = 0;
        var falseCount = 0;

        Parallel.For(0, ParallelDepth, par =>
        {
            var evalFacade = CreateNoStringFacade();

            var formula1 = "(A_bool && B_bool && C_bool) || D_bool";
            var formula2 = "(A_number * B_number > 1000) && ( A_bool || C_bool) ";

            var parsed1 = evalFacade.FormulaParser.Parse(formula1);
            var parsed2 = evalFacade.FormulaParser.Parse(formula2);

            for (int i = 0; i < Iterations; i++)
            {
                Increment(evalFacade.Evaluator.CalcBoolean(parsed1, _variables), ref trueCount, ref falseCount);
                Increment(evalFacade.Evaluator.CalcBoolean(parsed2, _variables), ref trueCount, ref falseCount);
            }
        });

        Debug.Assert(trueCount == falseCount);
    }

    [Benchmark]
    public void Eval_DateTime()
    {
        var expected1 = DateTime.Parse("08/01/2005", CultureInfo.InvariantCulture);
        var expected2 = DateTime.Parse("07/04/2005", CultureInfo.InvariantCulture);

        var res1 = true;
        var res2 = true;

        Parallel.For(0, ParallelDepth, par =>
        {
            var evalFacade = CreateNoStringFacade();

            var formula1 = "A_datetime + 1 + 1 + 1 + 1 + 10";
            var formula2 = "A_datetime - 10 - 1 - 1 - 1 - 1";

            var parsed1 = evalFacade.FormulaParser.Parse(formula1);
            var parsed2 = evalFacade.FormulaParser.Parse(formula2);

            for (int i = 0; i < Iterations; i++)
            {
                res1 &= evalFacade.Evaluator.CalcDateTime(parsed1, _variables).Equals(expected1);
                res2 &= evalFacade.Evaluator.CalcDateTime(parsed2, _variables).Equals(expected2);
            }
        });

        Debug.Assert(res1);
        Debug.Assert(res2);
    }

    private static void Increment(bool value, ref int trueCount, ref int falseCount)
    {
        if (value)
        {
            Interlocked.Increment(ref trueCount);
        }
        else
        {
            Interlocked.Increment(ref falseCount);
        }
    }
}
