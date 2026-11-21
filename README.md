# Aardvark.Data.Durable

[![Build](https://github.com/aardvark-community/aardvark.data.durable.dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/aardvark-community/aardvark.data.durable.dotnet/actions/workflows/build.yml)
[![Publish](https://github.com/aardvark-community/aardvark.data.durable.dotnet/actions/workflows/publish.yml/badge.svg)](https://github.com/aardvark-community/aardvark.data.durable.dotnet/actions/workflows/publish.yml)
[![Version](https://img.shields.io/nuget/vpre/aardvark.data.durable)](https://www.nuget.org/packages/aardvark.data.durable/)
[![Downloads](https://img.shields.io/nuget/dt/aardvark.data.durable)](https://www.nuget.org/packages/aardvark.data.durable/)

Automatically generated .NET types for Aardvark's durable data definitions, and
a binary serializer for durable data.
See [the durable definitions repository](https://github.com/aardvark-community/aardvark.data.durable.definitions).

## Supported targets

.NET 10 is the primary supported runtime. Other targets provide backwards
compatibility.

| Package | Build targets |
| --- | --- |
| `Aardvark.Data.Durable` | `netstandard2.0` |
| `Aardvark.Data.Durable.Codec` | `net10.0`, `netstandard2.0` |

The codec uses a span-based implementation on .NET 10 and a
`BinaryReader`/`BinaryWriter` implementation on `netstandard2.0`. .NET Framework
4.7.2 and later consumers use the `netstandard2.0` assemblies. The tests also
target `net472` to verify this compatibility; the codec does not ship a separate
.NET Framework assembly.

Enable automatic binding redirects in .NET Framework executable projects:

```xml
<PropertyGroup>
  <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
  <GenerateBindingRedirectsOutputType>true</GenerateBindingRedirectsOutputType>
</PropertyGroup>
```

## Build

Install the .NET 10 SDK selected by [global.json](global.json). Run these commands
from the repository root:

```sh
dotnet tool restore
dotnet paket restore
dotnet build src/Aardvark.Data.Durable.sln -c Release --warnaserror
```

Alternatively, run `.\build.cmd` on Windows or `bash ./build.sh` on Unix. These
scripts restore tools and dependencies and build in Debug; they do not run tests.

Dependencies and their resolved versions are maintained in
[paket.dependencies](paket.dependencies) and [paket.lock](paket.lock). Local
Paket and aardpack versions are pinned in
[the tool manifest](.config/dotnet-tools.json). To update the full dependency
graph after reviewing version constraints, run `dotnet paket update`, then
rebuild and test both runtimes.

## Test

Tests use xUnit.net v3 with the VSTest adapter. The `xunit.v3.mtp-off` package
retains the existing `dotnet test` interface. After the Release build above, run:

```sh
dotnet test src/Aardvark.Data.Durable.Tests/Aardvark.Data.Durable.Tests.csproj -c Release -f net10.0 --no-build
```

On Windows with .NET Framework 4.7.2 or later installed, also run:

```sh
dotnet test src/Aardvark.Data.Durable.Tests/Aardvark.Data.Durable.Tests.csproj -c Release -f net472 --no-build
```

The publish workflow runs both targets. To collect .NET 10 coverage:

```sh
dotnet test src/Aardvark.Data.Durable.Tests/Aardvark.Data.Durable.Tests.csproj -c Release -f net10.0 --no-build --collect:"XPlat Code Coverage" --results-directory bin/TestResults
```

The Cobertura report is written below `bin/TestResults`.

## Packaging and publishing

[Aardvark.Build](https://github.com/aardvark-platform/aardvark.build) derives
assembly and package versions from the latest version entry in
[RELEASE_NOTES.md](RELEASE_NOTES.md). The
[publish workflow](.github/workflows/publish.yml) runs when release notes or the
workflow change on `main` or `base-v53`: it builds, tests, packages with aardpack,
creates a GitHub release, and pushes packages to NuGet.

For local package validation after the Release build, run:

```sh
dotnet aardpack src/Aardvark.Data.Durable.sln --skip-build --no-release --no-tag
```

This writes packages to `bin/pack` without publishing a release or creating a
tag. The pinned aardpack tool requires the .NET 8 runtime. The workflow's
`--notag` option suppresses a separate Git tag operation; creating a GitHub
release can still create its associated tag.

Validate packages in a fresh consumer project with an isolated NuGet cache
outside the project directory, and build with `--warnaserror`. Keeping the cache
outside the project avoids the SDK's default file globs including package DLLs
for unrelated target frameworks. Builds and package checks must finish with
zero errors and zero warnings.
