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

## Observability

- [ ] Logging - `Serilog` + `Elastic Stack`
- [ ] Alerts and Monitoring - `Prometheus` and `Grafana`
- [ ] Distributed Tracing - `Open Telemetry`
- [ ] Health Checks

## DevOps

- [ ] Build Script - `PowerShell` / `Cake`
- [ ] Helm Charts
- [ ] Deployment Scripts - *yet to be decided*

## Aspire

- [ ] Distributed Cache - `Redis`
- [ ] Output Caching - `Redis`
- [x] RDBMS - `PostgreSQL`
- [ ] NoSQL - `MongoDB`
- [ ] ~~E2E Tests* - Aspire has coverage issues~~

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
