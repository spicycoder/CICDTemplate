using Bogus;

using CICDTemplate.Domain.Entities;
using CICDTemplate.Infrastructure;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CICDTemplate.Api.Extensions;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "app cannot be null")]
public static class ApplicationBuilderExtensions
{
    public static async Task MigrateAndSeed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var exist = await dbContext.Database.CanConnectAsync()
            && await dbContext
            .Database
            .GetService<IRelationalDatabaseCreator>()
            .HasTablesAsync();

        if (!exist)
        {
            await dbContext.Database.EnsureCreatedAsync();
            await Seed(dbContext);
        }
    }

    private static async Task Seed(ApplicationDbContext dbContext)
    {
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
            .AddRangeAsync(products);

        await dbContext
            .SaveChangesAsync();
    }
}
