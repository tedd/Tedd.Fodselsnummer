# Tedd.Fodselsnummer

**C#/.NET framework for deterministic parsing and validation of Norwegian national identity numbers (fødselsnummer, D-nummer, H-nummer, FH-nummer).**

## Architectural Articulation

The `Tedd.Fodselsnummer` framework operates as a highly specialized, stateless validation engine. Its core mandate is the empirical verification of Norwegian national identity numbers according to established cryptographic checksum algorithms and chronological range assertions.

The framework is strictly bounded to validation and metadata extraction; it does not implement any hierarchical data binding, routed event infrastructures, or network-bound resolution services. It provides deterministic, memory-efficient analysis of string and numeric inputs, yielding a comprehensive data transfer object (`FodselsnummerResult`) detailing the specific identity type (Standard, D, H, or FH), extracted chronological data, and binary gender classification.

Future enhancements (hypotheses) may involve zero-allocation span-based parsing strategies utilizing `ReadOnlySpan<char>` to further minimize memory pressure during high-throughput validation cycles.

## Implementation Example

The following code block demonstrates the contemporary implementation of the validation protocol utilizing modern .NET syntactic paradigms (C# 9.0+ pattern matching, top-level statements, and interpolated string structures).

```csharp
using System;
using Tedd.Fodselsnummer;

var result = FodselsnummerValidator.Validate("19121950041");

if (result is { Success: true })
{
    Console.WriteLine($"""
        Validation Successful:
        Identity Type: {result.Type}
        Gender Classification: {result.Gender}
        Chronological Extraction (Birthday): {result.Birthday:yyyy-MM-dd}
        """);
}
else
{
    Console.WriteLine($"Validation Anomaly Detected: {result.ErrorMessage}");
}
```
