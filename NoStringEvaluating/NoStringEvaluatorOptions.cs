using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluating
{
    /// <summary>
    /// Options
    /// </summary>
    public class NoStringEvaluatorOptions
    {
        /// <summary>
        /// Floating tolerance for understanding Zero number
        /// </summary>
        public double FloatingTolerance { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public NoStringEvaluatorOptions()
        {
            FloatingTolerance = NoStringEvaluatorConstants.FloatingTolerance;
        }

        internal void UpdateConstants()
        {
            NoStringEvaluatorConstants.FloatingTolerance = FloatingTolerance;
        }
    }
}
