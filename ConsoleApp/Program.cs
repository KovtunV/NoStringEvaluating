using System.Reflection;
using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Ninject;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models;

namespace ConsoleApp;

class Program
{
    static void Main()
    {
        Run(Key.Number);
    }

    static void Run(Key key)
    {
        if (key == Key.Number)
        {
            BenchmarkRunner.Run<BenchNumbers>();
        }
        else if (key == Key.Parallel)
        {
            BenchmarkRunner.Run<BenchParallel>();
        }
    }

    static NoStringEvaluator CreateNoString()
    {
        static void Configure(NoStringEvaluatorOptions opt)
        {
            opt
                .SetFloatingPointSymbol(FloatingPointSymbol.DotComma)
                .WithFunctionsFrom(typeof(Program))
                .SetThrowIfVariableNotFound(false);
        }

        return NoStringEvaluator.CreateFacade(Configure).Evaluator;
    }

    static INoStringEvaluator CreateNoStringFromNinject(out StandardKernel kernel)
    {
        kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());

        var eval = kernel.Get<INoStringEvaluator>();
        return eval;
    }

    private enum Key
    {
        Number,
        Parallel
    }
}
