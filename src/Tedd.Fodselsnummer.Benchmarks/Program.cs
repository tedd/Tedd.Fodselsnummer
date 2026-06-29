using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class ValidationBenchmarks
{
    private const string validNumber = "19121950041";

    [Benchmark(Baseline = true)]
    public void Legacy()
    {
        Tedd.Fodselsnummer.Archive.FodselsnummerValidator.ValidateLegacy(validNumber);
    }

    [Benchmark]
    public void Optimized()
    {
        Tedd.Fodselsnummer.FodselsnummerValidator.Validate(validNumber);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidationBenchmarks>();
    }
}
