using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Tan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Tan;

internal class TghFunctionTests : FunctionTests<TghFunction>
{
    [TestCase(0.23, 0.226)]
    public void Should_Tgh(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
