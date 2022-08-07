using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Data;
using NoStringEvaluatingTests.Helpers;
using NoStringEvaluatingTests.Models;
using NUnit.Framework;

namespace NoStringEvaluatingTests;

internal class NoStringEvaluatorTests
{
    private IServiceProvider _serviceProvider;

    private NoStringEvaluator _service;

    [SetUp]
    public void Setup()
    {
        _serviceProvider = ServiceProviderFactory.Create();

        _service = _serviceProvider.GetRequiredService<NoStringEvaluator>();
    }

    [TestCaseSource(typeof(EvaluateNumber), nameof(EvaluateNumber.Get))]
    public void Should_Evaluate_Number(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcNumber(model.Formula, model.Arguments);

        // assert
        actual.Should().BeApproximatelyNumber(model.Result.Number);
    }

    [TestCaseSource(typeof(EvaluateWord), nameof(EvaluateWord.Get))]
    public void Should_Evaluate_Word(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcWord(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.Word);
    }

    [TestCaseSource(typeof(EvaluateDateTime), nameof(EvaluateDateTime.Get))]
    public void Should_Evaluate_DateTime(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcDateTime(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.DateTime);
    }

    [TestCaseSource(typeof(EvaluateWordList), nameof(EvaluateWordList.Get))]
    public void Should_Evaluate_WordList(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcWordList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.WordList);
    }

    [TestCaseSource(typeof(EvaluateNumberList), nameof(EvaluateNumberList.Get))]
    public void Should_Evaluate_NumberList(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcNumberList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.NumberList);
    }

    [TestCaseSource(typeof(EvaluateBoolean), nameof(EvaluateBoolean.Get))]
    public void Should_Evaluate_Boolean(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CalcBoolean(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.Boolean);
    }

    // TODO: change
    [Test]
    public void Should_Throw_If_Variable_Not_Found()
    {
        // arrange
        var vars = new Dictionary<string, EvaluatorValue> { ["myVariable1"] = 7 };
        var formula = "myVariable + 5";

        // act
        var act = () => _service.CalcNumber(formula, vars);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    // TODO: change
    [Test]
    public void Should_Throw_If_Variable_Dictionary_Not_Found()
    {
        // arrange
        var formula = "[my var!] + 5";

        // act
        var act = () => _service.CalcNumber(formula);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    [Test]
    public async Task Should_Be_Thread_Safe_With_Extra_Type()
    {
        // arrange
        var functionReader = _serviceProvider.GetRequiredService<IFunctionReader>();
        functionReader.AddFunction(new TestSleepFunction());

        // act
        var resTask = Task.Run(() => _service.CalcWord("TestSleep('sleep word')"));

        // Creates extra DateTime, in another thread extra type 'sleep word' must exists
        var preCalc = _service.Calc("Now()");
        var actual = await resTask;

        // assert
        actual.Should().Be("sleep word");
    }

    private class TestSleepFunction : IFunction
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
}
