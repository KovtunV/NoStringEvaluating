using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NoStringEvaluatingTests.Data;
using NoStringEvaluatingTests.Helpers;
using NoStringEvaluatingTests.Models;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Services.Checking;

internal class FormulaCheckerTests
{
    private IServiceProvider _serviceProvider;

    private IFormulaChecker _service;

    [SetUp]
    public void Setup()
    {
        _serviceProvider = ServiceProviderFactory.Create();

        _service = _serviceProvider.GetRequiredService<IFormulaChecker>();
    }

    [TestCaseSource(typeof(CheckFormula), nameof(CheckFormula.Get))]
    public void Should_Check_Syntax(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CheckSyntax(model.Formula);

        // assert
        actual.Ok.Should().Be(model.ExpectedOkResult);
    }
}
