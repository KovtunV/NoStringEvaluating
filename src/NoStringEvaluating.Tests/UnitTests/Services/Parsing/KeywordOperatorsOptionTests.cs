using FluentAssertions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.UnitTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Services.Parsing;

// The keyword operators and, or, not are an option, off by default: off, the three words are plain variable names
internal class KeywordOperatorsOptionTests
{
    [Test]
    public void Should_Be_Off_By_Default()
    {
        // arrange, act
        GlobalOptions.Reset();
        var options = new NoStringEvaluatorOptions();

        // assert
        GlobalOptions.UseKeywordOperators.Should().BeFalse();
        options.UseKeywordOperators.Should().BeFalse();
    }

    [Test]
    public void Should_Treat_The_Words_As_Names_When_Off()
    {
        // arrange
        var facade = EvaluatorFacadeFactory.Create(opt => opt.WithKeywordOperators(false).SetThrowIfVariableNotFound(false));
        var variables = new Dictionary<string, EvaluatorValue> { ["and"] = true, ["or"] = 2, ["not"] = false, ["android"] = 3 };

        // act, assert
        facade.Evaluator.CalcBoolean("and", variables).Should().BeTrue();
        facade.Evaluator.CalcNumber("or + android", variables).Should().Be(5);
        facade.Evaluator.CalcBoolean("not", variables).Should().BeFalse();
        facade.Evaluator.CalcBoolean("!not", variables).Should().BeTrue();
        facade.Evaluator.CalcBoolean("and(true; false)", variables).Should().BeFalse();
        facade.Evaluator.CalcBoolean("or(true; false) && not(false)", variables).Should().BeTrue();
        facade.FormulaChecker.CheckSyntax("1 > 0 and 2 > 1").Ok.Should().BeFalse();
        facade.FormulaChecker.CheckSyntax("or").Ok.Should().BeTrue();
    }

    [Test]
    public void Should_Parse_The_Keywords_When_On()
    {
        // arrange
        var facade = EvaluatorFacadeFactory.Create(opt => opt.WithKeywordOperators().SetThrowIfVariableNotFound(false));
        var variables = new Dictionary<string, EvaluatorValue> { ["a"] = true, ["b"] = false };

        // act, assert
        facade.Evaluator.CalcBoolean("1 > 0 and 2 > 1").Should().BeTrue();
        facade.Evaluator.CalcBoolean("a or b", variables).Should().BeTrue();
        facade.Evaluator.CalcBoolean("not b and a", variables).Should().BeTrue();
        facade.FormulaChecker.CheckSyntax("or").Ok.Should().BeFalse();
    }

    [Test]
    public void Should_Follow_The_Option_At_Parse_Time()
    {
        // arrange: parsed with the keywords on, evaluated after they were switched off - the nodes keep their meaning
        var facade = EvaluatorFacadeFactory.Create(opt => opt.WithKeywordOperators().SetThrowIfVariableNotFound(false));
        var nodes = facade.FormulaParser.Parse("1 > 0 and 2 > 3");
        new NoStringEvaluatorOptions { UseKeywordOperators = false }.UpdateGlobalOptions();

        // act, assert
        facade.Evaluator.CalcBoolean(nodes).Should().BeFalse();
        facade.FormulaParser.Parse("1 > 0 and 2 > 3").ToString().Should().Be("1 0 [and] 2 > 3 >", "off, and is a plain name between the two comparisons");
    }
}
