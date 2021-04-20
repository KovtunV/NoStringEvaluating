using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        private static FormulaModel[] CreateTestModel(string formula, bool expectedOkresult)
        {
            var model = new FormulaModel(formula, "NULL", double.NaN, expectedOkresult);
            return new[] { model };
        }

        private static FormulaModel[] CreateTestModel(string formula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
        {
            return CreateTestModel(formula, "NULL", result, arguments);
        }

        private static FormulaModel[] CreateTestModel(string formula, string parsedFormula, EvaluatorValue result, params (string, EvaluatorValue)[] arguments)
        {
            var model = new FormulaModel(formula, parsedFormula, result);
            foreach (var argument in arguments)
            {
                model.Arguments[argument.Item1] = argument.Item2;
            }

            return new[] { model };
        }
    }
}
