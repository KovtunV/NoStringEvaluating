using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// BooleanFactory
/// </summary>
public readonly struct BooleanFactory
{
    /// <summary>
    /// Creates boolean value
    /// </summary>
    public InternalEvaluatorValue Create(bool? boolean)
    {
        if (boolean.HasValue)
        {
            return Create(boolean.Value);
        }

        return default;
    }

    /// <summary>
    /// Creates boolean value
    /// </summary>
    public InternalEvaluatorValue Create(bool boolean)
    {
        return new InternalEvaluatorValue(boolean);
    }
}
