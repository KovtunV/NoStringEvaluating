using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Word;

internal class LeftFunctionTests : FunctionTests<LeftFunction>
{
    [Test]
    public void Should_Return_Left_Char()
    {
        // arrange
        var word = _fixture.Create<string>();
        var expected = word[0].ToString();

        // act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("Hello world", 4, "Hell")]
    [TestCase("Hello world", -4, "")]
    [TestCase("Hello world", 40, "Hello world")]
    public void Should_Return_Left_Chars_By_Count(string word, int count, string expected)
    {
        // arrange, act
        var actual = Execute(word, count);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("Hello world", "or", "Hello w")]
    [TestCase("Hello world", "vita", "Hello world")]
    [TestCase("Hello world", "l", "He")]
    public void Should_Return_Left_Chars_By_Word(string word, string subText, string expected)
    {
        // arrange, act
        var actual = Execute(word, subText);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Left_Char_For_List()
    {
        // arrange
        var wordList = _fixture.Create<List<string>>();
        var expected = wordList.Select(x => x[0].ToString()).ToList();

        // act
        var actual = Execute(wordList);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Left_Chars_By_Count_For_List()
    {
        // arrange
        var count = 4;
        var wordList = new[] { "Hello world", "My name is" }.ToList();
        var expected = new[] { "Hell", "My n" }.ToList();

        // act
        var actual = Execute(wordList, count);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Left_Chars_By_Word_For_List()
    {
        // arrange
        var subText = "wo";
        var wordLIst = new[] { "Hello world", "My n millwon" }.ToList();
        var expected = new[] { "Hello ", "My n mill" }.ToList();

        // act
        var actual = Execute(wordLIst, subText);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Null_On_Wrong_Value()
    {
        // arrange
        var val = true;

        // act
        var actual = Execute(val);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
