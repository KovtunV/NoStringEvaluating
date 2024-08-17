﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - if
/// </summary>
public sealed class IfFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "IF";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; } = true;

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        if (!args[0].IsNull && args[0].Boolean)
        {
            return args[1];
        }

        return args[2];
    }
}
