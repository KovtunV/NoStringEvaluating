using System;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class DayFunctionTests : FunctionTests<DayFunction>
{
    [TestCase("05.01.2001", "5")]
    [TestCase("16.01.2001", "16")]
    public void Should_Return_Day_From_Date(string dateStr, string expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }

    [TestCase("05.01.2001", "D", "5")]
    [TestCase("05.01.2001", "DD", "05")]
    [TestCase("16.01.2001", "DD", "16")]
    [TestCase("16.01.2001", "DDD", "016")]
    public void Should_Return_Formatted_Day_From_date(string dateStr, string format, string expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date, format);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.GetWord().Should().Be(expected);
    }
}
