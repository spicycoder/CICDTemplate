using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Domain.Repositories;
using CICDTemplate.Infrastructure.Clock;
using CICDTemplate.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CICDTemplate.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IDateTimeProvider, DateTimeProvider>();

        string connectionString = configuration.GetConnectionString("cicdtemplatedb")
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
        });

        services
            .AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
