using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class SgnFunctionTests : FunctionTests<SgnFunction>
{
    [TestCase(5, 1)]
    [TestCase(-5, -1)]
    [TestCase(0, 0)]
    public void Should_Sgn(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
