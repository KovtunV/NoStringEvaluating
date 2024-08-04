using FluentAssertions;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Tests.Data;
using NoStringEvaluating.Tests.Helpers;
using NoStringEvaluating.Tests.Models;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Services.Checking;

internal class FormulaCheckerTests
{
    private FormulaChecker _service;

    [SetUp]
    public void Setup()
    {
        _service = EvaluatorFacadeFactory.Create().FormulaChecker;
    }

    [TestCaseSource(typeof(CheckFormula), nameof(CheckFormula.Get))]
    public void Should_Check_Syntax(FormulaModel model)
    {
        // arrange, act
        var actual = _service.CheckSyntax(model.Formula);

        // assert
        actual.Ok.Should().Be(model.ExpectedOkResult, $"Formula: \"{model.Formula}\"");
    }
}
