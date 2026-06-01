## 2026-06-01 - FodselsnummerValidator Optimization
**Observation:** The legacy implementation of `FodselsnummerValidator.Validate` utilized `Regex` for initial parsing and `string.Substring` during validation, resulting in superfluous heap allocations.
**Strategic Action:** Substituted `Regex` with `ReadOnlySpan<char>` slicing and direct index access. Replaced `string.Substring` loops with zero-allocation pointer arithmetic and math calculations for modern .NET targets, while explicitly maintaining `netstandard1.2` compatibility through conditional compilation.
