# Tedd.Fodselsnummer

## Architectural Overview
Tedd.Fodselsnummer is a high-performance C#/.NET structural library engineered for the parsing, validation, and temporal data extraction of Norwegian national identity numbers.

The operational reality of the framework encompasses formal Mod11 checksum verification and deterministic parsing of the following supported identity protocols:
*   **Fødselsnummer (Normal):** Standard national identity number.
*   **D-nummer (D):** Temporary identity numbers with mathematically shifted day components.
*   **H-nummer (H):** Health institution specific identifiers with shifted month components.
*   **FH-nummer (FH):** Shared health identifiers featuring specific structural prefixes.

The framework's internal mechanics operate via synchronous direct-computation and explicit manual character indexing. It is completely independent of external framework dependencies, legacy regex-based extraction, hierarchical data binding, or routed event infrastructures. This ensures absolute deterministic execution flow.

## Contemporary Implementation Example
The following code exemplifies the API surface utilizing modern .NET 9.0/10.0+ top-level statements and contemporary pattern matching for deterministic validation and epistemological extraction.

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
