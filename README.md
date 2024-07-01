# CI / CD Template

CI / CD Template - for ASP.NET 8 Web API

## Clean Architecture

- [x] `CQRS` - with `EF Core` - In Memory
- [x] `MediatR`

## Test Automation

- [x] Unit Tests - `xUnit` and `NSubstitute`
- [x] Functional Tests - In Memory API Testing `WebApplicationFactory` using `Testcontainers`
- [x] Mutation Tests - `Stryker` *recommended for local only*
- [x] Architecture Tests - `NetArchTest.Rules`
- [x] Code Coverage

## DevOps

- [ ] Build Script - `PowerShell` / `Cake`
    - [x] Local build [build.ps1](./build.ps1)
    - [ ] CI / CD Pipeline Scripts
- [ ] Helm Charts
- [ ] Deployment Scripts - *yet to be decided*

## Orchestration

- [x] Docker compose
    - [x] RDBMS - `PostgreSQL`
    - [x] Aspire Dashboard

## Aspire

- [x] Observability
    - [x] Logging
    - [x] Metrics
    - [x] Distributed Tracing
- [x] Health Checks
    - [x] Self
    - [x] PostgreSQL Database
    - [ ] Redis
- [ ] Distributed Cache - `Redis`
- [ ] Output Caching - `Redis`
- [ ] NoSQL - `MongoDB`

## DAPR

- [ ] Pub / Sub - `Redis Streams`
- [ ] Service Invocation
- [ ] State Management - `Redis`
- [ ] Secrets Management - Json File Based
- [ ] Bindings - Input: `Cron`, Output: `PostgreSQL`

## Performance

- [ ] Benchmarking - using `Benchmark.NET`
- [ ] Load Testing - using `NBomber`

## Misc

- [x] Git ignore
- [x] Editor config
- [x] Build props
- [x] Code Analysis
- [x] Tool manifest
