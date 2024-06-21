using CICDTemplate.Application.Products.Commands;
using CICDTemplate.Application.Products.Queries;
using CICDTemplate.Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CICDTemplate.Api.Controllers.Products;

[Route("api/products")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "CreateProductRequest cannot be null");
        }

        Guid? productId = await sender.Send(
            new CreateProductCommand(request.Name, request.Description),
            cancellationToken)
            .ConfigureAwait(false);

        return productId is null ? (ActionResult<Guid>)BadRequest() : (ActionResult<Guid>)Ok(productId);
    }

    [HttpGet]
    public async Task<ActionResult<ReadProductsResponse>> GetProducts(
        CancellationToken cancellationToken = default)
    {
        Product[] products = await sender.Send(
            new ReadProductsQuery(),
            cancellationToken)
            .ConfigureAwait(false);

        var productsResponse = products
            .Select(x => new ProductResponse(x.Id, x.Name, x.Description)).ToArray();

        var response = new ReadProductsResponse(new(productsResponse));

        return Ok(response);
    }
}
