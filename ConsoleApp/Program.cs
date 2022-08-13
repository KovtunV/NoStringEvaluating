using System.Reflection;
using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Ninject;
using NoStringEvaluating;
using NoStringEvaluating.Contract;

namespace ConsoleApp;

class Program
{
    static void Main()
    {
        //var eval = CreateNoString();

        //var args = new Dictionary<string, EvaluatorValue>();
        //args["my variable"] = true;

        // var res = eval.Calc("Add(5; 3; 12)");

        BenchmarkRunner.Run<BenchmarkNumberService>();
    }

    static NoStringEvaluator CreateNoString()
    {
        void Configure(NoStringEvaluatorOptions opt)
        {
            opt
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
