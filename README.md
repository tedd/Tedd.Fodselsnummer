# Tedd.Fodselsnummer

## Architectural Overview
Tedd.Fodselsnummer is a high-performance C#/.NET structural library engineered for the parsing, validation, and temporal data extraction of Norwegian national identity numbers.

The internal architecture operates via synchronous direct-computation and explicit manual character indexing, completely independent of external framework dependencies. The framework explicitly rejects the use of legacy regex-based extraction, hierarchical data binding, and routed event infrastructures in favor of deterministic, O(1) execution paths.

The operational reality of the framework encompasses formal Mod11 checksum verification and deterministic parsing of the following supported identity protocols:
*   **Fødselsnummer (Normal):** Standard national identity number.
*   **D-nummer (D):** Temporary identity numbers with mathematically shifted day components.
*   **H-nummer (H):** Health institution specific identifiers with shifted month components.
*   **FH-nummer (FH):** Shared health identifiers featuring specific structural prefixes.

## Contemporary Implementation Example
The following code exemplifies the API surface utilizing modern .NET 9.0+ top-level statements and property pattern matching for deterministic validation and epistemological extraction.

```csharp
using System;
using Tedd.Fodselsnummer;

var validationResult = FodselsnummerValidator.Validate("19121950041");

if (validationResult is { Success: true })
{
    Console.WriteLine($"Identity Type: {validationResult.Type}");
    Console.WriteLine($"Biological Gender: {validationResult.Gender}");
    Console.WriteLine($"Temporal Origin (Birthday): {validationResult.Birthday:yyyy-MM-dd}");
}
else
{
    Console.WriteLine($"Validation Error: {validationResult.ErrorMessage}");
}
```

## Planned Enhancements (Hypotheses)
The current framework relies on `System.String` implementations. Future iterations hypothesize zero-allocation parsing utilizing `ReadOnlySpan<char>` paradigms to minimize Garbage Collector pressure and increase processing throughput.
