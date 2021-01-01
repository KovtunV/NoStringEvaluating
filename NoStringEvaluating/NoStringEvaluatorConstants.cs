using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluating
{
    /// <summary>
    /// Global constants
    /// </summary>
    public static class NoStringEvaluatorConstants
    {
        /// <summary>
        /// Floating tolerance for understanding Zero number
        /// </summary>
        public static double FloatingTolerance { get; internal set; }

        /// <summary>
        /// Global constants
        /// </summary>
        static NoStringEvaluatorConstants()
        {
            FloatingTolerance = 0.0001;
        }
    }
}
