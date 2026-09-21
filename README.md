# Tedd.Fodselsnummer

[Website](https://tedd.no/Tedd.Fodselsnummer/) · [NuGet](https://www.nuget.org/packages/Tedd.Fodselsnummer) · [Source](https://github.com/tedd/Tedd.Fodselsnummer)

## Architectural Overview
Tedd.Fodselsnummer is a high-performance C#/.NET structural library engineered for the parsing, validation, and temporal data extraction of Norwegian national identity numbers.

The operational reality of the framework encompasses formal Mod11 checksum verification and deterministic parsing of the following supported identity protocols:
*   **Fødselsnummer (Normal):** Standard national identity number.
*   **D-nummer (D):** Temporary identity numbers with mathematically shifted day components.
*   **H-nummer (H):** Health institution specific identifiers with shifted month components.
*   **FH-nummer (FH):** Shared health identifiers featuring specific structural prefixes.

### Internal Execution Mechanics
The framework's internal execution path functions via deterministic, synchronous direct-computation. Component extraction and structural validation are executed via explicit manual character indexing (allocating zero heap memory per operation). It is imperative to clarify that the framework explicitly **does not** implement hierarchical data binding, routed event infrastructures, or legacy regex-based extraction for its primary validation flow. The absence of these layers guarantees O(1) time and space complexity.

## Contemporary Implementation Example
The following code exemplifies the API surface utilizing modern .NET top-level statements for deterministic validation and epistemological extraction.

```csharp
using System;
using Tedd.Fodselsnummer;

var validationResult = FodselsnummerValidator.Validate("19121950041");

if (!validationResult.Success)
{
    Console.WriteLine($"Validation Error: {validationResult.ErrorMessage}");
}
else
{
    Console.WriteLine($"Identity Type: {validationResult.Type}");
    Console.WriteLine($"Biological Gender: {validationResult.Gender}");
    Console.WriteLine($"Temporal Origin (Birthday): {validationResult.Birthday:yyyy-MM-dd}");
}
```

## Planned Enhancements (Hypotheses)
The current framework relies on `System.String` implementations. Future iterations hypothesize zero-allocation parsing utilizing `ReadOnlySpan<char>` paradigms to minimize Garbage Collector pressure and increase processing throughput.
