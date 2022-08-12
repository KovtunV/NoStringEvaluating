using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel.Word;

internal class UpperFunctionTests : FunctionTests<UpperFunction>
{
    [Test]
    public void Should_Return_UpperCase_For_Word()
    {
        // arrange
        var word = "SOMe text";
        var expected = "SOME TEXT";

        // act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }

    [Test]
    public void Should_Return_UpperCase_For_WordList()
    {
        // arrange
        var wordList = new[] { "SOMe text", "AN", "OTHER", "text" }.ToList();
        var expected = new[] { "SOME TEXT", "AN", "OTHER", "TEXT" }.ToList();

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
