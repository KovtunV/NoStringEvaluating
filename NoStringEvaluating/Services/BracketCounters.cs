using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes;

namespace NoStringEvaluating.Services
{
    /// <summary>
    /// Contains bracket counters
    /// </summary>
    public class BracketCounters
    {
        private readonly List<BorderCounter<BracketNode>> _borderCounters;

        /// <summary>
        /// Contains bracket counters
        /// </summary>
        public BracketCounters()
        {
            _borderCounters = new List<BorderCounter<BracketNode>>();
        }

        /// <summary>
        /// Create new counter
        /// </summary>
        public void CreateNewCounter()
        {
            var counter = new BorderCounter<BracketNode>(n => n.Bracket == Bracket.Open);
            _borderCounters.Add(counter);
        }

        /// <summary>
        /// Return true, if bracket area is closed
        /// </summary>
        public bool Proceed(BracketNode node)
        {
            if (_borderCounters.Count is 0)
                return false;

            var last = _borderCounters[^1];
            if (last.Proceed(node))
            {
                _borderCounters.Remove(last);
                return true;
            }

            return false;
        }
    }
}
