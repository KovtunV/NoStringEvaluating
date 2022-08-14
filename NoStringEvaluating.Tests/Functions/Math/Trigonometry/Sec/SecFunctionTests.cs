using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math.Trigonometry.Sec;

internal class SecFunctionTests : FunctionTests<SecFunction>
{
    [TestCase(0.8, 1.435)]
    public void Should_Sec(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
