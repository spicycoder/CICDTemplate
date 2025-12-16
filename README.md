# CI / CD Template

Cloud Native - Clean Architecture template, for ASP.NET 10 Web API, powered by .NET Aspire and DAPR

| Component | Badge |
| --- | --- |
| Build Status | [![cn-ca build](https://github.com/spicycoder/CICDTemplate/actions/workflows/build.yml/badge.svg)](https://github.com/spicycoder/CICDTemplate/actions/workflows/build.yml) |
| Nuget Version | [![NuGet Version](https://img.shields.io/nuget/v/CloudNative.CleanArchitecture.Template)](https://www.nuget.org/packages/CloudNative.CleanArchitecture.Template) |
| Security Score Card | [![Scorecard supply-chain security](https://github.com/spicycoder/CICDTemplate/actions/workflows/scorecard.yml/badge.svg)](https://github.com/spicycoder/CICDTemplate/actions/workflows/scorecard.yml) [![OpenSSF Scorecard](https://api.scorecard.dev/projects/github.com/spicycoder/CICDTemplate/badge)](https://scorecard.dev/viewer/?uri=github.com/spicycoder/CICDTemplate) [![OpenSSF Best Practices](https://www.bestpractices.dev/projects/9743/badge)](https://www.bestpractices.dev/projects/9743) |

---

## Usage

Install the template

```sh
dotnet new install CloudNative.CleanArchitecture.Template
```

To create a new solution

```sh
dotnet new cn-ca -n MyCloudNativeApi
```

---

## Getting Started

Once you clone the solution, open with Visual Studio Code

Install the extension - [Polyglot Notebooks](https://marketplace.visualstudio.com/items/?itemName=ms-dotnettools.dotnet-interactive-vscode), better to install all recommended extensions, suggested by VS Code, when you open the workspace

The documents under `docs` directory would be a good start

To build and deploy the project, follow the interactive notebooks under `build` and `deployment` directories.

---
# Dapr Aspire Sample (ServiceA → ServiceB → ServiceC)

This sample demonstrates how to use **.NET Aspire** with **Dapr sidecars** to connect multiple services through event-driven communication.

- **ServiceA**: Publishes an event to a Dapr **pub/sub** component.
- **ServiceB**: Subscribes to ServiceA’s topic, processes the event, and publishes a new one.
- **ServiceC**: Subscribes to ServiceB’s topic and handles the final event.
- **Dapr Components**: Each service runs with its own Dapr sidecar, which provides access to pub/sub, state store, and other building blocks.

---
