using System.Reflection;
using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using NoStringEvaluating.Contract;

namespace ConsoleApp;

class Program
{
    static void Main()
    {
        // var eval = CreateNoString();

        //var args = new Dictionary<string, EvaluatorValue>();
        //args["my variable"] = true;

        //var res = eval.Calc("ToDateTime('04/17/2021')-1 < ToDateTime('04/17/2021')");
        BenchmarkRunner.Run<BenchmarkNumberService>();
        //BenchmarkRunner.Run<BenchmarkEvaluationPerformance>();
    }

    static INoStringEvaluator CreateNoString()
    {
        var container = new ServiceCollection()
            .AddNoStringEvaluator(opt => opt.WithFunctionsFrom(typeof(Program)));

        var services = container.BuildServiceProvider();

        return services.GetRequiredService<INoStringEvaluator>();
    }

    static INoStringEvaluator CreateNoStringFromNinject(out StandardKernel kernel)
    {
        kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());

        var eval = kernel.Get<INoStringEvaluator>();
        return eval;
    }
}
