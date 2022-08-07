using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel;

internal class SortFunctionTests : FunctionTests<SortFunction>
{
    [TestCase(1)]
    [TestCase(0)]
    public void Should_Sort_WordList(int isAscAsNum)
    {
        // arrange
        var isAsc = isAscAsNum == 1;
        var wordList = new[] { "b", "c", "a" }.ToList();
        var expected = isAsc
            ? wordList.OrderBy(x => x).ToList()
            : wordList.OrderByDescending(x => x).ToList();

        // act
        var actual = Execute(wordList, isAscAsNum);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.GetWordList().Should().BeEquivalentTo(expected);
    }

    [TestCase(1)]
    [TestCase(0)]
    public void Should_Sort_NumberList(int isAscAsNum)
    {
        // arrange
        var isAsc = isAscAsNum == 1;
        var wordList = new[] { 2d, 3, 1 }.ToList();
        var expected = isAsc
            ? wordList.OrderBy(x => x).ToList()
            : wordList.OrderByDescending(x => x).ToList();

        // act
        var actual = Execute(wordList, isAscAsNum);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.NumberList);
        actual.GetNumberList().Should().BeEquivalentTo(expected);
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
