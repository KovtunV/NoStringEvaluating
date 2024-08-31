using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel;

internal class IsMemberFunctionTests : FunctionTests<IsMemberFunction>
{
    [Test]
    public void Should_Find_Word()
    {
        // arrange
        var wordList = new[] { "one", "two" }.ToList();
        var word = "two";
        var expected = true;

        // act
        var actual = Execute(wordList, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }

    [Test]
    public void Should_Not_Find_Word()
    {
        // arrange
        var wordList = new[] { "one", "two" }.ToList();
        var word = "twor";
        var expected = false;

        // act
        var actual = Execute(wordList, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }

    [Test]
    public void Should_Find_Number()
    {
        // arrange
        var numberList = new[] { 3d, 4, 5 }.ToList();
        var number = 4;
        var expected = true;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }

    [Test]
    public void Should_Not_Find_Number()
    {
        // arrange
        var numberList = new[] { 3d, 4, 5 }.ToList();
        var number = 44;
        var expected = false;

        // arrange, act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Boolean);
        actual.Boolean.Should().Be(expected);
    }
}
