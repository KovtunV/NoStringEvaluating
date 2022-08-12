using FluentAssertions;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluatingTests.Data;
using NoStringEvaluatingTests.Helpers;
using NoStringEvaluatingTests.Models;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Services.Parsing;

internal class FormulaParserTests
{
    private FormulaParser _service;

    [SetUp]
    public void Setup()
    {
        _service = EvaluatorFacadeFactory.Create().FormulaParser;
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
