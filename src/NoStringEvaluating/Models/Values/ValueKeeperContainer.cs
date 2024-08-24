using Microsoft.Extensions.ObjectPool;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Contains list of ids for extra types
/// </summary>
public sealed class ValueKeeperContainer : IDisposable
{
    private readonly List<ValueKeeper> _valueKeeperList = [];
    private ObjectPool<ValueKeeperContainer> _pool;
    private int _index;

    /// <summary>
    /// Set pool to release
    /// </summary>
    public ValueKeeperContainer SetPool(ObjectPool<ValueKeeperContainer> pool)
    {
        _pool = pool;
        return this;
    }

    /// <summary>
    /// Reset keepers index
    /// </summary>
    public ValueKeeperContainer ResetIndex()
    {
        _index = 0;
        return this;
    }

    /// <summary>
    /// Return valueKeeper
    /// </summary>
    public ValueKeeper GetValueKeeper()
    {
        ValueKeeper valueKeeper;

        if (_index >= _valueKeeperList.Count)
        {
            valueKeeper = new();
            _valueKeeperList.Add(valueKeeper);
        }
        else
        {
            valueKeeper = _valueKeeperList[_index];
        }

        _index++;

        return valueKeeper;
    }

    /// <summary>
    /// Release container
    /// </summary>
    public void Release()
    {
        _pool.Return(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _valueKeeperList.ForEach(x => x.Dispose());
        _valueKeeperList.Clear();
    }
}
