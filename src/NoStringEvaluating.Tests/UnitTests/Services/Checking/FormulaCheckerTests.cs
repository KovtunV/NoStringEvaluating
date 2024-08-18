using FluentAssertions;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Tests.UnitTests.Data;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Checking;

internal class FormulaCheckerTests
{
    private FormulaChecker _service;

    [SetUp]
    public void Setup()
    {
        _service = EvaluatorFacadeFactory.Create().FormulaChecker;
    }

    [TestCaseSource(typeof(CheckFormula), nameof(CheckFormula.Get))]
    public void Should_Check_Syntax(Models.FormulaModel model)
    {
        // arrange, act
        var actual = _service.CheckSyntax(model.Formula);

        // assert
        actual.Ok.Should().Be(model.ExpectedOkResult, $"Formula: \"{model.Formula}\"");
    }
}
