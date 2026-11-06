using System.Globalization;
using System.Runtime.InteropServices;
using NoStringEvaluating.Extensions;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Value for internal processing
/// </summary>
public readonly struct InternalEvaluatorValue : IEquatable<InternalEvaluatorValue>
{
    // Ticks take the low 62 bits of a packed DateTime, the kind the top 2
    private const int KindShift = 62;
    private const long TicksMask = (1L << KindShift) - 1;

    // The keeper handle for Word, WordList, NumberList and Object; the value itself for Boolean (0 or 1) and DateTime (packed ticks and kind).
    // A long, not an IntPtr: the packed DateTime needs 64 bits in a 32-bit process too. No object reference in the struct: a struct with one
    // costs a GC write barrier on every push on the evaluation stack, which was measured at 30-50% of the evaluation time
    private readonly long _data = default;

    /// <summary>
    /// Value for internal processing
    /// </summary>
    internal InternalEvaluatorValue(IntPtr valueKeeperPtr, ValueTypeKey typeKey)
    {
        TypeKey = typeKey;
        _data = (long)valueKeeperPtr;
    }

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
            TypeKey = ValueTypeKey.Null;
        }
    }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(bool boolean)
    {
        TypeKey = ValueTypeKey.Boolean;
        _data = boolean ? 1 : 0;
    }

    /// <summary>
    /// Value for internal processing
    /// </summary>
    public InternalEvaluatorValue(DateTime dateTime)
    {
        TypeKey = ValueTypeKey.DateTime;
        _data = dateTime.Ticks | ((long)dateTime.Kind << KindShift);
    }

    /// <summary>
    /// Type key
    /// </summary>
    public ValueTypeKey TypeKey { get; }

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

    #region ValueProperties

    /// <summary>
    /// Number value
    /// </summary>
    public double Number { get; } = default;

    /// <summary>
    /// DateTime value
    /// </summary>
    public DateTime DateTime => IsDateTime ? new DateTime(_data & TicksMask, (DateTimeKind)(int)((ulong)_data >> KindShift)) : default;

    /// <summary>
    /// Boolean value
    /// </summary>
    public bool Boolean => IsBoolean && _data != 0;

    /// <summary>
    /// Word value
    /// </summary>
    public string Word => ValueKeeper.Word;

    /// <summary>
    /// WordList value
    /// </summary>
    public List<string> WordList => ValueKeeper.WordList;

    /// <summary>
    /// NumberList value
    /// </summary>
    public List<double> NumberList => ValueKeeper.NumberList;

    /// <summary>
    /// Object value
    /// </summary>
    public object Object => ValueKeeper.Object;

    // The keeper of a reference value; the zero keeper for every other type, so that the inline data is never taken for a handle
    private ValueKeeper ValueKeeper => HasKeeper && _data != 0
        ? (ValueKeeper)GCHandle.FromIntPtr((IntPtr)_data).Target
        : ValueKeeper.Zero;

    private bool HasKeeper => TypeKey is ValueTypeKey.Word or ValueTypeKey.WordList or ValueTypeKey.NumberList or ValueTypeKey.Object;

    /// <summary>
    /// Returns object
    /// </summary>
    public T GetObject<T>()
        where T : class
    {
        return Object as T;
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
        return TypeKey == other.TypeKey && (
            (TypeKey == ValueTypeKey.Boolean && Boolean == other.Boolean) ||
            (TypeKey == ValueTypeKey.Word && Word == other.Word) ||
            (TypeKey == ValueTypeKey.DateTime && DateTime.Equals(other.DateTime)) ||
            (TypeKey == ValueTypeKey.Number && Math.Abs(Number - other.Number) < GlobalOptions.FloatingTolerance) ||
            (TypeKey == ValueTypeKey.WordList && EqualityComparer<List<string>>.Default.Equals(WordList, other.WordList)) ||
            (TypeKey == ValueTypeKey.NumberList && EqualityComparer<List<double>>.Default.Equals(NumberList, other.NumberList)) ||
            (TypeKey == ValueTypeKey.Object && Equals(Object, other.Object)) ||
            (TypeKey == ValueTypeKey.Null));
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
            return HashCode.Combine(TypeKey, DateTime);
        }

        if (IsBoolean)
        {
            return HashCode.Combine(TypeKey, Boolean);
        }

        if (IsWord)
        {
            return HashCode.Combine(TypeKey, Word);
        }

        if (IsWordList)
        {
            return HashCode.Combine(TypeKey, WordList);
        }

        if (IsNumberList)
        {
            return HashCode.Combine(TypeKey, NumberList);
        }

        if (IsObject)
        {
            return HashCode.Combine(TypeKey, Object);
        }

        return HashCode.Combine(TypeKey);
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
    /// To number
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
        return a.Word;
    }

    /// <summary>
    /// To DateTime
    /// </summary>
    public static implicit operator DateTime(InternalEvaluatorValue a)
    {
        return a.DateTime;
    }

    /// <summary>
    /// To boolean
    /// </summary>
    public static implicit operator bool(InternalEvaluatorValue a)
    {
        return a.Boolean;
    }

    /// <summary>
    /// To word List
    /// </summary>
    public static implicit operator List<string>(InternalEvaluatorValue a)
    {
        return a.WordList;
    }

    /// <summary>
    /// To number List
    /// </summary>
    public static implicit operator List<double>(InternalEvaluatorValue a)
    {
        return a.NumberList;
    }

    /// <summary>
    /// To InternalEvaluatorValue
    /// </summary>
    public static implicit operator InternalEvaluatorValue(double a)
    {
        return new(a);
    }

    #endregion

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        if (IsDateTime)
        {
            return DateTime.ToString(CultureInfo.InvariantCulture);
        }

        if (IsBoolean)
        {
            return Boolean.ToString(CultureInfo.InvariantCulture);
        }

        if (IsWord)
        {
            return Word;
        }

        if (IsWordList)
        {
            var wList = WordList;
            return wList is null ? string.Empty : string.Join(", ", wList);
        }

        if (IsNumberList)
        {
            var nList = NumberList;
            return nList is null ? string.Empty : string.Join(", ", nList);
        }

        if (IsObject)
        {
            return Object?.ToString();
        }

        if (IsNull)
        {
            return "Null";
        }

        return Number.ToNonScientificString();
    }
}
