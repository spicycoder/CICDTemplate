# CI / CD Template

Cloud Native - Clean Architecture template, ideal for

1. Web API 🕸️
2. Pub / Sub 👂
3. Schedulers ⏱️

## Clean Architecture 🧹

- [x] `CQRS` - with `EF Core` - In Memory
- [x] `MediatR`
- [ ] Code Tour ✈️

## Test Automation 🧪

- [x] Unit Tests - `xUnit` and `NSubstitute`
- [x] Functional Tests - In Memory API Testing `WebApplicationFactory` using `Testcontainers`
- [x] Mutation Tests - `Stryker` *recommended for local only*
- [x] Architecture Tests - `NetArchTest.Rules`
- [x] Code Coverage
- [ ] Code Tour ✈️

## DevOps 🏗️

- [ ] Build Script - `PowerShell` / `Cake`
    - [x] Local build [build.ps1](./build.ps1)
    - [ ] CI / CD Pipeline Scripts
- [ ] Helm Charts
- [ ] Deployment Scripts - *yet to be decided*
- [ ] Code Tour ✈️

## Orchestration

- [x] Docker compose
    - [x] RDBMS - `PostgreSQL`
    - [ ] ~~NoSQL - `MongoDB`~~
    - [x] Distributed Cache - `Redis`
- [ ] Code Tour ✈️

## Health Checks 🩺

- [x] Self
- [x] PostgreSQL Database
- [x] Redis
- [ ] ~~MongoDB~~
- [ ] Code Tour ✈️

## Observability

- [x] Logging - `Aspire`
- [x] Metrics - `Aspire`
- [x] Distributed Tracing - `Aspire` & `Open Zipkin`
- [x] Aspire Dashboard
- [ ] Code Tour✈️

## DAPR

- [x] Pub / Sub - `Redis Streams`
- [ ] Service Invocation
- [x] State Management - `Redis`
    - [x] Save state
    - [x] Read state
    - [x] Delete state
- [x] Secrets Management - Json File Based
- [x] Bindings
    - [x] Input: `Cron`
- [ ] Code Tour✈️

## Performance 📈

- [ ] Benchmarking - using `Benchmark.NET`
- [ ] Load Testing - using `K6`
- [ ] Code Tour✈️

## Misc

- [x] Git ignore
- [x] Editor config
- [x] Build props
- [x] Code Analysis
- [x] Tool manifest
- [ ] Package as Template
    - [x] Template Config
    - [ ] Publish as Nuget package
- [ ] Code Tour✈️
