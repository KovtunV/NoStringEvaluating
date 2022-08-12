using FluentAssertions;
using NoStringEvaluating.Functions.Null;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Null;

internal class NullIfFunctionTests : FunctionTests<NullIfFunction>
{
    [Test]
    public void Should_Return_Null_Argument()
    {
        // arrange
        var word = "hey";

        // arrange, act
        var actual = Execute(word, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
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
