using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// ObjectFactory
/// </summary>
public readonly struct ObjectFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// ObjectFactory
    /// </summary>
    public ObjectFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Creates object value
    /// </summary>
    public InternalEvaluatorValue Create(object objectValue)
    {
        // Save to keeper
        var idModel = ObjectKeeper.Instance.Save(objectValue);

        // Save to scoped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
