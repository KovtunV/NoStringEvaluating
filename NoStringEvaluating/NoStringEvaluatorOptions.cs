using NoStringEvaluating.Models;

namespace NoStringEvaluating;

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
    /// Quotation mark
    /// </summary>
    public string WordQuotationMark { get; set; }

    /// <summary>
    /// If set true - throws exception when variable not found, if set false - returns Null
    /// </summary>
    public static bool ThrowIfVariableNotFound { get; set; }

    /// <summary>
    /// Options
    /// </summary>
    public NoStringEvaluatorOptions()
    {
        FloatingTolerance = NoStringEvaluatorConstants.FloatingTolerance;
        FloatingPointSymbol = NoStringEvaluatorConstants.FloatingPointSymbol;
        WordQuotationMark = NoStringEvaluatorConstants.WordQuotationMark;
        ThrowIfVariableNotFound = NoStringEvaluatorConstants.ThrowIfVariableNotFound;
    }

    /// <summary>
    /// Set word quotation mark - '
    /// </summary>
    public NoStringEvaluatorOptions SetWordQuotationSingleQuote()
    {
        return SetWordQuotationMark("'");
    }

    /// <summary>
    /// Set word quotation mark - "
    /// </summary>
    public NoStringEvaluatorOptions SetWordQuotationDoubleQuote()
    {
        return SetWordQuotationMark("\"");
    }

    /// <summary>
    /// Set word quotation mark
    /// </summary>
    public NoStringEvaluatorOptions SetWordQuotationMark(string mark)
    {
        WordQuotationMark = mark;
        return this;
    }

    /// <summary>
    /// Set floating tolerance
    /// </summary>
    public NoStringEvaluatorOptions SetFloatingTolerance(double floatingTolerance)
    {
        FloatingTolerance = floatingTolerance;
        return this;
    }

    /// <summary>
    /// Set floating point symbol
    /// </summary>
    public NoStringEvaluatorOptions SetFloatingPointSymbol(FloatingPointSymbol floatingPointSymbol)
    {
        FloatingPointSymbol = floatingPointSymbol;
        return this;
    }

    /// <summary>
    /// Set throw if variable not found
    /// </summary>
    /// <param name="isThrow"></param>
    /// <returns></returns>
    public NoStringEvaluatorOptions SetThrowIfVariableNotFound(bool isThrow)
    {
        ThrowIfVariableNotFound = isThrow;
        return this;
    }

    /// <summary>
    /// Update constants <see cref="NoStringEvaluatorConstants"/>
    /// </summary>
    public void UpdateConstants()
    {
        NoStringEvaluatorConstants.FloatingTolerance = FloatingTolerance;
        NoStringEvaluatorConstants.FloatingPointSymbol = FloatingPointSymbol;
        NoStringEvaluatorConstants.WordQuotationMark = WordQuotationMark;
        NoStringEvaluatorConstants.UseWordQuotationMark = !string.IsNullOrEmpty(WordQuotationMark);
        NoStringEvaluatorConstants.ThrowIfVariableNotFound = ThrowIfVariableNotFound;
    }
}
