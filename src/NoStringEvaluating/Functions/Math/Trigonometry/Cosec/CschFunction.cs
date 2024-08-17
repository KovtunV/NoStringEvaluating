﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Cosec;

/// <summary>
/// Function - csch
/// </summary>
public sealed class CschFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "CSCH";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return 1 / System.Math.Sinh(args[0].Number);
    }
}
