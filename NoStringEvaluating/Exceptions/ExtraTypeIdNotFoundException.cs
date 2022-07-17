using System;

namespace NoStringEvaluating.Exceptions;

/// <summary>
/// Raises when can't find an id in keeper
/// </summary>
public class ExtraTypeIdNotFoundException : Exception
{
    /// <summary>
    /// Raises when can't find an id in keeper
    /// </summary>
    public ExtraTypeIdNotFoundException(int id, string keeperName) : base($"id {id} not found in keeper \"{keeperName}\"")
    {

    }
}
