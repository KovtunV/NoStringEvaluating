using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Services.Checking;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var bench = new Benchmark(Benchmark.Two);
            bench.RunNoString(Benchmark.Formula4);

            Console.ReadLine();
        }
    }
}
