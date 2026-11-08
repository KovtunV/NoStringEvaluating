using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Services.Binding;
using NoStringEvaluating.Services.Variables;
using NoStringEvaluating.Tests.UnitTests.Data;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Binding;

internal class FormulaBinderTests
{
    private NoStringEvaluator.Facade _facade;

    [SetUp]
    public void Setup()
    {
        _facade = EvaluatorFacadeFactory.Create(opt => opt.SetThrowIfVariableNotFound(false));
    }

    [TestCaseSource(typeof(EvaluateNumber), nameof(EvaluateNumber.Get))]
    public void Should_Evaluate_Bound_Number(Models.FormulaModel model)
    {
        // arrange
        var container = new IndexedVariablesContainer(model.Arguments);
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse(model.Formula), container);

        // act
        var actual = _facade.Evaluator.CalcNumber(bound, container);

        // assert
        actual.Should().BeApproximatelyNumber(model.Result.Number);
    }

    [TestCaseSource(typeof(EvaluateBoolean), nameof(EvaluateBoolean.Get))]
    public void Should_Evaluate_Bound_Boolean(Models.FormulaModel model)
    {
        // arrange
        var container = new IndexedVariablesContainer(model.Arguments);
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse(model.Formula), container);

        // act
        var actual = _facade.Evaluator.CalcBoolean(bound, container);

        // assert
        actual.Should().Be(model.Result.Boolean);
    }

    [TestCaseSource(typeof(EvaluateWord), nameof(EvaluateWord.Get))]
    public void Should_Evaluate_Bound_Word(Models.FormulaModel model)
    {
        // arrange
        var container = new IndexedVariablesContainer(model.Arguments);
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse(model.Formula), container);

        // act
        var actual = _facade.Evaluator.CalcWord(bound, container);

        // assert
        actual.Should().Be(model.Result.Word);
    }

    [Test]
    public void Should_Bind_Indexed_Names_Only_And_Leave_The_Original()
    {
        // arrange
        var container = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["c d"] = 5 });
        var nodes = _facade.FormulaParser.Parse("a + kov(b; [c d]) * 2");

        // act
        var bound = FormulaBinder.Bind(nodes, container);

        // assert
        bound.ToString().Should().Be(nodes.ToString());
        bound.Nodes.Should().NotBeSameAs(nodes.Nodes);
        ((VariableNode)bound.Nodes[0]).Index.Should().Be(0);
        var wrapper = (FunctionWrapperNode)bound.Nodes[1];
        ((VariableNode)wrapper.FunctionArgumentNodes[0][0]).Index.Should().Be(-1, "b has no index in the container");
        ((VariableNode)wrapper.FunctionArgumentNodes[1][0]).Index.Should().Be(1);
        nodes.Nodes.OfType<VariableNode>().Should().OnlyContain(n => n.Index == -1, "the original nodes are not touched");
    }

    [Test]
    public void Should_Read_Unindexed_Names_By_Name()
    {
        // arrange
        var container = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["c d"] = 5 });
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a + kov(b; [c d]) * 2"), container);
        container.AddOrUpdate("b", 3);

        // act
        var actual = _facade.Evaluator.CalcNumber(bound, container);

        // assert
        actual.Should().BeApproximatelyNumber(32);
        container.IndexReads.Should().Be(2);
        container.NameReads.Should().Be(1);
    }

    [Test]
    public void Should_Read_Bound_Nodes_By_Name_With_Another_Container()
    {
        // arrange
        var container = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["b"] = 3 });
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a * b"), container);
        var other = new VariablesContainer();
        other.AddOrUpdate("a", 4);
        other.AddOrUpdate("b", 5);
        var dictionary = new Dictionary<string, EvaluatorValue> { ["a"] = 6, ["b"] = 7 };

        // act, assert
        _facade.Evaluator.CalcNumber(bound, other).Should().BeApproximatelyNumber(20);
        _facade.Evaluator.CalcNumber(bound, dictionary).Should().BeApproximatelyNumber(42);
        _facade.Evaluator.CalcNumber(bound, container).Should().BeApproximatelyNumber(6);
    }

    [Test]
    public void Should_Read_By_Name_With_Another_Indexed_Container()
    {
        // arrange: a is index 0 in the first container and index 1 in the second
        var first = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 7 });
        var second = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["b"] = 99, ["a"] = 7 });
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a"), first);

        // act, assert
        _facade.Evaluator.CalcNumber(bound, second).Should().BeApproximatelyNumber(7);
        second.IndexReads.Should().Be(0);
        second.NameReads.Should().Be(1);
    }

    [Test]
    public void Should_Read_By_Index_With_A_Container_Of_The_Same_Key()
    {
        // arrange: two containers with one index layout share a key
        var key = new object();
        var first = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 7 }) { BindingKey = key };
        var second = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 8 }) { BindingKey = key };
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a"), first);

        // act, assert
        bound.BindingKey.Should().BeSameAs(key);
        _facade.Evaluator.CalcNumber(bound, second).Should().BeApproximatelyNumber(8);
        second.IndexReads.Should().Be(1);
        second.NameReads.Should().Be(0);
    }

    [Test]
    public void Should_Drop_Stale_Indexes_When_Rebinding()
    {
        // arrange: bound to a container with a at index 0, rebound to one that indexes nothing
        var first = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 7 });
        var nameOnly = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 7 }, indexed: false);
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a + kov(a; 2)"), first);

        // act
        var rebound = FormulaBinder.Bind(bound, nameOnly);

        // assert
        rebound.BindingKey.Should().BeSameAs(nameOnly);
        rebound.Nodes.OfType<VariableNode>().Should().OnlyContain(n => n.Index == -1);
        ((VariableNode)((FunctionWrapperNode)rebound.Nodes[1]).FunctionArgumentNodes[0][0]).Index.Should().Be(-1);
        _facade.Evaluator.CalcNumber(rebound, nameOnly).Should().BeApproximatelyNumber(21);
        nameOnly.IndexReads.Should().Be(0);
    }

    [Test]
    public void Should_Return_Null_For_A_Bound_Index_Without_Value()
    {
        // arrange
        var container = new TrackingIndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2 });
        var bound = FormulaBinder.Bind(_facade.FormulaParser.Parse("a + 1"), container);
        var empty = new TrackingIndexedVariablesContainer();

        // act
        var actual = _facade.Evaluator.Calc(bound, empty);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
