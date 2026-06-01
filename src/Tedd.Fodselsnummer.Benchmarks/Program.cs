using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tedd.Fodselsnummer.Benchmarks
{
    [MemoryDiagnoser]
    public class ValidateBenchmarks
    {
        private const string TestNumber = "12345678901"; // Placeholder for a valid format

        [Benchmark(Baseline = true)]
        public Fodselsnummer.Archive.FodselsnummerResult ValidateArchive()
        {
            return Fodselsnummer.Archive.FodselsnummerValidator.Validate(TestNumber);
        }

        [Benchmark]
        public Fodselsnummer.FodselsnummerResult ValidateOptimized()
        {
            return Fodselsnummer.FodselsnummerValidator.Validate(TestNumber);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<ValidateBenchmarks>();
        }
    }
}
