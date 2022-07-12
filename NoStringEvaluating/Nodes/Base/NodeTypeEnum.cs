namespace NoStringEvaluating.Nodes.Base;

/// <summary>
/// Node type matching
/// </summary>
public enum NodeTypeEnum
{
    /// <summary>
    /// Number
    /// </summary>
    Number,

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
    /// WordList
    /// </summary>
    WordList,

    /// <summary>
    /// NumberList
    /// </summary>
    NumberList,

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
