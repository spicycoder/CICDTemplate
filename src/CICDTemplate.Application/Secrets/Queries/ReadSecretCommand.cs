using MediatR;

namespace CICDTemplate.Application.Secrets.Queries;

public record ReadSecretCommand(string Key): IRequest<string?>;