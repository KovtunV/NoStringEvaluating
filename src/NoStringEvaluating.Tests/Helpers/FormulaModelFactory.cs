using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Models;

namespace NoStringEvaluating.Tests.Helpers;

internal static class FormulaModelFactory
{
    public static FormulaModel CreateTestModelToCheck(string formula, bool expectedOkresult)
    {
        var model = new FormulaModel(formula, "NULL", double.NaN, expectedOkresult);
        return model;
    }

    public static FormulaModel CreateTestModel(string formula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
    {
        return CreateTestModel(formula, "NULL", result, arguments);
    }

    public static FormulaModel CreateTestModel(string formula, string parsedFormula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
    {
        var model = new FormulaModel(formula, parsedFormula, result);
        foreach (var argument in arguments)
        {
            model.Arguments[argument.Item1] = argument.Item2;
        }

        return model;
    }
}
