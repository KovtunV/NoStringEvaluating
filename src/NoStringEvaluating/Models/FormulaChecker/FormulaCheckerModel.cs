namespace NoStringEvaluating.Models.FormulaChecker;

/// <summary>
/// Formula checker result item
/// </summary>
public class FormulaCheckerModel(FormulaCheckerMistakeType type, string message, string[] messageParts)
{
    /// <summary>
    /// Mistake's type
    /// </summary>
    public FormulaCheckerMistakeType MistakeType { get; } = type;

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    /// Important message parts
    /// </summary>
    public string[] MessageParts { get; } = messageParts;

    /// <summary>
    /// Formula checker result item
    /// </summary>
    public FormulaCheckerModel(FormulaCheckerMistakeType type, string message)
        : this(type, message, [])
    {
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return Message;
    }

    /// <summary>
    /// Cast to string
    /// </summary>
    public static implicit operator string(FormulaCheckerModel model)
    {
        return model?.Message ?? string.Empty;
    }
}
