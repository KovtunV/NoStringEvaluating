using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Logic;

internal class AndFunctionTests : FunctionTests<AndFunction>
{
    [TestCase(true, true, true)]
    [TestCase(false, true, false)]
    [TestCase(true, false, false)]
    [TestCase(false, false, false)]
    public void Should_And(bool a, bool b, bool expected)
    {
        // arrange, act
        var actual = Execute(a, b);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.GetBoolean().Should().Be(expected);
    }
}
