using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluating.Models
{
    /// <summary>
    /// Syntax checking result
    /// </summary>
    public class CheckFormulaResult
    {
        /// <summary>
        /// Mistakes
        /// </summary>
        public List<string> Messages { get; }

        /// <summary>
        /// Is checking OK
        /// </summary>
        public bool Ok
        {
            get => Messages.Count is 0;
        }

        /// <summary>
        /// Syntax checking result
        /// </summary>
        public CheckFormulaResult(List<string> messages)
        {
            Messages = messages;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return Ok.ToString();
        }

        /// <summary>
        /// Cast to bool
        /// </summary>
        public static implicit operator bool(CheckFormulaResult result)
        {
            return result.Ok;
        }
    }
}
