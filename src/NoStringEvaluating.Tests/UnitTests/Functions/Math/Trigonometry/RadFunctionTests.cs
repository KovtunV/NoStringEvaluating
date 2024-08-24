using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math.Trigonometry;

internal class RadFunctionTests : FunctionTests<RadFunction>
{
    [TestCase(-145.23, -2.535)]
    public void Should_Rad(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
