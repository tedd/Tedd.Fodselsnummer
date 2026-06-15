using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Tedd.Fodselsnummer;

namespace Tedd.Fodselsnummer.Benchmarks
{
    [MemoryDiagnoser]
    public class ValidatorBenchmarks
    {
        private const string ValidNumber = "19121999768";
        private const string InvalidLengthNumber = "12345";
        private const string InvalidFormatNumber = "191219997XX";

        [Benchmark(Baseline = true)]
        public Tedd.Fodselsnummer.Archive.FodselsnummerResult ValidateLegacy_Valid()
        {
            return Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(ValidNumber);
        }

        [Benchmark]
        public Tedd.Fodselsnummer.FodselsnummerResult ValidateOptimized_Valid()
        {
            return Tedd.Fodselsnummer.FodselsnummerValidator.Validate(ValidNumber);
        }

        [Benchmark]
        public Tedd.Fodselsnummer.Archive.FodselsnummerResult ValidateLegacy_InvalidLength()
        {
            return Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(InvalidLengthNumber);
        }

        [Benchmark]
        public Tedd.Fodselsnummer.FodselsnummerResult ValidateOptimized_InvalidLength()
        {
            return Tedd.Fodselsnummer.FodselsnummerValidator.Validate(InvalidLengthNumber);
        }

        [Benchmark]
        public Tedd.Fodselsnummer.Archive.FodselsnummerResult ValidateLegacy_InvalidFormat()
        {
            return Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(InvalidFormatNumber);
        }

        [Benchmark]
        public Tedd.Fodselsnummer.FodselsnummerResult ValidateOptimized_InvalidFormat()
        {
            return Tedd.Fodselsnummer.FodselsnummerValidator.Validate(InvalidFormatNumber);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ValidatorBenchmarks>();
        }
    }
}
