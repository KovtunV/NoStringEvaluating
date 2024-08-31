using FluentAssertions;
using NoStringEvaluating.Functions.Logic;
using NoStringEvaluating.Models.Values;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Functions.Logic;

internal class IffFunctionTests : FunctionTests<IffFunction>
{
    [TestCase(true, "my word")]
    public void Should_Iff_First(bool ifValue, string expected)
    {
        // arrange, act
        var actual = Execute(ifValue, expected);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }

    [TestCase(false, "my word")]
    public void Should_Not_Iff_First(bool ifValue, string expected)
    {
        // arrange, act
        var actual = Execute(ifValue, expected);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Null);
    }

    [TestCase(false, "my word one", true, "my word two")]
    public void Should_Iff_Second(bool ifFirst, string resFirst, bool ifSecond, string expected)
    {
        // arrange, act
        var actual = Execute(ifFirst, resFirst, ifSecond, expected);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Word);
        actual.Word.Should().Be(expected);
    }
}
