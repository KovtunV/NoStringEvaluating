using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel;

internal class ToNumberFunctionTests : FunctionTests<ToNumberFunction>
{
    [TestCaseSource(nameof(GetCases))]
    public void Should_To_Number(EvaluatorValue value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }

    public static IEnumerable<object[]> GetCases()
    {
        yield return new object[] { new EvaluatorValue(5), 5 };
        yield return new object[] { new EvaluatorValue("16"), 16 };
        yield return new object[] { new EvaluatorValue(), double.NaN };
        yield return new object[] { new EvaluatorValue(true), double.NaN };
        yield return new object[] { new EvaluatorValue(new[] { 1d, 2 }.ToList()), double.NaN };
        yield return new object[] { new EvaluatorValue(new[] { "one", "two" }.ToList()), double.NaN };
    }
}
