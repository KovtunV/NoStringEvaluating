namespace NoStringEvaluating.Models.FormulaChecker;

/// <summary>
/// Syntax checking result
/// </summary>
public class CheckFormulaResult(List<FormulaCheckerModel> mistakes)
{
    /// <summary>
    /// Mistakes
    /// </summary>
    public List<FormulaCheckerModel> Mistakes { get; } = mistakes;

    /// <summary>
    /// Is checking OK
    /// </summary>
    public bool Ok => Mistakes.Count is 0;

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Ok.ToString();
    }

    /// <summary>
    /// Cast to bool
    /// </summary>
    public static implicit operator bool(CheckFormulaResult result)
    {
        return result.Ok;
    }
}
