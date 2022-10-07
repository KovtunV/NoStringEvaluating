using System.Collections.Generic;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// NumberListFactory
/// </summary>
public readonly struct NumberListFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    /// <summary>
    /// NumberListFactory
    /// </summary>
    internal NumberListFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// Creates NumberList value
    /// </summary>
    public InternalEvaluatorValue Create(List<double> numberList)
    {
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.NumberList = numberList;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.NumberList);
    }
}
