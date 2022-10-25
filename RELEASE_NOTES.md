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