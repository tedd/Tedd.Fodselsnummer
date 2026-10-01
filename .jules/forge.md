## 2024-03-24 - Modernize Test Dependencies

**Observation:** Test dependencies were outdated. Newtonsoft.Json had a vulnerability in older version.

**Strategic Action:** Upgraded Microsoft.NET.Test.Sdk to 17.13.0, xunit to 2.9.3, and xunit.runner.visualstudio to 3.0.2 in Tedd.Fodselsnummer.Test.csproj.

## 2024-03-24 - Evaluate Target Frameworks

**Observation:** The main library Tedd.Fodselsnummer targets netstandard1.2, which is no longer recommended (.NET Standard prior to 2.0). Test and Benchmarks projects target net10.0.

**Strategic Action:** No action required on frameworks per modernization constraints without explicit authorization to drop targets, though multi-targeting could add `netstandard2.0` and `net10.0` or similar. I'll ask for plan review on upgrading frameworks.

## 2024-03-24 - Framework Modernization

**Observation:** Tedd.Fodselsnummer and Tedd.Fodselsnummer.Archive solely targeted netstandard1.2, which restricts modern API usages, while Test and Benchmarks targeted only net10.0, limiting verification coverage across multiple targets.

**Strategic Action:** Multi-targeted Tedd.Fodselsnummer and Tedd.Fodselsnummer.Archive to `netstandard1.2;netstandard2.0;net8.0;net9.0;net10.0`, and multi-targeted Tedd.Fodselsnummer.Test to `net8.0;net9.0;net10.0` to preserve the previous netstandard1.2 support while unlocking compatibility validation on modern runtimes.
