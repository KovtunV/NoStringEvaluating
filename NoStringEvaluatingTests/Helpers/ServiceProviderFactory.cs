using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluatingTests.Helpers;

internal static class ServiceProviderFactory
{
    public static IServiceProvider Create(Action<NoStringEvaluatorOptions> options = null)
    {
        var serviceProvider = new ServiceCollection()
          .AddNoStringEvaluator(opt =>
          {
              opt.SetFloatingPointSymbol(FloatingPointSymbol.DotComma);
              options?.Invoke(opt);
          })
          .BuildServiceProvider();

        var functionReader = serviceProvider.GetRequiredService<IFunctionReader>();
        functionReader.AddFunction(new Func_kov());
        functionReader.AddFunction(new Func_kovt());

        return serviceProvider;
    }

    private class Func_kov : IFunction
    {
        public string Name { get; } = "KOV";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var res = 1d;

            for (int i = 0; i < args.Count; i++)
            {
                res *= args[i];
            }

            return res;
        }
    }

    private class Func_kovt : IFunction
    {
        public string Name { get; } = "KOVT";

        public bool CanHandleNullArguments { get; }

        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            return args[0] - args[1];
        }
    }
}
