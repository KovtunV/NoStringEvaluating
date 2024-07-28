using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel;

internal class IsNumberFunctionTests : FunctionTests<IsNumberFunction>
{
    [TestCaseSource(nameof(GetCases))]
    public void Should_Check_Is_Number(EvaluatorValue value, bool isNumber)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(isNumber);
    }

    private static IEnumerable<object[]> GetCases()
    {
        yield return new object[] { new EvaluatorValue("text"), false };
        yield return new object[] { new EvaluatorValue(1), true };
        yield return new object[] { new EvaluatorValue(new List<double>()), false };
        yield return new object[] { new EvaluatorValue(new List<string>()), false };
        yield return new object[] { new EvaluatorValue(true), false };
        yield return new object[] { default(EvaluatorValue), false };
    }
}
