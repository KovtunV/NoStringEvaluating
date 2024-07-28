using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class MonthFunctionTests : FunctionTests<MonthFunction>
{
    [TestCase("05.01.2001", "1")]
    [TestCase("16.12.2001", "12")]
    public void Should_Return_Month_From_Date(string dateStr, string expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("16.01.2001", "M", "1")]
    [TestCase("05.01.2001", "MM", "01")]
    [TestCase("05.12.2001", "MM", "12")]
    [TestCase("16.04.2001", "MMM", "004")]
    public void Should_Return_Formatted_Month_From_date(string dateStr, string format, string expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date, format);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }
}
