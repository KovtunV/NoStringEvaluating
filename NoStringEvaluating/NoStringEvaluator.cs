using System;
using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Variables;

namespace NoStringEvaluating
{
    /// <summary>
    /// Math expression evaluator
    /// </summary>
    public class NoStringEvaluator : INoStringEvaluator
    {
        private readonly ObjectPool<Stack<double>> _stackPool;
        private readonly ObjectPool<List<double>> _argsPool;
        private readonly IFormulaCache _formulaCache;

        /// <summary>
        /// Math expression evaluator
        /// </summary>
        public NoStringEvaluator(ObjectPool<Stack<double>> stackPool, ObjectPool<List<double>> argsPool, IFormulaCache formulaCache)
        {
            _stackPool = stackPool;
            _argsPool = argsPool;
            _formulaCache = formulaCache;
        }

        #region Endpoints

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(string formula, IVariablesContainer variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return CalcInternal(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(FormulaNodes formulaNodes, IVariablesContainer variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return CalcInternal(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(string formula, IDictionary<string, double> variables)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            var wrapper = VariablesSource.Create(variables);
            return CalcInternal(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(FormulaNodes formulaNodes, IDictionary<string, double> variables)
        {
            var wrapper = VariablesSource.Create(variables);
            return CalcInternal(formulaNodes.Nodes, wrapper);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(string formula)
        {
            var formulaNodes = _formulaCache.GetFormulaNodes(formula);
            return CalcInternal(formulaNodes.Nodes, default);
        }

        /// <summary>
        /// Calculate formula
        /// </summary>
        /// <exception cref="VariableNotFoundException"></exception>
        public double Calc(FormulaNodes formulaNodes)
        {
            return CalcInternal(formulaNodes.Nodes, default);
        }

        #endregion

        #region Calculating

        private double CalcInternal(List<IFormulaNode> nodes, VariablesSource variables)
        {
            // If no nodes return default value
            if (nodes.Count == 0)
            {
                return default;
            }

            // Rent stack
            var stack = _stackPool.Get();

            // Prevent dirty collection
            if (stack.Count > 0)
            {
                stack.Clear();
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node is FunctionWrapperNode functionWrapper)
                {
                    var functionVal = CalcFunction(functionWrapper, variables);
                    stack.Push(functionVal);
                }
                else if (node is ValueNode valNode)
                {
                    stack.Push(valNode.Value);
                }
                else if (node is VariableNode variableNode)
                {
                    var val = variables.GetValue(variableNode.Name);

                    if (variableNode.IsNegative)
                    {
                        val *= -1;
                    }

                    stack.Push(val);
                }
                else if (node is OperatorNode mathOperationNode)
                {
                    double a;
                    double b;

                    switch (mathOperationNode.OperatorKey)
                    {
                        // Math
                        case Operator.Multiply: stack.Push(stack.Pop() * stack.Pop()); break;
                        case Operator.Divide:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b / a); break;
                        case Operator.Plus: stack.Push(stack.Pop() + stack.Pop()); break;
                        case Operator.Minus:
                            a = stack.Pop();
                            b = stack.Pop();
                            stack.Push(b - a); break;
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
            }

            var res = stack.Pop();

            // Return to a pool
            _stackPool.Return(stack);

            return res;
        }

        private double CalcFunction(FunctionWrapperNode functionWrapper, VariablesSource variables)
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
                var subRes = CalcInternal(subNodes, variables);
                args.Add(subRes);
            }

            var res = functionWrapper.FunctionNode.Function.Execute(args);
            if (functionWrapper.FunctionNode.IsNegative)
            {
                res *= -1;
            }

            // Clear collection
            args.Clear();

            // Return to a pool
            _argsPool.Return(args);

            return res;
        }

        #endregion

    }
}
