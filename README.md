# Credit + Special Thanks

A large portion of the tooling is based on [Silksong.GameLibs](https://github.com/silksong-modding/Silksong.GameLibs) by BadMagic100. This package was reworked at the request of Hadean Tactics developer Doug so that **no game files are redistributed**.

# NuggetTactics.SDK

MSBuild SDK NuGet package for modding Hadean Tactics with BepInEx 5. It wires up references and publicization against **your local game install** at build time. The package itself contains only props and documentation.

## Setup

1. Add the [BepInEx NuGet feed](https://nuget.bepinex.dev/) to your mod project (`nuget.config` or IDE package sources).
2. Reference this package from your mod `.csproj`:

```xml
<PackageReference Include="NuggetTactics.SDK" Version="1.0.0" />
```

3. Point at your owned copy of the game (override the default Steam path if needed):

```xml
<!-- Directory.Build.props in your mod repo -->
<Project>
  <PropertyGroup>
    <HadeanTacticsDir>C:\Path\To\Hadean Tactics</HadeanTacticsDir>
  </PropertyGroup>
</Project>
```

4. Add the usual BepInEx plugin packages (`BepInEx.Core`, `BepInEx.PluginInfoProps`, etc.) to your mod project as normal.

## Versioning

This package uses [semantic versioning](https://semver.org/). Bump the package version when you change the props file (e.g. add assemblies, change Unity module version). It is **not** tied to game patch versions — modders always compile against their own installed game.

## Publishing this package

```powershell
nuke package
nuke publish --nuget-api-key <key>
```

See [CONTRIBUTING.md](CONTRIBUTING.md) for maintainer details.
