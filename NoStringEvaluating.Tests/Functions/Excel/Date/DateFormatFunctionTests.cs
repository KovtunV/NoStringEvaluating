using System;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class DateFormatFunctionTests : FunctionTests<DateFormatFunction>
{
    [TestCase("12.02.2002", "yy", "02")]
    [TestCase("12.02.2002 18:05:33", "HH:mm:ss", "18:05:33")]
    [TestCase("12.02.2002 18:05:33", "dd|MM|yyyy HH:mm:ss", "12|02|2002 18:05:33")]
    public void Should_Return_Date_String_Accoarding_To_Format(string dateStr, string format, string expected)
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
