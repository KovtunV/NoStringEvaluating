using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// NumberListFactory
/// </summary>
public readonly struct NumberListFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// NumberListFactory
    /// </summary>
    public NumberListFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Creates default
    /// </summary>
    public InternalEvaluatorValue Empty()
    {
        return Create(new List<double>());
    }

    /// <summary>
    /// Creates double List value
    /// </summary>
    public InternalEvaluatorValue Create(List<double> numberList)
    {
        // Save to keeper
        var idModel = NumberListKeeper.Instance.Save(numberList);

        // Save to scouped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
