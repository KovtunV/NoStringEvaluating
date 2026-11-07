using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Models.Values;

internal class InternalEvaluatorValueTests
{
    [Test]
    public void Should_Return_Non_Scientific_Notation()
    {
        // arrange
        var value = new InternalEvaluatorValue(0.0001 / 7);
        var expected = "0.0000142857142857143";

        // act
        var vlueStr = value.ToString();

        // assert
        vlueStr.Should().Be(expected);
    }

    [TestCase(DateTimeKind.Utc)]
    [TestCase(DateTimeKind.Local)]
    [TestCase(DateTimeKind.Unspecified)]
    public void Should_Keep_DateTime_Ticks_And_Kind(DateTimeKind kind)
    {
        // arrange
        var dateTime = new DateTime(2026, 11, 6, 12, 34, 56, 789, kind);

        // act
        var value = new InternalEvaluatorValue(dateTime);

        // assert
        value.IsDateTime.Should().BeTrue();
        value.DateTime.Should().Be(dateTime);
        value.DateTime.Kind.Should().Be(kind);
        value.Word.Should().BeNull();
    }

    [Test]
    public void Should_Keep_DateTime_Extremes()
    {
        // arrange, act
        var min = new InternalEvaluatorValue(DateTime.MinValue);
        var max = new InternalEvaluatorValue(DateTime.MaxValue);

        // assert
        min.DateTime.Should().Be(DateTime.MinValue);
        max.DateTime.Should().Be(DateTime.MaxValue);
        max.DateTime.Kind.Should().Be(DateTime.MaxValue.Kind);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Should_Keep_Boolean(bool boolean)
    {
        // arrange, act
        var value = new InternalEvaluatorValue(boolean);

        // assert
        value.IsBoolean.Should().BeTrue();
        value.Boolean.Should().Be(boolean);
        value.Number.Should().Be(0);
        value.Word.Should().BeNull();
        value.Object.Should().BeNull();
    }
}
