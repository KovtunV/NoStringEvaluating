using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry;

internal class ExpFunctionTests : FunctionTests<ExpFunction>
{
    [TestCase(-1, 0.368)]
    public void Should_Exp(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
