# CI / CD Template

[![cn-ca build](https://github.com/spicycoder/CICDTemplate/actions/workflows/build.yml/badge.svg)](https://github.com/spicycoder/CICDTemplate/actions/workflows/build.yml)

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

- [x] Logging
    - [x] `Aspire Dashboard`
    - [x] `Seq`
    - [ ] `OpenSearch` + `OpenSearch-Dashboard`
- [x] Metrics
    - [x] `Aspire Dashboard`
    - [ ] `Prometheus` + `Grafana`
- [x] Distributed Tracing
    - [x] `Aspire Dashboard`
    - [x] `Jaeger`

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
- [x] GitHub Action
    - [x] Tool Restore
    - [x] Package Restore
    - [x] Build
    - [x] Test
    - [x] Publish Coverage Report
    - [x] Pack as nuget package
    - [x] Push to nuget.org

## Deployment

- [x] Install Dependencies
    - [x] Dapr
    - [x] Redis
    - [x] Postgresql
    - [x] Aspire Dashboard
- [x] Dapr Components
    - [x] State store
    - [x] Pub / Sub
    - [x] Secret store
    - [x] Configuration store
    - [x] Cron Binding
- [x] Deploy API

## Performance

- [ ] Benchmarking - using `Benchmark.NET`
- [ ] Load Testing - using `K6`
