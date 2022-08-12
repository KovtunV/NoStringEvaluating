using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NUnit.Framework;

namespace NoStringEvaluating.Extensions.Microsoft.DependencyInjection.Tests;

internal class NoStringEvaluatorExtensionsTests
{
    private IFixture _fixture;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
    }

    [Test]
    public void Should_Create_Evaluator()
    {
        // arrange
        var serviceProvider = new ServiceCollection()
            .AddNoStringEvaluator()
            .BuildServiceProvider();

        // act
        var actual = serviceProvider.GetRequiredService<INoStringEvaluator>();

        // assert
        actual.Should().NotBeNull();
    }

    [Test]
    public void Should_Apply_Options()
    {
        // arrange
        var mark = _fixture.Create<string>();

        var serviceProvider = new ServiceCollection()
            .AddNoStringEvaluator(opt => opt.SetWordQuotationMark(mark))
            .BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<INoStringEvaluator>();

        // act
        var actual = service.CalcWord("'test'");

        // assert
        actual.Should().Be($"{mark}test{mark}");
    }
}
