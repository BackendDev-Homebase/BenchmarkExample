using BenchmarkDotNet.Running;

namespace BenchmarkExample;

internal class Program
{
    static void Main(string[] args)
    {
        // RELEASE build is necessary!
        BenchmarkRunner.Run(typeof(BenchmarkExample).Assembly);
        Console.ReadKey();
    }
}