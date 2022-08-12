using FluentAssertions;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluatingTests.Data;
using NoStringEvaluatingTests.Helpers;
using NoStringEvaluatingTests.Models;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Services.Checking;

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
        actual.Ok.Should().Be(model.ExpectedOkResult);
    }
}
