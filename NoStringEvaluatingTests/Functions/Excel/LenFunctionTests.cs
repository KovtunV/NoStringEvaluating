using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel;

internal class LenFunctionTests : FunctionTests<LenFunction>
{
    [TestCase("hello world", 11)]
    [TestCase(" ", 1)]
    [TestCase("", 0)]
    public void Should_Return_Length(string word, int expected)
    {
        // arrange, act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }
}
