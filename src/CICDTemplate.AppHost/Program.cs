using CICDTemplate.AppHost.Extensions;

using CommunityToolkit.Aspire.Hosting.Dapr;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("RedisPassword", true);
var redisPort = builder.AddParameter("RedisPort");

var portSpecifiedValue = await redisPort.Resource.GetValueAsync(CancellationToken.None);

bool portSpecified = int.TryParse(portSpecifiedValue, out int port);

IResourceBuilder<RedisResource> redis = builder
    .AddRedis("redis", portSpecified ? port : null, password)
    .WithRedisInsight();

var statestore = builder.AddDaprStateStore(
    "statestore",
    new DaprComponentOptions
    {
        LocalPath = "../../components/statestore.yaml"
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
    .WithDaprSidecar(options =>
    {
        options
            .WithReference(statestore).WaitFor(redis)
            .WithReference(secretstore)
            .WithReference(pubsub)
            .WithReference(configstore).WaitFor(redis)
            .WithReference(cron);
    });

migration.WithParentRelationship(apiService);

await builder.Build().RunAsync().ConfigureAwait(false);
