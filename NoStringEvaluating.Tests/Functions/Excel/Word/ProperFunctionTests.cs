using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class ProperFunctionTests : FunctionTests<ProperFunction>
{
    [TestCase("hello world heeey", "Hello World Heeey")]
    [TestCase("HELLO", "Hello")]
    [TestCase("Hey", "Hey")]
    public void Should_Capitalize_First_Letter(string word, string expected)
    {
        // arrange, act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }

    [Test]
    public void Should_Capitalize_First_Letter_For_List()
    {
        // arrange
        var wordList = new[] { "one two", "tHREE" }.ToList();
        var expected = new[] { "One Two", "Three" }.ToList();

        // act
        var actual = Execute(wordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Shoult_Return_Null_On_Wrong_Value()
    {
        // arrange
        var wrongValue = true;

        // act
        var actual = Execute(wrongValue);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
