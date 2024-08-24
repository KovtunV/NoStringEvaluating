using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cotan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Cotan;

internal class CtanFunctionTests : FunctionTests<CtanFunction>
{
    [TestCase(1.8, -0.233)]
    public void Should_Ctan(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
