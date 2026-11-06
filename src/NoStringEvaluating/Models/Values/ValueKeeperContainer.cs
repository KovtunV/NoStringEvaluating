using Microsoft.Extensions.ObjectPool;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// The state of one evaluation: the keepers of its reference values, its stack and the argument lists of its function calls. Rented from a pool once per evaluation
/// </summary>
public sealed class ValueKeeperContainer : IDisposable
{
    private readonly List<ValueKeeper> _valueKeeperList = [];
    private readonly List<List<InternalEvaluatorValue>> _argsLists = [];
    private ObjectPool<ValueKeeperContainer> _pool;
    private int _index;
    private int _argsDepth;

    /// <summary>
    /// Evaluation stack
    /// </summary>
    internal Stack<InternalEvaluatorValue> Stack { get; } = new();

    /// <summary>
    /// An evaluation holds the container (the thread's own one): a nested evaluation on the thread takes another
    /// </summary>
    internal bool InUse { get; set; }

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
    /// Release container: back to its pool, or free for the thread's next evaluation
    /// </summary>
    public void Release()
    {
        InUse = false;
        _pool?.Return(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _valueKeeperList.ForEach(x => x.Dispose());
        _valueKeeperList.Clear();
    }

    /// <summary>
    /// Ready for a new evaluation: keepers index, stack and argument lists back to the start (an evaluation that threw leaves them dirty)
    /// </summary>
    internal ValueKeeperContainer Reset()
    {
        ResetIndex();
        _argsDepth = 0;

        if (Stack.Count > 0)
        {
            Stack.Clear();
        }

        return this;
    }

    /// <summary>
    /// An empty argument list for a function call; one per nesting depth, so that the arguments of an outer call stay while an inner call runs
    /// </summary>
    internal List<InternalEvaluatorValue> RentArgs()
    {
        if (_argsDepth == _argsLists.Count)
        {
            _argsLists.Add([]);
        }

        var args = _argsLists[_argsDepth];
        _argsDepth++;

        if (args.Count > 0)
        {
            args.Clear();
        }

        return args;
    }

    /// <summary>
    /// The function call is done with its argument list
    /// </summary>
    internal void ReturnArgs(List<InternalEvaluatorValue> args)
    {
        args.Clear();
        _argsDepth--;
    }
}
