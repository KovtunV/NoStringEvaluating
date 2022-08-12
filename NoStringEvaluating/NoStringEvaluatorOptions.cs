using System;
using System.Collections.Generic;
using System.Reflection;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
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
    public bool ThrowIfVariableNotFound { get; set; }

    /// <summary>
    /// Don't register default functions
    /// </summary>
    public bool IsWithoutDefaultFunctions { get; set; }

    /// <summary>
    /// Assemblies with functions
    /// </summary>
    public HashSet<Assembly> FunctionsAssemblies { get; } = new();

    /// <summary>
    /// Functions
    /// </summary>
    public HashSet<IFunction> Functions { get; } = new();

    /// <summary>
    /// Options
    /// </summary>
    public NoStringEvaluatorOptions()
    {
        FloatingTolerance = GlobalOptions.FloatingTolerance;
        FloatingPointSymbol = GlobalOptions.FloatingPointSymbol;
        WordQuotationMark = GlobalOptions.WordQuotationMark;
        ThrowIfVariableNotFound = GlobalOptions.ThrowIfVariableNotFound;
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
    public NoStringEvaluatorOptions SetThrowIfVariableNotFound(bool isThrow)
    {
        ThrowIfVariableNotFound = isThrow;
        return this;
    }

    /// <summary>
    /// Add assembly to register functions
    /// </summary>
    public NoStringEvaluatorOptions WithFunctionsFrom(Type typeFromSourceAssembly)
    {
        return WithFunctionsFrom(typeFromSourceAssembly.Assembly);
    }

    /// <summary>
    /// Add assembly to register functions
    /// </summary>
    public NoStringEvaluatorOptions WithFunctionsFrom(Assembly sourceAssembly)
    {
        FunctionsAssemblies.Add(sourceAssembly);
        return this;
    }

    /// <summary>
    /// Remove root assembly from functions registration
    /// </summary>
    public NoStringEvaluatorOptions WithoutDefaultFunctions(bool withoutDefaultFunctions = true)
    {
        IsWithoutDefaultFunctions = withoutDefaultFunctions;
        return this;
    }

    /// <summary>
    /// Add functions
    /// </summary>
    public NoStringEvaluatorOptions WithFunctions(params IFunction[] functions)
    {
        functions ??= Array.Empty<IFunction>();
        functions.ForEach(x => Functions.Add(x));
        return this;
    }

    /// <summary>
    /// Update global constants <see cref="GlobalOptions"/>
    /// </summary>
    public void UpdateGlobalOptions()
    {
        GlobalOptions.FloatingTolerance = FloatingTolerance;
        GlobalOptions.FloatingPointSymbol = FloatingPointSymbol;
        GlobalOptions.WordQuotationMark = WordQuotationMark;
        GlobalOptions.UseWordQuotationMark = !string.IsNullOrEmpty(WordQuotationMark);
        GlobalOptions.ThrowIfVariableNotFound = ThrowIfVariableNotFound;

        FunctionsAssemblies.ForEach(x => GlobalOptions.FunctionsAssemblies.Add(x));
        Functions.ForEach(x => GlobalOptions.Functions.Add(x));

        if (IsWithoutDefaultFunctions)
        {
            GlobalOptions.FunctionsAssemblies.Remove(typeof(NoStringEvaluatorOptions).Assembly);
        }
    }
}
