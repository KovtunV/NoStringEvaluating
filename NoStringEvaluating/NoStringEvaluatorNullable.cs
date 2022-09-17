using System;
using System.Collections.Generic;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Cache;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Services.Parsing.NodeReaders;

namespace NoStringEvaluating;

/// <summary>
/// Expression evaluator with nullable result
/// </summary>
public class NoStringEvaluatorNullable : INoStringEvaluatorNullable
{
    private readonly INoStringEvaluator _evaluator;

    /// <summary>
    /// Expression evaluator with nullable result
    /// </summary>
    public NoStringEvaluatorNullable(INoStringEvaluator evaluator)
    {
        _evaluator = evaluator;
    }

    #region NumberEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(string formula, IVariablesContainer variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(string formula)
    {
        var res = Calc(formula);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public double? CalcNumber(FormulaNodes formulaNodes)
    {
        var res = Calc(formulaNodes);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Number;
    }

    #endregion

    #region WordEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(string formula, IVariablesContainer variables)
    {
        return _evaluator.CalcWord(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        return _evaluator.CalcWord(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcWord(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcWord(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(string formula)
    {
        return _evaluator.CalcWord(formula);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public string CalcWord(FormulaNodes formulaNodes)
    {
        return _evaluator.CalcWord(formulaNodes);
    }

    #endregion

    #region DateTimeEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(string formula, IVariablesContainer variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(string formula)
    {
        var res = Calc(formula);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public DateTime? CalcDateTime(FormulaNodes formulaNodes)
    {
        var res = Calc(formulaNodes);
        return res.TypeKey == ValueTypeKey.Null ? null : res.DateTime;
    }

    #endregion

    #region WordListEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(string formula, IVariablesContainer variables)
    {
        return _evaluator.CalcWordList(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        return _evaluator.CalcWordList(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcWordList(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcWordList(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(string formula)
    {
        return _evaluator.CalcWordList(formula);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes)
    {
        return _evaluator.CalcWordList(formulaNodes);
    }

    #endregion

    #region NumberListEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(string formula, IVariablesContainer variables)
    {
        return _evaluator.CalcNumberList(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        return _evaluator.CalcNumberList(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcNumberList(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.CalcNumberList(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(string formula)
    {
        return _evaluator.CalcNumberList(formula);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes)
    {
        return _evaluator.CalcNumberList(formulaNodes);
    }

    #endregion

    #region BooleanEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(string formula, IVariablesContainer variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formula, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var res = Calc(formulaNodes, variables);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(string formula)
    {
        var res = Calc(formula);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public bool? CalcBoolean(FormulaNodes formulaNodes)
    {
        var res = Calc(formulaNodes);
        return res.TypeKey == ValueTypeKey.Null ? null : res.Boolean;
    }

    #endregion

    #region AggregatedEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(string formula, IVariablesContainer variables)
    {
        return _evaluator.Calc(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        return _evaluator.Calc(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.Calc(formula, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        return _evaluator.Calc(formulaNodes, variables);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(string formula)
    {
        return _evaluator.Calc(formula);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="ExtraTypeIdNotFoundException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes)
    {
        return _evaluator.Calc(formulaNodes);
    }

    #endregion

    /// <summary>
    /// Create evaluator facade
    /// </summary>
    public static Facade CreateFacade(Action<NoStringEvaluatorOptions> options = null)
    {
        var evaluatorFacade = NoStringEvaluator.CreateFacade(options);
        return new Facade(evaluatorFacade);
    }

    /// <summary>
    /// Facade
    /// </summary>
    public class Facade
    {
        /// <summary>
        /// Facade
        /// </summary>
        public Facade(NoStringEvaluator.Facade facade)
        {
            Evaluator = new(facade.Evaluator);

            FunctionReader = facade.FunctionReader;
            FormulaParser = facade.FormulaParser;
            FormulaCache = facade.FormulaCache;
            FormulaChecker = facade.FormulaChecker;
        }

        /// <summary>
        /// Evaluator
        /// </summary>
        public NoStringEvaluatorNullable Evaluator { get; }

        /// <summary>
        /// FunctionReader
        /// </summary>
        public FunctionReader FunctionReader { get; }

        /// <summary>
        /// FormulaParser
        /// </summary>
        public FormulaParser FormulaParser { get; }

        /// <summary>
        /// FormulaCache
        /// </summary>
        public FormulaCache FormulaCache { get; }

        /// <summary>
        /// FormulaChecker
        /// </summary>
        public FormulaChecker FormulaChecker { get; }
    }
}
