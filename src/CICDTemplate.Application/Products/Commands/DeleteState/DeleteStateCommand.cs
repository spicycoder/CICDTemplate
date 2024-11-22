using MediatR;

namespace CICDTemplate.Application.Products.Commands.DeleteState;

public record DeleteStateCommand(string ProductName) : IRequest;