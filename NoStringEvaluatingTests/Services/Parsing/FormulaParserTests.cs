using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NoStringEvaluatingTests.Data;
using NoStringEvaluatingTests.Helpers;
using NoStringEvaluatingTests.Models;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Services.Parsing;

internal class FormulaParserTests
{
    private IServiceProvider _serviceProvider;

    private IFormulaParser _service;

    [SetUp]
    public void Setup()
    {
        _serviceProvider = ServiceProviderFactory.Create();

        _service = _serviceProvider.GetRequiredService<IFormulaParser>();
    }

    [TestCaseSource(typeof(ParseFormula), nameof(ParseFormula.Get))]
    public void Should_Parse_Formula(FormulaModel model)
    {
        // arrange, act
        var actual = _service.Parse(model.Formula);

        // assert
        actual.ToString().Should().Be(model.ParsedFormula);
    }
}
