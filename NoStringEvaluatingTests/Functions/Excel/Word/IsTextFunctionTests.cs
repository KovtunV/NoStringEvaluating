using System.Collections.Generic;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel.Word;

internal class IsTextFunctionTests : FunctionTests<IsTextFunction>
{
    [TestCaseSource(nameof(GetCases))]
    public void Should_Check_Is_Text(EvaluatorValue value, bool isText)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.GetBoolean().Should().Be(isText);
    }

    private static IEnumerable<object[]> GetCases()
    {
        yield return new object[] { new EvaluatorValue("text"), true };
        yield return new object[] { new EvaluatorValue(1), false };
        yield return new object[] { new EvaluatorValue(new List<double>()), false };
        yield return new object[] { new EvaluatorValue(new List<string>()), false };
        yield return new object[] { new EvaluatorValue(true), false };
        yield return new object[] { new EvaluatorValue(), false };
    }
}
