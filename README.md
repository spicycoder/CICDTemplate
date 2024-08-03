# CI / CD Template

[![cn-ca build](https://github.com/spicycoder/CICDTemplate/actions/workflows/dotnet.yml/badge.svg)](https://github.com/spicycoder/CICDTemplate/actions/workflows/dotnet.yml)

Cloud Native - Clean Architecture template, ideal for

1. Web API
2. Pub / Sub
3. Schedulers

## Clean Architecture

- [x] `CQRS` - with `EF Core` - In Memory
- [x] `MediatR`

## Test Automation

- [x] Unit Tests - `xUnit` and `NSubstitute`
- [x] Functional Tests - In Memory API Testing `WebApplicationFactory` using `Testcontainers`
- [x] Mutation Tests - `Stryker` *recommended for local only*
- [x] Architecture Tests - `NetArchTest.Rules`
- [x] Code Coverage

## Orchestration

- [x] Docker compose
    - [x] RDBMS - `PostgreSQL`
    - [ ] ~~NoSQL - `MongoDB`~~
    - [x] Distributed Cache - `Redis`
    - [x] Aspire Dashboard
    - [x] Jaeger

## Health Checks

- [x] Self
- [x] PostgreSQL Database
- [x] Redis
- [ ] ~~MongoDB~~

## Observability

- [x] Logging - `Aspire`
- [x] Metrics - `Aspire`
- [x] Distributed Tracing - `Aspire` & `Jaeger`
- [x] Aspire Dashboard

## DAPR

- [x] Pub / Sub - `Redis Streams`
    - [x] Code Tour
- [ ] Service Invocation
- [x] State Management - `Redis`
    - [x] Save state
    - [x] Read state
    - [x] Delete state
- [x] Secrets Management - Json File Based
- [x] Configurations Store
- [x] Bindings
    - [x] Input: `Cron`
        - [x] Code Tour

## Misc

- [x] Git ignore
- [x] Editor config
- [x] Build props
- [x] Code Analysis
- [x] Tool manifest
- [ ] Automapper
- [x] Validation `Fluent Validation`

## Build

- [x] Local build - Notebook
- [ ] GitHub Action
    - [ ] Clean
    - [ ] Restore
    - [ ] Build
    - [ ] Test
    - [ ] Publish Coverage Report
    - [ ] Publish (nuget package)
- [ ] Package as Template
    - [x] Template Config
    - [ ] Publish as Nuget package

## Deployment

- [ ] Install Dependencies
    - [ ] Redis
    - [ ] Postgresql
    - [ ] Aspire Dashboard
- [ ] Dapr Components
    - [ ] State store
    - [ ] Pub / Sub
    - [ ] Secret store
    - [ ] Configuration store
- [ ] Deploy API

## Performance

- [ ] Benchmarking - using `Benchmark.NET`
- [ ] Load Testing - using `K6`
