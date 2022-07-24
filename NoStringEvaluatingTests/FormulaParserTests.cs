using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Formulas;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests;

[TestClass]
public class FormulaParserTests
{
    private readonly IServiceProvider _serviceProvider;

    public FormulaParserTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddNoStringEvaluator(opt => opt.SetFloatingPointSymbol(FloatingPointSymbol.DotComma))
            .BuildServiceProvider();

        var functionReader = _serviceProvider.GetRequiredService<IFunctionReader>();
        functionReader.AddFunction(new Func_kov());
        functionReader.AddFunction(new Func_kovt());
    }

    [TestMethod]
    [DynamicData(nameof(GetFormulasToCheck), DynamicDataSourceType.Method)]
    public void CheckFormula(FormulaModel model)
    {
        var parser = _serviceProvider.GetRequiredService<IFormulaChecker>();
        var res = parser.CheckSyntax(model.Formula);

        Assert.AreEqual(model.ExpectedOkResult, res.Ok);
    }

    [TestMethod]
    [DynamicData(nameof(GetFormulasToParse), DynamicDataSourceType.Method)]
    public void ParseFormula(FormulaModel model)
    {
        var parser = _serviceProvider.GetRequiredService<IFormulaParser>();
        var res = parser.Parse(model.Formula);

        Assert.AreEqual(model.ParsedFormula, res.ToString());
    }

    [TestMethod]
    [DynamicData(nameof(GetFormulasToCalculate), DynamicDataSourceType.Method)]
    public void CalculateNumberFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();

        var res = evaluator.CalcNumber(model.Formula, model.Arguments);
        var roundedRes = Math.Round(res, 3);

        Assert.AreEqual(model.Result, roundedRes);
    }

    [TestMethod]
    [DynamicData(nameof(GetWordFormulas), DynamicDataSourceType.Method)]
    public void CalculateWordFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcWord(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result, res);
    }

    [TestMethod]
    [DynamicData(nameof(GetDateTimeFormulas), DynamicDataSourceType.Method)]
    public void CalculateDateTimeFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcDateTime(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result, res);
    }

    [TestMethod]
    [DynamicData(nameof(GetWordListFormulas), DynamicDataSourceType.Method)]
    public void CalculateWordListFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcWordList(model.Formula, model.Arguments);

        var sequenceEqual = model.Result.WordList.SequenceEqual(res);
        Assert.IsTrue(sequenceEqual);
    }

    [TestMethod]
    [DynamicData(nameof(GetNumberListFormulas), DynamicDataSourceType.Method)]
    public void CalculateNumberListFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcNumberList(model.Formula, model.Arguments);

        var sequenceEqual = model.Result.NumberList.SequenceEqual(res);
        Assert.IsTrue(sequenceEqual);
    }

    [TestMethod]
    [DynamicData(nameof(GetBooleanFormulas), DynamicDataSourceType.Method)]
    public void CalculateBooleanFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcBoolean(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result, res);
    }

    [TestMethod]
    public void VariableNotFoundException()
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var vars = new Dictionary<string, EvaluatorValue> { ["myVariable1"] = 7 };
        var formula = "myVariable + 5";

        try
        {
            var res = evaluator.CalcNumber(formula, vars);
        }
        catch (VariableNotFoundException ex)
        {
            // This exception is OK
            Assert.AreEqual("myVariable", ex.VariableName);
        }

        // Now it should calculate     
        vars["myVariable"] = 7;
        var resOk = evaluator.CalcNumber(formula, vars);
        Assert.AreEqual(12, resOk);
    }

    [TestMethod]
    public void VariableNotFoundExceptionNoDictionary()
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var formula = "[my var!] + 5";

        try
        {
            var res = evaluator.CalcNumber(formula);
        }
        catch (VariableNotFoundException ex)
        {
            // This exception is OK
            Assert.AreEqual("my var!", ex.VariableName);
            return;
        }

        // TODO: improve tests
        // Assert.Fail();
    }

    [TestMethod]
    public async Task ExtraTypeConcurrent()
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var functionReader = _serviceProvider.GetRequiredService<IFunctionReader>();
        functionReader.AddFunction(new TestSleepFunction());

        var resTask = Task.Run(() =>
        {
            var sleepRes = evaluator.CalcWord("TestSleep('sleep word')");
            return sleepRes;
        });

        // Creates extra DateTime, in another thread extra type 'sleep word' must exists
        var preCalc = evaluator.Calc("Now()");
        var res = await resTask;

        Assert.AreEqual("sleep word", res);
    }

    #region DataSource

    private static IEnumerable<FormulaModel[]> GetFormulasToCheck()
        => FormulasContainer.GetFormulasToCheck();

    private static IEnumerable<FormulaModel[]> GetFormulasToParse()
        => FormulasContainer.GetFormulasToParse();

    private static IEnumerable<FormulaModel[]> GetFormulasToCalculate()
        => FormulasContainer.GetFormulasToCalculate();

    private static IEnumerable<FormulaModel[]> GetWordFormulas()
        => FormulasContainer.GetWordFormulas();

    private static IEnumerable<FormulaModel[]> GetDateTimeFormulas()
        => FormulasContainer.GetDateTimeFormulas();

    private static IEnumerable<FormulaModel[]> GetWordListFormulas()
        => FormulasContainer.GetWordListFormulas();

    private static IEnumerable<FormulaModel[]> GetNumberListFormulas()
        => FormulasContainer.GetNumberListFormulas();

    private static IEnumerable<FormulaModel[]> GetBooleanFormulas()
        => FormulasContainer.GetBooleanFormulas();

    #endregion
}

#region Custom functions

public class Func_kov : IFunction
{
    public string Name { get; } = "KOV";

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
    public string Name { get; } = "KOVT";

    public bool CanHandleNullArguments { get; }

    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return args[0] - args[1];
    }
}

public class TestSleepFunction : IFunction
{
    public string Name { get; } = "TestSleep";

    public bool CanHandleNullArguments { get; }

    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        var arg = args[0];
        var w1 = arg.GetWord();
        Thread.Sleep(100);

        var w2 = arg.GetWord();

        return factory.Word.Create(w2);
    }
}

#endregion
