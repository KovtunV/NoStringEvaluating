using System.Linq;
using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math;

internal class LcmFunctionTests : FunctionTests<LcmFunction>
{
    [Test]
    public void Should_Lcm()
    {
        // arrange
        var numberList = new[] { 12d, 15, 10 }.ToList();
        var expected = 300;

        // act
        var actual = Execute(numberList, 75);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }

    [Test]
    public void Should_Return_First_With_Single_Argument()
    {
        // arrange
        var number = _fixture.Create<double>();

        // act
        var actual = Execute(number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(number);
    }
}
