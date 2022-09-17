using System.Collections.Generic;
using System.Threading;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Services.Keepers.Base;

internal abstract class BaseValueKeeper<TModel>
{
    private readonly ReaderWriterLockSlim _locker = new();
    private readonly Dictionary<int, TModel> _values;
    private readonly ValueTypeKey _typeKey;
    private int _id;

    protected BaseValueKeeper(ValueTypeKey typeKey)
    {
        _values = new Dictionary<int, TModel>();
        _typeKey = typeKey;
    }

    public ValueKeeperId Save(TModel item)
    {
        _locker.EnterWriteLock();

        try
        {
            _id++;
            _values.Add(_id, item);
            return new ValueKeeperId(_id, _typeKey);
        }
        finally
        {
            _locker.ExitWriteLock();
        }
    }

    public TModel Get(int id)
    {
        _locker.EnterReadLock();

        try
        {
            if (_values.TryGetValue(id, out var res))
            {
                return res;
            }
        }
        finally
        {
            _locker.ExitReadLock();
        }

        throw new ExtraTypeIdNotFoundException(id, GetType().Name);
    }

    public void Clear(List<ValueKeeperId> idModels)
    {
        if (idModels.Count == 0)
        {
            return;
        }

        _locker.EnterWriteLock();

        try
        {
            for (int i = 0; i < idModels.Count; i++)
            {
                var idModel = idModels[i];
                if (idModel.TypeKey != _typeKey)
                    continue;

                _values.Remove(idModel.Id);
            }
        }
        finally
        {
            _locker.ExitWriteLock();
        }
    }
}
