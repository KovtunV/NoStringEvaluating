using System.Collections.Generic;
using NoStringEvaluating.Exceptions;

namespace NoStringEvaluating.Services.Keepers
{
    internal class WordKeeper
    {
        private readonly object _locker = new object();
        private readonly Dictionary<int, string> _words;

        private WordKeeper()
        {
            _words = new Dictionary<int, string>();
        }

        public int Save(string item)
        {
            lock (_locker)
            {
                for (var i = 0; i < int.MaxValue; i++)
                {
                    if (!_words.ContainsKey(i))
                    {
                        _words.Add(i, item);
                        return i;
                    }
                }
            }

            throw new ExtraTypeNoFreeIdException(nameof(WordKeeper));
        }

        public string Get(int id)
        {
            // Dictionary is thread safe for reading
            if (_words.TryGetValue(id, out var res))
            {
                return res;
            }

            throw new ExtraTypeIdNotFoundException(id, nameof(WordKeeper));
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
                    _words.Remove(ids[i]);
                }
            }
        }

        #region Static

        internal static WordKeeper Instance { get; }

        static WordKeeper()
        {
            Instance = new WordKeeper();
        }

        #endregion
    }
}
