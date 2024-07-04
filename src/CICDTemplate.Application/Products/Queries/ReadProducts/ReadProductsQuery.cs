using CICDTemplate.Domain.Entities;

using MediatR;

namespace CICDTemplate.Application.Products.Queries.ReadProducts;

public sealed record ReadProductsQuery() : IRequest<Product[]>;