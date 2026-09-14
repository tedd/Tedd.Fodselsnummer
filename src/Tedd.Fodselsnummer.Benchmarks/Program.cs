using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class ValidationBenchmarks
{
    private const string validNumber = "19121950041";
    private const long validLongNumber = 19121950041;

    [Benchmark(Baseline = true)]
    public void LegacyLong()
    {
        Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(validLongNumber);
    }

    [Benchmark]
    public void OptimizedLong()
    {
        Tedd.Fodselsnummer.FodselsnummerValidator.Validate(validLongNumber);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidationBenchmarks>();
    }
}
