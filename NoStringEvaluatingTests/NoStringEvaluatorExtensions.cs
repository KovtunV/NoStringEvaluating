using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Cache;
using NoStringEvaluating.Services.Checking;
using NoStringEvaluating.Services.Parsing;
using NoStringEvaluating.Services.Parsing.NodeReaders;

namespace NoStringEvaluatingTests
{
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
            // Pooling
            services.TryAddSingleton(ObjectPool.Create<Stack<InternalEvaluatorValue>>());
            services.TryAddSingleton(ObjectPool.Create<List<InternalEvaluatorValue>>());
            services.TryAddSingleton(ObjectPool.Create<ExtraTypeIdContainer>());

            // Parser
            services.TryAddSingleton<IFormulaCache, FormulaCache>();
            services.TryAddSingleton<IFunctionReader, FunctionReader>();
            services.TryAddSingleton<IFormulaParser, FormulaParser>();

            // Checker
            services.TryAddSingleton<IFormulaChecker, FormulaChecker>();
            
            // Evaluator
            services.TryAddSingleton<INoStringEvaluator, NoStringEvaluator>();

            // Update constants
            if (options != null)
            {
                var opt = new NoStringEvaluatorOptions();
                options(opt);
                opt.UpdateConstants();
            }

            return services;
        }
    }
}
