using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Operator
    /// </summary>
    public class OperatorNode : IFormulaNode
    {
        /// <summary>
        /// Operator
        /// </summary>
        public Operator OperatorKey { get; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Formula node - Operator
        /// </summary>
        public OperatorNode(Operator operatorKey)
        {
            OperatorKey = operatorKey;
            Priority = GetPriority();
        }

        private int GetPriority()
        {
            var priority = OperatorKey switch
            {
                Operator.Power => 6,

                Operator.Multiply => 5,
                Operator.Divide => 5,

                Operator.Plus => 4,
                Operator.Minus => 4,

                Operator.Less => 3,
                Operator.LessEqual => 3,
                Operator.More => 3,
                Operator.MoreEqual => 3,
                Operator.Equal => 3,
                Operator.NotEqual => 3,

                Operator.And => 2,
                Operator.Or => 1,
                _ => 0
            };

            return priority;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return GetOperationString(OperatorKey);
        }

        private static string GetOperationString(Operator operatorKey)
        {
            return operatorKey switch
            {
                Operator.Plus => "+",
                Operator.Minus => "-",
                Operator.Multiply => "*",
                Operator.Divide => "/",
                Operator.Power => "^",
                Operator.Less => "<",
                Operator.LessEqual => "<=",
                Operator.More => ">",
                Operator.MoreEqual => ">=",
                Operator.Equal => "==",
                Operator.NotEqual => "!=",
                Operator.And => "&&",
                Operator.Or => "||",
                Operator.Undefined => "ERROR",
                _ => "ERROR"
            };
        }
    }
}
