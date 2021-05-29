using NoStringEvaluating.Services.Value;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NoStringEvaluating.Models.Values
{
    /// <summary>
    /// Value
    /// </summary>
    public readonly struct EvaluatorValue : IEquatable<EvaluatorValue>
    {
        /// <summary>
        /// Type key
        /// </summary>
        public ValueTypeKey TypeKey { get; }

        /// <summary>
        /// Number
        /// </summary>
        public double Number { get; }

        /// <summary>
        /// Word
        /// </summary>
        public string Word { get; }

        /// <summary>
        /// Boolean
        /// </summary>
        public bool Boolean { get; }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// WordList
        /// </summary>
        public List<string> WordList { get; }

        /// <summary>
        /// NumberList
        /// </summary>
        public List<double> NumberList { get; }

        #region Ctors

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(double number)
        {
            TypeKey = ValueTypeKey.Number;
            Number = number;

            Word = null;
            WordList = null;
            NumberList = null;
            DateTime = DateTime.MinValue;
            Boolean = false;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(string word)
        {
            TypeKey = ValueTypeKey.Word;
            Word = word;

            Number = double.NaN;
            WordList = null;
            NumberList = null;
            DateTime = DateTime.MinValue;
            Boolean = false;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(DateTime dateTime)
        {
            TypeKey = ValueTypeKey.DateTime;
            DateTime = dateTime;

            Number = double.NaN;
            Word = null;
            WordList = null;
            NumberList = null;
            Boolean = false;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(List<string> wordList)
        {
            TypeKey = ValueTypeKey.WordList;
            WordList = wordList;

            Number = double.NaN;
            Word = null;
            NumberList = null;
            DateTime = DateTime.MinValue;
            Boolean = false;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(List<double> numberList)
        {
            TypeKey = ValueTypeKey.NumberList;
            NumberList = numberList;

            Number = double.NaN;
            Word = null;
            WordList = null;
            DateTime = DateTime.MinValue;
            Boolean = false;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(bool boolean)
        {
            TypeKey = ValueTypeKey.Boolean;
            Boolean = boolean;

            NumberList = null;
            Number = double.NaN;
            Word = null;
            WordList = null;
            DateTime = DateTime.MinValue;
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

            if (TypeKey == ValueTypeKey.Boolean)
            {
                return Boolean.ToString(CultureInfo.InvariantCulture);
            }

            return Number.ToString(CultureInfo.InvariantCulture);
        }

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
        public static implicit operator EvaluatorValue(string a)
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
        public static implicit operator EvaluatorValue(bool a)
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

            if (a.IsWord)
            {
                return new EvaluatorValue(WordFormatter.Format(a.GetWord()));
            }

            if (a.IsDateTime)
            {
                return new EvaluatorValue(a.GetDateTime());
            }

            if (a.IsWordList)
            {
                return new EvaluatorValue(WordFormatter.Format(a.GetWordList()));
            }

            if (a.IsNumberList)
            {
                return new EvaluatorValue(a.GetNumberList());
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
                   Word == other.Word &&
                   Boolean == other.Boolean &&
                   Number.Equals(other.Number) &&
                   DateTime.Equals(other.DateTime) &&
                   EqualityComparer<List<string>>.Default.Equals(WordList, other.WordList) &&
                   EqualityComparer<List<double>>.Default.Equals(NumberList, other.NumberList);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(TypeKey, Number, Word, Boolean, DateTime, WordList, NumberList);
        }

        #endregion
    }
}
