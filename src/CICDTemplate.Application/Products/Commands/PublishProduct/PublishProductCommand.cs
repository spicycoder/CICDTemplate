using MediatR;

namespace CICDTemplate.Application.Products.Commands.PublishProduct;

public record PublishProductCommand(string Name, string Description) : IRequest;