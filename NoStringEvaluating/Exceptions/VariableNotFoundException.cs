namespace NoStringEvaluating.Exceptions;

/// <summary>
/// Raises when can't find a variable
/// </summary>
public class VariableNotFoundException : Exception
{
    /// <summary>
    /// VariableName
    /// </summary>
    public string VariableName { get; set; }

    /// <summary>
    /// Raises when can't find a variable
    /// </summary>
    public VariableNotFoundException(string variableName, string message)
        : base(message)
    {
        VariableName = variableName;
    }
}
