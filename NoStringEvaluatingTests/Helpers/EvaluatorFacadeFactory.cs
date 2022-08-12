using System;
using System.Collections.Generic;
using NoStringEvaluating;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluatingTests.Helpers;

internal static class EvaluatorFacadeFactory
{
    public static NoStringEvaluator.Facade Create(Action<NoStringEvaluatorOptions> options = null)
    {
        NoStringEvaluatorConstants.Reset();

        var evaluatorFacade = NoStringEvaluator.CreateFacade(opt =>
        {
            opt.SetFloatingPointSymbol(FloatingPointSymbol.DotComma);
            opt.WithFunctionsFrom(typeof(EvaluatorFacadeFactory));

            options?.Invoke(opt);
        });

        return evaluatorFacade;
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
