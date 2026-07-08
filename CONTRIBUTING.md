## Build instructions

This section is for maintainers publishing the SDK package. If you are writing a mod, you only need to reference the published NuGet package — see [README.md](README.md).

### Prerequisites

- The .NET SDK
- [Optional] The [Nuke global tool](https://nuke.build/docs/getting-started/installation/) (or use `build.ps1` / `build.cmd`)

### Parameters

- `--dry-run`: Log actions without performing them

### Secret parameters

Publishing requires a NuGet API key (`--nuget-api-key`). You can store it via Nuke secrets:

1. Create `.nuke/parameters.local-secrets.json` (copy `$schema` from `parameters.json`)
2. Run `nuke :secrets local-secrets`
3. Pass `--profile local-secrets` when publishing

### Building and publishing

```powershell
nuke package
nuke publish --nuget-api-key <key>
```

The published package contains **only** `NuggetTactics.SDK.props` and the readme. No game DLLs, assets, or decompiled output are included.

`nuke publish` guardrails:

1. Clean rebuild
2. On `main`, up to date with `origin/main`
3. No uncommitted changes

### Optional maintainer targets

`download-depots` and `get-current-version-info` remain for local tooling experiments. They are **not** part of the publish pipeline and must not be used to bundle game content into the NuGet package.
