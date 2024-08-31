using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class FactFunctionTests : FunctionTests<FactFunction>
{
    [TestCase(6, 720)]
    [TestCase(5, 120)]
    [TestCase(3, 6)]
    [TestCase(2, 2)]
    [TestCase(1, 1)]
    [TestCase(0, 1)]
    public void Should_Fact(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
