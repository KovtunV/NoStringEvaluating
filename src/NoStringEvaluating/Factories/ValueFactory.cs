using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Value;

namespace NoStringEvaluating.Factories;

/// <summary>
/// Factory fro values
/// </summary>
public readonly struct ValueFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer;

    internal ValueFactory(ValueKeeperContainer valueKeeperContainer)
    {
        _valueKeeperContainer = valueKeeperContainer;
    }

    /// <summary>
    /// DateTimeFactory
    /// </summary>
    public DateTimeFactory DateTime => new(_valueKeeperContainer);

    /// <summary>
    /// BooleanFactory
    /// </summary>
    public BooleanFactory Boolean => new(_valueKeeperContainer);

    /// <summary>
    /// WordFactory
    /// </summary>
    public WordFactory Word => new(_valueKeeperContainer);

    /// <summary>
    /// WordListFactory
    /// </summary>
    public WordListFactory WordList => new(_valueKeeperContainer);

    /// <summary>
    /// NumberListFactory
    /// </summary>
    public NumberListFactory NumberList => new(_valueKeeperContainer);

    /// <summary>
    /// ObjectFactory
    /// </summary>
    public ObjectFactory Object => new(_valueKeeperContainer);

    internal InternalEvaluatorValue Create(EvaluatorValue val)
    {
        if (val.TypeKey == ValueTypeKey.Number)
        {
            return val.Number;
        }

        if (val.TypeKey == ValueTypeKey.DateTime)
        {
            return DateTime.Create(val.DateTime);
        }

        if (val.TypeKey == ValueTypeKey.Boolean)
        {
            return Boolean.Create(val.Boolean);
        }

        if (val.TypeKey == ValueTypeKey.Word)
        {
            return Word.Create(val.Word);
        }

        if (val.TypeKey == ValueTypeKey.WordList)
        {
            return WordList.Create(val.WordList);
        }

        if (val.TypeKey == ValueTypeKey.NumberList)
        {
            return NumberList.Create(val.NumberList);
        }

        if (val.TypeKey == ValueTypeKey.Object)
        {
            return Object.Create(val.Object);
        }

        if (val.TypeKey == ValueTypeKey.Null)
        {
            return default;
        }

        throw new InvalidCastException($"Can't cast {nameof(EvaluatorValue)} with the typeKey = \"{val.TypeKey}\" to {nameof(InternalEvaluatorValue)}");
    }
}
