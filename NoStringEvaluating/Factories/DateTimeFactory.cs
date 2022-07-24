using System;
using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Factories;

/// <summary>
/// DateTimeFactory
/// </summary>
public readonly struct DateTimeFactory
{
    private readonly List<ValueKeeperId> _ids;

    /// <summary>
    /// DateTimeFactory
    /// </summary>
    public DateTimeFactory(List<ValueKeeperId> ids)
    {
        _ids = ids;
    }

    /// <summary>
    /// Default
    /// </summary>
    public InternalEvaluatorValue Empty => Create(DateTime.MinValue);

    /// <summary>
    /// Creates dateTime value
    /// </summary>
    public InternalEvaluatorValue Create(DateTime date)
    {
        // Save to keeper
        var idModel = DateTimeKeeper.Instance.Save(date);

        // Save to scouped list
        _ids.Add(idModel);

        // Create value
        return new InternalEvaluatorValue(idModel.Id, idModel.TypeKey);
    }
}
