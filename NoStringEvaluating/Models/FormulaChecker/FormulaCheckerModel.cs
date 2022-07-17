using System;

namespace NoStringEvaluating.Models.FormulaChecker;

/// <summary>
/// Formula checker result item
/// </summary>
public class FormulaCheckerModel
{
    /// <summary>
    /// Mistake's type
    /// </summary>
    public FormulaCheckerMistakeType MistakeType { get; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Important message parts
    /// </summary>
    public string[] MessageParts { get; }

    /// <summary>
    /// Formula checker result item
    /// </summary>
    public FormulaCheckerModel(FormulaCheckerMistakeType type, string message, string[] messageParts)
    {
        MistakeType = type;
        Message = message;
        MessageParts = messageParts;
    }

    /// <summary>
    /// Formula checker result item
    /// </summary>
    public FormulaCheckerModel(FormulaCheckerMistakeType type, string message)
        : this(type, message, Array.Empty<string>())
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
