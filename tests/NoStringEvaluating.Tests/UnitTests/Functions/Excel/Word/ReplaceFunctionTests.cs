using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Word;

internal class ReplaceFunctionTests : FunctionTests<ReplaceFunction>
{
    [TestCase("my name is vitaly", "vitaly", "git", "my name is git")]
    [TestCase("my name is vitaly", "name is", "", "my  vitaly")]
    public void Should_Replace(string word, string oldValue, string newValue, string expected)
    {
        // arrange, act
        var actual = Execute(word, oldValue, newValue);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [Test]
    public void Should_Replace_For_List()
    {
        // arrange
        var wordList = new[] { "hello", "goodbye" }.ToList();
        var oldValue = "o";
        var newValue = "HI";
        var expected = new[] { "hellHI", "gHIHIdbye" }.ToList();

        // arrange, act
        var actual = Execute(wordList, oldValue, newValue);

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
