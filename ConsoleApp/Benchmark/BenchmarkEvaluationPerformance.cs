using System;
using BenchmarkDotNet.Attributes;
using ConsoleApp.Benchmark.Base;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Models.Values;


namespace ConsoleApp.Benchmark;

public class BenchmarkEvaluationPerformance : BaseBenchmarkService, IVariablesContainer
{
    INoStringEvaluator eval;


    public BenchmarkEvaluationPerformance()
    {
        eval = CreateNoString();
    }

    [Benchmark]
    public void FormulaEval()
    {
        var f1 = "X+Y+Z+3+ X*Y*Z";
        var f2 = "X+Y/Z * X*Y+3";
        var f3 = "X+Y*4*7*Z";

        double tot = 0;
        for (int i = 0; i < N; i++)
        {
            tot += eval.CalcNumber(f1, this) ?? 0;
            tot += eval.CalcNumber(f2, this) ?? 0;
            tot += eval.CalcNumber(f3, this) ?? 0;
        }
    }

    private double x = 3;
    private double y = 4;
    private double z = 5;

    public IVariable AddOrUpdate(string name, double value)
    {
        throw new Exception("NotNeededForBenchmark");
    }

    public EvaluatorValue GetValue(string name)
    {
        if (TryGetValue(name, out var value)) return value; else return default;
    }

    public bool TryGetValue(string name, out EvaluatorValue value)
    {
        switch (name)
        {
            case "X": value = x; return true;
            case "Y": value = y; return true;
            case "Z": value = z; return true;
        }
        value = default;
        return false;
    }
}
