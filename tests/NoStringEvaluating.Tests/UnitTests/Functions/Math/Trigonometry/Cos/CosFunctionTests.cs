using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cos;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Cos;

internal class CosFunctionTests : FunctionTests<CosFunction>
{
    [TestCase(18, 0.66)]
    public void Should_Cos(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
