using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class ValidationBenchmarks
{
    private const long validNumberLong = 19121950041L;

    [Benchmark(Baseline = true)]
    public void LegacyLong()
    {
        Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(validNumberLong);
    }

    [Benchmark]
    public void OptimizedLong()
    {
        Tedd.Fodselsnummer.FodselsnummerValidator.Validate(validNumberLong);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidationBenchmarks>();
    }
}
