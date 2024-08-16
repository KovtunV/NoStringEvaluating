using NoStringEvaluating.Contract;
using NoStringEvaluating.Nodes.Common;

namespace NoStringEvaluating.Services.Cache;

/// <summary>
/// Parsed formula cache
/// </summary>
public class FormulaCache(IFormulaParser formulaParser) : IFormulaCache
{
    private readonly object _locker = new();
    private readonly Dictionary<string, FormulaNodes> _formulaNodes = [];
    private readonly IFormulaParser _formulaParser = formulaParser;

    /// <summary>
    /// Return cached formula nodes
    /// </summary>
    public FormulaNodes GetFormulaNodes(string formula)
    {
        lock (_locker)
        {
            if (!_formulaNodes.TryGetValue(formula, out var formulaNodes))
            {
                formulaNodes = _formulaParser.Parse(formula);
                _formulaNodes.Add(formula, formulaNodes);
            }

            return formulaNodes;
        }
    }
}
