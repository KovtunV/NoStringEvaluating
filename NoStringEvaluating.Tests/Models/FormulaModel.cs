using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.Models;

public class FormulaModel
{
    public string Formula { get; }

    public string ParsedFormula { get; }

    public EvaluatorValue Result { get; }

    public bool ExpectedOkResult { get; }

    public Dictionary<string, EvaluatorValue> Arguments { get; }

    public FormulaModel(string formula, string parsedFormula, EvaluatorValue result, bool expectedOkResult = true)
    {
        Formula = formula;
        ParsedFormula = parsedFormula;
        Result = result;
        ExpectedOkResult = expectedOkResult;
        Arguments = new Dictionary<string, EvaluatorValue>();
    }

    public override string ToString()
    {
        return Formula;
    }
}
