using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// DateTimeFactory
/// </summary>
public readonly struct DateTimeFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    internal DateTimeFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// Default
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
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.DateTime = dateTime;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.DateTime);
    }
}
