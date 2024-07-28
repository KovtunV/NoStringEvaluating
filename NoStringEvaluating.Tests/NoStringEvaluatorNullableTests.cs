using AutoFixture;
using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests;

internal class NoStringEvaluatorNullableTests
{
    private IFixture _fixture;

    private NoStringEvaluatorNullable _service;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();

        _service = NoStringEvaluatorNullable
            .CreateFacade(opt => opt.SetThrowIfVariableNotFound(false))
            .Evaluator;
    }

    [Test]
    public void Should_Return_Null_Number()
    {
        // arrange, act
        var res = _service.CalcNumber("a + b");

        // assert
        res.Should().NotHaveValue();
    }

    [Test]
    public void Should_Return_Number()
    {
        // arrange
        var args = new Dictionary<string, EvaluatorValue>
        {
            { "a", _fixture.Create<double>() },
            { "b", _fixture.Create<double>() },
        };
        var expected = args.Sum(x => x.Value.Number);

        // act
        var res = _service.CalcNumber("a + b", args);

        // assert
        res.Should().HaveValue();
        res.Value.Should().BeApproximatelyNumber(expected);
    }

    [Test]
    public void Should_Return_Null_DateTime()
    {
        // arrange, act
        var res = _service.CalcDateTime("Today() + null");

        // assert
        res.Should().NotHaveValue();
    }

    [Test]
    public void Should_Return_DateTime()
    {
        // arrange
        var expected = DateTime.Today;

        // act
        var res = _service.CalcDateTime("Today()");

        // assert
        res.Should().HaveValue();
        res.Value.Should().Be(expected);
    }

    [Test]
    public void Should_Return_Null_Boolean()
    {
        // arrange, act
        var res = _service.CalcBoolean("true + null");

        // assert
        res.Should().NotHaveValue();
    }

    [Test]
    public void Should_Return_Boolean()
    {
        // arrange
        var expected = true;

        // act
        var res = _service.CalcBoolean("true");

        // assert
        res.Should().HaveValue();
        res.Value.Should().Be(expected);
    }
}
