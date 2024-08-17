using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class MiddleFunctionTests : FunctionTests<MiddleFunction>
{
    [TestCase("one two kovtun loves painting two", 0, 3, "one")]
    [TestCase("one two kovtun loves painting two", 4, 3, "two")]
    [TestCase("one two kovtun loves painting two", -56, 3, "")]
    [TestCase("one two kovtun loves painting two", 4, 30, "two kovtun loves painting two")]
    public void Shoult_Cut_Word(string word, int start, int end, string expected)
    {
        // arrange, act
        var actual = Execute(word, start, end);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("one two kovtun loves painting two", 8, "two", "kovtun loves painting ")]
    public void Shoult_Cut_Word_By_Number_And_Word(string word, int start, string end, string expected)
    {
        // arrange, act
        var actual = Execute(word, start, end);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("one two kovtun loves painting two", "two", 7, " kovtun")]
    public void Shoult_Cut_Word_By_Word_And_Number(string word, string start, int end, string expected)
    {
        // arrange, act
        var actual = Execute(word, start, end);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("one two kovtun loves painting two", "two ", " two", "kovtun loves painting")]
    public void Shoult_Cut_Word_By_Word(string word, string start, string end, string expected)
    {
        // arrange, act
        var actual = Execute(word, start, end);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [Test]
    public void Shoult_Cut_Word_By_Word_For_List()
    {
        // arrange
        var wordList = new[] { "one two kovtun loves painting two", "one two hey two" }.ToList();
        var start = "two ";
        var end = " two";
        var expected = new[] { "kovtun loves painting", "hey" }.ToList();

        // act
        var actual = Execute(wordList, start, end);

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
        var actual = Execute(wrongValue, 0, 0);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
