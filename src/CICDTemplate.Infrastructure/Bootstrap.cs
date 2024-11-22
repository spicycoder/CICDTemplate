using CICDTemplate.Application.Abstractions.Caching;
using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Domain.Abstract;
using CICDTemplate.Domain.Repositories;
using CICDTemplate.Infrastructure.Caching;
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

        string dbConnectionString = configuration.GetConnectionString(Constants.DatabaseConnectionstringName)
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
            .UseNpgsql(dbConnectionString);
        });

        string cacheConnectionString = configuration.GetConnectionString(Constants.CacheConnectionstringName)
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = cacheConnectionString);

        services
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<ICacheService, CacheService>();

        return services;
    }
}