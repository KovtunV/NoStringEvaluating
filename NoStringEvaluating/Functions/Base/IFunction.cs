using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Base;

/// <summary>
/// Function
/// </summary>
public interface IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory);
}
