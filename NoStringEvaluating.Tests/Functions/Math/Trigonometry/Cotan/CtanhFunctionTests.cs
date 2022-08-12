using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cotan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Cotan;

internal class CtanhFunctionTests : FunctionTests<CtanhFunction>
{
    [TestCase(1.8, 1.056)]
    public void Should_Ctanh(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
