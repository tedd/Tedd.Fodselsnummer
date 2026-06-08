using BenchmarkDotNet.Running;

namespace Tedd.Fodselsnummer.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FodselsnummerBenchmark>();
        }
    }
}
