using System;
using System.Collections.Generic;
using NoStringEvaluating.Exceptions;

namespace NoStringEvaluating.Services.Keepers
{
    internal class DateTimeKeeper
    {
        private readonly object _locker = new object();
        private readonly Dictionary<int, DateTime> _dates;

        private DateTimeKeeper()
        {
            _dates = new Dictionary<int, DateTime>();
        }

        public int Save(DateTime item)
        {
            lock (_locker)
            {
                for (var i = 0; i < int.MaxValue; i++)
                {
                    if (!_dates.ContainsKey(i))
                    {
                        _dates.Add(i, item);
                        return i;
                    }
                }
            }

            throw new ExtraTypeNoFreeIdException(nameof(DateTimeKeeper));
        }
        
        public DateTime Get(int id)
        {
            // Dictionary is thread safe for reading
            if (_dates.TryGetValue(id, out var res))
            {
                return res;
            }

            throw new ExtraTypeIdNotFoundException(id, nameof(DateTimeKeeper));
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
                    _dates.Remove(ids[i]);
                }
            }
        }

        #region Static

        internal static DateTimeKeeper Instance { get; }

        static DateTimeKeeper()
        {
            Instance = new DateTimeKeeper();
        }

        #endregion
    }
}
