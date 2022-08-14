using System;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class AddMinutesFunctionTests : FunctionTests<AddMinutesFunction>
{
    [Test]
    public void Should_Add_Minutes()
    {
        // arrange
        var dateTime = DateTime.Now;
        var minutes = 3;
        var expected = dateTime.AddMinutes(minutes);

        // act
        var actual = Execute(dateTime, minutes);

        // assert
        actual.GetDateTime().Should().Be(expected);
    }
}
