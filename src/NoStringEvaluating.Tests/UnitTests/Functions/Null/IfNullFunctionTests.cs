using FluentAssertions;
using NoStringEvaluating.Functions.Null;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Null;

internal class IfNullFunctionTests : FunctionTests<IfNullFunction>
{
    [Test]
    public void Should_Return_Second_Argument()
    {
        // arrange
        var word = "hey";

        // arrange, act
        var actual = Execute(default, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(word);
    }

    [Test]
    public void Should_Return_First_Argument()
    {
        // arrange
        var word = "hey";

        // arrange, act
        var actual = Execute(5, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(5);
    }
}
