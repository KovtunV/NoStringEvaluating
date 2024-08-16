using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.Models;

public class FormulaModel(string formula, string parsedFormula, EvaluatorValue result, bool expectedOkResult = true)
{
    public string Formula { get; } = formula;

    public string ParsedFormula { get; } = parsedFormula;

    public EvaluatorValue Result { get; } = result;

    public bool ExpectedOkResult { get; } = expectedOkResult;

    public Dictionary<string, EvaluatorValue> Arguments { get; } = [];

    public override string ToString()
    {
        return Formula;
    }
}
