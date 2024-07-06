using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

// cannot refer domain project's constants, until this issue is resolved
// https://github.com/dotnet/aspire/issues/2769
var postgres = builder
    .AddPostgres("DatabaseServer")
    .WithPgAdmin()
    .AddDatabase("Database");

var stateStore = builder.AddDaprStateStore(
    "statestore",
    new DaprComponentOptions
    {
        LocalPath = Path.Combine("../..", "components", "statestore.yaml")
    });

var pubsub = builder.AddDaprPubSub(
    "pubsub",
    new DaprComponentOptions
    {
        LocalPath = Path.Combine("../..", "components", "pubsub.yaml")
    });

var scheduler = builder.AddDaprComponent(
    "scheduler",
    "bindings.cron",
    new DaprComponentOptions
    {
        LocalPath = Path.Combine("../..", "components", "cron.yaml")
    });

var secretStore = builder.AddDaprComponent(
    "secretstore",
    "secretstores.local.file",
    new DaprComponentOptions
    {
        LocalPath = Path.Combine("../..", "components", "local-secret-store.yaml")
    });

builder
    .AddProject<Projects.CICDTemplate_Api>("cicdtemplateapi")
    .WithDaprSidecar()
    .WithReference(postgres)
    .WithReference(stateStore)
    .WithReference(pubsub)
    .WithReference(scheduler)
    .WithReference(secretStore);

await builder.Build().RunAsync();
