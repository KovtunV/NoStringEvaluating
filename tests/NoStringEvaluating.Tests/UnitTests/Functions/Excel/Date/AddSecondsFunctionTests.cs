using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Excel.Date;

internal class AddSecondsFunctionTests : FunctionTests<AddSecondsFunction>
{
    [Test]
    public void Should_Add_Seconds()
    {
        // arrange
        var dateTime = DateTime.Now;
        var seconds = 3;
        var expected = dateTime.AddSeconds(seconds);

        // act
        var actual = Execute(dateTime, seconds);

        // assert
        actual.DateTime.Should().Be(expected);
    }
}
