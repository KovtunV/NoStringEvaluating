using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using org.mariuszgromada.math.mxparser;
using static NoStringEvaluating.NoStringEvaluator;

namespace ConsoleApp.Benchmark.Base;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Monitoring, warmupCount: 2, invocationCount: 3, iterationCount: 10)]
public abstract class BenchBase
{
    public virtual int N { get; set; } = 1_000_000;

    private IFunction[] _usedFunctions;
    private Function[] _usedFunctionsMxParser;

    [GlobalSetup]
    public virtual void Setup()
    {
        _usedFunctions =
        [
            new AddFunction(),
            new IfFunction(),
            new OrFunction(),
            new Func_kov(),
            new Func_kovt(),
        ];

        _usedFunctionsMxParser =
        [
            new Function("kov", new FExtension_kov()),
            new Function("kovt", new FExtension_kovt()),
        ];

        mXparser.disableAlmostIntRounding();
        mXparser.disableUlpRounding();
        mXparser.disableCanonicalRounding();
    }

    #region NoString

    protected virtual void CalcNoString(string formula, params string[] argsNames)
    {
        var evalFacade = CreateNoStringFacade();
        var eval = evalFacade.Evaluator;
        var args = argsNames.ToDictionary(s => s, r => (EvaluatorValue)1.7);

        var formulaNodes = evalFacade.FormulaCache.GetFormulaNodes(formula);

        for (var i = 0; i < N; i++)
        {
            eval.CalcNumber(formulaNodes, args);
        }
    }

    protected virtual void CalcNoString(string formula)
    {
        var evalFacade = CreateNoStringFacade();
        var eval = evalFacade.Evaluator;

        var formulaNodes = evalFacade.FormulaCache.GetFormulaNodes(formula);

        for (var i = 0; i < N; i++)
        {
            eval.CalcNumber(formulaNodes);
        }
    }

    protected Facade CreateNoStringFacade()
    {
        return CreateFacade(opt => opt.WithoutDefaultFunctions().WithFunctions(_usedFunctions));
    }

    #endregion

    #region MxParser

    protected void CalcMxParser(string formula, params string[] argsNames)
    {
        var expression = CreateMxParser(formula);

        for (var i = 0; i < argsNames.Length; i++)
        {
            expression.defineArgument(argsNames[i], 1.7);
        }

        expression.calculate();

        for (var i = 0; i < N; i++)
        {
            expression.calculate();
        }
    }

    protected void CalcMxParser(string formula)
    {
        var expression = CreateMxParser(formula);

        expression.calculate();

        for (var i = 0; i < N; i++)
        {
            expression.calculate();
        }
    }

    private Expression CreateMxParser(string formula)
    {
        var expression = new Expression(formula);
        expression.addFunctions(_usedFunctionsMxParser);

        return expression;
    }

    #endregion

    public const string Arg1 = "arg1";
    public const string Arg2 = "arg2";
    public const string Arg3 = "arg3";
    public const string Arg4 = "arg4";
    public const string Arg5 = "arg5";
    public const string Arg6 = "arg6";
    public const string Arg7 = "arg7";
    public const string Arg8 = "arg8";
    public const string Arg9 = "arg9";
    public const string Arg10 = "arg10";

    public const string Empty = "";
    public const string NumberOnly = "3";
    public const string Formula1 = "3 * 9";
    public const string Formula2 = "3 * 9 / 456 * 32 + 12 / 17 - 3";
    public const string Formula3 = "3 * (9 / 456 * (32 + 12)) / 17 - 3";
    public const string Formula4 = "(2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) - (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) + (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) * 345 * ((897 - 323)/ 23)";

    public const string Formula5 = $"{Arg1} * {Arg2} + {Arg3} - {Arg4}";
    public const string Formula6 = $"{Arg1} * ({Arg2} + {Arg3}) - {Arg4} / ({Arg5} - {Arg6} + 1) + 45 * {Arg7} + (({Arg8} * 56 + (12 + {Arg9}))) - {Arg10}";

    public const string Formula7 = $"add(1; 2; 3)";
    public const string Formula8 = $"add(add(5; 1) - add(5; 2; 3))";
    public const string Formula9 = $"if({Arg1} > 0; add(56 + 9 / 12 * 123.596; or(78; 9; 5; 2; 4; 5; 8; 7); 45;5); 9) *     24 + 52 -33";
    public const string Formula10 = $"kov(1; 2; 3) - kovt(8; 9)"; // 6 - -1 = 7
}

#region CustomFunctions

[UnconditionalSuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type")]
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

[UnconditionalSuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type")]
public class Func_kovt : IFunction
{
    public string Name { get; } = "kovt";

    public bool CanHandleNullArguments { get; }

    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return args[0] - args[1];
    }
}

[UnconditionalSuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type")]
public class FExtension_kov : FunctionExtensionVariadic
{
    public double calculate(params double[] parameters)
    {
        var res = 1d;

        for (int i = 0; i < parameters.Length; i++)
        {
            res *= parameters[i];
        }

        return res;
    }

    public FunctionExtensionVariadic clone()
    {
        throw new NotImplementedException();
    }
}

[UnconditionalSuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type")]
public class FExtension_kovt : FunctionExtensionVariadic
{
    public double calculate(params double[] parameters)
    {
        return parameters[0] - parameters[1];
    }

    public FunctionExtensionVariadic clone()
    {
        throw new NotImplementedException();
    }
}

#endregion

