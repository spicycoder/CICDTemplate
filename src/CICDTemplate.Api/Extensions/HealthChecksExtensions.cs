using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CICDTemplate.Api.Extensions;

public static class HealthChecksExtensions
{
    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        string? dbConnectionString = builder
            .Configuration
        .GetConnectionString("Database");

        string? cacheConnectionString = builder
            .Configuration
            .GetConnectionString("Cache");

        ArgumentNullException.ThrowIfNull(dbConnectionString);
        ArgumentNullException.ThrowIfNull(cacheConnectionString);

        builder
            .Services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
            .AddNpgSql(dbConnectionString)
            .AddRedis(cacheConnectionString);

        return builder;
    }

    public static void MapHealthChecksEndpoints(this WebApplication? app)
    {
        ArgumentNullException.ThrowIfNull(app);
        if (app.Environment.IsDevelopment())
        {
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }
    }
}
