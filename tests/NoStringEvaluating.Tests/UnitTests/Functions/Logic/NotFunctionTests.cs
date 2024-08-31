using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Logic;

internal class NotFunctionTests : FunctionTests<NotFunction>
{
    [TestCase(true, false)]
    [TestCase(false, true)]
    public void Should_Reverse_Value(bool boolValue, bool expected)
    {
        // arrange, act
        var actual = Execute(boolValue);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }
}
