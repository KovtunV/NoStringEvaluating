using FluentAssertions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services;

internal class OperationProcessorTests
{
    [Test]
    public void Should_Multiply_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(3);
        var expected = 6;

        // act
        var res = OperationProcessor.Multiply(a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_When_Multiply_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.Word);

        // act
        var res = OperationProcessor.Multiply(a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Divide_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(6);
        var b = new InternalEvaluatorValue(3);
        var expected = 2;

        // act
        var res = OperationProcessor.Divide(a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_When_Divide_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.Word);

        // act
        var res = OperationProcessor.Divide(a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Plus_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(6);
        var b = new InternalEvaluatorValue(3);
        var expected = 9;

        // act
        var res = OperationProcessor.Plus(factory, a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_When_Plus_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.Object);

        // act
        var res = OperationProcessor.Plus(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Plus_Words()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.Word.Create("one");
        var b = factory.Word.Create("two");
        var expected = "onetwo";

        // act
        var res = OperationProcessor.Plus(factory, a, b);

        // assert
        res.Word.Should().Be(expected);
    }

    [Test]
    public void Should_Plus_Number_With_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var expected = new DateTime(2024, 10, 5, 5, 5, 5);

        // act
        var res = OperationProcessor.Plus(factory, a, b);

        // assert
        res.DateTime.Should().Be(expected);
    }

    [Test]
    public void Should_Plus_DateTime_With_Number()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = new InternalEvaluatorValue(2);
        var expected = new DateTime(2024, 10, 5, 5, 5, 5);

        // act
        var res = OperationProcessor.Plus(factory, a, b);

        // assert
        res.DateTime.Should().Be(expected);
    }

    [Test]
    public void Should_Minus_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(6);
        var b = new InternalEvaluatorValue(3);
        var expected = 3;

        // act
        var res = OperationProcessor.Minus(factory, a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_When_Minus_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.Object);

        // act
        var res = OperationProcessor.Minus(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Minus_DateTime_With_Number()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = new InternalEvaluatorValue(2);
        var expected = new DateTime(2024, 10, 1, 5, 5, 5);

        // act
        var res = OperationProcessor.Minus(factory, a, b);

        // assert
        res.DateTime.Should().Be(expected);
    }

    [Test]
    public void Should_Minus_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = factory.DateTime.Create(new DateTime(2024, 10, 1, 5, 5, 5));
        var expected = 2;

        // act
        var res = OperationProcessor.Minus(factory, a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Power_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(3);
        var expected = 8;

        // act
        var res = OperationProcessor.Power(a, b);

        // assert
        res.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_When_Power_Numbers()
    {
        // arrange
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.Word);

        // act
        var res = OperationProcessor.Power(a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Less_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.Less(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_Null_When_Less_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.Less(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_Less_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = factory.DateTime.Create(new DateTime(2024, 10, 7, 5, 5, 5));

        // act
        var res = OperationProcessor.Less(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_LessEqual_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(3);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.LessEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_Null_When_LessEqual_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.LessEqual(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_LessEqual_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));

        // act
        var res = OperationProcessor.LessEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_More_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(5);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.More(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_Null_When_More_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.More(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_More_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 7, 5, 5, 6));
        var b = factory.DateTime.Create(new DateTime(2024, 10, 7, 5, 5, 5));

        // act
        var res = OperationProcessor.More(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_MoreEqual_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(3);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.MoreEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_Null_When_MoreEqual_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.MoreEqual(factory, a, b);

        // assert
        res.IsNull.Should().BeTrue();
    }

    [Test]
    public void Should_MoreEqual_DateTime()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));
        var b = factory.DateTime.Create(new DateTime(2024, 10, 3, 5, 5, 5));

        // act
        var res = OperationProcessor.MoreEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Equal_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(3);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.Equal(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_False_When_Equal_Not_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.Equal(factory, a, b);

        // assert
        res.Boolean.Should().BeFalse();
    }

    [Test]
    public void Should_NotEqual_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(4);
        var b = new InternalEvaluatorValue(3);

        // act
        var res = OperationProcessor.NotEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Return_True_When_NotEqual_Not_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(2);
        var b = new InternalEvaluatorValue(0, ValueTypeKey.DateTime);

        // act
        var res = OperationProcessor.NotEqual(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_And_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(4);
        var b = new InternalEvaluatorValue(-4);

        // act
        var res = OperationProcessor.And(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_And_Booleans()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.Boolean.Create(true);
        var b = factory.Boolean.Create(true);

        // act
        var res = OperationProcessor.And(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Or_Numbers()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = new InternalEvaluatorValue(4);
        var b = new InternalEvaluatorValue(0);

        // act
        var res = OperationProcessor.Or(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }

    [Test]
    public void Should_Or_Booleans()
    {
        // arrange
        var factory = new ValueFactory(new());
        var a = factory.Boolean.Create(true);
        var b = factory.Boolean.Create(false);

        // act
        var res = OperationProcessor.Or(factory, a, b);

        // assert
        res.Boolean.Should().BeTrue();
    }
}
