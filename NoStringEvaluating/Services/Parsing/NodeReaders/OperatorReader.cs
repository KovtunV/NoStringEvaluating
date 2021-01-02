using System;
using System.Collections.Generic;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders
{
    /// <summary>
    /// Operator reader
    /// </summary>
    public static class OperatorReader
    {
        private static readonly string[] _operators;

        static OperatorReader()
        {
            // Pay attention and sort operators :)
            _operators = new[] { "+", "-", "*", "/", "^", "<=", "<", ">=", ">", "==", "!=", "&&", "||" };
        }

        /// <summary>
        /// Read operator
        /// </summary>
        public static bool TryProceedOperator(ICollection<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
        {
            var operatorNameBuilder = new NameBuilder();

            for (int operInd = 0; operInd < _operators.Length; operInd++)
            {
                var operatorName = _operators[operInd];

                // Set operator name
                operatorNameBuilder.Reset(operatorName);

                for (int i = index; i < formula.Length; i++)
                {
                    var ch = formula[i];
       
                    if (operatorNameBuilder.TryRemember(ch))
                    {
                        if (operatorNameBuilder.IsFinished)
                        {
                            var operatorKey = GetOperatorKey(operatorName);
                            var operatorNode = new OperatorNode(operatorKey);
                            nodes.Add(operatorNode);

                            index = i;
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return false;
        }

        private static Operator GetOperatorKey(string name)
        {
            return name switch
            {
                "+" => Operator.Plus,
                "-" => Operator.Minus,
                "*" => Operator.Multiply,
                "/" => Operator.Divide,
                "^" => Operator.Power,
                "<=" => Operator.LessEqual,
                "<" => Operator.Less,
                ">=" => Operator.MoreEqual,
                ">" => Operator.More,
                "==" => Operator.Equal,
                "!=" => Operator.NotEqual,
                "&&" => Operator.And,
                "||" => Operator.Or,

                _ => Operator.Undefined
            };
        }
    }
}
