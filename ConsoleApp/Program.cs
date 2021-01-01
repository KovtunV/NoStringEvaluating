using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;

namespace ConsoleApp
{

    class Program
    {
        static void Main()
        {
            var bench = new Benchmark(Benchmark.Three);
            bench.RunNoString(Benchmark.Formula4);

            Console.ReadLine();
        }

        private static void PrintAppStates(Stopwatch sw)
        {
            sw.Stop();

            Console.WriteLine("");
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms.");
            Console.WriteLine($"Physical memory usage: {Environment.WorkingSet / 1024 / 1024} Mb");
            Console.WriteLine($"GC cycle count (Gen 0): {GC.CollectionCount(0)}");
            Console.WriteLine($"GC cycle count (Gen 1): {GC.CollectionCount(1)}");
            Console.WriteLine($"GC cycle count (Gen 2): {GC.CollectionCount(2)}\n");
        }
    }
}
