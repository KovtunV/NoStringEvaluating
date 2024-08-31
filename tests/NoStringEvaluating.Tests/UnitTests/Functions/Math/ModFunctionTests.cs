using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class ModFunctionTests : FunctionTests<ModFunction>
{
    [TestCase(5, 2, 1)]
    public void Should_Mod(double a, double b, double expected)
    {
        // arrange, act
        var actual = Execute(a, b);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
