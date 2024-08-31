using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cotan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry.Cotan;

internal class ArccotFunctionTests : FunctionTests<ArccotFunction>
{
    [TestCase(0.8, 0.896)]
    public void Should_Arccot(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
