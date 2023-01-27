## Dot Net Core & React App

### How to Create Project using Dot Net Core and React APP
1. You can create project using CLI Command
2. This command will create React App within the directory Without Solution
3. The Folder in which the project will create it will be the project name by default
```c#
dotnet new react
```
4. You can Also Create  Project Dot Net & React using Visual Studio

### How does Visual Studio Runs React App Under the hood?
1. All the Dot Net and React App Configuration Resides in 8ReactProject.csproj
2. Some of them are mentioned below for full list open the file 8ReactProject.csproj
```xml
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
    <TypeScriptToolsVersion>Latest</TypeScriptToolsVersion>
    <IsPackable>false</IsPackable>
    <SpaRoot>ClientApp\</SpaRoot>
    <DefaultItemExcludes>$(DefaultItemExcludes);$(SpaRoot)node_modules\**</DefaultItemExcludes>
    <SpaProxyServerUrl>https://localhost:44488</SpaProxyServerUrl>
    <SpaProxyLaunchCommand>npm start</SpaProxyLaunchCommand>
    <RootNamespace>_8ReactProject</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
```