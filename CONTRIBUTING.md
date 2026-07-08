## Build instructions

This section is for maintainers publishing the SDK package. If you are writing a mod, you only need to reference the published NuGet package — see [README.md](README.md).

### Prerequisites

- The .NET SDK
- [Optional] The [Nuke global tool](https://nuke.build/docs/getting-started/installation/) (or use `build.ps1` / `build.cmd` / `build.sh`)

### Parameters

- `--dry-run`: Log actions without performing them

### Building

```powershell
nuke package
```

The published package contains **only** `NuggetTactics.SDK.props` and the readme. No game DLLs, assets, or decompiled output are included.

### Publishing to nuget.org

Modders consume `NuggetTactics.SDK` from [nuget.org](https://www.nuget.org/). The BepInEx feed (`https://nuget.bepinex.dev/v3/index.json`) is only needed for BepInEx/Unity dependencies referenced by the props file.

#### Option A: GitHub Actions (recommended — trusted publishing)

Trusted publishing uses short-lived OIDC credentials instead of a stored API key. See [NuGet trusted publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing).

**One-time nuget.org setup**

1. Sign in at [nuget.org](https://www.nuget.org/) → **Account** → **Trusted publishers** → **Add trusted publisher** → **GitHub Actions**
2. Configure the policy:
   - **Package owner:** `AZander48`
   - **Repository owner:** `AZander48`
   - **Repository:** `NuggetTactics.SDK`
   - **Workflow file:** `publish.yml` (filename only)
   - **Environment:** leave blank unless you add a GitHub environment to the workflow

**Publish from CI**

The [`.github/workflows/publish.yml`](.github/workflows/publish.yml) workflow runs on:

- Manual dispatch (**Actions** → **Publish** → **Run workflow**)
- Pushing a version tag (e.g. `v1.0.1`)

It packs with `dotnet pack -o artifacts`, exchanges a GitHub OIDC token for a temporary NuGet API key, and pushes the package to nuget.org.

#### Option B: Local publish (API key)

For publishing from your machine, use a [nuget.org API key](https://www.nuget.org/account/apikeys):

```powershell
nuke package
nuke publish --nuget-api-key <key>
```

Or push the package directly:

```powershell
dotnet nuget push bin\NuggetTactics.SDK.*.nupkg `
  --source https://api.nuget.org/v3/index.json `
  --api-key <key> `
  --skip-duplicate
```

You can store the API key via Nuke secrets:

1. Create `.nuke/parameters.local-secrets.json` (copy `$schema` from `parameters.json`)
2. Run `nuke :secrets local-secrets`
3. Pass `--profile local-secrets` when publishing

`nuke publish` guardrails:

1. Clean rebuild
2. On `main`, up to date with `origin/main`
3. No uncommitted changes

### Optional maintainer targets

`download-depots` and `get-current-version-info` remain for local tooling experiments. They are **not** part of the publish pipeline and must not be used to bundle game content into the NuGet package.

`get-current-version-info` requires `build/classdata.tpk` (TPK format v1 — see Silksong.GameLibs or an older AssetRipper Tpk build). Do not commit this file.
