using NoStringEvaluating.Services.Value;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace NoStringEvaluating.Models.Values
{

    /// <summary>
    /// Value
    /// </summary>
    public readonly struct EvaluatorValue : IEquatable<EvaluatorValue>
    {
        private object TypeValue { get; }

        /// <summary>
        /// Type key
        /// </summary>
        public ValueTypeKey TypeKey { get; }

        /// <summary>
        /// Number
        /// </summary>
        public double Number => (double)TypeValue;

        /// <summary>
        /// Word
        /// </summary>
        public string Word => (string)TypeValue;

        /// <summary>
        /// Boolean
        /// </summary>
        public bool Boolean => (bool)TypeValue;

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime DateTime => (DateTime) TypeValue;

        /// <summary>
        /// WordList
        /// </summary>
        public List<string> WordList => (List<string>)TypeValue;

        /// <summary>
        /// NumberList
        /// </summary>
        public List<double> NumberList =>(List<double>)TypeValue;

        #region Ctors

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(double number)
        {
            TypeKey = ValueTypeKey.Number;
            TypeValue = number;

        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(string word)
        {
            TypeKey = ValueTypeKey.Word;
            TypeValue = word;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(DateTime dateTime)
        {
            TypeKey = ValueTypeKey.DateTime;
            TypeValue = dateTime;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(List<string> wordList)
        {
            TypeKey = ValueTypeKey.WordList;
            TypeValue = wordList;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(List<double> numberList)
        {
            TypeKey = ValueTypeKey.NumberList;
            TypeValue = numberList;
        }

        /// <summary>
        /// Value
        /// </summary>
        public EvaluatorValue(bool boolean)
        {
            TypeKey = ValueTypeKey.Boolean;
            TypeValue = boolean;
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
                (
                (TypeKey == ValueTypeKey.WordList && EqualityComparer<List<string>>.Default.Equals(WordList, other.WordList)) ||
                (TypeKey == ValueTypeKey.NumberList && EqualityComparer<List<double>>.Default.Equals(NumberList, other.NumberList)) ||
                TypeValue.Equals(other.TypeValue)
                );
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(TypeKey, TypeValue);
        }

        #endregion
    }
}
