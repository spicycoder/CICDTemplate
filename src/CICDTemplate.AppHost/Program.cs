using CICDTemplate.AppHost.Extensions;

using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("RedisPassword", true);
var redisPort = builder.AddParameter("RedisPort");

var portSpecifiedValue = await redisPort.Resource.GetValueAsync(CancellationToken.None);

var portSpecified = int.TryParse(portSpecifiedValue, out int port);

IResourceBuilder<RedisResource> redis = builder
    .AddRedis("redis", portSpecified ? port : null, password)
    .WithRedisInsight();

var statestore = builder.AddDaprStateStore(
    "statestore",
    new DaprComponentOptions
    {
        LocalPath = "../../components/statestore.yaml"
    }).WaitFor(redis);

var pubsub = builder.AddDaprPubSub(
    "pubsub",
    new DaprComponentOptions
    {
        LocalPath = "../../components/pubsub.yaml"
    });

var secretstore = builder.AddDaprComponent(
    "secretstore",
    "secretstores.local.file",
    new DaprComponentOptions
    {
        LocalPath = "../../components/secretstore.yaml"
    });

var configstore = builder.AddDaprComponent(
    "configstore",
    "configuration.redis",
    new DaprComponentOptions
    {
        LocalPath = "../../components/configstore.yaml"
    }).WaitFor(redis);

var cron = builder.AddDaprComponent(
    "scheduler",
    "bindings.cron",
    new DaprComponentOptions
    {
        LocalPath = "../../components/cron.yaml"
    });

var server = builder
    .AddPostgres("cicdtemplate-db")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var db = server
    .AddDatabase("cicdtemplatedb");

var migration = builder
    .AddProject<Projects.CICDTemplate_MigrationService>("migrations")
    .WithReference(db).WaitFor(db);

var apiService = builder
    .AddProject<Projects.CICDTemplate_Api>("cicdtemplate-api")
    .WithSwaggerUI()
    .WithScalar()
    .WithReDoc()
    .WithReference(db).WaitFor(db)
    .WithReference(redis).WaitFor(redis)
    .WithDaprSidecar(sidecar =>
    {
        sidecar
            .WithReference(statestore)
            .WithReference(secretstore)
            .WithReference(pubsub)
            .WithReference(configstore)
            .WithReference(cron);
    });

migration.WithParentRelationship(apiService);

await builder.Build().RunAsync().ConfigureAwait(false);
