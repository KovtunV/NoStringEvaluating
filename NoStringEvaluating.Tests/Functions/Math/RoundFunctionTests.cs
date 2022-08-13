using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class RoundFunctionTests : FunctionTests<RoundFunction>
{
    [TestCase(12.235, 2, 12.24)]
    public void Should_Round(double value, int digits, double expected)
    {
        // arrange, act
        var actual = Execute(value, digits);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
