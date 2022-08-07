using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// WordListFactory
/// </summary>
public readonly struct WordListFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// WordListFactory
    /// </summary>
    public WordListFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Creates string List value
    /// </summary>
    public InternalEvaluatorValue Create(List<string> wordList)
    {
        // Save to keeper
        var idModel = WordListKeeper.Instance.Save(wordList);

        // Save to scoped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
