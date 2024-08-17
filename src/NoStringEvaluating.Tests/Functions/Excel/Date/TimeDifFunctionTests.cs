using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class TimeDifFunctionTests : FunctionTests<TimeDifFunction>
{
    [TestCase("01.01.2000 14:18:23", "01.01.2000 18:30:10", "H", 4.196)]
    [TestCase("01.01.2000 14:18:23", "01.01.2000 18:30:10", "M", 251.783)]
    [TestCase("01.01.2000 14:18:23", "01.01.2000 18:30:10", "S", 15107)]
    public void Should_Return_Number_Of_Scope_Between_Dates(string dateStartStr, string dateEndStr, string scope, double expected)
    {
        // arrange
        var dateStart = DateTime.Parse(dateStartStr, _culture);
        var dateEnd = DateTime.Parse(dateEndStr, _culture);

        // act
        var actual = Execute(dateStart, dateEnd, scope);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }

    [Test]
    public void Should_Return_Null_If_StartDate_More_Than_EndDate()
    {
        // arrange
        var scope = _fixture.Create<string>();
        var dateStart = DateTime.Parse("05.01.2001 14:18:23", _culture);
        var dateEnd = DateTime.Parse("04.01.2001 14:18:23", _culture);

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
        var dateStart = DateTime.Parse("05.01.2001 14:18:23", _culture);
        var dateEnd = DateTime.Parse("06.01.2001 14:18:23", _culture);

        // act
        var actual = Execute(dateStart, dateEnd, scope);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }
}
