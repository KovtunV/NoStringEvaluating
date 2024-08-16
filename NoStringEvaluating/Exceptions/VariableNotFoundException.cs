namespace NoStringEvaluating.Exceptions;

/// <summary>
/// Raises when can't find a variable
/// </summary>
public class VariableNotFoundException(string variableName, string message) : Exception(message)
{
    /// <summary>
    /// VariableName
    /// </summary>
    public string VariableName { get; set; } = variableName;
}
