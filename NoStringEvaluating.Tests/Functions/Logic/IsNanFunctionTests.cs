using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Logic;

internal class IsNanFunctionTests : FunctionTests<IsNanFunction>
{
    [TestCase(double.NaN, true)]
    [TestCase(13, false)]
    [TestCase(0, false)]
    public void Should_Return_Is_Nan(double number, bool expected)
    {
        // arrange, act
        var actual = Execute(number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.GetBoolean().Should().Be(expected);
    }
}
