using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Services.Value;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Value
/// </summary>
public readonly struct EvaluatorValue : IEquatable<EvaluatorValue>
{
    private readonly object _referenceValueFacade = default;

    /// <summary>
    /// Type key
    /// </summary>
    public ValueTypeKey TypeKey { get; }

    /// <summary>
    /// Number
    /// </summary>
    public double Number { get; } = default;

    /// <summary>
    /// Boolean
    /// </summary>
    public bool Boolean { get; } = default;

    /// <summary>
    /// DateTime
    /// </summary>
    public DateTime DateTime { get; } = default;

    /// <summary>
    /// Word
    /// </summary>
    public string Word => _referenceValueFacade as string;

    /// <summary>
    /// WordList
    /// </summary>
    public List<string> WordList => _referenceValueFacade as List<string>;

    /// <summary>
    /// NumberList
    /// </summary>
    public List<double> NumberList => _referenceValueFacade as List<double>;

    /// <summary>
    /// Object
    /// </summary>
    public object Object => _referenceValueFacade;

    #region Ctors

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(double number)
    {
        TypeKey = ValueTypeKey.Number;
        Number = number;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(bool boolean)
    {
        TypeKey = ValueTypeKey.Boolean;
        Boolean = boolean;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(DateTime dateTime)
    {
        TypeKey = ValueTypeKey.DateTime;
        DateTime = dateTime;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(string word)
    {
        TypeKey = ValueTypeKey.Word;
        _referenceValueFacade = word;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(List<string> wordList)
    {
        TypeKey = ValueTypeKey.WordList;
        _referenceValueFacade = wordList;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(List<double> numberList)
    {
        TypeKey = ValueTypeKey.NumberList;
        _referenceValueFacade = numberList;
    }

    /// <summary>
    /// Value
    /// </summary>
    public EvaluatorValue(object objectValue)
    {
        TypeKey = ValueTypeKey.Object;
        _referenceValueFacade = objectValue;
    }

    #endregion

    #region Cast

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(double a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(bool a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(DateTime a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(string a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(List<string> a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(List<double> a)
    {
        return new EvaluatorValue(a);
    }

    /// <summary>
    /// To EvaluatorValue
    /// </summary>
    public static implicit operator EvaluatorValue(InternalEvaluatorValue a)
    {
        if (a.IsNumber)
        {
            return new EvaluatorValue(a.Number);
        }

        if (a.IsDateTime)
        {
            return new EvaluatorValue(a.GetDateTime());
        }

        if (a.IsBoolean)
        {
            return new EvaluatorValue(a.GetBoolean());
        }

        if (a.IsWord)
        {
            return new EvaluatorValue(WordFormatter.Format(a.GetWord()));
        }

        if (a.IsWordList)
        {
            return new EvaluatorValue(WordFormatter.Format(a.GetWordList()));
        }

        if (a.IsNumberList)
        {
            return new EvaluatorValue(a.GetNumberList());
        }

        if (a.IsObject)
        {
            return new EvaluatorValue(a.GetObject());
        }

        if (a.IsNull)
        {
            return default;
        }

        throw new InvalidCastException($"Can't cast {nameof(InternalEvaluatorValue)} with the typeKey = \"{a.TypeKey}\" to {nameof(EvaluatorValue)}");
    }

    #endregion

    #region Equals

    /// <summary>
    /// Equals
    /// </summary>
    public override bool Equals(object obj)
    {
        return obj is EvaluatorValue value && Equals(value);
    }

    /// <summary>
    /// Equals
    /// </summary>
    public bool Equals(EvaluatorValue other)
    {
        return TypeKey == other.TypeKey &&
               Boolean == other.Boolean &&
               Number.Equals(other.Number) &&
               DateTime.Equals(other.DateTime) &&
               EqualityComparer<object>.Default.Equals(_referenceValueFacade, other._referenceValueFacade);
    }

    /// <summary>
    /// GetHashCode
    /// </summary>
    public override int GetHashCode()
    {
        return HashCode.Combine(_referenceValueFacade, TypeKey, Number, Boolean, DateTime);
    }

    #endregion

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        if (TypeKey == ValueTypeKey.Word)
        {
            return Word;
        }

        if (TypeKey == ValueTypeKey.Boolean)
        {
            return Boolean.ToString(CultureInfo.InvariantCulture);
        }

        if (TypeKey == ValueTypeKey.DateTime)
        {
            return DateTime.ToString(CultureInfo.InvariantCulture);
        }

        if (TypeKey == ValueTypeKey.WordList)
        {
            return string.Join(", ", WordList);
        }

        if (TypeKey == ValueTypeKey.NumberList)
        {
            return string.Join(", ", NumberList);
        }

        if (TypeKey == ValueTypeKey.Object)
        {
            return _referenceValueFacade?.ToString();
        }

        if (TypeKey == ValueTypeKey.Null)
        {
            return "Null";
        }

        return Number.ToString(CultureInfo.InvariantCulture);
    }
}
