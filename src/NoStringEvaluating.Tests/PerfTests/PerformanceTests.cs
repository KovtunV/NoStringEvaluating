using System.Diagnostics;
using FluentAssertions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Binding;
using NoStringEvaluating.Services.Variables;
using NoStringEvaluating.Tests.PerfTests.Report;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.PerfTests;

[NonParallelizable]
[Category("PerfTests")]
[Explicit("These tests are only run when explicitly specified")]
internal class PerformanceTests
{
    private const string Formula6 = "Arg1 * (Arg2 + Arg3) - Arg4 / (Arg5 - Arg6 + 1) + 45 * Arg7 + ((Arg8 * 56 + (12 + Arg9))) - Arg10";

    private NoStringEvaluator.Facade _serviceFacade;
    private ReportContainer _report;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _report = new();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        ReportWriter.Write(_report);
    }

    [SetUp]
    public void Setup()
    {
        var functions = new IFunction[]
        {
            new AddFunction(),
            new IfFunction(),
            new OrFunction(),
            new Func_kov(),
            new Func_kovt(),
        };

        _serviceFacade = NoStringEvaluator.CreateFacade(cfg => cfg.WithoutDefaultFunctions().WithFunctions(functions));
    }

    [TestCaseSource(nameof(RunSource))]
    public void RunFormula(string formulaName, string formula, IDictionary<string, EvaluatorValue> args, long targetElapsedMilliseconds)
    {
        // arrange
        var n = 1_000_000;

        var nodes = _serviceFacade.FormulaCache.GetFormulaNodes(formula);

        // act, assert
        var res = _serviceFacade.Evaluator.CalcNumber(nodes, args);

        var ela = Stopwatch.StartNew();
        for (var i = 0; i < n; i++)
        {
            _ = _serviceFacade.Evaluator.Calc(nodes, args);
        }

        ela.Stop();

        _report.Append(formulaName, res, ela.ElapsedMilliseconds, targetElapsedMilliseconds);
    }

    [Test]
    public void RunFormula6Indexed()
    {
        var args = Enumerable.Range(1, 10).ToDictionary(i => $"Arg{i}", _ => (EvaluatorValue)1.7);
        var container = new IndexedVariablesContainer(args);
        var nodes = _serviceFacade.FormulaCache.GetFormulaNodes(Formula6);
        var bound = FormulaBinder.Bind(nodes, container);
        var expected = _serviceFacade.Evaluator.CalcNumber(nodes, args);
        var result = _serviceFacade.Evaluator.CalcNumber(bound, container);
        result.Should().BeApproximately(expected, 1e-10);

        var elapsed = Stopwatch.StartNew();
        for (var i = 0; i < 1_000_000; i++)
        {
            _ = _serviceFacade.Evaluator.Calc(bound, container);
        }

        elapsed.Stop();
        _report.Append("Formula 6 indexed", result, elapsed.ElapsedMilliseconds, 600);

        container.AddOrUpdate("Arg1", 2.7);
        args["Arg1"] = 2.7;
        _serviceFacade.Evaluator.CalcNumber(bound, container).Should().BeApproximately(_serviceFacade.Evaluator.CalcNumber(nodes, args), 1e-10);
    }

    private static IEnumerable<object[]> RunSource()
    {
        var args = Enumerable.Range(1, 10).ToDictionary(i => $"Arg{i}", _ => (EvaluatorValue)1.7);

        yield return new object[] { "Formula 1", "3 * 9", args, 300 };
        yield return new object[] { "Formula 2", "3 * 9 / 456 * 32 + 12 / 17 - 3", args, 250 };
        yield return new object[] { "Formula 3", "3 * (9 / 456 * (32 + 12)) / 17 - 3", args, 250 };
        yield return new object[] { "Formula 4", "(2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) - (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) + (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) * 345 * ((897 - 323)/ 23)", args, 500 };
        yield return new object[] { "Formula 5", "Arg1 * Arg2 + Arg3 - Arg4", args, 400 };
        yield return new object[] { "Formula 6", Formula6, args, 600 };
        yield return new object[] { "Formula 7", "add(1; 2; 3)", args, 400 };
        yield return new object[] { "Formula 8", "add(add(5; 1) - add(5; 2; 3))", args, 400 };
        yield return new object[] { "Formula 9", "if(Arg1 > 0; add(56 + 9 / 12 * 123.596; or(78; 9; 5; 2; 4; 5; 8; 7); 45;5); 9) *     24 + 52 -33", args, 900 };
        yield return new object[] { "Formula 10", "kov(1; 2; 3) - kovt(8; 9)", args, 400 };
    }

    public class Func_kov : IFunction
    {
        public string Name { get; } = "kov";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var res = 1d;

            for (int i = 0; i < args.Count; i++)
            {
                res *= args[i];
            }

            return res;
        }
    }

    public class Func_kovt : IFunction
    {
        public string Name { get; } = "kovt";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return args[0] - args[1];
        }
    }
}
