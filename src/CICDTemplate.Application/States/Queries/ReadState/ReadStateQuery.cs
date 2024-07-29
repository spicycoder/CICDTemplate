using MediatR;

namespace CICDTemplate.Application.States.Queries.ReadState;

public record ReadStateQuery(string ProductName) : IRequest<ProductState?>;