using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Tests.Data;
using NoStringEvaluating.Tests.Helpers;
using NoStringEvaluating.Tests.Models;
using NUnit.Framework;

namespace NoStringEvaluating.Tests;

internal class NoStringEvaluatorTests
{
    [TestCaseSource(typeof(EvaluateNumber), nameof(EvaluateNumber.Get))]
    public void Should_Evaluate_Number(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcNumber(model.Formula, model.Arguments);

        // assert
        actual.Should().BeApproximatelyNumber(model.Result.Number);
    }

    [TestCaseSource(typeof(EvaluateWord), nameof(EvaluateWord.Get))]
    public void Should_Evaluate_Word(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcWord(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.Word);
    }

    [TestCaseSource(typeof(EvaluateDateTime), nameof(EvaluateDateTime.Get))]
    public void Should_Evaluate_DateTime(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcDateTime(model.Formula, model.Arguments);

        // assert
        actual.Should().Be(model.Result.DateTime);
    }

    [TestCaseSource(typeof(EvaluateWordList), nameof(EvaluateWordList.Get))]
    public void Should_Evaluate_WordList(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcWordList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.WordList);
    }

    [TestCaseSource(typeof(EvaluateNumberList), nameof(EvaluateNumberList.Get))]
    public void Should_Evaluate_NumberList(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcNumberList(model.Formula, model.Arguments);

        // assert
        actual.Should().BeEquivalentTo(model.Result.NumberList);
    }

    [TestCaseSource(typeof(EvaluateBoolean), nameof(EvaluateBoolean.Get))]
    public void Should_Evaluate_Boolean(FormulaModel model)
    {
        // arrange, act
        var actual = CreateService().CalcBoolean(model.Formula, model.Arguments);

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
        var act = () => CreateService(opt => opt.SetThrowIfVariableNotFound(true)).CalcNumber(formula, vars);

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
        var act = () => CreateService(opt => opt.SetThrowIfVariableNotFound(false)).CalcNumber(formula, vars);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    [Test]
    public void Should_Throw_If_Variable_Dictionary_Not_Found()
    {
        // arrange
        var formula = "[my var!] + 5";

        // act
        var act = () => CreateService(opt => opt.SetThrowIfVariableNotFound(true)).CalcNumber(formula);

        // assert
        act.Should().Throw<VariableNotFoundException>();
    }

    [Test]
    public void Should_Not_Throw_If_Variable_Dictionary_Not_Found()
    {
        // arrange
        var formula = "[my var!] + 5";

        // act
        var act = () => CreateService(opt => opt.SetThrowIfVariableNotFound(false)).CalcNumber(formula);

        // assert
        act.Should().NotThrow<VariableNotFoundException>();
    }

    [Test]
    public async Task Should_Be_Thread_Safe_With_Extra_Type()
    {
        // arrange
        var service = CreateService();

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
        var service = CreateService();
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
        var service = CreateService(o => o.WithoutDefaultFunctions());

        // act
        var act = () => service.CalcNumber("Add(3; 7)");

        // assert
        act.Should().Throw<Exception>();
    }

    [Test]
    public void Should_Return_Number()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.Number;

        var formula = "a + b";
        var formulaWOVariables = "1 + 2";

        var variablesDictionary = CreateVariablesDictionary(("a", 1), ("b", 2));
        var variablesContainer = CreateVariablesContainer(("a", 1), ("b", 2));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    [Test]
    public void Should_Return_Word()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.Word;

        var formula = "a + b";
        var formulaWOVariables = "'1' + '2'";

        var variablesDictionary = CreateVariablesDictionary(("a", "1"), ("b", "2"));
        var variablesContainer = CreateVariablesContainer(("a", "1"), ("b", "2"));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    [Test]
    public void Should_Return_DateTime()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.DateTime;

        var formula = "a + 2";
        var formulaWOVariables = "Now() + 2";

        var variablesDictionary = CreateVariablesDictionary(("a", DateTime.UtcNow));
        var variablesContainer = CreateVariablesContainer(("a", DateTime.UtcNow));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    [Test]
    public void Should_Return_WordList()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.WordList;

        var formula = "a";
        var formulaWOVariables = "{'1', '2'}";

        var variablesDictionary = CreateVariablesDictionary(("a", new[] { "one", "two" }.ToList()));
        var variablesContainer = CreateVariablesContainer(("a", new[] { "one", "two" }.ToList()));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    [Test]
    public void Should_Return_NumberList()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.NumberList;

        var formula = "a";
        var formulaWOVariables = "{1, 2}";

        var variablesDictionary = CreateVariablesDictionary(("a", new[] { 1d, 2 }.ToList()));
        var variablesContainer = CreateVariablesContainer(("a", new[] { 1d, 2 }.ToList()));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    [Test]
    public void Should_Return_Boolean()
    {
        // arrange
        var valueTypeKey = ValueTypeKey.Boolean;

        var formula = "a > b";
        var formulaWOVariables = "1 > 2";

        var variablesDictionary = CreateVariablesDictionary(("a", 1), ("b", 2));
        var variablesContainer = CreateVariablesContainer(("a", 1), ("b", 2));

        // act, assert
        TestString(formulaWOVariables, valueTypeKey);
        TestNodes(formulaWOVariables, valueTypeKey);

        TestVariablesDictionaryWithString(formula, variablesDictionary, valueTypeKey);
        TestVariablesDictionaryWithNodes(formula, variablesDictionary, valueTypeKey);

        TestVariablesContainerWithString(formula, variablesContainer, valueTypeKey);
        TestVariablesContainerWithNodes(formula, variablesContainer, valueTypeKey);
    }

    private static void TestVariablesDictionaryWithString(string formula, IDictionary<string, EvaluatorValue> variablesDictionary, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();

        // act
        var res = service.Calc(formula, variablesDictionary);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(formula, variablesDictionary),
            ValueTypeKey.Word => service.CalcWord(formula, variablesDictionary),
            ValueTypeKey.DateTime => service.CalcDateTime(formula, variablesDictionary),
            ValueTypeKey.WordList => service.CalcWordList(formula, variablesDictionary),
            ValueTypeKey.NumberList => service.CalcNumberList(formula, variablesDictionary),
            ValueTypeKey.Boolean => service.CalcBoolean(formula, variablesDictionary),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static void TestVariablesDictionaryWithNodes(string formula, IDictionary<string, EvaluatorValue> variablesDictionary, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();
        var nodes = CreateParser().Parse(formula);

        // act
        var res = service.Calc(nodes, variablesDictionary);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(nodes, variablesDictionary),
            ValueTypeKey.Word => service.CalcWord(nodes, variablesDictionary),
            ValueTypeKey.DateTime => service.CalcDateTime(nodes, variablesDictionary),
            ValueTypeKey.WordList => service.CalcWordList(nodes, variablesDictionary),
            ValueTypeKey.NumberList => service.CalcNumberList(nodes, variablesDictionary),
            ValueTypeKey.Boolean => service.CalcBoolean(nodes, variablesDictionary),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static void TestVariablesContainerWithString(string formula, IVariablesContainer variablesContainer, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();

        // act
        var res = service.Calc(formula, variablesContainer);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(formula, variablesContainer),
            ValueTypeKey.Word => service.CalcWord(formula, variablesContainer),
            ValueTypeKey.DateTime => service.CalcDateTime(formula, variablesContainer),
            ValueTypeKey.WordList => service.CalcWordList(formula, variablesContainer),
            ValueTypeKey.NumberList => service.CalcNumberList(formula, variablesContainer),
            ValueTypeKey.Boolean => service.CalcBoolean(formula, variablesContainer),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static void TestVariablesContainerWithNodes(string formula, IVariablesContainer variablesContainer, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();
        var nodes = CreateParser().Parse(formula);

        // act
        var res = service.Calc(nodes, variablesContainer);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(nodes, variablesContainer),
            ValueTypeKey.Word => service.CalcWord(nodes, variablesContainer),
            ValueTypeKey.DateTime => service.CalcDateTime(nodes, variablesContainer),
            ValueTypeKey.WordList => service.CalcWordList(nodes, variablesContainer),
            ValueTypeKey.NumberList => service.CalcNumberList(nodes, variablesContainer),
            ValueTypeKey.Boolean => service.CalcBoolean(nodes, variablesContainer),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static void TestString(string formula, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();

        // act
        var res = service.Calc(formula);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(formula),
            ValueTypeKey.Word => service.CalcWord(formula),
            ValueTypeKey.DateTime => service.CalcDateTime(formula),
            ValueTypeKey.WordList => service.CalcWordList(formula),
            ValueTypeKey.NumberList => service.CalcNumberList(formula),
            ValueTypeKey.Boolean => service.CalcBoolean(formula),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static void TestNodes(string formula, ValueTypeKey expectedValueType)
    {
        // arrange
        var service = CreateService();
        var nodes = CreateParser().Parse(formula);

        // act
        var res = service.Calc(nodes);
        object typedRes = expectedValueType switch
        {
            ValueTypeKey.Number => service.CalcNumber(nodes),
            ValueTypeKey.Word => service.CalcWord(nodes),
            ValueTypeKey.DateTime => service.CalcDateTime(nodes),
            ValueTypeKey.WordList => service.CalcWordList(nodes),
            ValueTypeKey.NumberList => service.CalcNumberList(nodes),
            ValueTypeKey.Boolean => service.CalcBoolean(nodes),
            _ => throw new NotImplementedException(),
        };

        // assert
        res.TypeKey.Should().Be(expectedValueType);
    }

    private static IVariablesContainer CreateVariablesContainer(params (string, EvaluatorValue)[] variables)
    {
        var variableContainer = new VariablesContainer();

        foreach (var variable in variables)
        {
            variableContainer.AddOrUpdate(variable.Item1, variable.Item2);
        }

        return variableContainer;
    }

    private static IDictionary<string, EvaluatorValue> CreateVariablesDictionary(params (string, EvaluatorValue)[] variables)
    {
        return variables.ToDictionary(key => key.Item1, val => val.Item2);
    }

    private static NoStringEvaluator CreateService(Action<NoStringEvaluatorOptions> opt = null)
    {
        opt ??= subOpt => subOpt.SetThrowIfVariableNotFound(false);
        return EvaluatorFacadeFactory.Create(opt).Evaluator;
    }

    private static FormulaParser CreateParser(Action<NoStringEvaluatorOptions> opt = null)
    {
        opt ??= subOpt => subOpt.SetThrowIfVariableNotFound(false);
        return EvaluatorFacadeFactory.Create(opt).FormulaParser;
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
