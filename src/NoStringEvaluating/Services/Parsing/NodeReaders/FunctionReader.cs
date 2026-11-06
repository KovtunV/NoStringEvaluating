using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;

namespace NoStringEvaluating.Services.Parsing.NodeReaders;

/// <summary>
/// Function reader
/// </summary>
public class FunctionReader : IFunctionReader
{
    private readonly List<IFunction> _functions = [];

    // The functions by the case-insensitive hash of their name, for the names that are identifiers (letters, digits, _ and .); the others, like "!", are scanned
    private readonly Dictionary<int, IFunction[]> _identifierFunctions = [];
    private readonly List<IFunction> _otherFunctions = [];

    /// <summary>
    /// Function reader
    /// </summary>
    public FunctionReader()
    {
        GlobalOptions.FunctionsAssemblies
            .CreateInstances<IFunction>()
            .ForEach(x => AddFunction(x, replace: true));

        GlobalOptions.Functions
            .ForEach(x => AddFunction(x, replace: true));
    }

    /// <summary>
    /// Add function
    /// </summary>
    /// <exception cref="NoStringFunctionException">if exists</exception>
    public void AddFunction(IFunction func, bool replace = false)
    {
        var existedFunc = _functions.Find(f => string.Equals(f.Name, func.Name, StringComparison.OrdinalIgnoreCase));
        if (existedFunc != null)
        {
            if (replace)
            {
                _functions.Remove(existedFunc);
            }
            else
            {
                throw new NoStringFunctionException(func.Name);
            }
        }

        _functions.Add(func);
        Index();
    }

    /// <summary>
    /// Remove function
    /// </summary>
    public void RemoveFunction(string functionName)
    {
        _functions.RemoveAll(q => string.Equals(q.Name, functionName, StringComparison.OrdinalIgnoreCase));
        Index();
    }

    /// <summary>
    /// Read function name
    /// </summary>
    public bool TryProceedFunction(List<BaseFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)
    {
        // Read unary minus
        var localIndex = UnaryMinusReader.ReadUnaryMinus(nodes, formula, index, out var isNegativeLocal);

        // Read negation
        localIndex = NegationReader.ReadNegation(formula, localIndex, out var isNegationLocal);

        // The identifier at the position, looked up once
        var endIndex = localIndex;
        while (endIndex < formula.Length && formula[endIndex].IsSimpleVariable())
        {
            endIndex++;
        }

        if (endIndex > localIndex)
        {
            var name = formula[localIndex..endIndex];

            if (TryFindIdentifierFunction(name, out var function) && IsBracketNext(formula, endIndex))
            {
                var functionNode = new FunctionNode(function, isNegativeLocal, isNegationLocal);
                nodes.Add(functionNode);

                index = endIndex - 1;
                return true;
            }

            return false;
        }

        // A name that is no identifier, like "!": matched char by char
        var functionNameBuilder = default(NameBuilder);
        for (int fInd = 0; fInd < _otherFunctions.Count; fInd++)
        {
            var function = _otherFunctions[fInd];

            // Set function name
            functionNameBuilder.Reset(function.Name);

            for (int i = localIndex; i < formula.Length; i++)
            {
                var ch = formula[i];

                if (functionNameBuilder.TryRemember(ch))
                {
                    if (functionNameBuilder.IsFinished && IsBracketNext(formula, i + 1))
                    {
                        var functionNode = new FunctionNode(function, isNegativeLocal, isNegationLocal);
                        nodes.Add(functionNode);

                        index = i;
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        return false;
    }

    private static bool IsBracketNext(ReadOnlySpan<char> formula, int index)
    {
        for (int i = index; i < formula.Length; i++)
        {
            var ch = formula[i];

            if (ch.IsWhiteSpace())
            {
                continue;
            }

            return ch is OPEN_BRACKET;
        }

        return false;
    }

    private static bool IsIdentifier(string name)
    {
        if (name.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < name.Length; i++)
        {
            if (!name[i].IsSimpleVariable())
            {
                return false;
            }
        }

        return true;
    }

    private static int HashOf(ReadOnlySpan<char> name)
    {
        return string.GetHashCode(name, StringComparison.OrdinalIgnoreCase);
    }

    private bool TryFindIdentifierFunction(ReadOnlySpan<char> name, out IFunction function)
    {
        if (_identifierFunctions.TryGetValue(HashOf(name), out var candidates))
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                if (name.Equals(candidates[i].Name, StringComparison.OrdinalIgnoreCase))
                {
                    function = candidates[i];
                    return true;
                }
            }
        }

        function = null;
        return false;
    }

    private void Index()
    {
        _identifierFunctions.Clear();
        _otherFunctions.Clear();

        for (int i = 0; i < _functions.Count; i++)
        {
            var function = _functions[i];

            if (!IsIdentifier(function.Name))
            {
                _otherFunctions.Add(function);
                continue;
            }

            var hash = HashOf(function.Name);
            _identifierFunctions[hash] = _identifierFunctions.TryGetValue(hash, out var same) ? [.. same, function] : [function];
        }
    }

    private const char OPEN_BRACKET = '(';
}
