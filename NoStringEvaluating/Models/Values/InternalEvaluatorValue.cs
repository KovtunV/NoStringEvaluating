using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Services.Keepers;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Value for internal processing
/// </summary>
public readonly struct InternalEvaluatorValue
{
    private readonly int _extraTypeId;

    /// <summary>
    /// Type key 
    /// </summary>
    public ValueTypeKey TypeKey { get; }

    /// <summary>
    /// Number value
    /// </summary>
    public double Number { get; }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(double number)
    {
        TypeKey = ValueTypeKey.Number;
        Number = number;
        _extraTypeId = NullId;
    }

    /// <summary>
    /// NULL Instance
    /// </summary>
    public InternalEvaluatorValue()
    {
        // Everything set to 0, the same as what you get when you do  default(InternalEvaluatorValue)
        TypeKey = ValueTypeKey.Null;
        Number = 0;
        _extraTypeId = NullId;
    }

    internal InternalEvaluatorValue(int extraTypeId, ValueTypeKey typeKey)
    {
        _extraTypeId = extraTypeId;
        TypeKey = typeKey;
        Number = double.NaN;
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        if (IsWord)
        {
            return GetWord();
        }

        if (IsDateTime)
        {
            return GetDateTime().ToString(CultureInfo.InvariantCulture);
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

        if (IsNull)
        {
            return "Null";
        }

        return Number.ToString(CultureInfo.InvariantCulture);
    }

    #region IsProperties

    /// <summary>
    /// IsNumber
    /// </summary>
    public bool IsNumber
    {
        get => TypeKey == ValueTypeKey.Number;
    }

    /// <summary>
    /// IsWord
    /// </summary>
    public bool IsWord
    {
        get => TypeKey == ValueTypeKey.Word;
    }

    /// <summary>
    /// IsDateTime
    /// </summary>
    public bool IsDateTime
    {
        get => TypeKey == ValueTypeKey.DateTime;
    }

    /// <summary>
    /// IsWordList
    /// </summary>
    public bool IsWordList
    {
        get => TypeKey == ValueTypeKey.WordList;
    }

    /// <summary>
    /// IsNumberList
    /// </summary>
    public bool IsNumberList
    {
        get => TypeKey == ValueTypeKey.NumberList;
    }

    /// <summary>
    /// IsNull
    /// </summary>
    public bool IsNull
    {
        get => TypeKey == ValueTypeKey.Null;
    }

    #endregion

    #region ExtraValue

    /// <summary>
    /// Returns string
    /// </summary>
    public string GetWord()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsWord ? WordKeeper.Instance.Get(_extraTypeId) : null;
    }

    /// <summary>
    /// Returns DateTime
    /// </summary>
    public DateTime GetDateTime()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsDateTime ? DateTimeKeeper.Instance.Get(_extraTypeId) : DateTime.MinValue;
    }

    /// <summary>
    /// Returns string List
    /// </summary>
    public List<string> GetWordList()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsWordList ? WordListKeeper.Instance.Get(_extraTypeId) : null;
    }

    /// <summary>
    /// Returns double List
    /// </summary>
    public List<double> GetNumberList()
    {
        // It has to be a method to avoid misunderstanding inside custom functions
        return IsNumberList ? NumberListKeeper.Instance.Get(_extraTypeId) : null;
    }

    #endregion

    #region Equals
    /// <summary>
    /// Equals
    /// </summary>
    public bool Equals(InternalEvaluatorValue other)
    {
        return TypeKey == other.TypeKey &&
            (
            (TypeKey == ValueTypeKey.Null) ||
            (TypeKey == ValueTypeKey.DateTime && GetDateTime() == other.GetDateTime()) ||
            (TypeKey == ValueTypeKey.Word && GetWord() == other.GetWord()) ||
            (TypeKey == ValueTypeKey.WordList && EqualityComparer<List<string>>.Default.Equals(GetWordList(), other.GetWordList())) ||
            (TypeKey == ValueTypeKey.NumberList && EqualityComparer<List<double>>.Default.Equals(GetNumberList(), other.GetNumberList())) ||
            Number == other.Number
            );
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
    /// To double?
    /// </summary>
    public static implicit operator double?(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : a.Number;
    }

    /// <summary>
    /// To string
    /// </summary>
    public static implicit operator string(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : a.GetWord();
    }


    /// <summary>
    /// To DateTime
    /// </summary>
    public static implicit operator DateTime(InternalEvaluatorValue a)
    {
        return a.GetDateTime();
    }

    /// <summary>
    /// To DateTime?
    /// </summary>
    public static implicit operator DateTime?(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : a.GetDateTime();
    }

    /// <summary>
    /// To string List
    /// </summary>
    public static implicit operator List<string>(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : a.GetWordList();
    }

    /// <summary>
    /// To double List
    /// </summary>
    public static implicit operator List<double>(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : a.GetNumberList();
    }

    /// <summary>
    /// To boolean
    /// </summary>
    public static implicit operator bool(InternalEvaluatorValue a)
    {
        return a.IsNull ? false : Math.Abs(a.Number) > NoStringEvaluatorConstants.FloatingTolerance;
    }


    /// <summary>
    /// To boolean?
    /// </summary>
    public static implicit operator bool?(InternalEvaluatorValue a)
    {
        return a.IsNull ? null : Math.Abs(a.Number) > NoStringEvaluatorConstants.FloatingTolerance;
    }

    /// <summary>
    /// To InternalEvaluatorValue
    /// </summary>
    public static implicit operator InternalEvaluatorValue(double a)
    {
        return new InternalEvaluatorValue(a);
    }

    #endregion

    /// <summary>
    /// Null Id
    /// </summary>
    private const int NullId = -1;
}

