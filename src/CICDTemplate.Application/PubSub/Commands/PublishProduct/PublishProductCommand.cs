using MediatR;

namespace CICDTemplate.Application.PubSub.Commands.PublishProduct;

public record PublishProductCommand(string Name, string Description) : IRequest;