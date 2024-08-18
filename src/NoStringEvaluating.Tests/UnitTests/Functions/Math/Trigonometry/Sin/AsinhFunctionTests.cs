using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sin;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Sin;

internal class AsinhFunctionTests : FunctionTests<AsinhFunction>
{
    [TestCase(0.23, 0.228)]
    public void Should_Asinh(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
