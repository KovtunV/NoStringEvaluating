using System;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services
{
    /// <summary>
    /// Border counter
    /// </summary>
    public class BorderCounter<TNode> where TNode : IFormulaNode
    {
        private readonly Func<TNode, bool> _countFunc;

        /// <summary>
        /// Border count
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Border counter
        /// </summary>
        public BorderCounter(Func<TNode, bool> countFunc)
        {
            _countFunc = countFunc;
            Count = 1;
        }

        /// <summary>
        /// Proceed border
        /// </summary>
        public bool Proceed(TNode node)
        {
            if (_countFunc(node))
            {
                Count++;
            }
            else
            {
                Count--;
            }

            return Count is 0;
        }
    }
}
