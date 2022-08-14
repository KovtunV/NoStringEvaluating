using System;
using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Word reader
/// </summary>
public static class WordReader
{
    private static readonly HashSet<char> _quotes;

    static WordReader()
    {
        _quotes = new[] { '\'', '"' }.ToHashSet();
    }

    /// <summary>
    /// Read word
    /// </summary>
    public static bool TryProceedWord(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out _);

        // Check out of range
        if (localIndex >= formula.Length)
            return false;

        // Read word
        if (!_quotes.Contains(formula[localIndex]))
        {
            return false;
        }

        // Skip start char
        localIndex++;

        var wordBuilder = new IndexWatcher();
        for (int i = localIndex; i < formula.Length; i++)
        {
            var ch = formula[i];

            if (_quotes.Contains(ch))
            {
                var wordSpan = formula.Slice(wordBuilder.StartIndex.GetValueOrDefault(), wordBuilder.Length);
                var word = wordSpan.ToString();

                var varNode = new WordNode(word);
                nodes.Add(varNode);

                index = i;
                return true;
            }

            wordBuilder.Remember(i);
        }

        return false;
    }
}
