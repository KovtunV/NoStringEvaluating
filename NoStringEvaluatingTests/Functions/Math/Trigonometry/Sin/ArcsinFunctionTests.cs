using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sin;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Sin;

internal class ArcsinFunctionTests : FunctionTests<ArcsinFunction>
{
    [TestCase(0.23, 0.232)]
    public void Should_Arcsin(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
