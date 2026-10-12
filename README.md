# Tedd.Fodselsnummer

[Website](https://tedd.no/Tedd.Fodselsnummer/) · [NuGet](https://www.nuget.org/packages/Tedd.Fodselsnummer) · [Source](https://github.com/tedd/Tedd.Fodselsnummer)

## Architectural Overview
Tedd.Fodselsnummer is a high-performance C#/.NET structural library engineered for the parsing, validation, and temporal data extraction of Norwegian national identity numbers.

The operational reality of the framework encompasses formal Mod11 checksum verification and deterministic parsing of the following supported identity protocols:
*   **Fødselsnummer (Normal):** Standard national identity number.
*   **D-nummer (D):** Temporary identity numbers with mathematically shifted day components.
*   **H-nummer (H):** Health institution specific identifiers with shifted month components.
*   **FH-nummer (FH):** Shared health identifiers featuring specific structural prefixes.

## Architectural Execution Flow
The framework's internal mechanics operate exclusively via synchronous direct-computation and explicit manual character indexing. Contrary to external hypothesis, the architecture fundamentally rejects and is completely independent of hierarchical data binding or routed event infrastructures. The epistemological execution flow is deterministically defined as follows:

1. **Structural Verification:** Immediate length and numeric bound validation.
2. **Deterministic Extraction:** Manual, indexed character parsing bypassing legacy regex-based extraction.
3. **Temporal and Protocol Classification:** Algorithmic validation against defined century control ranges to categorize temporal origin.
4. **Cryptographic Validation:** Dual Mod11 algorithmic checksum verification.

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
The current framework implementation inherently relies on `System.String` allocation parameters. Future architectural iterations hypothesize the integration of zero-allocation parsing utilizing `ReadOnlySpan<char>` paradigms to critically minimize Garbage Collector pressure and increase processing throughput.
