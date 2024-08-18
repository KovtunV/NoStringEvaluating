using FluentAssertions;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Tests.UnitTests.Data;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Parsing;

internal class FormulaParserTests
{
    private FormulaParser _service;

    [SetUp]
    public void Setup()
    {
        _service = EvaluatorFacadeFactory.Create().FormulaParser;
    }

    [TestCaseSource(typeof(ParseFormula), nameof(ParseFormula.Get))]
    public void Should_Parse_Formula(Models.FormulaModel model)
    {
        // arrange, act
        var actual = _service.Parse(model.Formula);

        // assert
        actual.ToString().Should().Be(model.ParsedFormula);
    }
}
