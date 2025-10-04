using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Sec;

internal class AsechFunctionTests : FunctionTests<AsechFunction>
{
    [TestCase(0.8, 0.693)]
    public void Should_Asech(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
