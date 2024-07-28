using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Data;
using NoStringEvaluating.Tests.Helpers;
using NoStringEvaluating.Tests.Models;
using NUnit.Framework;

namespace NoStringEvaluating.Tests;

internal class NoStringEvaluatorTests
{
    private Func<Action<NoStringEvaluatorOptions>, NoStringEvaluator> _serviceFactory;

    [SetUp]
    public void Setup()
    {
        _serviceFactory = opt =>
        {
            opt ??= subOpt => subOpt.SetThrowIfVariableNotFound(false);
            return EvaluatorFacadeFactory.Create(opt).Evaluator;
        };
    }

    [TestCaseSource(typeof(EvaluateNumber), nameof(EvaluateNumber.Get))]
    public void Should_Evaluate_Number(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcNumber(model.Formula, model.Arguments);

        // assert
        actual.Should().BeApproximatelyNumber(model.Result.Number);
    }

    [TestCaseSource(typeof(EvaluateWord), nameof(EvaluateWord.Get))]
    public void Should_Evaluate_Word(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcWord(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.Word);
    }

    [TestCaseSource(typeof(EvaluateDateTime), nameof(EvaluateDateTime.Get))]
    public void Should_Evaluate_DateTime(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcDateTime(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.DateTime);
    }

    [TestCaseSource(typeof(EvaluateWordList), nameof(EvaluateWordList.Get))]
    public void Should_Evaluate_WordList(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcWordList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.WordList);
    }

    [TestCaseSource(typeof(EvaluateNumberList), nameof(EvaluateNumberList.Get))]
    public void Should_Evaluate_NumberList(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcNumberList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.NumberList);
    }

    [TestCaseSource(typeof(EvaluateBoolean), nameof(EvaluateBoolean.Get))]
    public void Should_Evaluate_Boolean(FormulaModel model)
    {
        // arrange, act
        var actual = _serviceFactory(null).CalcBoolean(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.Boolean);
    }

    [Test]
    public void Should_Throw_If_Variable_Not_Found()
    {
        // arrange
        var vars = new Dictionary<string, EvaluatorValue> { ["myVariable1"] = 7 };
        var formula = "myVariable + 5";

        // act
        var act = () => _serviceFactory(opt => opt.SetThrowIfVariableNotFound(true)).CalcNumber(formula, vars);

        // assert
        act.Should().Throw<VariableNotFoundException>();
    }

    [Test]
    public void Should_Not_Throw_If_Variable_Not_Found()
    {
        // arrange
        var vars = new Dictionary<string, EvaluatorValue> { ["myVariable1"] = 7 };
        var formula = "myVariable + 5";

        // act
        var act = () => _serviceFactory(opt => opt.SetThrowIfVariableNotFound(false)).CalcNumber(formula, vars);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    [Test]
    public void Should_Throw_If_Variable_Dictionary_Not_Found()
    {
        // arrange
        var formula = "[my var!] + 5";

        // act
        var act = () => _serviceFactory(opt => opt.SetThrowIfVariableNotFound(true)).CalcNumber(formula);

        // assert
        act.Should().Throw<VariableNotFoundException>();
    }

    [Test]
    public void Should_Not_Throw_If_Variable_Dictionary_Not_Found()
    {
        // arrange
        var formula = "[my var!] + 5";

        // act
        var act = () => _serviceFactory(opt => opt.SetThrowIfVariableNotFound(false)).CalcNumber(formula);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    [Test]
    public async Task Should_Be_Thread_Safe_With_Extra_Type()
    {
        // arrange
        var service = _serviceFactory(null);

        // act
        var resTask = Task.Run(() => service.CalcWord("TestSleep('sleep word')"));

        // Creates extra DateTime, in another thread extra type 'sleep word' must exists
        var preCalc = service.Calc("Now()");
        var actual = await resTask;

        // assert
        actual.Should().Be("sleep word");
    }

    [Test]
    public void Should_Evaluate_Service()
    {
        // arrange
        var service = _serviceFactory(null);
        var args = new Dictionary<string, EvaluatorValue>
        {
            ["myService"] = new EvaluatorValue(new TestService()),
            ["myNum"] = 10,
        };
        var expected = 50.5;

        // act
        var actual = service.CalcNumber("TestService(myService; myNum)", args);

        // assert
        actual.Should().BeApproximatelyNumber(expected);
    }

    [Test]
    public void Should_Not_Add_Default_Functions()
    {
        // arrange
        var service = _serviceFactory(o => o.WithoutDefaultFunctions());

        // act
        var act = () => service.CalcNumber("Add(3; 7)");

        // assert
        act.Should().Throw<Exception>();
    }

    private class TestSleepFunction : IFunction
    {
        public string Name { get; } = "TestSleep";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var arg = args[0];
            _ = arg.Word;
            Thread.Sleep(100);

            var w2 = arg.Word;

            return factory.Word.Create(w2);
        }
    }

    private class ServiceFunction : IFunction
    {
        public string Name { get; } = "TestService";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return args[0].GetObject<TestService>().GetTemperature() + args[1].Number;
        }
    }

    private class TestService
    {
        [UnconditionalSuppressMessage("Performance", "CA1822:Mark members as static")]
        public double GetTemperature()
        {
            return 40.5;
        }
    }
}
