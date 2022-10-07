using System.Linq;
using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class GcdFunctionTests : FunctionTests<GcdFunction>
{
    [Test]
    public void Should_Gcd()
    {
        // arrange
        var numberList = new[] { 56d, 24 }.ToList();
        var expected = 2;

        // act
        var actual = Execute(numberList, 6);

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

    [Test]
    public void Should_Return_Null_With_Zero()
    {
        // arrange
        var numberList = new[] { 56d, 24, 0 }.ToList();

        // act
        var actual = Execute(numberList);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
