using CICDTemplate.AppHost.Extensions;

using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("RedisPassword", true);
var redisPort = builder.AddParameter("RedisPort");

var portSpecifiedValue = await redisPort.Resource.GetValueAsync(CancellationToken.None);

var portSpecified = int.TryParse(portSpecifiedValue, out int port);

IResourceBuilder<RedisResource> redis = builder
    .AddRedis("redis", portSpecified ? port : null, password)
    .WithRedisInsight()
    .WithLifetime(ContainerLifetime.Persistent);

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
    .WithDaprSidecar(new DaprSidecarOptions
    {
        ResourcesPaths = [Path.Combine("..", "..", "components")]
    });

migration.WithParentRelationship(apiService);

await builder.Build().RunAsync().ConfigureAwait(false);
