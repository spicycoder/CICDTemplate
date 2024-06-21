using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace CICDTemplate.Infrastructure.Repositories;

// <see cref="IProductsRepository"/>
public sealed class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
{
    // <see cref="IProductsRepository"/>
    public async Task<Guid?> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        await dbContext
            .AddAsync(product, cancellationToken)
            .ConfigureAwait(false);

        await dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return product?.Id;
    }

    // <see cref="IProductsRepository"/>
    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        Product[] products = await dbContext
            .Set<Product>()
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        return products;
    }
}
