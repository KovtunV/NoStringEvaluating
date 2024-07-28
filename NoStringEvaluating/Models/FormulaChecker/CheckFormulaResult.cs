namespace NoStringEvaluating.Models.FormulaChecker;

/// <summary>
/// Syntax checking result
/// </summary>
public class CheckFormulaResult
{
    /// <summary>
    /// Mistakes
    /// </summary>
    public List<FormulaCheckerModel> Mistakes { get; }

    /// <summary>
    /// Is checking OK
    /// </summary>
    public bool Ok
    {
        get => Mistakes.Count is 0;
    }

    /// <summary>
    /// Syntax checking result
    /// </summary>
    public CheckFormulaResult(List<FormulaCheckerModel> mistakes)
    {
        Mistakes = mistakes;
    }

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
