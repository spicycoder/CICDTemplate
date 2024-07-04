using Bogus;

using CICDTemplate.Domain.Entities;
using CICDTemplate.Infrastructure;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CICDTemplate.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task MigrateAndSeed(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

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
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, x => string.Concat(x.Commerce.ProductName().Take(20)))
            .RuleFor(x => x.Description, x => string.Concat(x.Commerce.ProductDescription().Take(200)))
            .RuleFor(x => x.CreatedAtUtc, x => DateTime.UtcNow.AddDays(x.Random.Number(0, 6)))
            .Generate(10);

        await dbContext
            .AddRangeAsync(products);

        await dbContext
            .SaveChangesAsync();
    }
}
