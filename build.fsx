#load @"paket-files/build/aardvark-platform/aardvark.fake/DefaultSetup.fsx"

open Fake
open System
open System.IO
open System.Diagnostics
open Aardvark.Fake

DefaultSetup.install ["src/Aardvark.Data.Durable.sln"]

entry()