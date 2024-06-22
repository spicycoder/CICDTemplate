using Bogus;

using CICDTemplate.Domain.Entities;
using CICDTemplate.Infrastructure;

namespace CICDTemplate.Api.Extensions;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "app cannot be null")]
public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Apply database migrations
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }

    public static async Task Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var faker = new Faker();

        var products = Enumerable
            .Range(1, 10)
            .Select(i =>
                Product.Create(
                    string.Concat(faker.Commerce.ProductName().Take(20)),
                    string.Concat(faker.Commerce.ProductDescription().Take(200)),
                    DateTime.UtcNow.AddDays(-1 * faker.Random.Int(0, 6))))
            .ToArray();

        await dbContext
            .AddRangeAsync(products)
            .ConfigureAwait(false);

        await dbContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }
}
