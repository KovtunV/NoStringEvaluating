using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math.Trigonometry;

internal class DegFunctionTests : FunctionTests<DegFunction>
{
    [TestCase(-145.23, -8321.066)]
    public void Should_Deg(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
