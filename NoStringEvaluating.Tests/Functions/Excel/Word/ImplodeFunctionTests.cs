using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class ImplodeFunctionTests : FunctionTests<ImplodeFunction>
{
    [Test]
    public void Should_Return_Joined_List_By_Empty()
    {
        // arrange
        var expected = "onetwothree";
        var wordLIst = new[] { "one", "two", "three" }.ToList();

        // act
        var actual = Execute(wordLIst);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }

    [Test]
    public void Should_Return_Joined_By_Separator()
    {
        // arrange
        var separator = " apple ";
        var wordList = new[] { "one", "two" }.ToList();
        var numberList = new[] { 3d, 4 }.ToList();
        var boolValue = true;

        var expected = "one apple two apple 3 apple 4 apple True";

        // act
        var actual = Execute(wordList, numberList, boolValue, separator);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }
}
