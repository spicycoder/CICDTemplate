using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Testcontainers.PostgreSql;

namespace CICDTemplate.FunctionalTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("cicdtemplatedb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .ConfigureHostConfiguration(config =>
            {
                config.AddInMemoryCollection(
                [
                    new("ConnectionStrings:cicdtemplatedb", _container.GetConnectionString())
                ]);
            });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}
