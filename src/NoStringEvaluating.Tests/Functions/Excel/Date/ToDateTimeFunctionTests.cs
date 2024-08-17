using System.Globalization;
using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Functions.Excel.Date;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Excel.Date;

internal class ToDateTimeFunctionTests : FunctionTests<ToDateTimeFunction>
{
    [TestCase("01/01/2000 14:18:23")]
    [TestCase("12/08/2022 13:18:23")]
    public void Should_Parse_Invariant_String(string invariantDateStr)
    {
        // arrange
        var expected = DateTime.Parse(invariantDateStr, CultureInfo.InvariantCulture);

        // act
        var actual = Execute(invariantDateStr);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.DateTime);
        actual.DateTime.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Empty_Date_If_Cant_Parse()
    {
        // arrange
        var mess = _fixture.Create<string>();

        // act
        var actual = Execute(mess);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.DateTime);
        actual.DateTime.Should().Be(DateTime.MinValue);
    }
}
