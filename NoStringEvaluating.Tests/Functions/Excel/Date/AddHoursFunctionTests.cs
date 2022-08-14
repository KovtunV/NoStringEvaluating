using System;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class AddHoursFunctionTests : FunctionTests<AddHoursFunction>
{
    [Test]
    public void Should_Add_Hours()
    {
        // arrange
        var dateTime = DateTime.Now;
        var hours = 3;
        var expected = dateTime.AddHours(hours);

        // act
        var actual = Execute(dateTime, hours);

        // assert
        actual.GetDateTime().Should().Be(expected);
    }
}
