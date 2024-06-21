using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using MediatR;

namespace CICDTemplate.Application.Products.Commands;

public sealed class CreateProductCommandHandler(
    IProductsRepository repository,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateProductCommand, Guid?>
{
    public async Task<Guid?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        Guid? productId = await repository.CreateProductAsync(
            Product.Create(
                request.Name,
                request.Description,
                dateTimeProvider.Now),
            cancellationToken).ConfigureAwait(false);

        return productId;
    }
}
