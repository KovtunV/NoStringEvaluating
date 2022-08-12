using System;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Excel.Date;

internal class NowFunctionTests : FunctionTests<NowFunction>
{
    [Test]
    public void Should_Return_Now_Date()
    {
        // arrange
        var now = DateTime.Now;
        var precision = TimeSpan.FromMilliseconds(100);

        // act
        var actual = Execute();

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.DateTime);
        actual.GetDateTime().Should().BeCloseTo(now, precision);
    }
}
