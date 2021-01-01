namespace NoStringEvaluating.Contract.Variables
{
    /// <summary>
    /// Variable model
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        double Value { get; set; }
    }
}
