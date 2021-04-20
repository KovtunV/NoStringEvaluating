namespace NoStringEvaluating.Nodes.Base
{
    /// <summary>
    /// Node type matching
    /// </summary>
    public enum NodeTypeEnum
    {
        /// <summary>
        /// Value
        /// </summary>
        Value,

        /// <summary>
        /// Operator
        /// </summary>
        Operator,

        /// <summary>
        /// Variable
        /// </summary>
        Variable,

        /// <summary>
        /// Function wrapper
        /// </summary>
        FunctionWrapper,

        /// <summary>
        /// Word
        /// </summary>
        Word,

        /// <summary>
        /// Bracket
        /// </summary>
        Bracket,

        /// <summary>
        /// Function char
        /// </summary>
        FunctionChar,

        /// <summary>
        /// Function
        /// </summary>
        Function
    }
}
