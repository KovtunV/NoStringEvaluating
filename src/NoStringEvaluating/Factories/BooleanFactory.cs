using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Value;

namespace NoStringEvaluating.Factories;

/// <summary>
/// BooleanFactory
/// </summary>
public readonly struct BooleanFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    /// <summary>
    /// BooleanFactory
    /// </summary>
    internal BooleanFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

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
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.Boolean = boolean;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.Boolean);
    }
}