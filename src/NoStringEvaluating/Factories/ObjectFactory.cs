using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// ObjectFactory
/// </summary>
public readonly struct ObjectFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    /// <summary>
    /// ObjectFactory
    /// </summary>
    internal ObjectFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// Creates object value
    /// </summary>
    public InternalEvaluatorValue Create(object objectValue)
    {
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.Object = objectValue;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.Object);
    }
}
