using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Value;

namespace NoStringEvaluating.Factories;

/// <summary>
/// WordListFactory
/// </summary>
public readonly struct WordListFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    /// <summary>
    /// WordListFactory
    /// </summary>
    internal WordListFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// Creates WordList value
    /// </summary>
    public InternalEvaluatorValue Create(List<string> wordList)
    {
        var valueKeeper = _valueKeeperContainer.GetValueKeeper();
        valueKeeper.WordList = wordList;

        return new InternalEvaluatorValue(valueKeeper.Ptr, ValueTypeKey.WordList);
    }
}
