using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// WordFactory
/// </summary>
public readonly struct WordFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    /// <summary>
    /// WordFactory
    /// </summary>
    internal WordFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// Empty word
    /// </summary>
    public InternalEvaluatorValue Empty => Create(string.Empty);

    /// <summary>
    /// Concat values
    /// </summary>
    public InternalEvaluatorValue Concat(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        var word = $"{a}{b}";
        return Create(word);
    }

    /// <summary>
    /// Creates word value
    /// </summary>
    public InternalEvaluatorValue Create(string word)
    {
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.Word = word;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.Word);
    }
}
