# Robotico.Outbox

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![GitHub Packages](https://img.shields.io/badge/GitHub%20Packages-Robotico.Outbox-blue?logo=github)](https://github.com/robotico-dev/robotico-outbox-csharp/packages)
[![Build](https://github.com/robotico-dev/robotico-outbox-csharp/actions/workflows/publish.yml/badge.svg)](https://github.com/robotico-dev/robotico-outbox-csharp/actions/workflows/publish.yml)

Reference **Robotico.Outbox** when you use the **transactional outbox pattern**. Interface: `IOutbox` (EnqueueAsync, CommitAsync returning `Result`).

## Robotico dependencies

```mermaid
flowchart LR
  A[Robotico.Outbox] --> B[Robotico.Result]
```

## Installation

```bash
dotnet add package Robotico.Outbox
```

## License

See repository license file.
