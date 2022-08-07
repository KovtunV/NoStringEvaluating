using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Sec;

internal class ArsechFunctionTests : FunctionTests<ArsechFunction>
{
    [TestCase(0.8, 0.693)]
    public void Should_Arsech(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
