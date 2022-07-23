namespace NoStringEvaluating.Models.Values
{
    /// <summary>
    /// Type matching
    /// </summary>
    public enum ValueTypeKey
    {
        /// <summary>
        /// NULL, must be zero so that if default(EvaluatorValue) is used the typekeyis automatically null
        /// </summary>
        Null = 0,

        /// <summary>
        /// Number
        /// </summary>
        Number,

        /// <summary>
        /// Word
        /// </summary>
        Word,

        /// <summary>
        /// DateTime
        /// </summary>
        DateTime,

        /// <summary>
        /// Word list
        /// </summary>
        WordList,

        /// <summary>
        /// Number list
        /// </summary>
        NumberList,

        /// <summary>
        /// Boolean
        /// </summary>
        Boolean,


    }
}