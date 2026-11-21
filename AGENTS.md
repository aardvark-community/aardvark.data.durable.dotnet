# Repository guidance

## Overview and layout

This repository provides .NET types for Aardvark durable data definitions and a
binary serializer. The solution is `src/Aardvark.Data.Durable.sln`.

`net10.0` is the primary supported target. Other targets exist for backwards
compatibility. Prioritize .NET 10 when making implementation and performance
decisions, while preserving compatibility in the other supported targets.

- `src/Aardvark.Data.Durable/`: `Durable.Def`, definition registration, aliases,
  and generated definitions. Targets `netstandard2.0`.
- `src/Aardvark.Data.Durable.Codec/`: the `DurableCodec` partial class, handwritten
  serialization logic, and generated codec registration. Targets `net10.0` and
  `netstandard2.0`; .NET Framework consumers use the latter.
- `src/Aardvark.Data.Durable.Codegen/`: F# generator for the definitions and codec.
- `src/Aardvark.Data.Durable.Tests/`: xUnit.net v3 definition and serialization
  tests, targeting `net10.0` and `net472`. The Framework target verifies the
  codec's `netstandard2.0` build.
- `src/Scratch.CSharp/` and `src/Scratch.FSharp/`: executable examples and experiments.

The public library namespace is `Aardvark.Data`; do not infer namespaces from
project names.

## Build and test

Use the .NET SDK selected by `global.json` (.NET 10, with `latestFeature` roll
forward). Run commands from the repository root. Dependencies are managed with
Paket; local tools are pinned in `.config/dotnet-tools.json`.

```powershell
dotnet tool restore
dotnet paket restore
dotnet build src/Aardvark.Data.Durable.sln --warnaserror
```

`./build.cmd` on Windows and `bash ./build.sh` on Unix run these same steps.
The build scripts do not run tests.

For tests on the modern .NET runtime:

```powershell
dotnet test src/Aardvark.Data.Durable.Tests/Aardvark.Data.Durable.Tests.csproj -f net10.0
```

On Windows with .NET Framework 4.7.2 or later available, also test `-f net472`
when changing codec behavior or dependencies. The tests are executables and use
`xunit.v3.mtp-off`, `Microsoft.NET.Test.Sdk`, and `xunit.runner.visualstudio` to
preserve the VSTest-based `dotnet test` interface. The publish workflow builds
and tests the full solution in Release configuration:

```powershell
dotnet build src/Aardvark.Data.Durable.sln -c Release --warnaserror
dotnet test src/Aardvark.Data.Durable.sln -c Release --no-build
```

For .NET 10 coverage after the Release build:

```powershell
dotnet test src/Aardvark.Data.Durable.Tests/Aardvark.Data.Durable.Tests.csproj -c Release -f net10.0 --no-build --collect:"XPlat Code Coverage" --results-directory bin/TestResults
```

For local packaging, the pinned aardpack tool requires the .NET 8 runtime. Run
`dotnet aardpack src/Aardvark.Data.Durable.sln --skip-build --no-release --no-tag`
after the Release build. Packages are written to `bin/pack`; these options avoid
GitHub release and tag creation.

## Generated code

`src/Aardvark.Data.Durable/DurableDataDefs.cs` and
`src/Aardvark.Data.Durable.Codec/Codec_auto.cs` are generated. Prefer updating
`src/Aardvark.Data.Durable.Codegen/Codegen.fs` or the source definitions and
regenerating them over making isolated edits to the generated output.

The definitions live in the separate `aardvark-community/aardvark.data.durable.definitions`
repository. Inspect `src/Aardvark.Data.Durable.Codegen/Program.fs` before running
the generator: it downloads the upstream JSON, then replaces that input with a
local file read from the separate checkout. Its input and output paths are
relative to the process working directory. A run from the repository root will
not resolve those paths as intended. Building the solution does not regenerate
these files.

## Implementation conventions

- Match the surrounding C# or F# style; handwritten C# generally uses four spaces
  and braces on separate lines. Preserve existing license headers.
- Preserve durable GUIDs, serialized layouts, and decoding of deprecated types
  when extending the format. Add regression coverage for affected behavior.
- Keep codec implementations consistent across targets:
  `Codec.BinaryReaderWriter.cs` provides the compatibility implementation;
  `Codec.Span.cs` provides the modern implementation used on .NET 10. Existing
  guards also mention historical frameworks, but the codec builds only for
  `net10.0` and `netstandard2.0`.
- Extend `DefTests.cs` for definition behavior and `SerializationTests.cs` for
  codec changes. Use the existing xUnit.net v3 conventions.
- Manage package changes through `paket.dependencies`, `paket.lock`, and each
  project's `paket.references`. Avoid introducing a parallel package-management
  scheme. Keep `net472` in Paket's shared framework restriction while the tests
  and C# scratch project target it. Use `dotnet paket update` for a full dependency
  refresh, then build and test both runtimes. Keep generated restore/build
  outputs out of commits. Read versions from the manifests and lockfile rather
  than duplicating them in documentation.
- Declare runtime packages used directly by a library in its `paket.references`
  so packing records the required versions. Validate packages with an isolated
  consumer: tests sharing the repository lockfile can hide missing package
  dependencies. Keep its isolated NuGet cache outside the consumer project so
  SDK default file globs do not include package DLLs for unrelated targets.
  Enable automatic binding redirects for Framework executables. Require zero
  errors and zero warnings in builds and package checks using `--warnaserror`;
  fix warning causes rather than introducing warning suppression.
- Aardvark.Build reads versions from `RELEASE_NOTES.md`; aardpack packages the
  libraries and creates GitHub releases in the publish workflow, which then
  pushes packages to NuGet. Changes to release notes or the publish workflow
  trigger publishing on its configured branches; edit them intentionally for
  release work. `--notag` alone does not prevent a GitHub release creating a tag.

## Python tooling

- Always use `uv` for anything Python-related.
- Never invoke bare `python`, `py`, `pip`, or `pip3` unless the user explicitly
  asks for that exact tool.
- For inline Python snippets, use `uv run python -`.
- Run Python scripts through `uv run ...`.
- For one-off extra packages, use `uv run --with <package> ...`; do not install
  them globally.
- Prefer PowerShell or another available non-Python tool when it handles the
  task cleanly. If Python is not needed, do not use it.
