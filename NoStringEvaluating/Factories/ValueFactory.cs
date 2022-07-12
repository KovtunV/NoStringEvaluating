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
    /// Returns word factory
    /// </summary>
    public WordFactory Word()
    {
        return new WordFactory(_idContainer.Ids);
    }

    /// <summary>
    /// Returns dateTime factory
    /// </summary>
    public DateTimeFactory DateTime()
    {
        return new DateTimeFactory(_idContainer.Ids);
    }

    /// <summary>
    /// Returns wordList factory
    /// </summary>
    public WordListFactory WordList()
    {
        return new WordListFactory(_idContainer.Ids);
    }

    /// <summary>
    /// Returns numberList factory
    /// </summary>
    public NumberListFactory NumberList()
    {
        return new NumberListFactory(_idContainer.Ids);
    }

    internal InternalEvaluatorValue Create(EvaluatorValue val)
    {
        if (val.TypeKey == ValueTypeKey.Number)
        {
            return val.Number;
        }

        if (val.TypeKey == ValueTypeKey.Word)
        {
            return Word().Create(val.Word);
        }

        if (val.TypeKey == ValueTypeKey.DateTime)
        {
            return DateTime().Create(val.DateTime);
        }

        if (val.TypeKey == ValueTypeKey.WordList)
        {
            return WordList().Create(val.WordList);
        }

        if (val.TypeKey == ValueTypeKey.NumberList)
        {
            return NumberList().Create(val.NumberList);
        }

        if (val.TypeKey == ValueTypeKey.Boolean)
        {
            // Bool represent as number in any cases
            return val.Boolean ? 1 : 0;
        }

        throw new InvalidCastException($"Can't cast {nameof(EvaluatorValue)} with the typeKey = \"{val.TypeKey}\" to {nameof(InternalEvaluatorValue)}");
    }
}

