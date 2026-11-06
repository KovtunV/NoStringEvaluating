using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// DateTimeFactory
/// </summary>
public readonly struct DateTimeFactory
{
    /// <summary>
    /// Empty DateTime value
    /// </summary>
    public InternalEvaluatorValue Empty => Create(DateTime.MinValue);

    /// <summary>
    /// Creates DateTime value
    /// </summary>
    public InternalEvaluatorValue Create(DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            return Create(dateTime.Value);
        }

        return default;
    }

    /// <summary>
    /// Creates DateTime value
    /// </summary>
    public InternalEvaluatorValue Create(DateTime dateTime)
    {
        return new InternalEvaluatorValue(dateTime);
    }
}
