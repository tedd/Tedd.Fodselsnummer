# Tedd.Fodselsnummer

C#/.NET parser and validator for Norwegian national identity numbers (Fødselsnummer). This framework provides robust extraction and structural validation of individual identities through precise algorithmic analysis.

## Architectural Execution Flow

The internal mechanics of the `FodselsnummerValidator` module execute validation through a strictly deterministic sequence:

1. **Dimensional Validation:** Ensures the input conforms precisely to the required 11-digit scalar length.
2. **Protocol Compensation:** Automatically compensates for and identifies alternative national identity protocols, specifically routing D-numbers, H-numbers, and FH-numbers through respective normalization heuristics.
3. **Temporal Verification:** Validates the underlying chronological data against ISO 8601 constraints, cross-referencing individual centuries against designated control ranges (1854–2039).
4. **Mod11 Checksum Verification:** Conducts a dual-pass modulus 11 checksum calculation to definitively verify the integrity of the numeric sequence.

### Future Hypotheses
*It is a current hypothesis that a future iteration of this framework will integrate hierarchical data binding and a routed event infrastructure to further decouple the validation lifecycle.*

## Implementation Example

The following code demonstrates the framework's operational capability utilizing contemporary .NET 9.0/10.0+ syntax conventions.

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
