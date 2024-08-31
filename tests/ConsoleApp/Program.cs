using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Ninject;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Models;

namespace ConsoleApp;

internal class Program
{
    public static void Main()
    {
        Run(Key.Number);
    }

    private static void Run(Key key)
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

    [UnconditionalSuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static NoStringEvaluator CreateNoString()
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

    [UnconditionalSuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static INoStringEvaluator CreateNoStringFromNinject(out StandardKernel kernel)
    {
        kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());

        var eval = kernel.Get<INoStringEvaluator>();
        return eval;
    }

    private enum Key
    {
        Number,
        Parallel,
    }
}
