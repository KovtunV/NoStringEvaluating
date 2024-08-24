namespace NoStringEvaluating.Exceptions;

/// <summary>
/// Raises when function exception
/// </summary>
public class NoStringFunctionException(string funcName) : Exception($"Function \"{funcName}\" has already added")
{
}
