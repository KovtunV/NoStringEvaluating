using System;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Factories;

/// <summary>
/// Factory fro values
/// </summary>
public readonly struct ValueFactory
{
    private readonly ExtraTypeIdContainer _idContainer;

    internal ValueFactory(ExtraTypeIdContainer idContainer)
    {
        _idContainer = idContainer;
    }

    /// <summary>
    /// WordFactory
    /// </summary>
    public WordFactory Word => new(_idContainer.Ids);

    /// <summary>
    /// DateTimeFactory
    /// </summary>
    public DateTimeFactory DateTime => new(_idContainer.Ids);

    /// <summary>
    /// WordListFactory
    /// </summary>
    public WordListFactory WordList => new(_idContainer.Ids);

    /// <summary>
    /// NumberListFactory
    /// </summary>
    public NumberListFactory NumberList => new(_idContainer.Ids);

    /// <summary>
    /// ObjectFactory
    /// </summary>
    public ObjectFactory Object => new(_idContainer.Ids);

    internal InternalEvaluatorValue Create(EvaluatorValue val)
    {
        if (val.TypeKey == ValueTypeKey.Number)
        {
            return val.Number;
        }

        if (val.TypeKey == ValueTypeKey.Boolean)
        {
            return val.Boolean;
        }

        if (val.TypeKey == ValueTypeKey.DateTime)
        {
            return DateTime.Create(val.DateTime);
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
