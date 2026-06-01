# Tedd.Fodselsnummer

**C#/.NET framework for the deterministic parsing, validation, and semantic extraction of Norwegian National Identity Numbers (Fødselsnummer).**

## Architectural Execution Flow

The `Tedd.Fodselsnummer` framework implements a robust, multi-stage validation pipeline designed to ensure absolute data integrity. The execution flow is strictly delineated into the following phases:

1. **Format Verification:** Validates the fundamental structural integrity of the input, verifying an exact 11-digit numerical length and conducting preliminary regex pattern matching.
2. **Semantic Extraction:** Parses the distinct components of the numerical string (day, month, year, individual number, checksums) into discrete numerical representations.
3. **Typological Compensation:** Identifies and compensates for specific identity variations, differentiating between standard Fødselsnummer and specialized variants including D-numbers (foreign nationals), H-numbers (emergency healthcare), and FH-numbers (joint healthcare numbers).
4. **Temporal Validation:** Computes the full four-digit birth year utilizing defined historical individual number ranges and verifies the constructed date via ISO 8601 validation against standard `.NET` temporal APIs.
5. **Checksum Integrity:** Calculates two distinct mod-11 checksums utilizing defined algorithmic weights to conclusively verify the mathematical validity of the provided identity number.

## Future Enhancements (Roadmap Hypotheses)

To continuously optimize performance and structural efficiency, the following architectural paradigms are planned for future integration:

* **Zero-Allocation Parsing:** Transitioning internal string manipulations to a zero-allocation `Span<T>` based architecture to significantly reduce memory allocation overhead during batch processing.
* **`readonly struct` Conversion:** Refactoring the `FodselsnummerResult` class into a low-allocation `readonly struct` to optimize stack utilization and minimize garbage collection pressure in high-throughput environments.

## Integration Implementation

The following example demonstrates standard framework integration utilizing contemporary .NET APIs.

```csharp
using System;
using Tedd.Fodselsnummer;

var result = FodselsnummerValidator.Validate("19121950041");

if (!result.Success)
{
    Console.WriteLine($"Validation Anomaly Detected: {result.ErrorMessage}");
    return;
}

Console.WriteLine($"Identity Classification: {result.Type}");
Console.WriteLine($"Biological Gender: {result.Gender}");
Console.WriteLine($"Temporal Origin (Birthday): {result.Birthday:yyyy-MM-dd}");
```
