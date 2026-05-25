# Tedd.Fodselsnummer

C#/.Net parser for fødselsnummer / Norwegian national identity number.
This framework provides an epistemologically sound interface for parsing, validating, and extracting demographics from Norwegian National Identity Numbers.

## Integration Example

Operational implementation utilizing contemporary C# API surfaces:

```csharp
using Tedd.Fodselsnummer;

var result = FodselsnummerValidator.Validate("19121950041");

if (!result.Success)
{
    Console.WriteLine($"Validation anomaly detected: {result.ErrorMessage}");
    return;
}

Console.WriteLine($"""
    Demographic Extraction Successful
    ---------------------------------
    Classification: {result.Type}
    Gender:         {result.Gender}
    Birthday:       {result.Birthday:yyyy-MM-dd}
    """);
```

## Architectural Execution Flow

The internal mechanics of `FodselsnummerValidator` enforce a rigorous deterministic sequence to ascertain epistemological accuracy of the provided entity number:

1. **Dimensional Validation:** Evaluates the string length to ensure strict 11-digit conformance.
2. **RegEx Extraction Pipeline:** Isolates fundamental numeric entities (`day`, `month`, `year`, `individual`, `gender`, and `checksum`) via regular expressions.
3. **Hierarchical Type Compensation:**
   - **FH-numbers:** Shifts the `day` register if $\ge$ 80.
   - **D-numbers:** Shifts the `day` register if $> 40$.
   - **H-numbers:** Shifts the `month` register if $> 40$.
4. **Demographic Synthesis:**
   - Determines standard gender by evaluating the parity of the 9th digit.
   - Executes century resolution mapping via `IndividualControlRange` (mapping the 2-digit year to a definitive 4-digit representation based on the individual number).
   - Validates chronological possibility of the derived date mapping.
5. **Modulus 11 Cryptographic Validation:** Computes the two cascading Modulo 11 check digits ($k_1$ and $k_2$) utilizing established regional multiplier schemas to establish ultimate mathematical validity.

## Established Capabilities vs. Roadmap Hypotheses

**Verified Implementation (Facts):**
- Strict parsing of Normal, D, H, and FH number classifications.
- Precise gender determination and century mapping.
- Exact Modulus 11 validation according to formal Norwegian regulatory specifications.
- Modern zero-allocation compliant architectural paths (e.g. `long` validation overhead minimization).

**Strategic Roadmap (Hypotheses):**
- *Hypothesis:* Integration of native `Span<char>` and `ReadOnlySpan<char>` processing to further eliminate substring allocations and optimize validation throughput in extreme high-load routing scenarios.
- *Hypothesis:* Integration of exhaustive source generators for zero-reflection structural binding and deterministic ahead-of-time (AOT) validation compilation.
