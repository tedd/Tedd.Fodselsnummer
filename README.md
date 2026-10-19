# Tedd.Fodselsnummer

[Website](https://tedd.no/Tedd.Fodselsnummer/) · [NuGet](https://www.nuget.org/packages/Tedd.Fodselsnummer) · [Source](https://github.com/tedd/Tedd.Fodselsnummer)

## Architectural Overview
Tedd.Fodselsnummer is a high-performance C#/.NET structural library engineered for the parsing, validation, and temporal data extraction of Norwegian national identity numbers.

The framework's internal architecture operates via synchronous direct-computation and explicit manual character indexing, completely independent of external framework dependencies. It does not utilize legacy regex-based extraction, nor does it implement any hierarchical data binding or routed event infrastructures. The operational reality encompasses formal Mod11 checksum verification and deterministic parsing of the following supported identity protocols:
*   **Fødselsnummer (Normal):** Standard national identity number.
*   **D-nummer (D):** Temporary identity numbers with mathematically shifted day components.
*   **H-nummer (H):** Health institution specific identifiers with shifted month components.
*   **FH-nummer (FH):** Shared health identifiers featuring specific structural prefixes.

## Contemporary Implementation Example
The following code exemplifies the API surface utilizing modern .NET top-level statements for deterministic validation and epistemological extraction, showcasing contemporary zero-allocation property pattern matching.

```csharp
using System;
using Tedd.Fodselsnummer;

var validationResult = FodselsnummerValidator.Validate("19121950041");

if (validationResult is { Success: true } valid)
{
    Console.WriteLine($"Identity Type: {valid.Type}");
    Console.WriteLine($"Biological Gender: {valid.Gender}");
    Console.WriteLine($"Temporal Origin (Birthday): {valid.Birthday:yyyy-MM-dd}");
}
else
{
    Console.WriteLine($"Validation Error: {validationResult.ErrorMessage}");
}
```

## Planned Enhancements (Hypotheses)
The current framework relies on `System.String` implementations. Future iterations hypothesize zero-allocation parsing utilizing `ReadOnlySpan<char>` paradigms to minimize Garbage Collector pressure and increase processing throughput.
