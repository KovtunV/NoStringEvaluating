using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Binding;
using NoStringEvaluating.Services.Variables;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Variables;

internal class IndexedVariablesContainerTests
{
    [Test]
    public void Should_Preserve_Indexes_And_Binding_When_Updating_And_Adding()
    {
        var container = new IndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["b"] = 3 });
        var facade = NoStringEvaluator.CreateFacade();
        var bound = FormulaBinder.Bind(facade.FormulaParser.Parse("a * b"), container);

        container.AddOrUpdate("a", 4);
        container.AddOrUpdate("c", 99);

        container.IndexOf("a").Should().Be(0);
        container.IndexOf("b").Should().Be(1);
        container.IndexOf("c").Should().Be(2);
        container.TryGetValue(0, out var value).Should().BeTrue();
        value.Number.Should().Be(4);
        container.GetValue("a").Number.Should().Be(4);
        bound.BindingKey.Should().BeSameAs(container.BindingKey);
        facade.Evaluator.CalcNumber(bound, container).Should().Be(12);
    }

    [Test]
    public void Should_Copy_Initial_Values_And_Use_Case_Sensitive_Names()
    {
        var initial = new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["A"] = 3 };
        var container = new IndexedVariablesContainer(initial);

        initial["a"] = 100;
        container.AddOrUpdate("A", 4);

        container.IndexOf("a").Should().Be(0);
        container.IndexOf("A").Should().Be(1);
        container.GetValue("a").Number.Should().Be(2);
        container.GetValue("A").Number.Should().Be(4);
        initial["A"].Number.Should().Be(3);
    }

    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(int.MaxValue)]
    public void Should_Return_False_For_Invalid_Indexes(int index)
    {
        var container = new IndexedVariablesContainer();
        container.AddOrUpdate("a", 2);

        container.TryGetValue(index, out var value).Should().BeFalse();
        value.TypeKey.Should().Be(ValueTypeKey.Null);
    }

    [Test]
    public void Should_Distinguish_Missing_And_Null_Values()
    {
        var container = new IndexedVariablesContainer();
        container.AddOrUpdate("present", default);

        container.TryGetValue("present", out var present).Should().BeTrue();
        present.TypeKey.Should().Be(ValueTypeKey.Null);
        container.TryGetValue(0, out present).Should().BeTrue();
        present.TypeKey.Should().Be(ValueTypeKey.Null);
        container.IndexOf("missing").Should().Be(-1);
        container.TryGetValue("missing", out var missing).Should().BeFalse();
        missing.TypeKey.Should().Be(ValueTypeKey.Null);
        container.GetValue("missing").TypeKey.Should().Be(ValueTypeKey.Null);
    }

    [Test]
    public void Should_Return_A_Snapshot_From_AddOrUpdate()
    {
        var container = new IndexedVariablesContainer();
        var snapshot = container.AddOrUpdate("a", 2);

        snapshot.Name.Should().Be("a");
        snapshot.Value.Number.Should().Be(2);
        snapshot.Name = "b";
        snapshot.Value = 99;

        container.GetValue("a").Number.Should().Be(2);
        container.IndexOf("b").Should().Be(-1);
    }

    [Test]
    public void Should_Fall_Back_To_Names_For_Another_Container_Or_Dictionary()
    {
        var first = new IndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["a"] = 2, ["b"] = 3 });
        var second = new IndexedVariablesContainer(new Dictionary<string, EvaluatorValue> { ["b"] = 5, ["a"] = 4 });
        var dictionary = new Dictionary<string, EvaluatorValue> { ["a"] = 6, ["b"] = 7 };
        var facade = NoStringEvaluator.CreateFacade();
        var bound = FormulaBinder.Bind(facade.FormulaParser.Parse("a * b"), first);

        first.BindingKey.Should().NotBeSameAs(second.BindingKey);
        facade.Evaluator.CalcNumber(bound, first).Should().Be(6);
        facade.Evaluator.CalcNumber(bound, second).Should().Be(20);
        facade.Evaluator.CalcNumber(bound, dictionary).Should().Be(42);
    }
}
