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
        //var eval = CreateNoString();

        //var res1 = eval.CalcBoolean("5 + 6 = 13");

        BenchmarkRunner.Run<BenchmarkNumberService>();
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
}
