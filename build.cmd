@echo off
SETLOCAL
PUSHD %~dp0

dotnet tool restore
dotnet paket restore
dotnet build src/Aardvark.Data.Durable.sln --configuration Release