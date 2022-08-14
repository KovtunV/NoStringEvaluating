using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class MultiFunctionTests : FunctionTests<MultiFunction>
{
    [Test]
    public void Should_Multi()
    {
        // arrange
        var numberList = new[] { 2048d, 2, 897 }.ToList();
        var number = 23000;
        var expected = 84504576000;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
