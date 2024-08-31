using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.UnitTests.Helpers;

internal static class FormulaModelFactory
{
    public static Models.FormulaModel CreateTestModelToCheck(string formula, bool expectedOkresult)
    {
        var model = new Models.FormulaModel(formula, "NULL", double.NaN, expectedOkresult);
        return model;
    }

    public static Models.FormulaModel CreateTestModel(string formula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
    {
        return CreateTestModel(formula, "NULL", result, arguments);
    }

    public static Models.FormulaModel CreateTestModel(string formula, string parsedFormula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
    {
        var model = new Models.FormulaModel(formula, parsedFormula, result);
        foreach (var argument in arguments)
        {
            model.Arguments[argument.Item1] = argument.Item2;
        }

        return model;
    }
}
