## 2026-06-01 - Target Framework and Package Modernization

**Observation:** The main library targeted only `netstandard1.2`, missing modern optimizations. The test project targeted `net6.0` (end-of-life) and used outdated testing packages. Legacy dependencies in `netstandard1.x` and `netstandard2.x` were being unnecessarily imported when targeting modern `.NET`.

**Strategic Action:** Multi-target `Tedd.Fodselsnummer` to `netstandard1.2;netstandard2.0;net8.0;net10.0` to preserve legacy compatibility while enabling modern capabilities. Updated `Tedd.Fodselsnummer.Test` to target `net8.0;net10.0` and upgraded all test dependencies to their latest stable versions. Placed explicit `ItemGroup` conditions on legacy packages to prevent leaking them into modern consuming projects.
