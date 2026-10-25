## 2026-09-01 - Dependency and Framework Drift Observation

**Observation:** `Tedd.Fodselsnummer` and `Tedd.Fodselsnummer.Archive` rely on `netstandard1.2` which exposes transitive vulnerabilities in `System.Net.Http` (4.3.0) and `System.Text.RegularExpressions` (4.3.0). The test project relies on obsolete versions of `Microsoft.NET.Test.Sdk` (17.1.0), `xunit` (2.4.1), and `xunit.runner.visualstudio` (2.4.3).

**Strategic Action:** Multi-target the main and archive projects to `netstandard1.2;netstandard2.0;net8.0;net9.0;net10.0` and the test project to `net8.0;net10.0`. Fix the transitive vulnerabilities for `netstandard1.2` using explicit `PackageReference` conditions. Upgrade the test dependencies to `Microsoft.NET.Test.Sdk` 18.9.0, `xunit` 2.9.3, and `xunit.runner.visualstudio` 2.8.2.
