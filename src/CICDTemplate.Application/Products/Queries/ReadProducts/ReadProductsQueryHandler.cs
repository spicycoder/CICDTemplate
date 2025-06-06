using CICDTemplate.Application.Abstractions.Caching;
using CICDTemplate.Domain.Abstract;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CICDTemplate.Application.Products.Queries.ReadProducts;

public sealed class ReadProductsQueryHandler(
    IProductsRepository repository,
    ICacheService cacheService,
    ILogger<ReadProductsQueryHandler> logger) : IRequestHandler<ReadProductsQuery, Product[]>
{
    public async Task<Product[]> Handle(ReadProductsQuery request, CancellationToken cancellationToken)
    {
        Product[]? cachedProducts = await cacheService.GetAsync<Product[]>(Constants.AllProductsCacheKey, cancellationToken);

        if (cachedProducts is null)
        {
            cachedProducts = [.. (await repository.GetProductsAsync(cancellationToken))];

            await cacheService.SetAsync(
                Constants.AllProductsCacheKey,
                cachedProducts!,
                null,
                cancellationToken);
        }

        logger.LogInformation("Querying products: {@Products}", cachedProducts);

        return cachedProducts;
    }
}