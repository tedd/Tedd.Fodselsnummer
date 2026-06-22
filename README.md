# Tedd.Fodselsnummer

C#/.NET parser for fødselsnummer / Norwegian national identity number.

This framework facilitates the extraction and rigorous validation of demographic parameters embedded within the Norwegian national identity number (`fødselsnummer`), including derivatives such as D-numbers, H-numbers, and FH-numbers.

## Architectural Delineation

The framework employs a deterministic, synchronous direct-computation paradigm to resolve and validate identity numbers. It operates purely on functional transformations and mathematical validation, strictly isolating itself from any form of hierarchical data binding or routed event infrastructure.

### Execution Flow

The internal validation mechanism processes input via the following sequential phases:

1. **Component Extraction:** A sophisticated Regular Expression (Regex) identifies and isolates the constituent segments of the numerical sequence (birth date, individual identifier, and dual checksums).
2. **Contextual Normalization:** Compensatory algorithms detect and normalize specialized number classes (D, H, and FH-numbers), adjusting the chronological and demographic segments for precise evaluation.
3. **Century Resolution:** The individual identifier is mapped against established control ranges (`IndividualNumberControlRange`) to deduce the exact century of birth, thus overcoming the intrinsic Y2K ambiguity of the raw date string.
4. **Algorithmic Validation:** The ultimate validation is performed via a rigid modulo 11 checksum calculation applied against the first and second checksum digits, detecting invalid or mistyped sequences.

### Future Hypotheses (Roadmap)

While the established capabilities rely on `String`-based Regex parsing, ongoing research hypothetically explores the integration of high-performance, low-allocation memory structures (e.g., zero-allocation `Span<T>` and `ReadOnlySpan<char>` parsing methodologies) to mitigate garbage collection pressure during bulk validation scenarios. These enhancements remain speculative and are not present in the current operational runtime.

## Implementation Example

The following example demonstrates contemporary framework usage leveraging .NET 9.0/10.0+ syntax (including pattern matching) for maximum lexical precision and conciseness:

```csharp
using System;
using Tedd.Fodselsnummer;

var result = FodselsnummerValidator.Validate("19121950041");

if (result is { Success: true })
{
    Console.WriteLine($"Fødselsnummer type: {result.Type}");
    Console.WriteLine($"Kjønn: {result.Gender}");
    Console.WriteLine($"Fødselsdato: {result.Birthday:yyyy-MM-dd}");
}
else
{
    Console.WriteLine($"Validation failed: {result.ErrorMessage}");
}
```
