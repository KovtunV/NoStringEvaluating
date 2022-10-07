using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class RightFunctionTests : FunctionTests<RightFunction>
{
    [Test]
    public void Should_Return_Right_Char()
    {
        // arrange
        var word = _fixture.Create<string>();
        var expected = word[^1].ToString();

        // act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("Hello world", 4, "orld")]
    [TestCase("Hello world", -4, "")]
    [TestCase("Hello world", 40, "Hello world")]
    public void Should_Return_Right_Chars_By_Count(string word, int count, string expected)
    {
        // arrange, act
        var actual = Execute(word, count);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("Hello world", "or", "ld")]
    [TestCase("Hello world", "vita", "Hello world")]
    [TestCase("Hello world", "H", "ello world")]
    public void Should_Return_Right_Chars_By_Word(string word, string subText, string expected)
    {
        // arrange, act
        var actual = Execute(word, subText);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Right_Char_For_List()
    {
        // arrange
        var wordList = _fixture.Create<List<string>>();
        var expected = wordList.Select(x => x[^1].ToString()).ToList();

        // act
        var actual = Execute(wordList);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Right_Chars_By_Count_For_List()
    {
        // arrange
        var count = 4;
        var wordList = new[] { "Hello world", "My name is" }.ToList();
        var expected = new[] { "orld", "e is" }.ToList();

        // act
        var actual = Execute(wordList, count);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Right_Chars_By_Word_For_List()
    {
        // arrange
        var subText = "wo";
        var wordLIst = new[] { "Hello world", "My n millwon" }.ToList();
        var expected = new[] { "rld", "n" }.ToList();

        // act
        var actual = Execute(wordLIst, subText);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Shoult_Return_Null_On_Wrong_Value()
    {
        // arrange
        var wrongValue = true;

        // act
        var actual = Execute(wrongValue, string.Empty, string.Empty);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
