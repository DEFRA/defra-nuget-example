# Defra NuGet Example

An example .NET 10 package that returns friendly greetings.

## Install

```bash
dotnet add package DefraDigital.Example.Greetings
```

## Usage

```csharp
using DefraDigital.Example.Greetings;

Console.WriteLine(Greeting.Hello("John"));
// Hello, John!
```

## Publishing NuGet packages to the Defra organisation

This repository is an example of how to create, package and publish a .NET NuGet package to the **Defra** NuGet.org organisation using **Trusted Publishing** from GitHub Actions.

Trusted Publishing allows GitHub Actions to publish packages to NuGet.org without storing a long-lived NuGet API key in GitHub secrets.

## Who this is for

This guidance is for Defra teams who want to publish public NuGet packages under the Defra NuGet.org organisation.

It covers:

- how to request access to the Defra NuGet.org organisation
- what a NuGet organisation collaborator can do
- what Defra NuGet organisation admins need to do
- how to create a new package
- how to publish using GitHub Actions Trusted Publishing
- how to choose an appropriate package ID
- how Trusted Publishing supports succession planning

## Requesting access to the Defra NuGet organisation

To publish a package to the Defra NuGet.org organisation, you need to be added as a **Collaborator**.

To request access:

1. Create or sign in to your NuGet.org account.
2. Make sure your NuGet.org account uses a secure Microsoft account with MFA enabled.
3. Find your NuGet.org username.
4. Contact `#github-support` in Slack.
5. Include:

   - your NuGet.org username
   - your team name
   - the GitHub repository that will publish the package
   - the proposed package ID
   - whether this is a new package or an existing package
   - confirmation that the package is suitable for public publishing

Example Slack request:

```text
Hi, please can I be added as a Collaborator to the Defra NuGet.org organisation?

NuGet username: <your-nuget-username>
Team: <team-name>
GitHub repository: <github-org>/<repo-name>
Proposed package ID: DefraDigital.<PackageName>
Reason: We want to publish a public NuGet package using GitHub Actions Trusted Publishing.
```

## What Defra NuGet organisation admins need to do

When someone requests access via `#github-support`, a Defra NuGet organisation admin should:

1. Confirm the request is from an appropriate Defra team or maintainer.
2. Confirm the package is intended to be public.
3. Confirm the proposed package ID is appropriate.
4. Ask the requester for their NuGet.org username if they have not provided it.
5. Sign in to NuGet.org.
6. Go to the **Defra** organisation.
7. Open the organisation members area.
8. Invite the requester by their NuGet.org username.
9. Add them as a **Collaborator**.
10. Confirm in `#github-support` once they have been added.

## Package ID naming

The `Defra` package ID prefix is reserved on NuGet.org.

This means package IDs such as the following may be rejected:

```text
Defra.Example.Greetings
Defra.SomePackage
Defra.Team.Component
```

For most new packages, use the `DefraDigital` prefix instead:

```text
DefraDigital.Example.Greetings
DefraDigital.SomePackage
DefraDigital.Team.Component
```

Use a different prefix if it better reflects the ownership or context of the package.

Examples:

```text
DefraDigital.Payments.Client
DefraDigital.Forms
DefraDigital.Messaging
EnvironmentAgency.SomePackage
Rpa.SomePackage
```

Before publishing, check that the package ID is:

- globally unique on NuGet.org
- clearly associated with the owning team or product
- unlikely to conflict with another Defra, ALB or external package
- suitable for public use
- stable enough to keep long term

NuGet package IDs are immutable once published. You cannot rename a package after publishing; you would need to publish a new package with a different ID.

## What this example repository includes

This example repository demonstrates a minimal .NET package publishing setup.

It includes:

- a .NET class library project
- a test project
- package metadata in the `.csproj`
- a package README
- a package licence file
- GitHub Actions CI with version bump enforcement
- GitHub Actions NuGet publishing
- Trusted Publishing with NuGet.org
- package versioning via `<Version>` in the `.csproj`
- publishing without storing a NuGet API key in GitHub secrets

The example package contains a simple greeting helper so the publishing process can be tested end to end without introducing application-specific logic.

Example package structure:

```text
repo-root/
├─ .github/
│  └─ workflows/
│     ├─ check-pull-request.yml
│     └─ publish.yml
├─ DefraDigital.Example.Greetings/
│  ├─ Greetings.cs
│  └─ DefraDigital.Example.Greetings.csproj
├─ DefraDigital.Example.Greetings.
│  ├─ Greeting.cs
│  └─ DefraDigital.Example.Greetings.Tests.csproj
├─ README.md
├─ LICENCE
└─ DefraDigital.Example.Greetings.slnx
```

## Creating a new package

Create a new repository for your package.

Then create a solution, class library and test project:

```bash
mkdir DefraDigital.Example.Greetings
cd DefraDigital.Example.Greetings

dotnet new sln --name DefraDigital.Example.Greetings

dotnet new classlib \
  --name DefraDigital.Example.Greetings \
  --output DefraDigital.Example.Greetings

dotnet new xunit \
  --name DefraDigital.Example.Greetings.Tests \
  --output DefraDigital.Example.Greetings.Tests

dotnet sln add DefraDigital.Example.Greetings/DefraDigital.Example.Greetings.csproj
dotnet sln add DefraDigital.Example.Greetings.DefraDigital.Example.Greetings.Tests.csproj

dotnet add DefraDigital.Example.Greetings.DefraDigital.Example.Greetings.Tests.csproj \
  reference DefraDigital.Example.Greetings/DefraDigital.Example.Greetings.csproj
```

Replace `DefraDigital.Example.Greetings` with your actual package ID.

## Adding package metadata

Add NuGet package metadata to your project file. See [DefraDigital.Example.Greetings.csproj](DefraDigital.Example.Greetings/DefraDigital.Example.Greetings.csproj) for a full example.

The key metadata properties to set are:

```xml
<PackageId>DefraDigital.Example.Greetings</PackageId>
<Version>0.1.0</Version>
<Authors>Authors</Authors>
<Company>Defra</Company>
<Description>A short description of what the package does.</Description>
<PackageTags>defra;dotnet;example</PackageTags>

<PackageProjectUrl>https://github.com/DEFRA/repo-name</PackageProjectUrl>
<RepositoryUrl>https://github.com/DEFRA/repo-name</RepositoryUrl>
<RepositoryType>git</RepositoryType>

<PackageLicenseFile>LICENCE</PackageLicenseFile>
<PackageReadmeFile>README.md</PackageReadmeFile>
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

Include the licence and README in the package:

```xml
<ItemGroup>
  <None Include="../LICENCE" Pack="true" PackagePath="\" />
  <None Include="../README.md" Pack="true" PackagePath="\" />
</ItemGroup>
```

Do not use `PackageLicenseExpression` unless the licence is accepted by NuGet.org as an approved licence expression. For licences not accepted as NuGet licence expressions, include the licence as a file instead.

The `<Version>` element controls the published package version. It is read directly at pack time by both local builds and the GitHub Actions publish workflow. Increment it in every pull request that should result in a new published version. The CI check enforces this before merging.

## Building and packing locally

Build, test and pack the package locally before publishing:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build

dotnet pack DefraDigital.Example.Greetings/DefraDigital.Example.Greetings.csproj \
  --configuration Release \
  --output artifacts
```

The package file will be created in the `artifacts` directory:

```text
artifacts/DefraDigital.Example.Greetings.0.1.0.nupkg
```

You can test the package locally from another project (run this command from the consuming project directory, adjusting the source path to where your artifacts folder is):

```bash
dotnet add package DefraDigital.Example.Greetings \
  --source /path/to/DefraDigital.Example.Greetings/artifacts
```

## GitHub Actions CI workflow

The CI workflow builds, tests and checks that the package version has been incremented on every pull request to `main`.

See [.github/workflows/check-pull-request.yml](.github/workflows/check-pull-request.yml) for the full workflow.

Key steps:

```yaml
- name: Restore
  run: dotnet restore

- name: Build
  run: dotnet build --configuration Release --no-restore

- name: Test
  run: dotnet test --configuration Release --no-build
```

The workflow also enforces that the `<Version>` in the `.csproj` is higher than the version on `main` before the pull request can be merged. This is the mechanism that controls which version is published next.

### Why version bumping matters

The package version is read directly from `<Version>` in the `.csproj` at pack time. There is no tag-based or automatic versioning. This means:

- every change merged to `main` that should result in a new published version must include a version increment in the `.csproj`
- if you merge without bumping the version, the publish workflow will attempt to push a version that already exists on NuGet.org and will fail
- NuGet package versions are immutable, so you cannot overwrite a published version

The CI check in [.github/workflows/check-pull-request.yml](.github/workflows/check-pull-request.yml) prevents merging if the version has not been incremented:

```yaml
- name: Check version incremented
  run: |
    CURRENT="${{ steps.current-version.outputs.version }}"
    BASE="${{ steps.base-version.outputs.version }}"
    # fails if CURRENT is not greater than BASE
```

This means every merged pull request is guaranteed to carry a new version, and the publish workflow can push it to NuGet.org immediately.

## GitHub Actions publish workflow

See [.github/workflows/publish.yml](.github/workflows/publish.yml) for the full workflow.

The workflow is triggered on every push to `main` (i.e. when a pull request is merged) and can also be run manually:

```yaml
on:
  workflow_dispatch:
  push:
    branches:
      - main
```

It requires `id-token: write` for Trusted Publishing:

```yaml
permissions:
  contents: read
  id-token: write
```

The pack step uses the `<Version>` from the `.csproj` directly:

```yaml
- name: Pack
  run: dotnet pack DefraDigital.Example.Greetings/DefraDigital.Example.Greetings.csproj --configuration Release --no-build --output artifacts
```

Replace `DefraDigital.Example.Greetings` with your package ID and project path.

## GitHub repository secret

Add a GitHub Actions secret named:

```text
NUGET_USERNAME
```

The value must be the NuGet.org username of the person who created the Trusted Publishing policy.

It should be a NuGet.org username, not:

- the NuGet organisation name
- an email address
- a Microsoft account login
- a GitHub username, unless that is also the NuGet.org username

Example:

```text
my-nuget-username
```

## Setting up Trusted Publishing in NuGet.org

A NuGet organisation collaborator or admin should create the Trusted Publishing policy.

In NuGet.org:

1. Sign in as the NuGet.org user that will be used in `NUGET_USERNAME`.
2. Go to **Trusted Publishing**.
3. Add a new policy.
4. Choose the package owner as the **Defra** organisation.
5. Enter the GitHub repository details.
6. Enter the workflow file name.
7. Enter the GitHub environment name, if one is used.

Example policy values:

```text
Package owner: Defra
Repository owner: DEFRA
Repository: defra-nuget-example
Workflow file: publish.yml
```

The workflow file should be only the file name:

```text
publish.yml
```

Do not enter the full path:

```text
.github/workflows/publish.yml
```

Leave the environment field blank if the publish workflow does not specify a GitHub environment.

## Publishing a package

Publishing happens automatically when a pull request is merged to `main`. Before merging, ensure the `<Version>` in the `.csproj` has been incremented. The CI check will block the merge if it has not.

For example, to publish version `0.1.1`:

1. Update `<Version>` in the `.csproj` to `0.1.1`.
2. Open a pull request.
3. The CI check confirms the version is higher than `main`.
4. Merge the pull request.

The publish workflow will then:

1. build the project
2. run the tests
3. create a NuGet package using the version from the `.csproj`
4. use GitHub OIDC to authenticate with NuGet.org
5. receive a short-lived NuGet API key
6. push the package to NuGet.org

NuGet package versions are immutable. Once a version has been published, it cannot be overwritten. If you merge without bumping the version, the push step will fail because the version already exists on NuGet.org. Increment the version in a follow-up pull request to recover.

## Trusted Publishing and succession planning

Trusted Publishing helps with succession planning because publishing is tied to:

- the Defra NuGet.org organisation
- a GitHub repository
- a GitHub Actions workflow
- a NuGet.org policy

It avoids depending on a long-lived NuGet API key stored in a single repository secret.

This has several benefits:

- no permanent NuGet API key needs to be shared
- no individual needs to keep a personal API key alive
- access can be managed through NuGet organisation membership
- responsibility can move between maintainers without rotating package API keys
- organisation admins can add or remove collaborators as teams change

Recommended succession setup:

```text
NuGet.org organisation: Defra
Organisation admins: 2 or more trusted support/admin users
Package maintainers: added as Collaborators
Publishing identity: GitHub Actions Trusted Publishing
```

When a maintainer leaves a team:

1. Remove them from the relevant GitHub repository or team.
2. Remove or update their NuGet organisation membership if they no longer need it.
3. Confirm another maintainer can create releases.
4. Confirm the Trusted Publishing policy still points to the correct repository and workflow.
5. Confirm the `NUGET_USERNAME` secret still refers to the NuGet.org user who created the policy, or recreate the policy with a different user if needed.

For long-lived packages, avoid having only one person able to manage the package or its release process.

## Troubleshooting

### `No matching trust policy owned by user was found`

Check that `NUGET_USERNAME` is the NuGet.org username of the person who created the Trusted Publishing policy.

It must not be the organisation name.

For example, use:

```text
my-nuget-username
```

not:

```text
Defra
```

### `The package ID is reserved`

The package ID probably uses a reserved prefix.

For example:

```text
Defra.Example.Greetings
```

may fail because the `Defra` prefix is reserved.

Use `DefraDigital` or another appropriate package prefix instead:

```text
DefraDigital.Example.Greetings
```

### `License expression must only contain licences that are approved`

If NuGet.org rejects your licence expression, use a packaged licence file instead:

```xml
<PackageLicenseFile>LICENCE</PackageLicenseFile>
```

and include the file in the package:

```xml
<ItemGroup>
  <None Include="../LICENCE" Pack="true" PackagePath="\" />
</ItemGroup>
```

### `Package already exists`

NuGet package versions are immutable.

If this version has already been published, increment `<Version>` in the `.csproj` and open a new pull request.

### Package does not appear in search immediately

NuGet.org may take some time to validate and index a package.

Check the package directly using:

```text
https://www.nuget.org/packages/<PackageId>
```

For example:

```text
https://www.nuget.org/packages/DefraDigital.Example.Greetings
```

## Licence

THIS INFORMATION IS LICENSED UNDER THE CONDITIONS OF THE OPEN GOVERNMENT LICENCE found at:

<http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3>

The following attribution statement MUST be cited in your products and applications when using this information.

> Contains public sector information licensed under the Open Government license v3

### About the licence

The Open Government Licence (OGL) was developed by the Controller of Her Majesty's Stationery Office (HMSO) to enable
information providers in the public sector to license the use and re-use of their information under a common open
licence.

It is designed to encourage use and re-use of information freely and flexibly, with only a few conditions.
