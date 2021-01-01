using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluating.Exceptions
{
    /// <summary>
    /// Raises when function exception
    /// </summary>
    public class NoStringFunctionException : Exception
    {
        /// <summary>
        /// Raises when function exception
        /// </summary>
        public NoStringFunctionException(string funcName)
            : base($"Function \"{funcName}\" has already added")
        {
            
        }
    }
}
