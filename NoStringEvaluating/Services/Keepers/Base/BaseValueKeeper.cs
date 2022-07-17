using System.Collections.Generic;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Models;

namespace NoStringEvaluating.Services.Keepers.Base;

internal abstract class BaseValueKeeper<TModel>
{
    private readonly object _locker = new object();
    private readonly Dictionary<int, TModel> _values;
    private readonly ValueTypeKey _typeKey;

    protected BaseValueKeeper(ValueTypeKey typeKey)
    {
        _values = new Dictionary<int, TModel>();
        _typeKey = typeKey;
    }

    public ValueKeeperId Save(TModel item)
    {
        lock (_locker)
        {
            for (var i = 0; i < int.MaxValue; i++)
            {
                if (!_values.ContainsKey(i))
                {
                    _values.Add(i, item);
                    return new ValueKeeperId(i, _typeKey);
                }
            }
        }

        throw new ExtraTypeNoFreeIdException(GetType().Name);
    }

    public TModel Get(int id)
    {
        // Dictionary is thread safe for reading
        if (_values.TryGetValue(id, out var res))
        {
            return res;
        }

        throw new ExtraTypeIdNotFoundException(id, GetType().Name);
    }

    public void Clear(List<ValueKeeperId> idModels)
    {
        if (idModels.Count == 0)
        {
            return;
        }

        lock (_locker)
        {
            for (int i = 0; i < idModels.Count; i++)
            {
                var idModel = idModels[i];
                if (idModel.TypeKey != _typeKey)
                    continue;

                _values.Remove(idModel.Id);
            }
        }
    }
}
