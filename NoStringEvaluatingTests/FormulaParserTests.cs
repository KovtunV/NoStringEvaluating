using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Formulas;
using NoStringEvaluatingTests.Model;
using NUnit.Framework;

namespace NoStringEvaluatingTests;

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

    [TestCaseSource(nameof(GetFormulasToCheck))]
    public void CheckFormula(FormulaModel model)
    {
        var parser = _serviceProvider.GetRequiredService<IFormulaChecker>();
        var res = parser.CheckSyntax(model.Formula);

        Assert.AreEqual(model.ExpectedOkResult, res.Ok);
    }

    [TestCaseSource(nameof(GetFormulasToParse))]
    public void ParseFormula(FormulaModel model)
    {
        var parser = _serviceProvider.GetRequiredService<IFormulaParser>();
        var res = parser.Parse(model.Formula);

        Assert.AreEqual(model.ParsedFormula, res.ToString());
    }

    [TestCaseSource(nameof(GetFormulasToCalculate))]
    public void CalculateNumberFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();

        var res = evaluator.CalcNumber(model.Formula, model.Arguments);
        var roundedRes = Math.Round(res, 3);

        Assert.AreEqual(model.Result.Number, roundedRes);
    }

    [TestCaseSource(nameof(GetWordFormulas))]
    public void CalculateWordFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcWord(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result.Word, res);
    }

    [TestCaseSource(nameof(GetDateTimeFormulas))]
    public void CalculateDateTimeFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcDateTime(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result.DateTime, res);
    }

    [TestCaseSource(nameof(GetWordListFormulas))]
    public void CalculateWordListFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcWordList(model.Formula, model.Arguments);

        var sequenceEqual = model.Result.WordList.SequenceEqual(res);
        Assert.IsTrue(sequenceEqual);
    }

    [TestCaseSource(nameof(GetNumberListFormulas))]
    public void CalculateNumberListFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcNumberList(model.Formula, model.Arguments);

        var sequenceEqual = model.Result.NumberList.SequenceEqual(res);
        Assert.IsTrue(sequenceEqual);
    }

    [TestCaseSource(nameof(GetBooleanFormulas))]
    public void CalculateBooleanFormula(FormulaModel model)
    {
        var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
        var res = evaluator.CalcBoolean(model.Formula, model.Arguments);

        Assert.AreEqual(model.Result.Boolean, res);
    }

    [Test]
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

    [Test]
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

    [Test]
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
