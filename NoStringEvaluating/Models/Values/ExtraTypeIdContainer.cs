using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Services.Keepers;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Contains list of ids for extra types
/// </summary>
public class ExtraTypeIdContainer
{
    private ObjectPool<ExtraTypeIdContainer> _pool;

    /// <summary>
    /// Ids
    /// </summary>
    public List<ValueKeeperId> Ids { get; }

    /// <summary>
    /// Contains list of ids for extra types
    /// </summary>
    public ExtraTypeIdContainer()
    {
        Ids = new List<ValueKeeperId>();
    }

    internal ExtraTypeIdContainer SetPool(ObjectPool<ExtraTypeIdContainer> pool)
    {
        _pool = pool;

        return this;
    }

    internal ExtraTypeIdContainer Clear()
    {
        // Prevent dirty collection
        Ids.Clear();

        return this;
    }

    /// <summary>
    /// Releases container
    /// </summary>
    public void Release()
    {
        WordKeeper.Instance.Clear(Ids);
        DateTimeKeeper.Instance.Clear(Ids);
        BooleanKeeper.Instance.Clear(Ids);
        WordListKeeper.Instance.Clear(Ids);
        NumberListKeeper.Instance.Clear(Ids);

        Ids.Clear();

        // Return itself
        _pool.Return(this);
    }
}
