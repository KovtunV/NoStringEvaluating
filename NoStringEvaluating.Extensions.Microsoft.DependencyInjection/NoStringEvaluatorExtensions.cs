using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Cache;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Services.Parsing.NodeReaders;

namespace NoStringEvaluating.Extensions.Microsoft.DependencyInjection;

/// <summary>
/// NoStringEvaluator registrar
/// </summary>
public static class NoStringEvaluatorExtensions
{
    /// <summary>
    /// Add NoString math evaluator
    /// </summary>
    public static IServiceCollection AddNoStringEvaluator(this IServiceCollection services, Action<NoStringEvaluatorOptions> options = null)
    {
        // Update options
        if (options != null)
        {
            var opt = new NoStringEvaluatorOptions();
            options(opt);
            opt.UpdateGlobalOptions();
        }

        // Pooling
        services.TryAddSingleton(ObjectPool.Create<Stack<InternalEvaluatorValue>>());
        services.TryAddSingleton(ObjectPool.Create<List<InternalEvaluatorValue>>());
        services.TryAddSingleton(ObjectPool.Create<ValueKeeperContainer>());

        // Parser
        services.TryAddSingleton<IFormulaCache, FormulaCache>();
        services.TryAddSingleton<IFunctionReader, FunctionReader>();
        services.TryAddSingleton<IFormulaParser, FormulaParser>();

        // Checker
        services.TryAddSingleton<IFormulaChecker, FormulaChecker>();

        // Evaluator
        services.TryAddSingleton<INoStringEvaluator, NoStringEvaluator>();
        services.TryAddSingleton<INoStringEvaluatorNullable, NoStringEvaluatorNullable>();

        return services;
    }
}
