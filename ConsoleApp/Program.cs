using System;

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
