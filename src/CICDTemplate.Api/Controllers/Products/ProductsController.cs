﻿using CICDTemplate.Application.Products.Commands.CreateProduct;
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
            return BadRequest();
        }

        Guid? productId = await sender.Send(
            new CreateProductCommand(request.Name, request.Description),
            cancellationToken);

        return productId is null ? BadRequest() : Ok(productId);
    }

    [HttpGet]
    public async Task<ActionResult<ReadProductsResponse>> GetProducts(
        CancellationToken cancellationToken = default)
    {
        Product[] products = await sender.Send(
            new ReadProductsQuery(),
            cancellationToken);

        var productsResponse = products
            .Select(x => new ProductResponse(x.Id, x.Name, x.Description)).ToArray();

        var response = new ReadProductsResponse(new(productsResponse));

        return Ok(response);
    }
}