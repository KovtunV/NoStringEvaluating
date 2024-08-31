using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Date;

internal class WeekDayFunctionTests : FunctionTests<WeekDayFunction>
{
    [TestCase("18.04.2021", 1)]
    [TestCase("19.04.2021", 2)]
    [TestCase("20.04.2021", 3)]
    [TestCase("21.04.2021", 4)]
    [TestCase("22.04.2021", 5)]
    [TestCase("23.04.2021", 6)]
    [TestCase("24.04.2021", 7)]
    [TestCase("25.04.2021", 1)]
    public void Should_Return_Day_Of_Week(string dateStr, int expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date);

        // assert
        actual.TypeKey.Should().Be(NoStringEvaluating.Models.Values.ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }
}
