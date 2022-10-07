using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Word;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Word;

internal class ExplodeFunctionTests : FunctionTests<ExplodeFunction>
{
    [Test]
    public void Should_Return_Splitted_List_By_Space()
    {
        // arrange
        var word = "one two three";
        var expected = new[] { "one", "two", "three" }.ToList();

        // act
        var actual = Execute(word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Should_Return_Splitted_List_By_Separator()
    {
        // arrange
        var separator = "apple";
        var word = "I took one apple and ate that apple, that's why I was over the moon";
        var expected = new[]
        {
            "I took one ",
            " and ate that ",
            ", that's why I was over the moon"
        }.ToList();

        // act
        var actual = Execute(word, separator);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.WordList);
        actual.WordList.Should().BeEquivalentTo(expected);
    }
}
