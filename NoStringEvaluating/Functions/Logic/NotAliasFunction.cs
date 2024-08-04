using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Logic;

/// <summary>
/// Function - not
/// </summary>
public sealed class NotAliasFunction : IFunction
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; } = "!";

    /// <summary>
    /// Can handle IsNull arguments?
    /// </summary>
    public bool CanHandleNullArguments { get; }

    /// <summary>
    /// Evaluate value
    /// </summary>
    public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
    {
        if (args.Count == 0)
        {
            return default;
        }

        return factory.Boolean.Create(!args[0].Boolean);
    }
}
