@echo off
SETLOCAL
PUSHD %~dp0

dotnet build src\Aardvark.Data.Durable.sln --configuration Release

git tag %1
git push --tags

dotnet paket pack bin --version %1
dotnet nuget push "bin\*.%1.nupkg" --skip-duplicate --source https://api.nuget.org/v3/index.json