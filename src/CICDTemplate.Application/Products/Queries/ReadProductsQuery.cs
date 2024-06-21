using CICDTemplate.Domain.Entities;

using MediatR;

namespace CICDTemplate.Application.Products.Queries;

public sealed record ReadProductsQuery() : IRequest<Product[]>;