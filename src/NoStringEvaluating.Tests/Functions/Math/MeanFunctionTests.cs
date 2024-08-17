using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class MeanFunctionTests : FunctionTests<MeanFunction>
{
    [Test]
    public void Should_Mean()
    {
        // arrange
        var numberList = new[] { 2048d, 2, 897 }.ToList();
        var number = 23000;
        var expected = 6486.75;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
