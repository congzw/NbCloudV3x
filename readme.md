# NbCloudV3x

## 结构简介

	- Master(Trunk, Main, etc.)
		- /build
		- /doc
		- /lib
		- /setup
		- /src
		- /readme.md
	- Release1.0
	- Release2.0
	- Release2.1
	- ...

## 详细说明
- **build** 包含构建脚本和一些支持性文件(e.g., PowerShell scripts, EXEs, and the parameters file, etc.)
- **doc** 包含构文档(e.g., developer documents, installation guides, tips, requirements, etc.) 
- **lib** 包含第三方依赖库(e.g., nuget repos, ect.)
- **setup** 包含部署脚本或安装包代码(e.g.,  PowerShell, MSBuild, or NAnt script, WiX source code, etc.) 
- **src** 源代码(e.g.,  Visual Studio solution, all project folders, etc.)