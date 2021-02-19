namespace NoStringEvaluating.Models.FormulaChecker
{
    /// <summary>
    /// Checker mistake type
    /// </summary>
    public enum FormulaCheckerMistakeType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Wrong number of brackets
        /// </summary>
        WrongBracketsNumber = 1,

        /// <summary>
        /// Empty brackets
        /// </summary>
        EmptyBrackets = 2,

        /// <summary>
        /// Between prevNode and node must be an operator
        /// </summary>
        OperatorBetweenPrevAndCurrentNode = 3,

        /// <summary>
        /// Between node and nextNode must be an operator
        /// </summary>
        OperatorBetweenCurrentAndNextNode = 4,

        /// <summary>
        /// Between prevNode and nextNode must be an operator, not node"
        /// </summary>
        OperatorBetweenPrevAndNextNode = 5,

        /// <summary>
        /// Before node must be a number or a closed bracket, not prevNodeName"
        /// </summary>
        NumberBeforeNode = 6,

        /// <summary>
        /// After node must be a number or an opened bracket, not nextNodeName"
        /// </summary>
        NumberAfterNode = 7,

        /// <summary>
        /// Empty function's body
        /// </summary>
        EmptyFunctionBody = 8
    }
}
