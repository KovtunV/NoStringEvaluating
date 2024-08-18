using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class LnFunctionTests : FunctionTests<LnFunction>
{
    [TestCase(12, 2.485)]
    public void Should_Ln(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
