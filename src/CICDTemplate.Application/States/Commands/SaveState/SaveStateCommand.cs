using MediatR;

namespace CICDTemplate.Application.States.Commands.SaveState;

public record SaveStateCommand(
    string Name,
    string Description) : IRequest;