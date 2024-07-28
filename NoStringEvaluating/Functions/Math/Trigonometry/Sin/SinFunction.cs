﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math.Trigonometry.Sin;

/// <summary>
/// Function - sin
/// </summary>
public sealed class SinFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "SIN";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        return System.Math.Sin(args[0].Number);
    }
}
