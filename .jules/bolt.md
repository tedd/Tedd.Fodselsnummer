## 2026-06-22 - Regex and LINQ Allocation Optimization in FodselsnummerValidator
**Observation:** `FodselsnummerValidator.Validate(string)` incurred heavy allocations (~2.4 KB) and latency (~2.7 µs) due to its use of `Regex.Match`, `int.Parse(match.Groups)`, `string.Substring()`, LINQ enumerations `Where(...).ToList()`, and parsing variables via arrays `number.Select(...).ToArray()`. This string manipulation and enumerator overhead caused completely unnecessary GC pressure.
**Strategic Action:** Decomposed parsing mathematically. Implemented explicit character arithmetic (`number[0] - '0'`) coupled with numeric composition operators. Replaced LINQ queries with a structural `foreach` array iteration and handled date instantiation structurally avoiding string conversions (`new DateTime(year, month, day)` vs parsing ISO 8601 representation strings). Reduced complexity to O(1) Time and O(1) Space with allocations dropping from 2456 B to 72 B and latency decreasing ~97%.
## 2026-06-29 - [FodselsnummerValidator] long.Parse Elimination

**Observation:** The use of `long.Parse(number, CultureInfo.InvariantCulture)` within `FodselsnummerValidator.Validate` incurred unnecessary string parsing overhead, leading to a baseline execution time of ~78.52 ns.

**Strategic Action:** Substituted `long.Parse` with manual mathematical calculation using pre-extracted integer components (`n0` to `n10`), reducing execution time to ~63.01 ns without memory impact.

```markdown
| Method    | Mean        | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|---------- |------------:|----------:|----------:|------:|-------:|----------:|------------:|
| Pre-Opt   |    78.52 ns |  0.275 ns |  0.244 ns |  1.00 | 0.0030 |      72 B |        1.00 |
| Optimized |    63.01 ns |  0.144 ns |  0.127 ns |  0.80 | 0.0030 |      72 B |        1.00 |
```
