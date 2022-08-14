using System;
using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class DateDifFunctionTests : FunctionTests<DateDifFunction>
{
    [TestCase("12.02.2002", "18.07.2005", "D", 1252)]
    [TestCase("12.02.2002", "18.07.2005", "M", 41)]
    [TestCase("12.02.2002", "11.07.2005", "M", 40)]
    [TestCase("12.02.2002", "18.07.2005", "Y", 3)]
    public void Should_Return_Number_Of_Scope_Between_Dates(string dateStartStr, string dateEndStr, string scope, int expected)
    {
        // arrange
        var dateStart = DateTime.Parse(dateStartStr, _culture);
        var dateEnd = DateTime.Parse(dateEndStr, _culture);

        // act
        var actual = Execute(dateStart, dateEnd, scope);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_If_StartDate_More_Than_EndDate()
    {
        // arrange
        var scope = _fixture.Create<string>();
        var dateStart = DateTime.Parse("05.01.2001", _culture);
        var dateEnd = DateTime.Parse("04.01.2001", _culture);

        // act
        var actual = Execute(dateStart, dateEnd, scope);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }

    [Test]
    public void Should_Return_Null_On_Unknown_Scope()
    {
        // arrange
        var scope = _fixture.Create<string>();
        var dateStart = DateTime.Parse("05.01.2001", _culture);
        var dateEnd = DateTime.Parse("06.01.2001", _culture);

        // act
        var actual = Execute(dateStart, dateEnd, scope);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
