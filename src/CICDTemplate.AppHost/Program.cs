using CommunityToolkit.Aspire.Hosting.Dapr;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

bool portSpecified = int.TryParse(builder.Configuration["Redis:Port"], out int port);
IResourceBuilder<RedisResource> redis = builder
    .AddRedis("redis", portSpecified ? port : null)
    .WithRedisCommander();

var statestore = builder.AddDaprStateStore(
    "statestore",
    new DaprComponentOptions
    {

    }).WaitFor(redis);

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
    }).WaitFor(redis);

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
    .WithReference(redis)
    .WithReference(db)
    .WithReference(statestore)
    .WithReference(pubsub)
    .WithReference(secretstore)
    .WithReference(configstore)
    .WithReference(cron)
    .WithDaprSidecar();

await builder.Build().RunAsync().ConfigureAwait(false);
