using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Value;
using NoStringEvaluating.Services.Variables;

namespace NoStringEvaluating
{
    /// <summary>
    /// Expression evaluator
    /// </summary>
    public class NoStringEvaluator : INoStringEvaluator
    {
        private readonly ObjectPool<Stack<InternalEvaluatorValue>> _stackPool;
        private readonly ObjectPool<List<InternalEvaluatorValue>> _argsPool;
        private readonly ObjectPool<ExtraTypeIdContainer> _extraTypeIdContainerPool;
        private readonly IFormulaCache _formulaCache;

        /// <summary>
        /// Expression evaluator
        /// </summary>
        public NoStringEvaluator(ObjectPool<Stack<InternalEvaluatorValue>> stackPool, ObjectPool<List<InternalEvaluatorValue>> argsPool, ObjectPool<ExtraTypeIdContainer> extraTypeIdContainerPool, IFormulaCache formulaCache)
        {
            _stackPool = stackPool;
            _argsPool = argsPool;
            _extraTypeIdContainerPool = extraTypeIdContainerPool;
            _formulaCache = formulaCache;
        }

        #region Endpoints

        #region NumberEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcNumber(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double? CalcNumber(FormulaNodes formulaNodes)
        {
            return OnCalcNumber(formulaNodes.Nodes, default);
        }

        #endregion

        #region WordEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcWord(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string? CalcWord(FormulaNodes formulaNodes)
        {
            return OnCalcWord(formulaNodes.Nodes, default);
        }

        #endregion

        #region DateTimeEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcDateTime(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime? CalcDateTime(FormulaNodes formulaNodes)
        {
            return OnCalcDateTime(formulaNodes.Nodes, default);
        }

        #endregion

        #region WordListEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcWordList(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string>? CalcWordList(FormulaNodes formulaNodes)
        {
            return OnCalcWordList(formulaNodes.Nodes, default);
        }

        #endregion

        #region NumberListEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcNumberList(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double>? CalcNumberList(FormulaNodes formulaNodes)
        {
            return OnCalcNumberList(formulaNodes.Nodes, default);
        }

        #endregion

        #region BooleanEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcBoolean(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcBoolean(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcBoolean(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcBoolean(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcBoolean(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public bool? CalcBoolean(FormulaNodes formulaNodes)
        {
            return OnCalcBoolean(formulaNodes.Nodes, default);
        }

        #endregion

        #region AggregatedEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcAggregated(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcAggregated(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcAggregated(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcAggregated(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcAggregated(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>

        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public EvaluatorValue Calc(FormulaNodes formulaNodes)
        {
            return OnCalcAggregated(formulaNodes.Nodes, default);
        }

        #endregion

        #endregion

        #region OnCalc

        private double? OnCalcNumber(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        private string? OnCalcWord(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            // Result
            return WordFormatter.Format(res.GetWord());
        }

        private DateTime? OnCalcDateTime(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            // Result
            return res;
        }

        private List<string>? OnCalcWordList(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            // Result
            return WordFormatter.Format(res.GetWordList());
        }

        private List<double>? OnCalcNumberList(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            // Result
            return res;
        }

        private bool? OnCalcBoolean(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            var res = CalcInternal(nodes, variables, idContainer);

            // Result
            return res;
        }

        private EvaluatorValue OnCalcAggregated(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            EvaluatorValue res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        #region VariableValuesUsed

        /// <summary>
        /// Return list of variable values used by formula
        /// </summary>
        public List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
        }
        /// <summary>
        /// Return list of variable values used by formula
        /// </summary>
        public List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return VariableValuesUsedByFormula(formulaNodes, variables);
        }

        /// <summary>
        /// Return list of variable values used by formula
        /// </summary>
        public List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return VariableValuesUsedByFormulaInternal(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Return list of variable values used by formula
        /// </summary>
        public List<(string name, EvaluatorValue value)> VariableValuesUsedByFormula(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return VariableValuesUsedByFormula(formulaNodes, variables);
        }

        /// <summary>
        /// Routine to check what variables the calculation actually receives and uses. Without doing the actual calculation. Usefull for debugging and logging
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="variables"></param>
        private List<(string name, EvaluatorValue value)> VariableValuesUsedByFormulaInternal(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            var res = new List<(string, EvaluatorValue)>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node.TypeKey == NodeTypeEnum.Variable)
                {
                    var variableNode = (VariableNode)node;
                    res.Add((variableNode.Name, variables.GetValue(variableNode.Name)));
                }
            }
            return res;
        }
        #endregion(string,EvaluatorValue)

        #endregion

        #region CalcInternal

        private InternalEvaluatorValue CalcInternal(List<BaseFormulaNode> nodes, VariablesSource variables, ExtraTypeIdContainer idContainer)
        {
            // If no nodes return NULL
            if (nodes.Count == 0)
            {
                return default(InternalEvaluatorValue);
            }

            // Rent stack
            var stack = _stackPool.Get();

            // Prevent dirty collection
            if (stack.Count > 0)
            {
                stack.Clear();
            }

            var factory = GetFactory(idContainer);

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node.TypeKey == NodeTypeEnum.Variable)
                {
                    var variableNode = (VariableNode)node;
                    var argumentVal = variables.GetValue(variableNode.Name);

                    var val = factory.Create(argumentVal);
                    if (variableNode.IsNegative && val.IsNumber)
                    {
                        val = val.Number * -1;
                    }

                    stack.Push(val);
                } else if (node.TypeKey == NodeTypeEnum.Operator)
                {
                    var mathOperationNode = (OperatorNode)node;
                    var a = stack.Pop();
                    var b = stack.Pop();
                    // any null values in the operation make for special handling
                    if (a.IsNull || b.IsNull)
                    {
                        switch (mathOperationNode.OperatorKey)
                        {
                            // Equal and UnEqual work
                            case Operator.Equal: stack.Push(factory.Create(new EvaluatorValue(a.IsNull && b.IsNull))); break;
                            case Operator.NotEqual: stack.Push(factory.Create(new EvaluatorValue(a.IsNull != b.IsNull))); break;
                            // all others operators we set the result to null
                            default: stack.Push(default); break;
                        }

                    } else // two non null variables
                    {
                        switch (mathOperationNode.OperatorKey)
                        {
                            // Math
                            case Operator.Multiply: stack.Push(a * b); break;
                            case Operator.Divide:
                                stack.Push(b / a); break;
                            case Operator.Plus:
                                if (a.IsWord || b.IsWord)
                                {
                                    stack.Push(factory.Word().Concat(b, a));
                                } else if (a.IsNumber && b.IsDateTime)
                                {
                                    stack.Push(factory.DateTime().Create(b.GetDateTime().AddDays(a.Number)));
                                } else if (a.IsDateTime && b.IsNumber)
                                {
                                    stack.Push(factory.DateTime().Create(a.GetDateTime().AddDays(b.Number)));
                                } else
                                {
                                    stack.Push(a + b);
                                }

                                break;
                            case Operator.Minus:
                                if (a.IsNumber && b.IsDateTime)
                                {
                                    stack.Push(factory.DateTime().Create(b.GetDateTime().AddDays(-a.Number)));
                                } else if (a.IsDateTime && b.IsDateTime)
                                {
                                    stack.Push(b.GetDateTime().Subtract(a.GetDateTime()).TotalDays);
                                } else
                                {
                                    stack.Push(b - a);
                                }
                                break;
                            case Operator.Power:
                                stack.Push(Math.Pow(b, a)); break;

                            // Logic
                            case Operator.Less:
                                if (a.IsDateTime && b.IsDateTime)
                                {
                                    stack.Push(b.GetDateTime() < a.GetDateTime() ? 1 : 0);
                                } else
                                {
                                    stack.Push(b < a ? 1 : 0);
                                }
                                break;
                            case Operator.LessEqual:
                                if (a.IsDateTime && b.IsDateTime)
                                {
                                    stack.Push(b.GetDateTime() <= a.GetDateTime() ? 1 : 0);
                                } else
                                {
                                    stack.Push(b <= a ? 1 : 0);
                                }
                                break;
                            case Operator.More:
                                if (a.IsDateTime && b.IsDateTime)
                                {
                                    stack.Push(b.GetDateTime() > a.GetDateTime() ? 1 : 0);
                                } else
                                {
                                    stack.Push(b > a ? 1 : 0);
                                }
                                break;
                            case Operator.MoreEqual:
                                if (a.IsDateTime && b.IsDateTime)
                                {
                                    stack.Push(b.GetDateTime() >= a.GetDateTime() ? 1 : 0);
                                } else
                                {
                                    stack.Push(b >= a ? 1 : 0);
                                }
                                break;
                            case Operator.Equal:
                                if (a.IsNumber || b.IsNumber) stack.Push(Math.Abs(b - a) < NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0);
                                else stack.Push(factory.Create(new EvaluatorValue(a.Equals(b))));
                                break;
                            case Operator.NotEqual:
                                if (a.IsNumber || b.IsNumber) stack.Push(Math.Abs(b - a) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0);
                                else stack.Push(factory.Create(new EvaluatorValue(!a.Equals(b)))); break;
                            // Additional logic
                            case Operator.And:
                                stack.Push(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance && Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;
                            case Operator.Or:
                                stack.Push(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance || Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;
                            // something really wrong
                            default: throw new Exception("Unsupported operator => " + mathOperationNode.OperatorKey);
                        }
                    }
                } else if (node.TypeKey == NodeTypeEnum.FunctionWrapper)
                {
                    var functionWrapper = (FunctionWrapperNode)node;
                    var functionVal = CalcFunction(functionWrapper, variables, idContainer);

                    stack.Push(functionVal);
                } else if (node.TypeKey == NodeTypeEnum.Number)
                {
                    var valNode = (NumberNode)node;
                    stack.Push(valNode.Number);
                } else if (node.TypeKey == NodeTypeEnum.Word)
                {
                    var wordNode = (WordNode)node;
                    var wordItem = factory.Word().Create(wordNode.Word);

                    stack.Push(wordItem);
                } else if (node.TypeKey == NodeTypeEnum.WordList)
                {
                    var wordListNode = (WordListNode)node;
                    var wordListItem = factory.WordList().Create(wordListNode.WordList);

                    stack.Push(wordListItem);
                } else if (node.TypeKey == NodeTypeEnum.NumberList)
                {
                    var numberListNode = (NumberListNode)node;
                    var numberListItem = factory.NumberList().Create(numberListNode.NumberList);

                    stack.Push(numberListItem);
                } else if (node.TypeKey == NodeTypeEnum.NullConst)
                {
                    stack.Push(default);
                }
            }

            // Result
            var res = stack.Pop();

            // Return to a pool
            _stackPool.Return(stack);

            return res;
        }

        private InternalEvaluatorValue CalcFunction(FunctionWrapperNode functionWrapper, VariablesSource variables, ExtraTypeIdContainer idContainer)
        {
            var args = _argsPool.Get();

            // Prevent dirty collection
            if (args.Count > 0)
            {
                args.Clear();
            }

            bool hasNullArgs = false;
            for (int i = 0; i < functionWrapper.FunctionArgumentNodes.Count; i++)
            {
                var subNodes = functionWrapper.FunctionArgumentNodes[i];
                var subRes = CalcInternal(subNodes, variables, idContainer);
                if (subRes.IsNull) hasNullArgs = true;
                args.Add(subRes);
            }

            var factory = GetFactory(idContainer);
            var func = functionWrapper.FunctionNode.Function;

            var res = default(InternalEvaluatorValue);

            // if we have NULL arguments and the function does not handle this we skip the functiona and return null
            if (func.CanHandleNullArguments || !hasNullArgs)
            {
                res = func.Execute(args, factory);
                if (functionWrapper.FunctionNode.IsNegative && res.IsNumber)
                {
                    res = res.Number * -1;
                }
            }

            // Clear collection
            args.Clear();

            // Return to a pool
            _argsPool.Return(args);

            return res;
        }

        #endregion

        private ExtraTypeIdContainerReleaser GetIdContainer()
        {
            return _extraTypeIdContainerPool
                .Get()
                .SetPool(_extraTypeIdContainerPool)
                .Clear();
        }

        private ValueFactory GetFactory(ExtraTypeIdContainer idContainer)
        {
            return new ValueFactory(idContainer);
        }

    }
}
