using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math;

internal class LogFunctionTests : FunctionTests<LogFunction>
{
    [TestCase(2048, 2, 11)]
    public void Should_Log(double value, double newBase, double expected)
    {
        // arrange, act
        var actual = Execute(value, newBase);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
