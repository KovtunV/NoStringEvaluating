using NoStringEvaluating.Models;

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
        /// Quotation mark
        /// </summary>
        public static string WordQuotationMark { get; internal set; }

        /// <summary>
        /// Use quotation mark
        /// </summary>
        public static bool UseWordQuotationMark { get; internal set; }

        /// <summary>
        /// Global constants
        /// </summary>
        static NoStringEvaluatorConstants()
        {
            FloatingTolerance = 0.0001;
            FloatingPointSymbol = FloatingPointSymbol.Dot;
            WordQuotationMark = string.Empty;
            UseWordQuotationMark = false;
        }
    }
}
