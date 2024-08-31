using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Date;

internal class YearFunctionTests : FunctionTests<YearFunction>
{
    [TestCase("05.01.2001", "2001")]
    [TestCase("16.12.2020", "2020")]
    public void Should_Return_Year_From_Date(string dateStr, string expected)
    {
        // arrange
        var date = DateTime.Parse(dateStr, _culture);

        // act
        var actual = Execute(date);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase("16.01.2022", "Y", "2022")]
    [TestCase("05.01.2001", "YY", "01")]
    public void Should_Return_Formatted_Year_From_date(string dateStr, string format, string expected)
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
