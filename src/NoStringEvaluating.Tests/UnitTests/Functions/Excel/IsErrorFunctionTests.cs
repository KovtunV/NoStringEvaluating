using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel;

internal class IsErrorFunctionTests : FunctionTests<IsErrorFunction>
{
    [TestCase(double.NaN, true)]
    [TestCase(13, false)]
    [TestCase(0, false)]
    public void Should_Return_Is_Error(double number, bool expected)
    {
        // arrange, act
        var actual = Execute(number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }
}
