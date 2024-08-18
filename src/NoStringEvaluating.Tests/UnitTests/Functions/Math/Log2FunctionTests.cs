using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Math;

internal class Log2FunctionTests : FunctionTests<Log2Function>
{
    [TestCase(2048, 11)]
    public void Should_Log2(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
