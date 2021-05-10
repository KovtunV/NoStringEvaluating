using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Models;
using NoStringEvaluating.Models.Values;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

            //var res = eval.Calc("DateDif(ToDateTime('06/01/2001'); ToDateTime('08/15/2002'); 'D')", args);

            BenchmarkRunner.Run<BenchmarkNumberService>();
        }


        static INoStringEvaluator CreateNoString()
        {
            var container = new ServiceCollection() .AddNoStringEvaluator();
            var services = container.BuildServiceProvider();

            return services.GetRequiredService<INoStringEvaluator>();
        }
    }
}
