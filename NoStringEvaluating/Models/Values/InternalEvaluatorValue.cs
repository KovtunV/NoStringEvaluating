using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Value for internal processing
/// </summary>
public readonly struct InternalEvaluatorValue : IEquatable<InternalEvaluatorValue>
{
    private readonly int _extraTypeId = default;

    /// <summary>
    /// Type key 
    /// </summary>
    public ValueTypeKey TypeKey { get; }

    /// <summary>
    /// Number value
    /// </summary>
    public double Number { get; }

    /// <summary>
    /// Boolean value
    /// </summary>
    public bool Boolean => Number != 0.0;

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(double number)
    {
        TypeKey = ValueTypeKey.Number;
        Number = number;
    }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(double? number)
    {
        if (number.HasValue)
        {
            TypeKey = ValueTypeKey.Number;
            Number = number.Value;
        }
        else
        {
            Number = default;
            TypeKey = ValueTypeKey.Null;
        }
    }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(bool boolean)
    {
        TypeKey = ValueTypeKey.Boolean;
        Number = boolean ? 1.0 : 0.0;
    }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(bool? boolean)
    {
        if (boolean.HasValue)
        {
            TypeKey = ValueTypeKey.Boolean;
            Number = boolean.Value ? 1.0 : 0.0;
        }
        else
        {
            Number = default;
            TypeKey = ValueTypeKey.Null;
        }
    }

    internal InternalEvaluatorValue(int extraTypeId, ValueTypeKey typeKey)
    {
        _extraTypeId = extraTypeId;
        TypeKey = typeKey;
        Number = double.NaN;
    }

    #region IsProperties

    /// <summary>
    /// IsNumber
    /// </summary>
    public bool IsNumber => TypeKey == ValueTypeKey.Number;

    /// <summary>
    /// IsDateTime
    /// </summary>
    public bool IsDateTime => TypeKey == ValueTypeKey.DateTime;

    /// <summary>
    /// IsBoolean
    /// </summary>
    public bool IsBoolean => TypeKey == ValueTypeKey.Boolean;

    /// <summary>
    /// IsWord
    /// </summary>
    public bool IsWord => TypeKey == ValueTypeKey.Word;

    /// <summary>
    /// IsWordList
    /// </summary>
    public bool IsWordList => TypeKey == ValueTypeKey.WordList;

    /// <summary>
    /// IsNumberList
    /// </summary>
    public bool IsNumberList => TypeKey == ValueTypeKey.NumberList;

    /// <summary>
    /// IsObject
    /// </summary>
    public bool IsObject => TypeKey == ValueTypeKey.Object;

    /// <summary>
    /// IsNull
    /// </summary>
    public bool IsNull => TypeKey == ValueTypeKey.Null;

    #endregion

    #region ExtraValue

    /// <summary>
    /// Returns DateTime
    /// </summary>
    public DateTime GetDateTime()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsDateTime ? DateTimeKeeper.Instance.Get(_extraTypeId) : default;
    }

    /// <summary>
    /// Returns string
    /// </summary>
    public string GetWord()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsWord ? WordKeeper.Instance.Get(_extraTypeId) : default;
    }

    /// <summary>
    /// Returns string List
    /// </summary>
    public List<string> GetWordList()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsWordList ? WordListKeeper.Instance.Get(_extraTypeId) : default;
    }

    /// <summary>
    /// Returns double List
    /// </summary>
    public List<double> GetNumberList()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsNumberList ? NumberListKeeper.Instance.Get(_extraTypeId) : default;
    }

    /// <summary>
    /// Returns object
    /// </summary>
    public T GetObject<T>()
        where T : class
    {
        return GetObject() as T;
    }

    /// <summary>
    /// Returns object
    /// </summary>
    public object GetObject()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsObject ? ObjectKeeper.Instance.Get(_extraTypeId) : default;
    }

    #endregion

    #region Equals

    /// <summary>
    /// Equals
    /// </summary>
    public override bool Equals(object obj)
    {
        return obj is InternalEvaluatorValue value && Equals(value);
    }

    /// <summary>
    /// Equals
    /// </summary>
    public bool Equals(InternalEvaluatorValue other)
    {
        return TypeKey == other.TypeKey &&
            (
            (TypeKey == ValueTypeKey.Boolean && Boolean == other.Boolean) ||
            (TypeKey == ValueTypeKey.Word && GetWord() == other.GetWord()) ||
            (TypeKey == ValueTypeKey.DateTime && GetDateTime().Equals(other.GetDateTime())) ||
            (TypeKey == ValueTypeKey.Number && Math.Abs(Number - other.Number) < GlobalOptions.FloatingTolerance) ||
            (TypeKey == ValueTypeKey.WordList && EqualityComparer<List<string>>.Default.Equals(GetWordList(), other.GetWordList())) ||
            (TypeKey == ValueTypeKey.NumberList && EqualityComparer<List<double>>.Default.Equals(GetNumberList(), other.GetNumberList())) ||
            (TypeKey == ValueTypeKey.Object && Equals(GetObject(), other.GetObject())) ||
            (TypeKey == ValueTypeKey.Null)
            );
    }

    /// <summary>
    /// GetHashCode
    /// </summary>
    public override int GetHashCode()
    {
        if (IsNumber)
        {
            return HashCode.Combine(TypeKey, Number);
        }

        if (IsDateTime)
        {
            return HashCode.Combine(TypeKey, GetDateTime());
        }

        if (IsBoolean)
        {
            return HashCode.Combine(TypeKey, Boolean);
        }

        if (IsWord)
        {
            return HashCode.Combine(TypeKey, GetWord());
        }

        if (IsWordList)
        {
            return HashCode.Combine(TypeKey, GetWordList());
        }

        if (IsNumberList)
        {
            return HashCode.Combine(TypeKey, GetNumberList());
        }

        if (IsObject)
        {
            return HashCode.Combine(TypeKey, GetObject());
        }

        return HashCode.Combine(TypeKey);
    }

    #endregion

    #region MathOperators

    /// <summary>
    /// Plus
    /// </summary>
    public static InternalEvaluatorValue operator +(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number + b.Number;
    }

    /// <summary>
    /// Minus
    /// </summary>
    public static InternalEvaluatorValue operator -(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number - b.Number;
    }

    /// <summary>
    /// Multiplication
    /// </summary>
    public static InternalEvaluatorValue operator *(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number * b.Number;
    }

    /// <summary>
    /// Division
    /// </summary>
    public static InternalEvaluatorValue operator /(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number / b.Number;
    }

    #endregion

    #region LogicOperators

    /// <summary>
    /// More
    /// </summary>
    public static bool operator >(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number > b.Number;
    }

    /// <summary>
    /// Less
    /// </summary>
    public static bool operator <(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number < b.Number;
    }

    /// <summary>
    /// More or Equal
    /// </summary>
    public static bool operator >=(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number >= b.Number;
    }

    /// <summary>
    /// Less or Equal
    /// </summary>
    public static bool operator <=(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        return a.Number <= b.Number;
    }

    /// <summary>
    /// Equal
    /// </summary>
    public static bool operator ==(InternalEvaluatorValue left, InternalEvaluatorValue right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Not equal
    /// </summary>
    public static bool operator !=(InternalEvaluatorValue left, InternalEvaluatorValue right)
    {
        return !(left == right);
    }

    #endregion

    #region Cast

    /// <summary>
    /// To double
    /// </summary>
    public static implicit operator double(InternalEvaluatorValue a)
    {
        return a.Number;
    }

    /// <summary>
    /// To string
    /// </summary>
    public static implicit operator string(InternalEvaluatorValue a)
    {
        return a.GetWord();
    }

    /// <summary>
    /// To DateTime
    /// </summary>
    public static implicit operator DateTime(InternalEvaluatorValue a)
    {
        return a.GetDateTime();
    }

    /// <summary>
    /// To boolean
    /// </summary>
    public static implicit operator bool(InternalEvaluatorValue a)
    {
        return a.Boolean;
    }

    /// <summary>
    /// To string List
    /// </summary>
    public static implicit operator List<string>(InternalEvaluatorValue a)
    {
        return a.GetWordList();
    }

    /// <summary>
    /// To double List
    /// </summary>
    public static implicit operator List<double>(InternalEvaluatorValue a)
    {
        return a.GetNumberList();
    }

    /// <summary>
    /// To InternalEvaluatorValue
    /// </summary>
    public static implicit operator InternalEvaluatorValue(double a)
    {
        return new InternalEvaluatorValue(a);
    }

    /// <summary>
    /// To InternalEvaluatorValue
    /// </summary>
    public static implicit operator InternalEvaluatorValue(bool a)
    {
        return new InternalEvaluatorValue(a);
    }

    #endregion

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        if (IsDateTime)
        {
            return GetDateTime().ToString(CultureInfo.InvariantCulture);
        }

        if (IsBoolean)
        {
            return Boolean.ToString(CultureInfo.InvariantCulture);
        }

        if (IsWord)
        {
            return GetWord();
        }

        if (IsWordList)
        {
            var wList = GetWordList();
            return wList is null ? string.Empty : string.Join(", ", wList);
        }

        if (IsNumberList)
        {
            var nList = GetNumberList();
            return nList is null ? string.Empty : string.Join(", ", nList);
        }

        if (IsObject)
        {
            return GetObject()?.ToString();
        }

        if (IsNull)
        {
            return "Null";
        }

        return Number.ToNonScientificString();
    }
}
