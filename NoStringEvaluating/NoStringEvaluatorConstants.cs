﻿using NoStringEvaluating.Models;

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
        /// Symbol of floating point
        /// </summary>
        public static FloatingPointSymbol FloatingPointSymbol { get; internal set; }

        /// <summary>
        /// Global constants
        /// </summary>
        static NoStringEvaluatorConstants()
        {
            FloatingTolerance = 0.0001;
            FloatingPointSymbol = FloatingPointSymbol.Dot;
        }
    }
}
