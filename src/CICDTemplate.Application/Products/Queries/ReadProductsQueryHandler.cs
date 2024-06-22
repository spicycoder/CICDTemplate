using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using MediatR;

namespace CICDTemplate.Application.Products.Queries;

public sealed class ReadProductsQueryHandler(IProductsRepository repository) : IRequestHandler<ReadProductsQuery, Product[]>
{
    public async Task<Product[]> Handle(ReadProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository
            .GetProductsAsync(cancellationToken);

        return products.ToArray();
    }
}