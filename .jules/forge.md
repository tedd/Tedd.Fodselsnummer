## 2026-11-01 - Modernize Target Frameworks and Test Dependencies

**Observation:**
The repository originally had vulnerabilities in its .NET Standard 1.2 and 2.0 targets due to transitive dependencies, which needed upgrading. Furthermore, the library and test projects had missing or outdated multi-targeting. Specifically, the test project used outdated versions of xUnit and the .NET Test SDK, while `Tedd.Fodselsnummer` needed multi-targeting for `netstandard1.2;netstandard2.0;net8.0;net10.0` and the test project needed multi-targeting for `net8.0;net10.0`.

**Strategic Action:**
Update the primary library (`Tedd.Fodselsnummer`) and archive project (`Tedd.Fodselsnummer.Archive`) to multi-target `netstandard1.2;netstandard2.0;net8.0;net10.0`. Conditionally add patched legacy dependencies for `netstandard1.2` (`System.Net.Http` to `4.3.4` and `System.Text.RegularExpressions` to `4.3.1`) to resolve vulnerabilities without breaking older targets. Upgrade test dependencies in `Tedd.Fodselsnummer.Test` to `Microsoft.NET.Test.Sdk` 18.11.0, `xunit` 2.9.3, and `xunit.runner.visualstudio` 4.0.1, and multi-target the test project for `net8.0;net10.0`.
