using NoStringEvaluating.Models;

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
        /// Symbol of floating point
        /// </summary>
        public FloatingPointSymbol FloatingPointSymbol { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public NoStringEvaluatorOptions()
        {
            FloatingTolerance = NoStringEvaluatorConstants.FloatingTolerance;
            FloatingPointSymbol = NoStringEvaluatorConstants.FloatingPointSymbol;
        }

        /// <summary>
        /// Update constants <see cref="NoStringEvaluatorConstants"/>
        /// </summary>
        public void UpdateConstants()
        {
            NoStringEvaluatorConstants.FloatingTolerance = FloatingTolerance;
            NoStringEvaluatorConstants.FloatingPointSymbol = FloatingPointSymbol;
        }
    }
}
