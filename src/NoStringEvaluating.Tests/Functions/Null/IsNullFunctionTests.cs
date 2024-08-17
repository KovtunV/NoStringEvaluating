using FluentAssertions;
using NoStringEvaluating.Functions.Null;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Null;

internal class IsNullFunctionTests : FunctionTests<IsNullFunction>
{
    [TestCaseSource(nameof(GetTestCases))]
    public void Should_IsNull(EvaluatorValue value, bool expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }

    private static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] { default(EvaluatorValue), true };
        yield return new object[] { new EvaluatorValue("some word"), false };
    }
}
