using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CICDTemplate.Application.Products.Queries.ReadProducts;

public sealed class ReadProductsQueryHandler(IProductsRepository repository, ILogger<ReadProductsQueryHandler> logger) : IRequestHandler<ReadProductsQuery, Product[]>
{
    public async Task<Product[]> Handle(ReadProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository
            .GetProductsAsync(cancellationToken);

        logger.LogInformation("Querying products: {@Products}", products);

        return products.ToArray();
    }
}