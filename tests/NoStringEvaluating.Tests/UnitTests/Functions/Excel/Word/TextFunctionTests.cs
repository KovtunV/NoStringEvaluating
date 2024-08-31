using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Word;

internal class TextFunctionTests : FunctionTests<TextFunction>
{
    [TestCaseSource(nameof(GetCases))]
    public void Should_To_String(EvaluatorValue value, string expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    public static IEnumerable<object[]> GetCases()
    {
        yield return new object[] { new EvaluatorValue(5), "5" };
        yield return new object[] { new EvaluatorValue("hello world"), "hello world" };
        yield return new object[] { default(EvaluatorValue), "Null" };
        yield return new object[] { new EvaluatorValue(true), "True" };
        yield return new object[] { new EvaluatorValue(new[] { 1d, 2 }.ToList()), "1, 2" };
        yield return new object[] { new EvaluatorValue(new[] { "one", "two" }.ToList()), "one, two" };
    }
}
