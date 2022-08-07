using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sin;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Sin;

internal class SinhFunctionTests : FunctionTests<SinhFunction>
{
    [TestCase(1.23, 1.564)]
    public void Should_Sinh(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
