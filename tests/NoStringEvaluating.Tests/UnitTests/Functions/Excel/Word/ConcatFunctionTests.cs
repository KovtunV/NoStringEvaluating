using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Word;

internal class ConcatFunctionTests : FunctionTests<ConcatFunction>
{
    [Test]
    public void Should_Concat()
    {
        // arrange
        var wordList = new[] { "one", "Two" }.ToList();
        var numberList = new[] { 12d, 3, 8 }.ToList();
        var boolValue = true;
        var wordValue = "any word";
        var numberValue = 55;
        var expected = "oneTwoany word1238True55";

        // act
        var actual = Execute(wordList, wordValue, numberList, boolValue, numberValue);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Empty_WO_Arguments()
    {
        // arrange
        var expected = string.Empty;

        // act
        var actual = Execute();

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }
}
