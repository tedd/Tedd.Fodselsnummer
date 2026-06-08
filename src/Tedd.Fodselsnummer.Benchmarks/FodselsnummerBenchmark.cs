using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tedd.Fodselsnummer.Benchmarks
{
    [MemoryDiagnoser]
    public class FodselsnummerBenchmark
    {
        private const string TestNumber = "01018012345";

        [Benchmark(Baseline = true)]
        public void LegacyValidate()
        {
            Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(TestNumber);
        }

        [Benchmark]
        public void OptimizedValidate()
        {
            Tedd.Fodselsnummer.FodselsnummerValidator.Validate(TestNumber);
        }
    }
}
