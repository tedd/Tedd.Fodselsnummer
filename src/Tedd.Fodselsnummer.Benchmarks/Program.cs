using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tedd.Fodselsnummer.Benchmarks;

[MemoryDiagnoser]
public class ValidatorBenchmarks
{
    private const string ValidFodselsnummer = "19121950041";

    [Benchmark(Baseline = true)]
    public void ArchiveValidator()
    {
        var result = Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(ValidFodselsnummer);
    }

    [Benchmark]
    public void NewValidator()
    {
        var result = Tedd.Fodselsnummer.FodselsnummerValidator.Validate(ValidFodselsnummer);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidatorBenchmarks>();
    }
}
