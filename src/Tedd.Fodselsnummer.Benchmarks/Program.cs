using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class ValidationBenchmarks
{
    private const string validString = "19121950041";
    private const long validLong = 19121950041L;

    [Benchmark(Baseline = true)]
    public void LegacyString()
    {
        Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(validString);
    }

    [Benchmark]
    public void OptimizedString()
    {
        Tedd.Fodselsnummer.FodselsnummerValidator.Validate(validString);
    }

    [Benchmark]
    public void LegacyLong()
    {
        Tedd.Fodselsnummer.Archive.FodselsnummerValidator.Validate(validLong);
    }

    [Benchmark]
    public void OptimizedLong()
    {
        Tedd.Fodselsnummer.FodselsnummerValidator.Validate(validLong);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ValidationBenchmarks>();
    }
}
