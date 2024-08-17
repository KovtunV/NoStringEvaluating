using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class MaxFunctionTests : FunctionTests<MaxFunction>
{
    [Test]
    public void Should_Max()
    {
        // arrange
        var numberList = new[] { 1d, 2, 3, 6 }.ToList();
        var number = 5;
        var expected = 6;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
