using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel.Word;

internal class UniqueFunctionTests : FunctionTests<UniqueFunction>
{
    [Test]
    public void Should_Return_Unique_Words()
    {
        // arrange
        var wordList = new[] { "one", "two", "one", "three" }.ToList();
        var expected = new[] { "two", "three" }.ToList();

        // act
        var actual = Execute(wordList, true);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Words_WO_Doubles()
    {
        // arrange
        var wordList = new[] { "one", "two", "one", "three" }.ToList();
        var expected = new[] { "one", "two", "three" }.ToList();

        // act
        var actual = Execute(wordList, false);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Words_WO_Doubles_WO_Arg()
    {
        // arrange
        var wordList = new[] { "one", "two", "one", "three" }.ToList();
        var expected = new[] { "one", "two", "three" }.ToList();

        // act
        var actual = Execute(wordList);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }
}
