using MediatR;

namespace CICDTemplate.Application.Configurations.Queries;

public record ReadConfigurationQuery(string Key) : IRequest<string?>;