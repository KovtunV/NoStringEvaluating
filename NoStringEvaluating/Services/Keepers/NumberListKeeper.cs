using System.Collections.Generic;
using NoStringEvaluating.Exceptions;

namespace NoStringEvaluating.Services.Keepers
{
    internal class NumberListKeeper
    {
        private readonly object _locker = new object();
        private readonly Dictionary<int, List<double>> _numberLists;

        private NumberListKeeper()
        {
            _numberLists = new Dictionary<int, List<double>>();
        }

        public int Save(List<double> item)
        {
            lock (_locker)
            {
                for (var i = 0; i < int.MaxValue; i++)
                {
                    if (!_numberLists.ContainsKey(i))
                    {
                        _numberLists.Add(i, item);
                        return i;
                    }
                }
            }

            throw new ExtraTypeNoFreeIdException(nameof(NumberListKeeper));
        }

        public List<double> Get(int id)
        {
            // Dictionary is thread safe for reading
            if (_numberLists.TryGetValue(id, out var res))
            {
                return res;
            }

            throw new ExtraTypeIdNotFoundException(id, nameof(NumberListKeeper));
        }

        public void Clear(List<int> ids)
        {
            if (ids.Count == 0)
            {
                return;
            }

            lock (_locker)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    _numberLists.Remove(ids[i]);
                }
            }
        }

        #region Static

        internal static NumberListKeeper Instance { get; }

        static NumberListKeeper()
        {
            Instance = new NumberListKeeper();
        }

        #endregion
    }
}
