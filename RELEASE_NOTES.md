### 0.4.4
- Updated to NET 8 and Aardvark.Base 5.3

### 0.4.4-prerelease0001
- Updated to NET 8 and Aardvark.Base 5.3 (prerelease)

### 0.3.15
- Octree.PartIndexRange, Octree.PerCellPartIndex1i

### 0.3.14
- fix typo (Octree.Cir3bRange)

### 0.3.13
- Def.AddAlias

### 0.3.12
- branch 0.3.x

### 0.3.11
- fixed version string

### 0.3.10
- sync with definitions file
    - add Octree.PerCellPartIndex1ui
    - add Octree.PerPointPartIndex1[bsi]
    - add Octree.Cir[34]b (color infrared)
- dotnet tool update
- update package Aardvark.Base 5.2.25
- update package Aardvark.Build 1.0.20

### 0.3.9
- sync with definitions file
    - add Durable.Octree.Subnodes.ByteRanges,
    - add Durable.Octree.NodeByteRange
    - add Durable.Octree.MultiNodeBlob
    - restore Durable.Primitives.DurableMapAligned8
    - restore Durable.Primitives.DurableMapAligned16
- dotnet tool update
- update package Aardvark.Build 1.0.18
- fix pushpackages_nuget.cmd

### 0.3.8
- cleanup, obsolete defs
- add auto-generated .gz/.lz4 defs for array types

### 0.3.7
- GZip and LZ4 variants for octree data
- remove net5.0 target

### 0.3.6
- netcore: fixed DecodeArray from stream 

### 0.3.5
- clean up recently added defs

### 0.3.4
- Octree.Colors.[Range1f|Average|StdDev].[Red|Green|Blue|Alpha], Octree.[Intensities|Densitites].[Average|StdDev]

### 0.3.3
- duplicate defs are ignored if semantically identical to simplify moving defs between libaries which might not be updated simultaniously
- add `Aardvark.Chunk.*`, `Octree.PositionsGlobal3[fd].DistToCentroid.[Average3d|StdDev3d]`, `Octree.*.Range*`
- add constant `DurableCodec.Version`

### 0.3.1
- add Octree.Intensities1b, Octree.Intensities1b.Reference

### 0.3.0
- Updated to Aardvark.Base 5.2.1