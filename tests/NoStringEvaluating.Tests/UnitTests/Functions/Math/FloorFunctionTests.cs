using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class FloorFunctionTests : FunctionTests<FloorFunction>
{
    [TestCase(1.6, 1)]
    public void Should_Floor(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
