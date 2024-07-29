using MediatR;

namespace CICDTemplate.Application.Secrets.Queries;

public record ReadSecretQuery(string Key) : IRequest<string?>;