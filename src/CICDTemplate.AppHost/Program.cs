using Aspire.Hosting.Dapr;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

bool portSpecified = int.TryParse(builder.Configuration["Redis:Port"], out int port);
IResourceBuilder<RedisResource> redis = builder
    .AddRedis("redis", portSpecified ? port : null)
    .WithRedisCommander();

IResourceBuilder<IDaprComponentResource> statestore = builder.AddDaprStateStore(
    "statestore",
    new DaprComponentOptions
    {
        LocalPath = "../../components/statestore.yaml"
    });

IResourceBuilder<IDaprComponentResource> pubsub = builder.AddDaprPubSub(
    "pubsub",
    new DaprComponentOptions
    {
        LocalPath = "../../components/pubsub.yaml"
    });

IResourceBuilder<IDaprComponentResource> secretstore = builder.AddDaprComponent(
    "secretstore",
    "secretstores.local.file",
    new DaprComponentOptions
    {
        LocalPath = "../../components/secretstore.yaml"
    });

IResourceBuilder<IDaprComponentResource> configstore = builder.AddDaprComponent(
    "configstore",
    "configuration.redis",
    new DaprComponentOptions
    {
        LocalPath = "../../components/configstore.yaml"
    });

IResourceBuilder<IDaprComponentResource> cron = builder.AddDaprComponent(
    "scheduler",
    "bindings.cron",
    new DaprComponentOptions
    {
        LocalPath = "../../components/cron.yaml"
    });

IResourceBuilder<PostgresDatabaseResource> db = builder
    .AddPostgres("cicdtemplate-db")
    .WithPgAdmin()
    .AddDatabase("cicdtemplatedb");

builder
    .AddProject<Projects.CICDTemplate_Api>("cicdtemplate-api")
    .WithReference(redis).WaitFor(redis)
    .WithReference(db).WaitFor(db)
    .WithReference(statestore).WaitFor(statestore)
    .WithReference(pubsub).WaitFor(pubsub)
    .WithReference(secretstore).WaitFor(secretstore)
    .WithReference(configstore).WaitFor(configstore)
    .WithReference(cron).WaitFor(cron)
    .WithDaprSidecar();

await builder.Build().RunAsync().ConfigureAwait(false);
