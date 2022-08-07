using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Tan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Tan;

internal class ArctgFunctionTests : FunctionTests<ArctgFunction>
{
    [TestCase(0.23, 0.226)]
    public void Should_Arctg(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
