using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Logic;

internal class OrFunctionTests : FunctionTests<OrFunction>
{
    [TestCase(true, true, true)]
    [TestCase(false, true, true)]
    [TestCase(true, false, true)]
    [TestCase(false, false, false)]
    public void Should_Or(bool a, bool b, bool expected)
    {
        // arrange, act
        var actual = Execute(a, b);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }
}
