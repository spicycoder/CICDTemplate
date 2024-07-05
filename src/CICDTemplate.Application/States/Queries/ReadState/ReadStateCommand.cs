using MediatR;

namespace CICDTemplate.Application.States.Queries.ReadState;

public record ReadStateCommand(string ProductName) : IRequest<ProductState?>;