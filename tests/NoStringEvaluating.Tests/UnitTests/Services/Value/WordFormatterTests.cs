using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Services.Value;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Value;

internal class WordFormatterTests
{
    private IFixture _fixture;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Should_Format_Word(bool useWordQuotationMark)
    {
        // arrange
        GlobalOptions.UseWordQuotationMark = useWordQuotationMark;
        GlobalOptions.WordQuotationMark = _fixture.Create<string>();

        var word = _fixture.Create<string>();
        var mark = GlobalOptions.WordQuotationMark;
        var expectedWord = useWordQuotationMark ? $"{mark}{word}{mark}" : word;

        // act
        var actualWord = WordFormatter.Format(word);

        // assert
        actualWord.Should().Be(expectedWord);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Should_Format_Words(bool useWordQuotationMark)
    {
        // arrange
        GlobalOptions.UseWordQuotationMark = useWordQuotationMark;
        GlobalOptions.WordQuotationMark = _fixture.Create<string>();

        var words = _fixture.CreateMany<string>().ToList();
        var mark = GlobalOptions.WordQuotationMark;
        var expectedWords = words.Select(x => useWordQuotationMark ? $"{mark}{x}{mark}" : x).ToList();

        // act
        var actualWord = WordFormatter.Format(words);

        // assert
        actualWord.Should().BeEquivalentTo(expectedWords);
    }

    [Test]
    public void Should_Not_Fail_On_Null_Words()
    {
        // arrange, act
        var act = () => WordFormatter.Format(list: null);

        // assert
        act.Should().NotThrow();
        act().Should().BeNull();
    }
}
