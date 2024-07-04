using Dapr.Client;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace CICDTemplate.FunctionalTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("cicdtemplatedb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis")
        .WithPortBinding(6500, 6379)
        .Build();

    private Mock<DaprClient>? _mockDaprClient;
    public Mock<DaprClient> MockDaprClient
    {
        get
        {
            _mockDaprClient ??= new Mock<DaprClient>();
            return _mockDaprClient!;
        }
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _redisContainer.StartAsync();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .ConfigureHostConfiguration(config =>
            {
                config.AddInMemoryCollection(
                [
                    new("ConnectionStrings:Database", _dbContainer.GetConnectionString()),
                    new("ConnectionStrings:Cache", _redisContainer.GetConnectionString())
                ]);
            });

        builder.ConfigureServices(services => services.AddSingleton(_ => MockDaprClient.Object));

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();

        await _redisContainer.StopAsync();
        await _redisContainer.DisposeAsync();
    }
}
