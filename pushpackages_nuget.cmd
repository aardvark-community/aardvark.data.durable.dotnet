@echo off
SETLOCAL
PUSHD %~dp0

dotnet build src/Aardvark.Data.Durable.sln --configuration Release

git tag %1
git push --tags

dotnet paket pack bin --version %1

set /p Key=<%HOMEPATH%\.ssh\nuget.key
dotnet nuget push "bin\Aardvark.Data.Durable.%1.nupkg" --skip-duplicate --source https://www.nuget.org/api/v2/package -k %Key%
dotnet nuget push "bin\Aardvark.Data.Durable.Codec.%1.nupkg" --skip-duplicate --source https://www.nuget.org/api/v2/package -k %Key%
dotnet nuget push "bin\Aardworx.Data.DurableCodec2.%1.nupkg" --skip-duplicate --source https://www.nuget.org/api/v2/package -k %Key%