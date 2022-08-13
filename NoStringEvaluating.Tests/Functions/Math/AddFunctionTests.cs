using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class AddFunctionTests : FunctionTests<AddFunction>
{
    [Test]
    public void Should_Add()
    {
        // arrange
        var numberList = new[] { 2d, 5, 5 }.ToList();
        var number = 10;
        var expected = 22d;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
