using System;

namespace NoStringEvaluating.Exceptions
{
    /// <summary>
    /// Raises when no free id
    /// </summary>
    public class ExtraTypeNoFreeIdException : Exception
    {
        /// <summary>
        /// Raises when no free id
        /// </summary>
        public ExtraTypeNoFreeIdException(string keeperName) : base($"There is no free id in keeper \"{keeperName}\"")
        {
            
        }
    }
}
