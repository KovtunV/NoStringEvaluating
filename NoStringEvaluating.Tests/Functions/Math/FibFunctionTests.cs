using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class FibFunctionTests : FunctionTests<FibFunction>
{
    [TestCase(16, 987)]
    [TestCase(8, 21)]
    [TestCase(4, 3)]
    [TestCase(3, 2)]
    [TestCase(2, 1)]
    [TestCase(1, 1)]
    [TestCase(0, 0)]
    public void Should_Fib(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
