using BenchmarkDotNet.Running;
using ConsoleApp.Benchmark;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<BenchmarkNumberService>();
        }
    }
}
