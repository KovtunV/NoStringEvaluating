using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Models.Values;

internal class EvaluatorValueTests
{
    [Test]
    public void Should_Return_Non_Scientific_Notation()
    {
        // arrange
        var value = new EvaluatorValue(0.0001 / 7);
        var expected = "0.0000142857142857143";

        // act
        var vlueStr = value.ToString();

        // assert
        vlueStr.Should().Be(expected);
    }
}
