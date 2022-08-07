using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cos;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Cos;

internal class AcosFunctionTests : FunctionTests<AcosFunction>
{
    [TestCase(-0.8, 2.498)]
    public void Should_Acos(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
