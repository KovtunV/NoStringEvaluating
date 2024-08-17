using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class TodayFunctionTests : FunctionTests<TodayFunction>
{
    [Test]
    public void Should_Return_Now_Date()
    {
        // arrange
        var today = DateTime.Today;

        // act
        var actual = Execute();

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.DateTime);
        actual.DateTime.Should().Be(today);
    }
}
