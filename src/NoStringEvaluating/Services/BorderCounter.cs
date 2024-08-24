using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services;

/// <summary>
/// Border counter
/// </summary>
public class BorderCounter<TNode>(Func<TNode, bool> countFunc)
    where TNode : BaseFormulaNode
{
    private readonly Func<TNode, bool> _countFunc = countFunc;

    /// <summary>
    /// Border count
    /// </summary>
    public int Count { get; private set; } = 1;

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
