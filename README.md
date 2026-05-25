# Tedd.Fodselsnummer

## Architectural Overview

**Tedd.Fodselsnummer** is a deterministic, stateless parsing and validation utility engineered for Norwegian national identity numbers (fødselsnummer). The framework operates entirely independently of hierarchical data binding or routed event infrastructures, prioritizing raw string parsing efficiency and structural validation.

### Execution Flow Protocol

The operational reality of the validation process consists of the following deterministic phases:

1.  **Format Verification:** Validates standard 11-digit numerical length structural integrity.
2.  **Component Extraction:** Parses discrete segments via regular expressions (date, individual number, gender, checksums).
3.  **Type Resolution:** Identifies specialized identity formats, explicitly differentiating Normal, D-numbers, H-numbers, and FH-numbers through day/month offset normalization.
4.  **Century Resolution:** Computes the full birth year utilizing configured historical and future individual control ranges.
5.  **Modulus-11 Validation:** Executes strict, dual Modulus-11 checksum algorithms against the extracted verification digits.

### Functional Paradigms

#### Established Capabilities
*   Syntactically flawless and strictly deterministic validation of Norwegian national identity numbers.
*   Reliable identification of Normal, D-number, H-number, and FH-number variations.
*   Extraction of demographic data: Gender, Birth Date, and Individual Control Numbers.

#### Roadmap Hypotheses (Future Enhancements)
*   Implementation of zero-allocation string parsing utilizing `Span<T>` and `ReadOnlySpan<T>` to bypass Regex overhead and eliminate heap allocations.
*   Integration of high-performance source generators for deterministic build-time validation of static identity markers.

## Implementation Example (.NET 9.0+)

The following structural API usage demonstrates modern C# top-level syntax and verbatim interpolated strings:

```csharp
using System;
using Tedd.Fodselsnummer;

var validationResult = FodselsnummerValidator.Validate("19121950041");

if (!validationResult.Success)
{
    Console.WriteLine($"Validation anomaly detected: Code {validationResult.ErrorCode} - {validationResult.ErrorMessage}");
    return;
}

Console.WriteLine($"""
    Validation Successful:
    - Identity Number: {validationResult.Fodselsnummer}
    - Type Protocol:   {validationResult.Type}
    - Gender Marker:   {validationResult.Gender}
    - Birth Date:      {(validationResult.Birthday.HasValue ? validationResult.Birthday.Value.ToString("yyyy-MM-dd") : "Unresolved")}
    - Individual No:   {validationResult.Individnummer}
    - Control Sums:    {validationResult.Kontrollsifre}
    """);
```
