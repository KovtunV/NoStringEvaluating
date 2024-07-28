using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Contract.Variables;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Nodes;
using NoStringEvaluating.Nodes.Base;
using NoStringEvaluating.Nodes.Common;
using NoStringEvaluating.Services.Cache;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Services.Parsing.NodeReaders;
using NoStringEvaluating.Services.Value;
using NoStringEvaluating.Services.Variables;

using static NoStringEvaluating.Services.OperationProcessor;

namespace NoStringEvaluating;

/// <summary>
/// Expression evaluator
/// </summary>
public class NoStringEvaluator : INoStringEvaluator
{
    private readonly ObjectPool<Stack<InternalEvaluatorValue>> _stackPool;
    private readonly ObjectPool<List<InternalEvaluatorValue>> _argsPool;
    private readonly ObjectPool<ValueKeeperContainer> _valueKeeperContainerPool;
    private readonly IFormulaCache _formulaCache;

    /// <summary>
    /// Expression evaluator
    /// </summary>
    public NoStringEvaluator(
        ObjectPool<Stack<InternalEvaluatorValue>> stackPool,
        ObjectPool<List<InternalEvaluatorValue>> argsPool,
        ObjectPool<ValueKeeperContainer> valueKeeperContainerPool,
        IFormulaCache formulaCache)
    {
        _stackPool = stackPool;
        _argsPool = argsPool;
        _valueKeeperContainerPool = valueKeeperContainerPool;
        _formulaCache = formulaCache;
    }

    #region Endpoints

    #region NumberEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumber(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumber(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumber(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumber(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcNumber(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public double CalcNumber(FormulaNodes formulaNodes)
    {
        return OnCalcNumber(formulaNodes.Nodes, default);
    }

    #endregion

    #region WordEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWord(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWord(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWord(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWord(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcWord(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public string CalcWord(FormulaNodes formulaNodes)
    {
        return OnCalcWord(formulaNodes.Nodes, default);
    }

    #endregion

    #region DateTimeEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcDateTime(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcDateTime(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcDateTime(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcDateTime(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcDateTime(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public DateTime CalcDateTime(FormulaNodes formulaNodes)
    {
        return OnCalcDateTime(formulaNodes.Nodes, default);
    }

    #endregion

    #region WordListEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWordList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWordList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWordList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcWordList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcWordList(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<string> CalcWordList(FormulaNodes formulaNodes)
    {
        return OnCalcWordList(formulaNodes.Nodes, default);
    }

    #endregion

    #region NumberListEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumberList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumberList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumberList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcNumberList(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcNumberList(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public List<double> CalcNumberList(FormulaNodes formulaNodes)
    {
        return OnCalcNumberList(formulaNodes.Nodes, default);
    }

    #endregion

    #region BooleanEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcBoolean(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcBoolean(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcBoolean(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcBoolean(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcBoolean(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public bool CalcBoolean(FormulaNodes formulaNodes)
    {
        return OnCalcBoolean(formulaNodes.Nodes, default);
    }

    #endregion

    #region AggregatedEndpoints

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(string formula, IVariablesContainer variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcAggregated(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes, IVariablesContainer variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcAggregated(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(string formula, IDictionary<string, EvaluatorValue> variables)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        var wrapper = VariablesSource.Create(variables);
        return OnCalcAggregated(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes, IDictionary<string, EvaluatorValue> variables)
    {
        var wrapper = VariablesSource.Create(variables);
        return OnCalcAggregated(formulaNodes.Nodes, wrapper);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(string formula)
    {
        var formulaNodes = _formulaCache.GetFormulaNodes(formula);
        return OnCalcAggregated(formulaNodes.Nodes, default);
    }

    /// <summary>
    /// Calculate formula
    /// </summary>
    /// <exception cref="VariableNotFoundException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public EvaluatorValue Calc(FormulaNodes formulaNodes)
    {
        return OnCalcAggregated(formulaNodes.Nodes, default);
    }

    #endregion

    #endregion

    #region OnCalc

    private double OnCalcNumber(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        double res = CalcInternal(nodes, variables, valueKeeperContainer);

        return res;
    }

    private string OnCalcWord(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        string res = CalcInternal(nodes, variables, valueKeeperContainer);

        // Result
        return WordFormatter.Format(res);
    }

    private DateTime OnCalcDateTime(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        DateTime res = CalcInternal(nodes, variables, valueKeeperContainer);

        // Result
        return res;
    }

    private List<string> OnCalcWordList(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        List<string> res = CalcInternal(nodes, variables, valueKeeperContainer);

        // Result
        return WordFormatter.Format(res);
    }

    private List<double> OnCalcNumberList(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        List<double> res = CalcInternal(nodes, variables, valueKeeperContainer);

        // Result
        return res;
    }

    private bool OnCalcBoolean(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        bool res = CalcInternal(nodes, variables, valueKeeperContainer);

        // Result
        return res;
    }

    private EvaluatorValue OnCalcAggregated(List<BaseFormulaNode> nodes, VariablesSource variables)
    {
        // Rent
        using var valueKeeperContainer = GetValueKeeperContainer();

        // Calculate with internal struct
        EvaluatorValue res = CalcInternal(nodes, variables, valueKeeperContainer);

        return res;
    }

    #endregion

    #region CalcInternal

    private InternalEvaluatorValue CalcInternal(List<BaseFormulaNode> nodes, VariablesSource variables, ValueKeeperContainer valueKeeperContainer)
    {
        // If no nodes return Null
        if (nodes.Count == 0)
        {
            return default;
        }

        // Rent stack
        var stack = _stackPool.Get();

        // Prevent dirty collection
        if (stack.Count > 0)
        {
            stack.Clear();
        }

        var factory = new ValueFactory(valueKeeperContainer);

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            if (node.TypeKey == NodeTypeEnum.Variable)
            {
                var variableNode = (VariableNode)node;
                var argumentVal = variables.GetValue(variableNode.Name);

                var val = factory.Create(argumentVal);
                if (variableNode.IsNegative && val.IsNumber)
                {
                    val = val.Number * -1;
                }
                else if (variableNode.IsNegation && val.IsBoolean)
                {
                    val = factory.Boolean.Create(!val.Boolean);
                }

                stack.Push(val);
            }
            else if (node.TypeKey == NodeTypeEnum.Operator)
            {
                var mathOperationNode = (OperatorNode)node;
                var b = stack.Pop();
                var a = stack.Pop();

                var value = mathOperationNode.OperatorKey switch
                {
                    // Math
                    Operator.Multiply => Multiply(a, b),
                    Operator.Divide => Divide(a, b),
                    Operator.Plus => Plus(factory, a, b),
                    Operator.Minus => Minus(factory, a, b),
                    Operator.Power => Power(factory, a, b),

                    // Logic
                    Operator.Less => Less(factory, a, b),
                    Operator.LessEqual => LessEqual(factory, a, b),
                    Operator.More => More(factory, a, b),
                    Operator.MoreEqual => MoreEqual(factory, a, b),
                    Operator.Equal => Equal(factory, a, b),
                    Operator.NotEqual => NotEqual(factory, a, b),

                    // Additional logic
                    Operator.And => And(factory, a, b),
                    Operator.Or => Or(factory, a, b),

                    _ => throw new InvalidOperationException(),
                };

                stack.Push(value);
            }
            else if (node.TypeKey == NodeTypeEnum.FunctionWrapper)
            {
                var functionWrapper = (FunctionWrapperNode)node;
                var functionVal = CalcFunction(functionWrapper, variables, valueKeeperContainer);
                stack.Push(functionVal);
            }
            else if (node.TypeKey == NodeTypeEnum.Number)
            {
                var valNode = (NumberNode)node;
                stack.Push(valNode.Number);
            }
            else if (node.TypeKey == NodeTypeEnum.Boolean)
            {
                var boolNode = (BooleanNode)node;
                stack.Push(factory.Boolean.Create(boolNode.Value));
            }
            else if (node.TypeKey == NodeTypeEnum.Word)
            {
                var wordNode = (WordNode)node;
                stack.Push(factory.Word.Create(wordNode.Word));
            }
            else if (node.TypeKey == NodeTypeEnum.WordList)
            {
                var wordListNode = (WordListNode)node;
                stack.Push(factory.WordList.Create(wordListNode.WordList));
            }
            else if (node.TypeKey == NodeTypeEnum.NumberList)
            {
                var numberListNode = (NumberListNode)node;
                stack.Push(factory.NumberList.Create(numberListNode.NumberList));
            }
            else if (node.TypeKey == NodeTypeEnum.Null)
            {
                stack.Push(default);
            }
        }

        // Result
        var res = stack.Pop();

        // Return to a pool
        _stackPool.Return(stack);

        return res;
    }

    private InternalEvaluatorValue CalcFunction(FunctionWrapperNode functionWrapper, VariablesSource variables, ValueKeeperContainer valueKeeperContainer)
    {
        var args = _argsPool.Get();

        // Prevent dirty collection
        if (args.Count > 0)
        {
            args.Clear();
        }

        var hasNullArgs = false;
        for (int i = 0; i < functionWrapper.FunctionArgumentNodes.Count; i++)
        {
            var subNodes = functionWrapper.FunctionArgumentNodes[i];
            var subRes = CalcInternal(subNodes, variables, valueKeeperContainer);
            args.Add(subRes);

            if (subRes.IsNull)
            {
                hasNullArgs = true;
            }
        }

        var factory = new ValueFactory(valueKeeperContainer);
        var funcNode = functionWrapper.FunctionNode;
        var shouldExecute = funcNode.Function.CanHandleNullArguments || !hasNullArgs;

        var res = shouldExecute ? funcNode.Function.Execute(args, factory) : default;
        if (funcNode.IsNegative && res.IsNumber)
        {
            res = res.Number * -1;
        }
        else if (funcNode.IsNegation && res.IsBoolean)
        {
            res = factory.Boolean.Create(!res.Boolean);
        }

        // Clear collection
        args.Clear();

        // Return to a pool
        _argsPool.Return(args);

        return res;
    }

    private ValueKeeperContainerReleaser GetValueKeeperContainer()
    {
        return _valueKeeperContainerPool
            .Get()
            .SetPool(_valueKeeperContainerPool)
            .ResetIndex();
    }

    #endregion

    /// <summary>
    /// Create evaluator facade
    /// </summary>
    public static Facade CreateFacade(Action<NoStringEvaluatorOptions> options = null)
    {
        // Update options
        if (options != null)
        {
            var opt = new NoStringEvaluatorOptions();
            options(opt);
            opt.UpdateGlobalOptions();
        }

        return new Facade();
    }

    /// <summary>
    /// Facade
    /// </summary>
    public class Facade
    {
        internal Facade()
        {
            // Pooling
            var stackPool = ObjectPool.Create<Stack<InternalEvaluatorValue>>();
            var argsPool = ObjectPool.Create<List<InternalEvaluatorValue>>();
            var valueKeeperContainerPool = ObjectPool.Create<ValueKeeperContainer>();

            // Parser
            FunctionReader = new();
            FormulaParser = new(FunctionReader);
            FormulaCache = new(FormulaParser);

            // Checker
            FormulaChecker = new(FormulaParser);

            // Evaluator
            Evaluator = new(stackPool, argsPool, valueKeeperContainerPool, FormulaCache);
        }

        /// <summary>
        /// Evaluator
        /// </summary>
        public NoStringEvaluator Evaluator { get; }

        /// <summary>
        /// FunctionReader
        /// </summary>
        public FunctionReader FunctionReader { get; }

        /// <summary>
        /// FormulaParser
        /// </summary>
        public FormulaParser FormulaParser { get; }

        /// <summary>
        /// FormulaCache
        /// </summary>
        public FormulaCache FormulaCache { get; }

        /// <summary>
        /// FormulaChecker
        /// </summary>
        public FormulaChecker FormulaChecker { get; }
    }
}
