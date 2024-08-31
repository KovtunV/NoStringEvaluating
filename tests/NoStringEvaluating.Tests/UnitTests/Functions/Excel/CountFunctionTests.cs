using FluentAssertions;
using NoStringEvaluating.Functions.Excel;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel;

internal class CountFunctionTests : FunctionTests<CountFunction>
{
    [Test]
    public void Should_Count()
    {
        // arrange
        var wordList = new[] { "one", "two" }.ToList();
        var numberList = new[] { 1d, 2, 3 }.ToList();
        var boolValue = true;
        var dateTimeValue = DateTime.Now;
        var numberValue = 45d;
        var word = "j";
        var expected = 9;

        // act
        var actual = Execute(wordList, numberList, boolValue, dateTimeValue, numberValue, word);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Zero_WO_Arguments()
    {
        // arrange
        var expected = 0;

        // act
        var actual = Execute();

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }
}
