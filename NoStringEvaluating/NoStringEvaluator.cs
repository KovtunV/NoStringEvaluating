using System;
using System.Collections.Generic;
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
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumber(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcNumber(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public double CalcNumber(FormulaNodes formulaNodes)
        {
            return OnCalcNumber(formulaNodes.Nodes, default);
        }

        #endregion

        #region WordEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWord(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcWord(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public string CalcWord(FormulaNodes formulaNodes)
        {
            return OnCalcWord(formulaNodes.Nodes, default);
        }

        #endregion

        #region DateTimeEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcDateTime(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcDateTime(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public DateTime CalcDateTime(FormulaNodes formulaNodes)
        {
            return OnCalcDateTime(formulaNodes.Nodes, default);
        }

        #endregion

        #region WordListEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcWordList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcWordList(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<string> CalcWordList(FormulaNodes formulaNodes)
        {
            return OnCalcWordList(formulaNodes.Nodes, default);
        }

        #endregion

        #region NumberListEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(string formula, IDictionary<string, EvaluatorValue> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return OnCalcNumberList(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return OnCalcNumberList(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ExtraTypeNoFreeIdException"></exception>
        /// <exception cref="ExtraTypeIdNotFoundException"></exception>
        public List<double> CalcNumberList(FormulaNodes formulaNodes)
        {
            return OnCalcNumberList(formulaNodes.Nodes, default);
        }

        #endregion

        #region AggregatedEndpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
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
        /// <exception cref="VariableNotFoundException"></exception>
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
        /// <exception cref="VariableNotFoundException"></exception>
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
        /// <exception cref="VariableNotFoundException"></exception>
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
        /// <exception cref="VariableNotFoundException"></exception>
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
        /// <exception cref="VariableNotFoundException"></exception>
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

        private double OnCalcNumber(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            double res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        private string OnCalcWord(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            string res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        private DateTime OnCalcDateTime(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            DateTime res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        private List<string> OnCalcWordList(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            List<string> res = CalcInternal(nodes, variables, idContainer);

            return res;
        }

        private List<double> OnCalcNumberList(List<BaseFormulaNode> nodes, VariablesSource variables)
        {
            // Rent
            using var idContainer = GetIdContainer();

            // Calculate with internal struct
            List<double> res = CalcInternal(nodes, variables, idContainer);

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

        #endregion

        #region CalcInternal

        private InternalEvaluatorValue CalcInternal(List<BaseFormulaNode> nodes, VariablesSource variables, ExtraTypeIdContainer idContainer)
        {
            // If no nodes return default value
            if (nodes.Count == 0)
            {
                return double.NaN;
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
                }
                else if (node.TypeKey == NodeTypeEnum.Operator)
                {
                    var mathOperationNode = (OperatorNode)node;
                    InternalEvaluatorValue a;
                    InternalEvaluatorValue b;

                    switch (mathOperationNode.OperatorKey)
                    {
                        // Math
                        case Operator.Multiply: stack.Push(stack.Pop() * stack.Pop()); break;
                        case Operator.Divide:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b / a); break;
                        case Operator.Plus:
                            a = stack.Pop();
                            b = stack.Pop();

                            if (a.IsWord || b.IsWord)
                            {
                                stack.Push(factory.Word().Concat(b, a));
                            }
                            else if (a.IsNumber && b.IsDateTime)
                            {
                                stack.Push(factory.DateTime().Create(b.GetDateTime().AddDays(a.Number)));
                            }
                            else if (a.IsDateTime && b.IsNumber)
                            {
                                stack.Push(factory.DateTime().Create(a.GetDateTime().AddDays(b.Number)));
                            }
                            else
                            {
                                stack.Push(a + b);
                            }

                            break;
                        case Operator.Minus:
                            a = stack.Pop();
                            b = stack.Pop();

                            if (a.IsNumber && b.IsDateTime)
                            {
                                stack.Push(factory.DateTime().Create(b.GetDateTime().AddDays(-a.Number)));
                            }
                            else if (a.IsDateTime && b.IsDateTime)
                            {
                                stack.Push(b.GetDateTime().Subtract(a.GetDateTime()).TotalDays);
                            }
                            else
                            {
                                stack.Push(b - a);
                            }

                            break;
                        case Operator.Power:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(Math.Pow(b, a)); break;

                        // Logic
                        case Operator.Less:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b < a ? 1 : 0); break;
                        case Operator.LessEqual:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b <= a ? 1 : 0); break;
                        case Operator.More:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b > a ? 1 : 0); break;
                        case Operator.MoreEqual:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b >= a ? 1 : 0); break;
                        case Operator.Equal:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(Math.Abs(b - a) < NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;
                        case Operator.NotEqual:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(Math.Abs(b - a) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;

                        // Additional logic
                        case Operator.And:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance && Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;
                        case Operator.Or:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance || Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance ? 1 : 0); break;
                    }
                }
                else if (node.TypeKey == NodeTypeEnum.FunctionWrapper)
                {
                    var functionWrapper = (FunctionWrapperNode)node;
                    var functionVal = CalcFunction(functionWrapper, variables, idContainer);

                    stack.Push(functionVal);
                }
                else if (node.TypeKey == NodeTypeEnum.Value)
                {
                    var valNode = (ValueNode)node;
                    stack.Push(valNode.Value);
                }
                else if (node.TypeKey == NodeTypeEnum.Word)
                {
                    var wordNode = (WordNode)node;
                    var wordItem = factory.Word().Create(wordNode.Word);

                    stack.Push(wordItem);
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

            for (int i = 0; i < functionWrapper.FunctionArgumentNodes.Count; i++)
            {
                var subNodes = functionWrapper.FunctionArgumentNodes[i];
                var subRes = CalcInternal(subNodes, variables, idContainer);
                args.Add(subRes);
            }

            var factory = GetFactory(idContainer);
            var res = functionWrapper.FunctionNode.Function.Execute(args, factory);
            if (functionWrapper.FunctionNode.IsNegative && res.IsNumber)
            {
                res = res.Number * -1;
            }

            // Clear collection
            args.Clear();

            // Return to a pool
            _argsPool.Return(args);

            return res;
        }

        #endregion

        private ExtraTypeIdContainer GetIdContainer()
        {
            return _extraTypeIdContainerPool.Get()
                .SetPool(_extraTypeIdContainerPool)
                .Clear();
        }

        private ValueFactory GetFactory(ExtraTypeIdContainer idContainer)
        {
            return new ValueFactory(idContainer);
        }
    }
}
