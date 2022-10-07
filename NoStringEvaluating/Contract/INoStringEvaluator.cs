using System;
using System.Collections.Generic;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Contract;

/// <summary>
/// Expression evaluator
/// </summary>
public interface INoStringEvaluator
{
    #region Number

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    double CalcNumber(FormulaNodes formulaNodes);

    #endregion

    #region Word

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    string CalcWord(FormulaNodes formulaNodes);

    #endregion

    #region DateTime

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    DateTime CalcDateTime(FormulaNodes formulaNodes);

    #endregion

    #region WordList

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<string> CalcWordList(FormulaNodes formulaNodes);

    #endregion

    #region NumberList

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    List<double> CalcNumberList(FormulaNodes formulaNodes);

    #endregion

    #region Boolean

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    bool CalcBoolean(FormulaNodes formulaNodes);

    #endregion

    #region Aggregated

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(string formula, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(FormulaNodes formulaNodes, IVariablesContainer variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(string formula, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(string formula);

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    EvaluatorValue Calc(FormulaNodes formulaNodes);

    #endregion
}