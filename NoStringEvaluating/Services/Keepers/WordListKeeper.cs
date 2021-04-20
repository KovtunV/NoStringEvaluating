using System.Collections.Generic;
using NoStringEvaluating.Exceptions;

namespace NoStringEvaluating.Services.Keepers
{
    internal class WordListKeeper
    {
        private readonly object _locker = new object();
        private readonly Dictionary<int, List<string>> _wordLists;

        private WordListKeeper()
        {
            _wordLists = new Dictionary<int, List<string>>();
        }

        public int Save(List<string> item)
        {
            lock (_locker)
            {
                for (var i = 0; i < int.MaxValue; i++)
                {
                    if (!_wordLists.ContainsKey(i))
                    {
                        _wordLists.Add(i, item);
                        return i;
                    }
                }
            }

            throw new ExtraTypeNoFreeIdException(nameof(WordListKeeper));
        }

        public List<string> Get(int id)
        {
            // Dictionary is thread safe for reading
            if (_wordLists.TryGetValue(id, out var res))
            {
                return res;
            }

            throw new ExtraTypeIdNotFoundException(id, nameof(WordListKeeper));
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
                    _wordLists.Remove(ids[i]);
                }
            }
        }

        #region Static

        internal static WordListKeeper Instance { get; }

        static WordListKeeper()
        {
            Instance = new WordListKeeper();
        }

        #endregion
    }
}
