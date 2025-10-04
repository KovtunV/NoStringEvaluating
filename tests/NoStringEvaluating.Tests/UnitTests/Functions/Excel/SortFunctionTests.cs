using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel;

internal class SortFunctionTests : FunctionTests<SortFunction>
{
    [TestCase(true)]
    [TestCase(false)]
    public void Should_Sort_WordList(bool isAsc)
    {
        // arrange
        var wordList = new[] { "b", "c", "a" }.ToList();
        var expected = isAsc
            ? wordList.OrderBy(x => x).ToList()
            : wordList.OrderByDescending(x => x).ToList();

        // act
        var actual = Execute(wordList, isAsc);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Should_Sort_NumberList(bool isAsc)
    {
        // arrange
        var wordList = new[] { 2d, 3, 1 }.ToList();
        var expected = isAsc
            ? wordList.OrderBy(x => x).ToList()
            : wordList.OrderByDescending(x => x).ToList();

        // act
        var actual = Execute(wordList, isAsc);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.NumberList);
        actual.NumberList.Should().BeEquivalentTo(expected);
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
