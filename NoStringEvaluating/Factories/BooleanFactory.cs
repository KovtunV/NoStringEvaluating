using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// BooleanFactory
/// </summary>
public readonly struct BooleanFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// BooleanFactory
    /// </summary>
    public BooleanFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Creates bool value
    /// </summary>
    public InternalEvaluatorValue Create(bool value)
    {
        // Save to keeper
        var idModel = BooleanKeeper.Instance.Save(value);

        // Save to scouped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
