using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// WordFactory
/// </summary>
public readonly struct WordFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// WordFactory
    /// </summary>
    public WordFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Concates two values to word
    /// </summary>
    public InternalEvaluatorValue Concat(InternalEvaluatorValue a, InternalEvaluatorValue b)
    {
        var word = $"{a}{b}";
        return Create(word);
    }

    /// <summary>
    /// Creates default
    /// </summary>
    public InternalEvaluatorValue Empty()
    {
        return Create(string.Empty);
    }

    /// <summary>
    /// Creates word value
    /// </summary>
    public InternalEvaluatorValue Create(string word)
    {
        // Save to keeper
        var idModel = WordKeeper.Instance.Save(word);

        // Save to scouped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
