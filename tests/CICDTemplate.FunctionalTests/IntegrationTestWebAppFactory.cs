using System.Threading.Tasks;

using Bogus;

using CICDTemplate.Domain.Entities;
using CICDTemplate.Infrastructure;

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
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:latest")
        .WithDatabase("cicdtemplatedb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder("redis")
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
            .ConfigureHostConfiguration(config => config.AddInMemoryCollection(
                [
                    new($"ConnectionStrings:{Domain.Abstract.Constants.DatabaseConnectionstringName}", _dbContainer.GetConnectionString()),
                    new($"ConnectionStrings:{Domain.Abstract.Constants.CacheConnectionstringName}", _redisContainer.GetConnectionString())
                ]));

        builder.ConfigureServices(services => services.AddSingleton(_ => MockDaprClient.Object));

        IHost host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
        Seed(dbContext, CancellationToken.None).GetAwaiter().GetResult();

        return host;
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();

        await _redisContainer.StopAsync();
        await _redisContainer.DisposeAsync();
    }

    private static async Task Seed(
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, x => string.Concat(x.Commerce.ProductName().Take(20)))
            .RuleFor(x => x.Description, x => string.Concat(x.Commerce.ProductDescription().Take(200)))
            .RuleFor(x => x.CreatedAtUtc, x => DateTime.UtcNow.AddDays(x.Random.Number(0, 6)))
            .Generate(10);

        await dbContext
            .AddRangeAsync(products, cancellationToken);

        await dbContext
            .SaveChangesAsync(cancellationToken);
    }
}