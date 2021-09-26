using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            //var eval = CreateNoString();

            //var time1 = DateTime.Parse("01/01/2000 14:18:23", CultureInfo.InvariantCulture);
            //var time2 = DateTime.Parse("01/01/2000 18:30:10", CultureInfo.InvariantCulture);
            //var args = new Dictionary<string, EvaluatorValue>();
            //args["date1"] = time1;
            //args["date2"] = time2;

            //var res = eval.CalcWord("5 + 'h'", args);

            BenchmarkRunner.Run<BenchmarkNumberService>();
        }


        static INoStringEvaluator CreateNoString()
        {
            var container = new ServiceCollection().AddNoStringEvaluator();
            var services = container.BuildServiceProvider();
            var functionReader = services.GetRequiredService<IFunctionReader>();

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
}
