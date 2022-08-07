using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Logic;

internal class IfFunctionTests : FunctionTests<IfFunction>
{
    [TestCase(true, "my word", "nope", "my word")]
    [TestCase(false, "my word", "nope", "nope")]
    public void Should_If(bool ifValue, string positive, string negative, string expected)
    {
        // arrange, act
        var actual = Execute(ifValue, positive, negative, expected);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }
}
