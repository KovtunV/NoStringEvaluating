using System.Collections.Generic;
using System.Reflection;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;

namespace NoStringEvaluating;

/// <summary>
/// Global options
/// </summary>
public static class GlobalOptions
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
    /// If set true - throws exception when variable not found, if set false - returns Null
    /// </summary>
    public static bool ThrowIfVariableNotFound { get; internal set; }

    /// <summary>
    /// Assemblies with functions
    /// </summary>
    public static HashSet<Assembly> FunctionsAssemblies { get; } = new();

    /// <summary>
    /// Functions
    /// </summary>
    public static HashSet<IFunction> Functions { get; } = new();

    /// <summary>
    /// Global options
    /// </summary>
    static GlobalOptions()
    {
        Reset();
    }

    /// <summary>
    /// Reset to default
    /// </summary>
    public static void Reset()
    {
        FloatingTolerance = 0.0001;
        FloatingPointSymbol = FloatingPointSymbol.Dot;
        WordQuotationMark = string.Empty;
        UseWordQuotationMark = false;
        ThrowIfVariableNotFound = true;

        FunctionsAssemblies.Clear();
        Functions.Clear();
        FunctionsAssemblies.Add(typeof(GlobalOptions).Assembly);
    }
}
