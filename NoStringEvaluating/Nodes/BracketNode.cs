using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Nodes
{
    /// <summary>
    /// Formula node - Bracket
    /// </summary>
    public class BracketNode : IFormulaNode
    {
        /// <summary>
        /// Bracket
        /// </summary>
        public Bracket Bracket { get; }

        /// <summary>
        /// Formula node - bracket
        /// </summary>
        public BracketNode(Bracket bracket)
        {
            Bracket = bracket;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return GetBracketString(Bracket);
        }

        private static string GetBracketString(Bracket bracket)
        {
            return bracket switch
            {
                Bracket.Open => "(",
                Bracket.Close => ")",
                Bracket.Undefined => "ERROR",
                _ => "ERROR"
            };
        }
    }
}
