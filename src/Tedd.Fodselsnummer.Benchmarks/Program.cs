using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tedd.Fodselsnummer.Benchmarks;

[MemoryDiagnoser]
public class ValidatorBenchmarks
{
    private const string ValidFodselsnummer = "19121950041";

    [Benchmark(Baseline = true)]
    public Tedd.Fodselsnummer.Archive.FodselsnummerResult ArchiveValidator()
    {
        return Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(ValidFodselsnummer);
    }

    [Benchmark]
    public Tedd.Fodselsnummer.FodselsnummerResult NewValidator()
    {
        return Tedd.Fodselsnummer.FodselsnummerValidator.Validate(ValidFodselsnummer);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidatorBenchmarks>();
    }
}
