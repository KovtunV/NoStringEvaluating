using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class LowerFunctionTests : FunctionTests<LowerFunction>
{
    [Test]
    public void Should_Return_LowerCase_For_Word()
    {
        // arrange
        var word = "SOMe text";
        var expected = "some text";

        // act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }

    [Test]
    public void Should_Return_LowerCase_For_WordList()
    {
        // arrange
        var wordList = new[] { "SOMe text", "AN", "OTHER", "text" }.ToList();
        var expected = new[] { "some text", "an", "other", "text" }.ToList();

        // act
        var actual = Execute(wordList);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Null_on_Wrong_Value()
    {
        // arrange
        var wrongValue = true;

        // act
        var actual = Execute(wrongValue);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
